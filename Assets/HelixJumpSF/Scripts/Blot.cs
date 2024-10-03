using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HelixJump
{
    public class Blot : MonoBehaviour
    {
        [SerializeField] private float offset;

        [SerializeField] private SpriteRenderer spriteRenderer;

        public void Init(Vector3 position, Color color)
        {
            transform.position = position + new Vector3(0, offset, 0);
            transform.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);

            spriteRenderer.color = color;
        }
    }
}

