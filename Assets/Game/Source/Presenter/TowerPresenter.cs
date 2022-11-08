using System.Collections.Generic;
using Splatrika.StackClone.Model;
using UnityEngine;
using Zenject;

namespace Splatrika.StackClone.Presenter
{
    public class TowerPresenter : Presenter<Tower>
    {
        public float Height { get; private set; }

        [SerializeField]
        private BlockPresenter _prefab;

        [SerializeField]
        private int _poolSize;

        [SerializeField]
        private Transform _towerHeight;

        [SerializeField]
        private Renderer _towerFundament;

        private ILogger _logger;
        private Stack<BlockPresenter> _free;
        private Queue<BlockPresenter> _used;


        [Inject]
        public void Construct(ILogger logger)
        {
            _logger = logger;
        }


        protected override sealed void OnInit(Tower model)
        {
            _free = new Stack<BlockPresenter>(_poolSize);
            _used = new Queue<BlockPresenter>(_poolSize);
            for (int i = 0; i < _poolSize; i++)
            {
                _free.Push(Instantiate(_prefab));
            }

            _towerFundament.material.color = model.Last.Color;

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
            BlockPresenter blockPresenter = null;
            if (_free.Count > 0)
            {
                blockPresenter = _free.Pop();
            }
            else if (!_used.TryDequeue(out blockPresenter))
            {
                _logger.LogError(nameof(TowerPresenter),
                    "There is no cubes");
                return;
            }
            blockPresenter.Init(block);
            blockPresenter.SetTop(Height);
            _used.Enqueue(blockPresenter);
            SetHeight(Height + blockPresenter.Height);

            // here will be perfect effect call
        }


        private void OnReseted()
        {
            foreach (var block in _used)
            {
                block.Reset();
            }
            SetHeight(0);
        }


        private void SetHeight(float height)
        {
            Height = height;
            _towerHeight.position = new Vector3(
                transform.position.x,
                Height,
                transform.position.z);
        }
    }
}
