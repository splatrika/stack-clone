using Splatrika.StackClone.Model;
using Splatrika.StackClone.Presenter;
using UnityEngine;
using Zenject;

namespace Splatrika.StackClone.Scene
{
    public class GameplayInstaller : MonoInstaller
    {
        [Header("Tower")]

        [SerializeField]
        private Vector2 _startSize;

        [SerializeField]
        private Vector2 _startCenter;

        [SerializeField]
        private float _perfectDistance;

        [Header("Moving Block")]

        [SerializeField]
        private float _minSpeed;

        [SerializeField]
        private float _acceleration;

        [SerializeField]
        private float _timeAcceleration;

        [SerializeField]
        private Vector2 _boundsSize;

        [SerializeField]
        private Vector2 _boundsCenter;

        [Header("Common")]

        [SerializeField]
        private Color[] _colors = new Color[1];

        [SerializeField]
        private float _changeColorSpeed;


        public override sealed void InstallBindings()
        {
            var colorServiceConfiguration =
                new ColorServiceConfiguration(_colors, _changeColorSpeed);
            Container.Bind<ColorServiceConfiguration>()
                .FromInstance(colorServiceConfiguration);

            Container.Bind<IColorService>()
                .To<ColorService>()
                .AsSingle();

            var towerConfiguration = CreateTowerConfiguration();
            Container.Bind<TowerConfiguration>()
                .FromInstance(towerConfiguration);

            var movingBlockConfiguration = CreateMovingBlockConfiguration();
            Container.Bind<MovingBlockConfiguration>()
                .FromInstance(movingBlockConfiguration);

            Container.BindInterfacesAndSelfTo<Tower>()
                .AsSingle();

            Container.Bind<MovingBlock>()
                .To<MovingBlock>()
                .AsSingle();
        }


        public override sealed void Start()
        {
            base.Start();

            var tower = Container.Resolve<Tower>();
            var movingBlock = Container.Resolve<MovingBlock>();

            var towerPresenter = FindObjectOfType<TowerPresenter>();
            Container.Inject(towerPresenter);
            towerPresenter.Init(tower);

            var movingBlockPresenter = FindObjectOfType<MovingBlockPresenter>();
            Container.Inject(movingBlockPresenter);
            movingBlockPresenter.Init(movingBlock);
        }


        private void OnValidate()
        {
            if (_colors == null || _colors.Length == 0)
            {
                Debug.LogError("Setup one or more colors");
            }
        }


        private TowerConfiguration CreateTowerConfiguration()
        {
            return new TowerConfiguration(
                GetStartRect(), _colors[0], _perfectDistance);
        }


        private MovingBlockConfiguration CreateMovingBlockConfiguration()
        {
            var bounds = new Rect();
            bounds.size = _boundsSize;
            bounds.center = _boundsCenter;

            return new MovingBlockConfiguration(
                GetStartRect(), _minSpeed, _acceleration, _timeAcceleration,
                bounds);
        }


        private Rect GetStartRect()
        {
            var startRect = new Rect();
            startRect.size = _startSize;
            startRect.center = _startCenter;
            return startRect;
        }
    }
}
