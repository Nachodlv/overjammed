using System;
using UnityEngine;
using UnityEngine.UI;

namespace Sound
{
    [RequireComponent(typeof(Button), typeof(Image))]
    public class AudioButton: MonoBehaviour
    {
        [SerializeField] private Sprite unMuteSprite;
        [SerializeField] private Sprite unMuteSelectedSprite;
        [SerializeField] private Sprite muteSprite;
        [SerializeField] private Sprite muteSelectedSprite;
        [SerializeField] private AudioClip clickSound;

        private Button _button;
        private Image _image;
        private void Awake()
        {
            _button = GetComponent<Button>();
            _image = GetComponent<Image>();
            _button.onClick.AddListener(OnClick);
        }

        private void Start()
        {
            if(AudioManager.Instance.Muted) Mute();
            else UnMute();
        }

        private void OnClick()
        {
            if (AudioManager.Instance.Muted)
            {
                UnMute();
                AudioManager.Instance.PlaySound(clickSound);
                return;
            }
            AudioManager.Instance.PlaySound(clickSound);
            Mute();
        }

        private void Mute()
        {
            _image.sprite = muteSprite;
            var spriteState = _button.spriteState;
            spriteState.highlightedSprite = muteSelectedSprite;
            _button.spriteState = spriteState;
            AudioManager.Instance.Mute();
        }

        private void UnMute()
        {
            _image.sprite = unMuteSprite;
            var spriteState = _button.spriteState;
            spriteState.highlightedSprite = unMuteSelectedSprite;
            _button.spriteState = spriteState;
            AudioManager.Instance.UnMute();
        }
    }
}