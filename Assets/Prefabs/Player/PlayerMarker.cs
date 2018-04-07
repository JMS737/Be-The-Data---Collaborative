using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataVis.Collaboration
{
    /**
     * This script is used to scale the marker above the player so that
     * it remains a constant size to the local player.
     * This makes identifying players in the world easier.
     */
    public class PlayerMarker : MonoBehaviour
    {
        public float highlightScale = 1.25f;

        private Transform targetPlayerTransform;
        private Transform localPlayerTransform;

        private Vector3 originalScale;

        // Values used to apply other scale factors, such as when highlighted.
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
            // Due to load times this did not work in the 'Start' method.
            if (localPlayerTransform == null)
            {
                localPlayerTransform = PlayerManager.LocalPlayerInstance.transform;
            }
            else
            {
                // Scale the object based on the players distance to the local player.
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
