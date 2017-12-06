﻿using UnityEngine;
using System.Collections;

namespace DataVis.Collaboration
{
    public class Axes : MonoBehaviour
    {
        private float thickness;

        private GameObject xzGrid;
        private GameObject xyGrid;
        private GameObject zyGrid_left;
        private GameObject zyGrid_right;

        private void Start()
        {
            xzGrid = gameObject.transform.GetChild(0).gameObject;
            xyGrid = gameObject.transform.GetChild(1).gameObject;
            zyGrid_left = gameObject.transform.GetChild(2).gameObject;
            zyGrid_right = gameObject.transform.GetChild(3).gameObject;

            thickness = xzGrid.transform.localScale.z;
        }

        // Renders the grids to the parameterised dimensions
        public void renderGrid(float x, float y, float z)
        {
            Debug.Log("Setting graph axis x=" + x.ToString() + " y=" + y.ToString() + " z=" + z.ToString());
            // Scale each grid to the relevant dimensions
            xzGrid.transform.localScale = new Vector3(x, z, thickness);
            Debug.Log("xz Works");
            xyGrid.transform.localScale = new Vector3(x, y, thickness);
            Debug.Log("xy Works");
            zyGrid_left.transform.localScale = new Vector3(z, y, thickness);
            Debug.Log("zy left Works");
            zyGrid_right.transform.localScale = new Vector3(z, y, thickness);
            Debug.Log("zy right Works");
            Debug.Log("Here 1");

            // Position the grids
            xzGrid.transform.position = new Vector3(x / 2.0f, 0.0f, z / 2.0f);
            xyGrid.transform.position = new Vector3(x / 2.0f, y / 2.0f, z);
            zyGrid_left.transform.position = new Vector3(0.0f, y / 2.0f, z / 2.0f);
            zyGrid_right.transform.position = new Vector3(x, y / 2.0f, z / 2.0f);
            Debug.Log("Here 2");

            // Adjust textures to spread evenly over the passed dimensions
            xzGrid.GetComponent<Renderer>().material.mainTextureScale = new Vector2(x, z);
            xyGrid.GetComponent<Renderer>().material.mainTextureScale = new Vector2(x, y);
            zyGrid_left.GetComponent<Renderer>().material.mainTextureScale = new Vector2(z, y);
            zyGrid_right.GetComponent<Renderer>().material.mainTextureScale = new Vector2(z, y);
            Debug.Log("Here 3");
        }
    }
}


