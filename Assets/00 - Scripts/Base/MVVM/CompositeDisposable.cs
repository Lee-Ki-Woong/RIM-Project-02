using System;
using System.Collections.Generic;

public sealed class CompositeDisposable : IDisposable
{
    private readonly List<IDisposable> _disposables = new();

    public void Add(IDisposable disposable)
    {
        if (disposable == null)
        {
            return;
        }

        _disposables.Add(disposable);
    }

    public void Dispose()
    {
        foreach (IDisposable disposable in _disposables)
        {
            disposable.Dispose();
        }

        _disposables.Clear();
    }
}
