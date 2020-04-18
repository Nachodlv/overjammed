using System;
using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SortOrderModifier : MonoBehaviour
    {
        [SerializeField] private float extraYAxis;

        private void Awake()
        {
            GetComponent<SpriteRenderer>().sortingOrder =
                Mathf.RoundToInt((transform.position.y + extraYAxis) * 100f) * -1;
        }
    }
}