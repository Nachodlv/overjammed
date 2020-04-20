using System;
using System.Collections;
using Programmer;
using Sound;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace DefaultNamespace
{
    public class GameController: MonoBehaviour
    {
        [SerializeField] private NecessityController[] programmers;
        [SerializeField] private Text timeDisplayer;
        [SerializeField] private float hours;
        [SerializeField] private float actualMinutes;
        [SerializeField] private PanelsSlide gameOverPanel;
        [SerializeField] private PanelsSlide winPanel;
        [SerializeField] private AudioClip mainGameAudio;
        [SerializeField] private AudioClip gameOverSound;
        [SerializeField] private Interference interference;
        
        private float _timeMultiplier;
        private float _minutesRemaining;
        private bool _gameOver;
        
        private void Awake()
        {
            _minutesRemaining = hours * 60;
            _timeMultiplier =  _minutesRemaining / actualMinutes;
            gameOverPanel.onFinishSlides.AddListener(GoToMenu);
            winPanel.onFinishSlides.AddListener(interference.ShowInterference);
            interference.OnFinishInterferation += GoToMenu;
            foreach (var programmer in programmers)
            {
                programmer.OnMaxStressLevel += GameOver;
            }
        }

        private void Start()
        {
            AudioManager.Instance.ChangeClip(mainGameAudio);
        }

        private void Update()
        {
            if (_gameOver) return;
            var hours = (int) (_minutesRemaining / 60);
            var minutes = (int)(_minutesRemaining % 60);
            timeDisplayer.text = $"{hours}h {minutes}m";
            actualMinutes -= Time.deltaTime / 60;
            _minutesRemaining = _timeMultiplier * actualMinutes;
            if(actualMinutes <= 0) Win();
        }

        private void Win()
        {
            if (_gameOver) return;
            LevelManager.Instance.NextLevel();
            _gameOver = true;
            winPanel.StartSlides();
            AudioManager.Instance.SoundEffectsMuted = true;
        }
        
        private void GameOver()
        {
            if (_gameOver) return;
            _gameOver = true;
            gameOverPanel.StartSlides();
            AudioManager.Instance.FadeOutClip();
            AudioManager.Instance.PlaySoundWithFade(gameOverSound, 0.5f);
            AudioManager.Instance.SoundEffectsMuted = true;
        }

        
        private void GoToMenu()
        {
            AudioManager.Instance.SoundEffectsMuted = false;
            SceneChanger.Instance.ChangeScene(0);
        }
    }
}