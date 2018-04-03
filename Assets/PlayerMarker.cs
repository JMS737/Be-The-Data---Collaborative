using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataVis.Collaboration
{
    public class PlayerMarker : MonoBehaviour
    {

        Transform targetPlayerTransform;
        Transform localPlayerTransform;

        // Use this for initialization
        void Start()
        {
            targetPlayerTransform = transform.parent.transform;
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
                Vector3 direction = ((targetPlayerTransform.position + (Vector3.up * 0.3f)) - localPlayerTransform.position).normalized;
                transform.position = localPlayerTransform.position + (direction * 1);
            }
        }

        public void SetColour(Color colour)
        {
            GetComponent<Renderer>().material.SetColor("_EmissionColor", colour);
        }
    }

}
