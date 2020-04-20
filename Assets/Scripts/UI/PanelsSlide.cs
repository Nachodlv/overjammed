using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class PanelsSlide: MonoBehaviour
    {
        [SerializeField] private CanvasGroup[] panels;
        [SerializeField] private float transitionTime = 1f;
        [SerializeField] private float secondsBlocked = 2f;
        
        public UnityEvent onFinishSlides;
        
        private int currentIndex;
        private bool blocked;
        private Func<IEnumerator> _waitForUnBlock;
        private Func<CanvasGroup, IEnumerator> _increaseAlphaPanel;
        private Func<CanvasGroup, IEnumerator> _decreaseAlphaPanel;
        private WaitForSeconds _waitTime;
        private void Awake()
        {
            _waitForUnBlock = WaitForUnBlock;
            _decreaseAlphaPanel = DecreaseAlphaPanel;
            _increaseAlphaPanel = IncreaseAlphaPanel;
            _waitTime = new WaitForSeconds(secondsBlocked);
            blocked = true;
        }

        public void StartSlides()
        {
            ShowPanel(panels[currentIndex]);
            StartCoroutine(_waitForUnBlock());
        }

        public void HideMe(CanvasGroup canvasGroup)
        {
            HidePanel(canvasGroup);
        }

        public Coroutine ShowMe(CanvasGroup canvasGroup)
        {
            return ShowPanel(canvasGroup);
        }

        private void Update()
        {
            if (blocked || !Input.anyKeyDown) return;
            HidePanel(panels[currentIndex]);
            currentIndex++;
            if (currentIndex == panels.Length)
            {
                onFinishSlides?.Invoke();
                blocked = true;
                return;
            }
            ShowPanel(panels[currentIndex]);
            blocked = true;
            StartCoroutine(_waitForUnBlock());
        }

        private Coroutine HidePanel(CanvasGroup panel)
        {
            panel.interactable = false;
            panel.blocksRaycasts = false;
            return StartCoroutine(_decreaseAlphaPanel(panel));
        }
        
        private Coroutine ShowPanel(CanvasGroup panel)
        {
            panel.interactable = true;
            panel.blocksRaycasts = true;
            return StartCoroutine(_increaseAlphaPanel(panel));
        }
        
        private IEnumerator WaitForUnBlock()
        {
            yield return _waitTime;
            blocked = false;
        }

        private IEnumerator IncreaseAlphaPanel(CanvasGroup panel)
        {
            while (Math.Abs(panel.alpha) < 0.99f)
            {
                panel.alpha += transitionTime * Time.deltaTime;
                yield return null;
            }
        }
        
        private IEnumerator DecreaseAlphaPanel(CanvasGroup panel)
        {
            while (Math.Abs(panel.alpha) > 0.001f)
            {
                panel.alpha -= transitionTime * Time.deltaTime;
                yield return null;
            }
        }
    }
}