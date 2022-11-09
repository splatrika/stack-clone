using Splatrika.StackClone.Model;
using UnityEngine;

namespace Splatrika.StackClone.Presenter
{
    public class FallingSlicePresenter : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody _rigidbody;

        [SerializeField]
        private BlockPresenter _block;

        private Transform _transform;


        public void Init(Block block, float top)
        {
            gameObject.SetActive(true);
            _transform.position = Vector3.zero;
            _transform.rotation = Quaternion.Euler(Vector3.zero);
            _block.Init(block);
            _block.SetTop(top);
            _rigidbody.isKinematic = false;
            _rigidbody.velocity = Vector3.zero;
        }


        public void DoReset()
        {
            _block.DoReset();
            gameObject.SetActive(false);
        }


        private void Awake()
        {
            _rigidbody.isKinematic = true;
            _transform = transform;
            gameObject.SetActive(false);
        }


        private void OnValidate()
        {
            if (_block.gameObject == gameObject ||
                !_block.transform.IsChildOf(transform))
            {
                Debug.LogError("Block should be a childer of " +
                    "FallingSlicePresenter");
                _block = null;
            }
        }
    }
}
