using System;
using Programmer;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(NecessityController))]
    public class StressLevelDisplayer: MonoBehaviour
    {
        [SerializeField] private StatBar statBar;
        
        private NecessityController _necessityController;

        private void Awake()
        {
            _necessityController = GetComponent<NecessityController>();
        }

        private void Start()
        {
            const int value = (int) NecessityController.MAX_STRESS_LEVEL;
            statBar.MaxValue = value;
            statBar.CurrentValue = value;
        }

        private void Update()
        {
            statBar.CurrentValue = (int) _necessityController.StressLevel;
        }
    }
}