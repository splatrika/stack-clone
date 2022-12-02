using System;

namespace Splatrika.StackClone.Model
{
    public interface IScenesService
    {
        event Action LoadingStarted;
        event Action LoadingFinished;

        void Load(string scene);
    }
}
