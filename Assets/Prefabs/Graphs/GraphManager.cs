using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataVis.Collaboration
{
    /**
     * Note: Datasets currently assume that dates align within the data 
     */
    public class GraphManager : MonoBehaviour
    {
        public GameObject dataSetPrefab;
        public List<GameObject> dataPointPrefabs;

        private List<DataSet> dataSets = new List<DataSet>();

        private Vector3 spawnPoint;

        private Axes axes;

        private float scaleFactorX = 1;
        private float scaleFactorY = 1;
        private float scaleFactorZ = 1;

        //private float scale = 1;
        //private bool down = true;

        // Use this for initialization
        void Start()
        {
            StartCoroutine("WaitAndLoad");
        }

        IEnumerator WaitAndLoad()
        {
            yield return new WaitForSeconds(0.01f);
            AddDataSet("data", 0, 9, 11, dataPointPrefabs[0]);
            AddDataSet("data", 2, 9, 11, dataPointPrefabs[1]);
            AddDataSet("data", 4, 9, 11, dataPointPrefabs[2]);
            axes = GetComponentInChildren<Axes>();
            SetMaxGridSize();
        }


        public void AddDataSet(string dataAssetName, int participant, int indexY, int indexZ, GameObject dataPointPrefab)
        {
            GameObject newDataSet = Instantiate(dataSetPrefab, this.transform);
            newDataSet.GetComponent<DataSet>().LoadData(dataAssetName, participant, indexY, indexZ, dataPointPrefab);
            dataSets.Add(newDataSet.GetComponent<DataSet>());
        }

        private void SetMaxGridSize()
        {
            float maxX = 0, maxY = 0, maxZ = 0;
            Vector3 dataSetMaxValues;
            for (int i = 0; i < dataSets.Count; i++)
            {
                dataSetMaxValues = dataSets[i].MaxValues;
                maxX = Math.Max(maxX, dataSetMaxValues.x);
                maxY = Math.Max(maxY, dataSetMaxValues.y);
                maxZ = Math.Max(maxZ, dataSetMaxValues.z);
            }
            axes.renderGrid((float)Math.Ceiling(maxX), (float)Math.Ceiling(maxY), (float)Math.Ceiling(maxZ));
            UpdateSpawnPoint(maxX / 2.0f, maxY / 2.0f, -3f);
        }

        private void Update()
        {
            //if (down)
            //{
            //    scale -= Time.deltaTime / 5.0f;

            //    if (scale < 0.1)
            //    {
            //        down = false;
            //    }
            //}
            //else
            //{
            //    scale += Time.deltaTime / 5.0f;

            //    if (scale > 2)
            //    {
            //        down = true;
            //    }
            //}
            //SetScale(x: scale);
        }

        void UpdateSpawnPoint(float x, float y, float z)
        {
            spawnPoint.x = x;
            spawnPoint.y = y;
            spawnPoint.z = z;
        }

        public Vector3 GetSpawnPoint()
        {
            return spawnPoint;
        }

        public void SetScale(float? x = null, float? y = null, float? z = null)
        {
            if (x.HasValue)
            {
                scaleFactorX = x.Value;
            }
            if (y.HasValue)
            {
                scaleFactorY = y.Value;
            }
            if (z.HasValue)
            {
                scaleFactorZ = z.Value;
            }
            UpdateScale();
        }

        private void UpdateScale()
        {
            
        }
    }
}

