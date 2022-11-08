using Splatrika.StackClone.Model;
using UnityEngine;

namespace Splatrika.StackClone.Presenter
{
    public class MovingBlockPresenter : Presenter<MovingBlock>
    {
        [SerializeField]
        private BlockPresenter _blockPresenter;

        [SerializeField]
        private TowerPresenter _towerPresenter;

        [SerializeField]
        private Rigidbody _rigidBody;


        protected override void OnInit(MovingBlock model)
        {
            model.PushedToTower += OnPushedToTower;
            model.Destroyed += OnDestroyed;
            _rigidBody.isKinematic = true;

            _blockPresenter.Init(model.Block);
        }


        protected override void OnUpdate(float deltaTime)
        {
            _blockPresenter.Move(Model.Rect.center);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Model.PushToTower();
            }
        }


        private void OnDestroy()
        {
            Model.PushedToTower -= OnPushedToTower;
            Model.Destroyed -= OnDestroyed;
        }


        private void OnPushedToTower()
        {
            _blockPresenter.SetTop(_towerPresenter.Height);
            _blockPresenter.Init(Model.Block);
        }


        private void OnDestroyed()
        {
            //_blockPresenter.Reset();
            _rigidBody.isKinematic = false;
        }
    }
}
