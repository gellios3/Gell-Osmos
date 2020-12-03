using System;
using Gameplay.Modules.Gui.Signals;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Gameplay.Modules.Gui.Views
{
    public class MessagePopup : MonoBehaviour
    {
        [SerializeField] private GameObject content;

        [SerializeField] private TMP_Text message;
        [SerializeField] private Button reloadBtn;
        
        
        [Inject]
        public void Construct(SignalBus signalBus)
        {
            content.SetActive(false);
            
            reloadBtn.onClick.AddListener(ReloadScene);
            signalBus.Subscribe<ShowMessagePopupSignal>(ShowMessagePopup);
            
        }

        private void ShowMessagePopup(ShowMessagePopupSignal obj)
        {
            content.SetActive(true);
            message.text = obj.Message;
        }

        private void ReloadScene()
        {
            SceneManager.LoadScene("Main");
        }
    }
}