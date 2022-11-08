using UnityEngine;
using Zenject;

namespace Splatrika.StackClone.Scene
{
    public class AppInstaller : MonoInstaller
    {
        public override sealed void InstallBindings()
        {
            Container.Bind<ILogger>()
                .FromInstance(Debug.unityLogger);
        }
    }
}
