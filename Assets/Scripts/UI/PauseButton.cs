using System;
using Sound;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Button), typeof(Image))]
    public class PauseButton : MonoBehaviour
    {
        [SerializeField] private Sprite pausedSprite;
        [SerializeField] private Sprite unPausedSprite;
        [SerializeField] private AudioClip buttonClicked;
        [SerializeField] private GameObject pausedPanel;
        [SerializeField] private AudioSource audioSource;

        private Button _button;
        private Image _image;
        private bool paused;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _button = GetComponent<Button>();
            _button.onClick.AddListener(ButtonClicked);
        }

        private void ButtonClicked()
        {
            AudioManager.Instance.PlaySound(buttonClicked);
            if (paused)
            {
                _image.sprite = unPausedSprite;
                Time.timeScale = 1;
                paused = false;
                pausedPanel.SetActive(false);
                audioSource.Stop();
                AudioManager.Instance.UnMute();
            }
            else
            {
                AudioManager.Instance.Mute();
                audioSource.Play();
                _image.sprite = pausedSprite;
                Time.timeScale = 0;
                paused = true;
                pausedPanel.SetActive(true);
            }
        }
    }
}