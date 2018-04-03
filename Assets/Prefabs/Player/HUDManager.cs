using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataVis.Collaboration
{
    public class HUDManager : MonoBehaviour
    {
        public GameObject playerLabelPrefab;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void AddPlayerLabel(string name, Color colour, Transform transform)
        {
            GameObject label = Instantiate(playerLabelPrefab, this.transform);
            label.GetComponent<PlayerLabel>().SetupLabel(name, colour, transform);
        }
    }
}

