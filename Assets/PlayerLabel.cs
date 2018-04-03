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

        RectTransform textTransfrom;

        // Use this for initialization
        void Start()
        {
            textTransfrom = GetComponent<RectTransform>();
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 direction = ((targetPlayerTransform.position + (Vector3.up * 0.5f)) - localPlayerTransform.position).normalized;
            textTransfrom.position = localPlayerTransform.position + (direction * 1);
        }

        public void SetupLabel(string name, Color colour, Transform playerTransform)
        {
            Text text = GetComponent<Text>();
            text.text = name;
            text.color = colour;

            targetPlayerTransform = playerTransform;
            localPlayerTransform = PlayerManager.LocalPlayerInstance.transform;
        }
    }

}