using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataVis.Collaboration
{
    public class HighlightGridLabels : MonoBehaviour
    {

        public GameObject LabelPrefabSmall;
        public GameObject LabelPrefabLarge;

        private GameObject xAxisLabelParent;
        private GameObject yLAxisLabelParent;
        private GameObject yRAxisLabelParent;
        private GameObject zLAxisLabelParent;
        private GameObject zRAxisLabelParent;

        private GameObject xLabel;
        private List<GameObject> yLLabels = new List<GameObject>();
        private List<GameObject> yRLabels = new List<GameObject>();
        private List<GameObject> zLLabels = new List<GameObject>();
        private List<GameObject> zRLabels = new List<GameObject>();

        private Transform playerTransform;

        // Use this for initialization
        void Start()
        {
            
        }

        private void Update()
        {
            if (playerTransform == null)
            {
                playerTransform = PlayerManager.LocalPlayerInstance.transform;
            }

            if (playerTransform.position.x < transform.position.x)
            {
                yLAxisLabelParent.SetActive(false);
                zLAxisLabelParent.SetActive(false);

                yRAxisLabelParent.SetActive(true);
                zRAxisLabelParent.SetActive(true);
            }
            else
            {
                yLAxisLabelParent.SetActive(true);
                zLAxisLabelParent.SetActive(true);

                yRAxisLabelParent.SetActive(false);
                zRAxisLabelParent.SetActive(false);
            }
        }

        public void SetupLabels(float maxY, float maxZ)
        {
            xAxisLabelParent = transform.GetChild(0).gameObject;
            yLAxisLabelParent = transform.GetChild(1).gameObject;
            yRAxisLabelParent = transform.GetChild(2).gameObject;
            zLAxisLabelParent = transform.GetChild(3).gameObject;
            zRAxisLabelParent = transform.GetChild(4).gameObject;

            xAxisLabelParent.transform.position = new Vector3(0.0f, -0.5f, 0.0f);
            yLAxisLabelParent.transform.position = new Vector3(0.0f, 0.0f, -0.5f);
            yRAxisLabelParent.transform.position = new Vector3(0.0f, 0.0f, -0.5f);
            zLAxisLabelParent.transform.position = new Vector3(0.0f, maxY + 0.5f, 0.0f);
            zRAxisLabelParent.transform.position = new Vector3(0.0f, maxY + 0.5f, 0.0f);

            GameObject labelTmp;

            for (int y = 0; y <= maxY; y++)
            {
                labelTmp = Instantiate(LabelPrefabLarge, yLAxisLabelParent.transform, false);
                labelTmp.transform.Translate(new Vector3(y, 0.0f, 0.0f));
                labelTmp.GetComponent<TextMesh>().text = y.ToString();
                yLLabels.Add(labelTmp);

                labelTmp = Instantiate(LabelPrefabLarge, yRAxisLabelParent.transform, false);
                labelTmp.transform.Translate(new Vector3(-y, 0.0f, 0.0f));
                labelTmp.GetComponent<TextMesh>().text = y.ToString();
                yRLabels.Add(labelTmp);
            }

            for (int z = 0; z <= maxZ; z++)
            {
                labelTmp = Instantiate(LabelPrefabLarge, zLAxisLabelParent.transform, false);
                labelTmp.transform.Translate(new Vector3(z, 0.0f, 0.0f));
                labelTmp.GetComponent<TextMesh>().text = z.ToString();
                zLLabels.Add(labelTmp);

                labelTmp = Instantiate(LabelPrefabLarge, zRAxisLabelParent.transform, false);
                labelTmp.transform.Translate(new Vector3(-z, 0.0f, 0.0f));
                labelTmp.GetComponent<TextMesh>().text = z.ToString();
                zRLabels.Add(labelTmp);
            }

            xLabel = Instantiate(LabelPrefabLarge, xAxisLabelParent.transform, false);
            xLabel.GetComponent<TextMesh>().text = "<date>";
        }

        public void SetDate(string date)
        {
            xLabel.GetComponent<TextMesh>().text = date;
        }
    }
}

