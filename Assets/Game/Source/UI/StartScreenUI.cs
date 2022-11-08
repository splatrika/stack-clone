using Splatrika.StackClone.Model;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Splatrika.StackClone.UI
{
    public class StartScreenUI : MonoBehaviour
    {
        [SerializeField]
        private Button _run;

        private IGameLifeService _game;


        [Inject]
        public void Construct(IGameLifeService game)
        {
            _game = game;
        }


        private void Start()
        {
            _run.onClick.AddListener(OnRunClicked);

            _game.Reseted += OnGameReseted;
        }


        private void OnDestroy()
        {
            _game.Reseted -= OnGameReseted;
        }


        private void OnRunClicked()
        {
            _game.Run();
            gameObject.SetActive(false);
        }


        private void OnGameReseted()
        {
            gameObject.SetActive(true);
        }
    }
}
