using UnityEngine;

namespace Programmer.Necessities.Feelings
{
    public class KeyUI: MonoBehaviour
    {
        [SerializeField] private KeyCode keyCode;
        [SerializeField] private Sprite normalSprite;
        [SerializeField] private Sprite successfulSprite;
        [SerializeField] private Sprite errorSprite;

        public KeyCode KeyCode => keyCode;
        public Sprite NormalSprite => normalSprite;
        public Sprite SuccessfulSprite => successfulSprite;
        public Sprite ErrorSprite => errorSprite;
    }
}