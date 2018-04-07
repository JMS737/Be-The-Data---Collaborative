using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DataVis.Collaboration
{
    /**
     * Used to display information about the selected data point.
     */
    public class HUDDataLabel : MonoBehaviour
    {

        Text textObj;

        // Use this for initialization
        void Start()
        {
            textObj = GetComponent<Text>();
        }

        public void SetDataLabel(string label)
        {
            textObj.text = label;
        }
    }
}

