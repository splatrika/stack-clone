using System.Collections.Generic;
using Splatrika.StackClone.Model;
using UnityEngine;
using Zenject;

namespace Splatrika.StackClone.Presenter
{
    public class SlicesPresenter : Presenter<Tower>
    {
        [SerializeField]
        private FallingSlicePresenter _prefab;

        [SerializeField]
        private TowerPresenter _towerPresenter;

        [SerializeField]
        private int _poolSize;

        private ILogger _logger;
        private Stack<FallingSlicePresenter> _free;
        private Queue<FallingSlicePresenter> _used;


        [Inject]
        public void Construct(ILogger logger)
        {
            _logger = logger;
        }


        protected override sealed void OnInit(Tower model)
        {
            _free = new Stack<FallingSlicePresenter>();
            _used = new Queue<FallingSlicePresenter>();

            for (int i = 0; i < _poolSize; i++)
            {
                _free.Push(Instantiate(_prefab));
            }

            model.BlockAdded += OnBlockAdded;
            model.Reseted += OnReseted;
        }


        private void OnDestroy()
        {
            Model.BlockAdded -= OnBlockAdded;
            Model.Reseted -= OnReseted;
        }


        private void OnBlockAdded(Block block, bool perfect)
        {
            FallingSlicePresenter slicePresenter = null;
            if (_free.Count > 0)
            {
                slicePresenter = _free.Pop();
            }
            else if (!_used.TryDequeue(out slicePresenter))
            {
                _logger.LogError(nameof(SlicesPresenter),
                    "There is no slice presnters");
                return;
            }
            var slice = Model.LastUncutted.InverseCut(Model.Last.Rect);
            slicePresenter.Init(slice, _towerPresenter.Height);
            _used.Enqueue(slicePresenter);
        }


        private void OnReseted()
        {
            foreach (var slice in _used)
            {
                slice.DoReset();
            }
        }
    }
}
