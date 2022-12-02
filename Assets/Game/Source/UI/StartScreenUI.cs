using Splatrika.StackClone.Model;
using Splatrika.StackClone.Presenter;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Splatrika.StackClone.UI
{
    public class StartScreenUI : MonoBehaviour
    {
        [SerializeField]
        private Button _run;

        [SerializeField]
        private Button _credits;

        private IGameLifeService _game;
        private IScenesService _scenesService;


        [Inject]
        public void Construct(
            IGameLifeService game,
            IScenesService scenesService)
        {
            _game = game;
            _scenesService = scenesService;
        }


        private void Start()
        {
            _run.onClick.AddListener(OnRunClicked);
            _credits.onClick.AddListener(OnCreditsClicked);

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


        private void OnCreditsClicked()
        {
            _scenesService.Load(Scenes.Credits);
        }


        private void OnGameReseted()
        {
            gameObject.SetActive(true);
        }
    }
}
