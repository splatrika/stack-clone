using System;
using Splatrika.StackClone.Model;
using UnityEngine.SceneManagement;

namespace Splatrika.StackClone.Presenter
{
    public class ScenesService : IScenesService
    {
        public event Action LoadingStarted;
        public event Action LoadingFinished;

        public void Load(string scene)
        {
            LoadingStarted?.Invoke();
            SceneManager.LoadScene(scene);
            LoadingFinished?.Invoke();
        }
    }
}
