using System;
using Player;
using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Grabbable : MonoBehaviour
    {
        [SerializeField] private ItemType itemType;
        public Sprite Sprite { get; private set; }
        public ItemType ItemType => itemType;

        private void Awake()
        {
            Sprite = GetComponent<SpriteRenderer>().sprite;
        }

    }
}