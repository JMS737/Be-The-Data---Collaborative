using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataVis.Collaboration
{
    public class GraphManager : MonoBehaviour
    {

        private Axes axes;

    // Use this for initialization
        void Start()
        {
            axes = GetComponentInChildren<Axes>();
            axes.renderGrid(10, 10, 10);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

