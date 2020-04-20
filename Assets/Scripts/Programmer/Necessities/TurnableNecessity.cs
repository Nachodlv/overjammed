using System;
using Interactables;
using UnityEngine;

namespace Programmer.Necessities
{
    public class TurnableNecessity: Necessity
    {
        [SerializeField] private Turnable turnable;
        
        protected override void Awake()
        {
            base.Awake();
            turnable.OnStartWorking += StartWorking;
            turnable.OnStopWorking += StopWorking;
            increaseRatio = 0;
        }

        private void StartWorking()
        {
            CurrentNeed = 0;
            Satisfy();
        }

        private void StopWorking()
        {
            CurrentNeed = 100;
            Need();
        }
    }
}