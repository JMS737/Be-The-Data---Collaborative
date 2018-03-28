using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataVis.Collaboration
{
    public class Billboard : MonoBehaviour
    {

        private Camera playerCamera;

        // Use this for initialization
        void Start()
        {
            playerCamera = PlayerManager.LocalPlayerInstance.GetComponentInChildren<Camera>();
        }

        // Update is called once per frame
        void Update()
        {
            transform.LookAt(transform.position + (playerCamera.transform.rotation * Vector3.forward), Vector3.up);
        }
    }
}

