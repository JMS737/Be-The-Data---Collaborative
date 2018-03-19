using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataVis.Collaboration
{
    public class LabelValueManager : MonoBehaviour
    {
        public GameObject LabelPrefabSmall;
        public GameObject LabelPrefabLarge;

        private GameObject xAxisLabelParent;
        private GameObject yLAxisLabelParent;
        private GameObject yRAxisLabelParent;
        private GameObject zLAxisLabelParent;
        private GameObject zRAxisLabelParent;

        private List<GameObject> xLabels = new List<GameObject>();
        private List<GameObject> yLLabels = new List<GameObject>();
        private List<GameObject> yRLabels = new List<GameObject>();
        private List<GameObject> zLLabels = new List<GameObject>();
        private List<GameObject> zRLabels = new List<GameObject>();

        // Use this for initialization
        void Start()
        {
            xAxisLabelParent = transform.GetChild(0).gameObject;
            yLAxisLabelParent = transform.GetChild(1).gameObject;
            yRAxisLabelParent = transform.GetChild(2).gameObject;
            zLAxisLabelParent = transform.GetChild(3).gameObject;
            zRAxisLabelParent = transform.GetChild(4).gameObject;
        }

        public void SetupLabels(DateTime minDate, DateTime maxDate, float maxY, float maxZ)
        {
            float maxX = (float)(maxDate - minDate).TotalDays;

            xAxisLabelParent.transform.position = new Vector3(0, maxY + 0.5f, maxZ);
            yLAxisLabelParent.transform.position = new Vector3(0.0f, 0.0f, -0.5f);
            yRAxisLabelParent.transform.position = new Vector3(maxX, 0.0f, -0.5f);
            zLAxisLabelParent.transform.position = new Vector3(0.0f, maxY + 0.5f, 0.0f);
            zRAxisLabelParent.transform.position = new Vector3(maxX, maxY + 0.5f, 0.0f);

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

            int count = 0;
            int interval = 2;
            foreach (DateTime dayInterval in EachDay(minDate, maxDate, interval))
            {
                labelTmp = Instantiate(LabelPrefabSmall, xAxisLabelParent.transform, false);
                labelTmp.transform.Translate(new Vector3(count, 0.0f, 0.0f));
                labelTmp.GetComponent<TextMesh>().text = dayInterval.ToString("dd/MM/yyyy");
                xLabels.Add(labelTmp);

                count += interval;
            }
        }

        public IEnumerable<DateTime> EachDay(DateTime from, DateTime to, int interval)
        {
            for (var day = from.Date; day.Date <= to.Date; day = day.AddDays(interval))
            {
                yield return day;
            }
        }
    }

}
