using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataVis.Collaboration
{
    public class GraphManager : MonoBehaviour
    {
        private Vector3 spawnPoint;

        private Axes axes;

    // Use this for initialization
        void Start()
        {
            UpdateSpawnPoint(5f, 5f, -2f);
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
    }
}

