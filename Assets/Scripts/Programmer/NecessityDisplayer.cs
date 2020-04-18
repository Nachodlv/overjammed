using UnityEngine;

namespace Programmer
{
    public class NecessityDisplayer : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteToRender;
        
        public void DisplayNecessity(Necessity necessity)
        {
            gameObject.SetActive(true);
            spriteToRender.sprite = necessity.Sprite;
        }

        public void HideNecessity()
        {
            gameObject.SetActive(false);
        }
    }
}