using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Text))]
    public class LevelDisplayer: MonoBehaviour
    {
        private Text _text;

        private void Awake()
        {
            _text = GetComponent<Text>();
        }

        private void Start()
        {
            _text.text = LevelManager.Instance.Level > 1 ? LevelManager.Instance.Level.ToString() : "";
        }
    }
}