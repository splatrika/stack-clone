using Splatrika.StackClone.Model;
using Splatrika.StackClone.Presenter;
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

            Container.Bind<IScenesService>()
                .To<ScenesService>()
                .AsSingle();
        }
    }
}
