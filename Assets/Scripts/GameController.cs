using System;
using Programmer;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class GameController: MonoBehaviour
    {
        [SerializeField] private NecessityController[] programmers;
        [SerializeField] private Text timeDisplayer;
        [SerializeField] private float hours;
        [SerializeField] private float actualMinutes;
        
        private float _timeMultiplier;
        private float _minutesRemaining;
        
        private void Awake()
        {
            _minutesRemaining = hours * 60;
            _timeMultiplier =  _minutesRemaining / actualMinutes;
            foreach (var programmer in programmers)
            {
                programmer.OnMaxStressLevel += GameOver;
            }
        }

        private void Update()
        {
            var hours = (int) (_minutesRemaining / 60);
            var minutes = (int)(_minutesRemaining % 60);
            timeDisplayer.text = $"{hours}h {minutes}m";
            actualMinutes -= Time.deltaTime / 60;
            _minutesRemaining = _timeMultiplier * actualMinutes;
        }

        private void GameOver()
        {
            Debug.Log("Game Over");
        }
    }
}