using Splatrika.StackClone.Model;
using UnityEngine;

namespace Splatrika.StackClone.Presenter
{
    public class BlockPresenter : Presenter<Block>
    {
        public float Height => _height;

        [SerializeField]
        private Renderer _cube;

        [SerializeField]
        private float _height;


        public void Reset()
        {
            _cube.enabled = false;
        }


        public void SetTop(float top)
        {
            _cube.transform.position = new Vector3(
                _cube.transform.position.x,
                top,
                _cube.transform.position.z);
        }


        public void Move(Vector2 center)
        {
            _cube.transform.position = new Vector3(
                center.x,
                _cube.transform.position.y,
                center.y);
        }


        protected override void OnInit(Block model)
        {
            _cube.enabled = true;
            _cube.material.color = model.Color;
            _cube.transform.localScale = new Vector3(
                model.Rect.width,
                _height,
                model.Rect.height);
            Move(model.Rect.center);
        }


        private void Awake()
        {
            Reset();
        }
    }
}
