﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataVis.Collaboration
{
    public class PlayerMarker : MonoBehaviour
    {

        Transform targetPlayerTransform;
        Transform localPlayerTransform;

        RectTransform textTransfrom;

        // Use this for initialization
        void Start()
        {
            textTransfrom = GetComponent<RectTransform>();
            localPlayerTransform = PlayerManager.LocalPlayerInstance.transform;
            targetPlayerTransform = transform.parent.transform;
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 direction = ((targetPlayerTransform.position + (Vector3.up * 0.1f)) - localPlayerTransform.position).normalized;
            textTransfrom.position = localPlayerTransform.position + (direction * 1);
        }

        public void SetColour(Color colour)
        {

            GetComponent<Renderer>().material.SetColor("_EmissionColor", colour);
        }
    }

}
