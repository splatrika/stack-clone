using Splatrika.StackClone.Model;
using Splatrika.StackClone.Presenter;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Splatrika.StackClone.UI
{
    public class CreditsUI : MonoBehaviour
    {
        [SerializeField]
        private Button _back;

        private IScenesService _scenesService;


        [Inject]
        public void Init(IScenesService scenesService)
        {
            _scenesService = scenesService;
        }


        private void Start()
        {
            _back.onClick.AddListener(OnBackClicked);
        }


        private void OnBackClicked()
        {
            _scenesService.Load(Scenes.Gameplay);
        }
    }
}
