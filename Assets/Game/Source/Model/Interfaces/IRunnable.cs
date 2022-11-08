using System;

namespace Splatrika.StackClone.Model
{
    public interface IRunnable
    {
        bool IsRunned { get; }

        event Action Runned;

        void Run();
    }
}
