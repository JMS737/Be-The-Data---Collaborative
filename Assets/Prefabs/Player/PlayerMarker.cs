using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataVis.Collaboration
{
    public class PlayerMarker : MonoBehaviour
    {
        public float highlightScale = 1.25f;

        Transform targetPlayerTransform;
        Transform localPlayerTransform;

        Vector3 originalScale;

        private float additionalScale = 1.0f;

        // Use this for initialization
        void Start()
        {
            targetPlayerTransform = transform.parent.transform;
            originalScale = transform.localScale;
        }

        // Update is called once per frame
        void Update()
        {
            if (localPlayerTransform == null)
            {
                localPlayerTransform = PlayerManager.LocalPlayerInstance.transform;
            }
            else
            {
                float distance = (targetPlayerTransform.position - localPlayerTransform.position).magnitude;
                transform.localScale = originalScale * distance * additionalScale;
            }
        }

        public void SetColour(Color colour)
        {
            GetComponent<Renderer>().material.SetColor("_EmissionColor", colour);
        }

        public void Highlight(bool IsHighlighted)
        {
            if (IsHighlighted)
            {
                additionalScale = highlightScale;
            }
            else
            {
                additionalScale = 1.0f;
            }
        }
    }

}
