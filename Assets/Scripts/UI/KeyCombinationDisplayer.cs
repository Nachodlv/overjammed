using Programmer;
using Programmer.Necessities.Feelings;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class KeyCombinationDisplayer: MonoBehaviour
    {
        [SerializeField] private NecessityDisplayer necessityDisplayer;
        [SerializeField] private Image key1;
        [SerializeField] private Image key2;
        [SerializeField] private Image key3;

        private KeyUI[] _keys;
        
        public void ShowKeyCombination(KeyUI[] keyUis)
        {
            necessityDisplayer.gameObject.SetActive(false);
            gameObject.SetActive(true);
            _keys = keyUis;
            key1.sprite = keyUis[0].NormalSprite;
            key2.sprite = keyUis[1].NormalSprite;
            key3.sprite = keyUis[2].NormalSprite;
        }

        public void HideKeyCombination()
        {
            necessityDisplayer.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }

        public void CorrectKey(int index)
        {
            GetImage(index).sprite = _keys[index].SuccessfulSprite;
        }

        public void IncorrectKey(int index)
        {
            GetImage(index).sprite = _keys[index].ErrorSprite;
        }

        private Image GetImage(int index)
        {
            // Unity doesn't support switch expressions
            switch (index)
            {
                case 0: return key1;
                case 1: return key2;
                case 2: return key3;
            }

            return null;
        }
    }
}