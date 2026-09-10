using UnityEngine;
using Spine;
using Spine.Unity;

public static class SkinCombiner
{
    public static bool ApplyCombinedSkin(SkeletonAnimation skeletonAnimation, params string[] skinNames)
    {
        if (skeletonAnimation == null)
        {
            Debug.LogWarning("SkinCombiner : 받아온 skeletonAnimation이 null입니다!!");
            return false;
        }

        if (skinNames == null || skinNames.Length == 0)
        {
            Debug.LogWarning("SkinCombiner : 받아온 skinNames가 null이거나 비어있습니다!!");
            return false;
        }

        Skeleton skeleton = skeletonAnimation.Skeleton;

        if (skeleton == null)
        {
            Debug.LogWarning("SkinCombiner : 받아온 skeletonAnimation의 skeleton이 null입니다!!");
            return false;
        }

        Skin combinedSkin = new Skin("CombinedSkin");

        int skinAddedCount = 0;

        foreach (string skinName in skinNames)
        {
            if (string.IsNullOrEmpty(skinName))
            {
                Debug.LogWarning("SkinCombiner : 받아온 skinNames 중 null이거나 비어있는 값이 있습니다!!");
                continue;
            }

            Skin skin = skeleton.Data.FindSkin(skinName);

            if (skin != null)
            {
                combinedSkin.AddSkin(skin);
                skinAddedCount++;
            }
            else
            {
                Debug.LogWarning($"SkinCombiner : [스킨 명 : {skinName}]를 찾을 수 없습니다!!");
            }
        }

        if (skinAddedCount == 0)
        {
            Debug.LogWarning("SkinCombiner : 유효한 스킨이 하나도 없습니다!!");
            return false;
        }

        skeleton.SetSkin(combinedSkin);
        skeleton.SetupPoseSlots();

        skeletonAnimation.AnimationState.Apply(skeleton);
        return true;
    }
}