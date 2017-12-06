using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataVis.Collaboration
{
    public class GraphManager : MonoBehaviour
    {
        private Vector3 spawnPoint;

        private Axes axes;
        private DataManager dataManager;

        private float scaleFactorX = 1;
        private float scaleFactorY = 1;
        private float scaleFactorZ = 1;

        // Use this for initialization
        void Start()
        {
            UpdateSpawnPoint(5f, 5f, -2f);
            axes = GetComponentInChildren<Axes>();
            dataManager = GetComponentInChildren<DataManager>();
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
            //for (int i = 0; i < dataPoints.Count; i++)
            //{
            //    dataPoints[i].GetComponent<DataPoint>().SetAxisScale(scaleFactorX, scaleFactorY, scaleFactorZ);
            //}
        }
    }
}

