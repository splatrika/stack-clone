using Splatrika.StackClone.Model;
using UnityEngine;

namespace Splatrika.StackClone.Presenter
{
    public class TowerSoundsPresenter : Presenter<Tower>
    {
        [SerializeField]
        private AudioSource _blockAdded;

        [SerializeField]
        private AudioSource _perfect;


        protected override void OnInit(Tower model)
        {
            model.BlockAdded += OnBlockAdded;
        }


        private void OnDestroy()
        {
            Model.BlockAdded -= OnBlockAdded;
        }


        private void OnBlockAdded(Block block, bool perfect)
        {
            if (perfect)
            {
                _perfect.Play();
                return;
            }
            _blockAdded.Play();
        }
    }
}
