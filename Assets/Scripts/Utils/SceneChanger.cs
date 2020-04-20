using System;
using Sound;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utils
{
    public class SceneChanger: MonoBehaviour
    {
        [SerializeField] private CanvasGroup loader;
        [SerializeField] private PanelsSlide panelsSlide;

        private static SceneChanger _instance;
        
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(this);
                DontDestroyOnLoad(loader);
            }
            else
            {
                Destroy(gameObject);
            }
            
        }

        public void ChangeScene(int index)
        {
            var coroutine = panelsSlide.ShowMe(loader);
            var asyncOperation = SceneManager.LoadSceneAsync(index);
            asyncOperation.completed += (operation) =>
            {
                StopCoroutine(coroutine);
                loader.alpha = 1;
                AudioManager.Instance.PoolAudioSources();
                panelsSlide.HideMe(loader);
            };
        }
    }
}