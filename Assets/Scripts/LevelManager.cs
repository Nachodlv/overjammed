using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class LevelManager: MonoBehaviour
    {
        public static LevelManager Instance;
        public int Level { get; private set; }
        private void Awake()
        {
            Level = 1;
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
        }

        public void NextLevel()
        {
            Level++;
        }
    }
}