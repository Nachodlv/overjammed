using System;
using System.Collections;
using Sound;
using UnityEngine;

namespace DefaultNamespace
{
    public class Interference: MonoBehaviour
    {
        [SerializeField] private float interferenceTime = 5f;
        [SerializeField] private GameObject interference;
        [SerializeField] private AudioClip interferenceAudio;
        [SerializeField] private GameObject playerGUI;

        public event Action OnFinishInterferation;
        
        public void ShowInterference()
        {
            interference.SetActive(true);
            playerGUI.SetActive(false);
            AudioManager.Instance.FadeOutClip(1f);
            AudioManager.Instance.PlaySound(interferenceAudio);
            StartCoroutine(WaitInterference());
        }

        private IEnumerator WaitInterference()
        {
            yield return new WaitForSeconds(interferenceTime);
            OnFinishInterferation?.Invoke();
        }
    }
}