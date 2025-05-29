using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class Scroller : MonoBehaviour
    {
        [SerializeField]
        RawImage image;

        [SerializeField] private float _x, _y;

        private void Update()
        {
            image.uvRect = new Rect(image.uvRect.position + new Vector2(_x,_y)*Time.deltaTime, image.uvRect.size);
        }
    }
}