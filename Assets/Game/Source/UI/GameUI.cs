using Splatrika.StackClone.Model;
using TMPro;
using UnityEngine;
using Zenject;

namespace Splatrika.StackClone.UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _score;

        private ITower _tower;
        private MovingBlock _movingBlock;
        private IGameLifeService _game;


        [Inject]
        public void Construct(
            ITower tower,
            MovingBlock movingBlock,
            IGameLifeService game)
        {
            _tower = tower;
            _movingBlock = movingBlock;
            _game = game;
        }


        private void Start()
        {
            gameObject.SetActive(false);

            _tower.BlockAdded += OnTowerBlockAdded;
            _tower.Finished += OnTowerFinished;
            _game.Runned += OnGameRunned;
        }


        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _movingBlock.PushToTower();
            }
        }


        private void OnTowerBlockAdded(Block block, bool perfect)
        {
            _score.text = _tower.Blocks.ToString();
        }


        private void OnTowerFinished()
        {
            gameObject.SetActive(false);
        }


        private void OnGameRunned()
        {
            gameObject.SetActive(true);
            _score.text = "0";
        }
    }
}
