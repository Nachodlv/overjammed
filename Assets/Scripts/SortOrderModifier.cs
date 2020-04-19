using System;
using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SortOrderModifier : MonoBehaviour
    {
        [SerializeField] private float extraYAxis;

        public float ExtraYAxis
        {
            get => extraYAxis;
            set => extraYAxis = value;
        }

        private void Awake()
        {
            PositionChanged();
        }

        public void PositionChanged()
        {
            GetComponent<SpriteRenderer>().sortingOrder =
                Mathf.RoundToInt((transform.position.y + extraYAxis) * 100f) * -1;
        }
    }
}