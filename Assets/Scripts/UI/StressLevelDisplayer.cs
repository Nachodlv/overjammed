using System;
using System.Collections.Generic;
using Programmer;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(NecessityController))]
    public class StressLevelDisplayer: MonoBehaviour
    {
        [SerializeField] private List<Sprite> sprites;
        [SerializeField] private Image stressLevelImage;
        
        private NecessityController _necessityController;
        private int _previousIndex;
        private float _range;

        private void Awake()
        {
            _necessityController = GetComponent<NecessityController>();
            _range = NecessityController.MAX_STRESS_LEVEL / sprites.Count;
        }

        private void Update()
        {
            var newIndex = Mathf.CeilToInt(_necessityController.StressLevel / _range);
            if (newIndex == _previousIndex || newIndex >= sprites.Count) return;
            stressLevelImage.sprite = sprites[newIndex];
            _previousIndex = newIndex;
        }
    }
}