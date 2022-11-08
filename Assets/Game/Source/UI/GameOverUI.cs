using Splatrika.StackClone.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Splatrika.StackClone.UI
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField]
        private Button _reset;

        [SerializeField]
        private TextMeshProUGUI _score;

        [SerializeField]
        private string _scoreFormat = "Score: {0}";

        private ITower _tower;
        private IGameLifeService _game;


        [Inject]
        public void Construct(ITower tower, IGameLifeService game)
        {
            _tower = tower;
            _game = game;
        }


        private void Start()
        {
            gameObject.SetActive(false);

            _tower.Finished += OnTowerFinished;
            _reset.onClick.AddListener(OnResetClick);
        }


        private void OnDestroy()
        {
            _tower.Finished -= OnTowerFinished;
        }


        private void OnTowerFinished()
        {
            gameObject.SetActive(true);

            _score.text = string.Format(_scoreFormat, _tower.Blocks.ToString());
        }


        private void OnResetClick()
        {
            _game.Reset();
            gameObject.SetActive(false);
        }
    }
}