using System;
using Sound;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class PlayButton: MonoBehaviour
    {
        [SerializeField] private AudioClip audioClip;
        [SerializeField] private PanelsSlide panelsSlide;
        [SerializeField] private AudioClip menuAudio;
        
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(ButtonClicked);
            panelsSlide.onFinishSlides.AddListener(GoToMainGame);
        }

        private void Start()
        {
            AudioManager.Instance.ChangeClip(menuAudio);
        }

        private void ButtonClicked()
        {
            AudioManager.Instance.PlaySound(audioClip);    
        }

        private void GoToMainGame()
        {
            SceneChanger.Instance.ChangeScene(1);
        }
    }
}