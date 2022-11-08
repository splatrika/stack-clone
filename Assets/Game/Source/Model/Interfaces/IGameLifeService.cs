using System;

namespace Splatrika.StackClone.Model
{
    public interface IGameLifeService
    {
        event Action Runned;
        event Action Reseted;

        void Run();
        void Reset();
    }
}
