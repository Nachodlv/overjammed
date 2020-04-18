using System;
using Player;
using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Grabbable : MonoBehaviour
    {
        [SerializeField] private ItemType itemType;
        [SerializeField] private Sprite sprite;
        public Sprite Sprite { get; private set; }
        public ItemType ItemType => itemType;

        private void Awake()
        {
            Sprite = sprite == null ? GetComponent<SpriteRenderer>().sprite : sprite;
        }

    }
}