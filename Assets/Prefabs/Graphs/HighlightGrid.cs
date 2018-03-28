using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataVis.Collaboration
{
    public class HighlightGrid : MonoBehaviour
    {

        private GameObject GridObject;
        private GameObject Labels;

        private float thickness;

        // Use this for initialization
        void Start()
        {
            GridObject = transform.GetChild(0).gameObject;
            Labels = transform.GetChild(1).gameObject;

            DisableGrid();
            thickness = GridObject.transform.localScale.z;
        }

        public void SetupGrid(float maxY, float maxZ)
        {
            GridObject.transform.localScale = new Vector3(maxZ, maxY, thickness);
            GridObject.transform.position = new Vector3(0.0f, maxY / 2.0f, maxZ / 2.0f);
            GridObject.GetComponent<Renderer>().material.mainTextureScale = new Vector2(maxZ, maxY);

            Labels.GetComponent<HighlightGridLabels>().SetupLabels(maxY, maxZ);
        }

        public void EnableGrid(float x, string date)
        {
            Vector3 newPosition = transform.position;
            newPosition.x = x;

            transform.position = newPosition;

            Labels.GetComponent<HighlightGridLabels>().SetDate(date);

            GridObject.SetActive(true);
            Labels.SetActive(true);
        }

        public void DisableGrid()
        {
            GridObject.SetActive(false);
            Labels.SetActive(false);
        }

    }

}
