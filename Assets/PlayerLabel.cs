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
            textTransfrom.position = new Vector3(0, 0, 0);
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