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
            model.Reseted += OnReseted;
            _rigidBody.isKinematic = true;

            _blockPresenter.Init(model.Block);
        }


        protected override void OnUpdate(float deltaTime)
        {
            if (Model.IsDestroyed || !Model.IsRunned)
            {
                return;
            }
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
            Model.Reseted -= OnReseted;
        }


        private void OnPushedToTower()
        {
            _blockPresenter.SetTop(_towerPresenter.Height);
            _blockPresenter.Init(Model.Block);
        }


        private void OnDestroyed()
        {
            _rigidBody.isKinematic = false;
        }


        private void OnReseted()
        {
            _rigidBody.isKinematic = true;
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.Euler(Vector3.zero);
            _blockPresenter.Init(Model.Block);
            _blockPresenter.SetTop(0);
        }
    }
}
