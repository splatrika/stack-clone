using System;
using System.Collections.Generic;

namespace Splatrika.StackClone.Model
{
    public class GameLifeService : IGameLifeService
    {
        private List<IRunnable> _runnables;
        private List<IResetable> _resetables;

        public event Action Runned;
        public event Action Reseted;


        public GameLifeService(
            List<IRunnable> runnables,
            List<IResetable> resetables)
        {
            _runnables = runnables;
            _resetables = resetables;
        }


        public void Reset()
        {
            foreach (var resetable in _resetables)
            {
                resetable.Reset();
            }
            Reseted?.Invoke();
        }


        public void Run()
        {
            foreach (var runnable in _runnables)
            {
                runnable.Run();
            }
            Runned?.Invoke();
        }
    }
}
