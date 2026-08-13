using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ResourceManager : BaseManager<ResourceManager>
{
    private readonly Dictionary<string, AsyncOperationHandle> _assetHandleDictionary = new();
    private readonly Dictionary<string, int> _assetLoadCount = new();

    public T LoadAssetSync<T>(string address) where T : Object
    {
        if (_assetHandleDictionary.TryGetValue(address, out AsyncOperationHandle existingHandle) && existingHandle.IsValid())
        {
            _assetLoadCount[address]++;
            T existingAsset = ResolveHandleSync<T>(address, existingHandle);

            return existingAsset;
        }

        AsyncOperationHandle handle = Addressables.LoadAssetAsync<T>(address);
        _assetHandleDictionary[address] = handle;
        _assetLoadCount[address] = 1;

        T asset = ResolveHandleSync<T>(address, handle);

        return asset;
    }

    public async UniTask<T> LoadAssetAsync<T>(string address, CancellationToken token = default) where T : Object
    {
        if (_assetHandleDictionary.TryGetValue(address, out AsyncOperationHandle existingHandle) && existingHandle.IsValid())
        {
            _assetLoadCount[address]++;

            T existingAsset = await AwaitHandleAsync<T>(address, existingHandle, token);

            return existingAsset;
        }

        AsyncOperationHandle handle = Addressables.LoadAssetAsync<T>(address);
        _assetHandleDictionary[address] = handle;
        _assetLoadCount[address] = 1;

        T asset = await AwaitHandleAsync<T>(address, handle, token);

        return asset;
    }

    public async UniTask PreloadAsync<T>(IEnumerable<string> addresses, int maxLoadCount = 10, System.Action<float> onProgress = null, CancellationToken token = default) where T : Object
    {
        IReadOnlyCollection<string> list = addresses as IReadOnlyCollection<string>;

        if(list == null)
        {
            list = addresses.ToList();
        }

        int total = list.Count;
        int done = 0;

        using SemaphoreSlim semaphore = new(maxLoadCount);
        List<UniTask> tasks = new(total);

        foreach(string existingAddress in list)
        {
            async UniTask LoadOne(string address)
            {
                await semaphore.WaitAsync(token);
            
                try
                {
                    await LoadAssetAsync<T>(address, token);
                }
                finally
                {
                    done++;

                    float successPercent = (done / (float)total);

                    onProgress?.Invoke(successPercent);
                    semaphore.Release();
                }
            }

            UniTask load = LoadOne(existingAddress);

            tasks.Add(load);
        }

        await UniTask.WhenAll(tasks);
    }

    private T ResolveHandleSync<T>(string address, AsyncOperationHandle handle) where T : Object
    {
        try
        {
            T asset = handle.WaitForCompletion() as T;

            if (handle.Status == AsyncOperationStatus.Failed)
            {
                LogError($"에셋 로드 중 문제가 발생하였습니다!! 에셋 주소 : {address}");
                UnloadAsset(address);

                return null;
            }

            if(asset == null)
            {
                LogError($"에셋의 형변환에 실패하였습니다!! 에셋 주소 : {address}");
                UnloadAsset(address);

                return null;
            }

            return asset;
        }
        catch (System.Exception ex)
        {
            LogError($"에셋 로드 중 문제가 발생하였습니다!! 에셋 주소 : {address}");
            UnloadAsset(address);
            Debug.LogException(ex);

            return null;
        }
    }


    private async UniTask<T> AwaitHandleAsync<T>(string address, AsyncOperationHandle handle, CancellationToken token) where T : Object
    {
        try
        {
            await handle.ToUniTask(cancellationToken: token);

            T asset = handle.Result as T;

            if (asset == null)
            {
                LogError($"에셋의 형변환에 실패하였습니다!! 에셋 주소 : {address}");
                UnloadAsset(address);

                return null;
            }

            return asset;
        }
        catch (System.OperationCanceledException)
        {
            LogWarning($"에셋 로드 중 취소를 하였습니다!! 에셋 주소 : {address}");
            UnloadAsset(address);

            return null;
        }
        catch (System.Exception ex)
        {
            LogError($"에셋 로드 중 문제가 발생하였습니다!! 에셋 주소 : {address}");
            UnloadAsset(address);
            Debug.LogException(ex);

            return null;
        }
    }

    public void UnloadAsset(string address)
    {
        if (_assetHandleDictionary.ContainsKey(address) == false || _assetLoadCount.TryGetValue(address, out int count) == false)
        {
            LogWarning($"로드된 적 없는 에셋의 언로드를 시도했습니다!! 에셋 주소 : {address}");
            return;
        }

        count--;

        if(count <= 0)
        {
            ReleaseAsset(address);
            return;
        }

        _assetLoadCount[address] = count;
    }

    private void ReleaseAsset(string address)
    {
        if (_assetHandleDictionary.TryGetValue(address, out AsyncOperationHandle handle) == false)
        {
            LogError($"성립 불가능한 오류가 발생하였습니다!! 에셋 주소 : {address}");

            return;
        }

        _assetHandleDictionary.Remove(address);
        _assetLoadCount.Remove(address);
        Addressables.Release(handle);
    }
}
