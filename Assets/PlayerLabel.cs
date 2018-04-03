using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DataVis.Collaboration
{
    public class PlayerLabel : MonoBehaviour
    {

        Transform targetPlayerTransform;
        Transform localPlayerTransform;

        Transform textTransfrom;

        // Use this for initialization
        void Start()
        {
            textTransfrom = GetComponent<Transform>();
        }

        // Update is called once per frame
        void Update()
        {
            //textTransfrom.position = targetPlayerTransform.position + Vector3.up * 5;
        }

        public void SetupLabel(string name, Color colour, Transform playerTransform)
        {
            TextMesh text = GetComponent<TextMesh>();
            text.text = name;
            text.color = colour;

            targetPlayerTransform = playerTransform;
            localPlayerTransform = PlayerManager.LocalPlayerInstance.transform;
        }
    }

}