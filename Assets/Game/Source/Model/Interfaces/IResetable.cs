using System;

namespace Splatrika.StackClone.Model
{
    public interface IResetable
    {
        event Action Reseted;

        void Reset();
    }
}
