using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataVis.Collaboration
{
    public class SphereButton : MonoBehaviour
    {
        public float highlightScaleFactor = 1.25f;

        private Vector3 originalScale;

        // Use this for initialization
        void Start()
        {
            originalScale = transform.localScale;
        }

        // If isHighlighted, scales the attached object by the highlightScaleFactor.
        private void Highlight(bool isHighlighted)
        {
            if (isHighlighted)
            {
                transform.localScale = originalScale * highlightScaleFactor;
            }
            else
            {
                transform.localScale = originalScale;
            }
        }

        public void OnPointerEnter()
        {
            Highlight(true);
        }

        public void OnPointerExit()
        {
            Highlight(false);
        }
    }
}