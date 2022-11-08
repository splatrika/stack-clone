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

            model.BlockAdded += OnBlockAdded;
        }


        private void OnDestroy()
        {
            Model.BlockAdded -= OnBlockAdded;
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
            Height += blockPresenter.Height;

            _towerHeight.position = new Vector3(
                transform.position.x,
                Height,
                transform.position.z);

            // here will be perfect effect call
        }
    }
}
