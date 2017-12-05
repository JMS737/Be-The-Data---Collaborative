using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataVis.Collaboration
{
    public class DataManager : MonoBehaviour
    {
        [Tooltip("The prefab to be used for the data points.")]
        public GameObject DataPointPrefab;

        [Tooltip("JSON file containing the participant data.")]
        public TextAsset data;
        public Axes axes;
        public int participantIndex = 0;
        public int attributeIndexForY = 9;
        public int attributeIndexForZ = 11;

        private float scaleFactorX = 1;
        private float scaleFactorY = 1;
        private float scaleFactorZ = 1;

        private ParticipantDatabase database;
        private List<GameObject> dataPoints;

        private float scale = 1;
        private bool down = true;

        // Use this for initialization
        void Start()
        {
            database = JsonUtility.FromJson<ParticipantDatabase>(data.text);
            CreateDataPoints();
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

        private void CreateDataPoints()
        {
            dataPoints = new List<GameObject>();

            int maxLength = Math.Min(
                database.participants[participantIndex].attributes[attributeIndexForY].attributeData.Count,
                database.participants[participantIndex].attributes[attributeIndexForZ].attributeData.Count);

            DateValuePair pairY, pairZ;
            float maxX = 0, maxY = 0, maxZ = 0;

            for (int i = 0; i < maxLength; i++)
            {
                maxX++;

                pairY = database.participants[participantIndex].attributes[attributeIndexForY].attributeData[i];
                pairZ = database.participants[participantIndex].attributes[attributeIndexForZ].attributeData[i];

                Vector3 position;
                if (float.TryParse(pairY.value, out position.y) && float.TryParse(pairZ.value, out position.z))
                {
                    position.x = (float)i;
                    position.y /= 60;
                    position.z /= 60;

                    maxY = Math.Max(position.y, maxY);
                    maxZ = Math.Max(position.z, maxZ);
                    

                    GameObject newPoint = Instantiate(DataPointPrefab, position, Quaternion.identity, this.transform);
                    newPoint.GetComponent<DataPoint>().id = i;
                    newPoint.GetComponent<DataPoint>().SetLabels(pairY.date, pairY.value, pairZ.value);
                    newPoint.GetComponent<DataPoint>().values = position;
                    dataPoints.Add(newPoint);
                }
            }
            Debug.Log("x=" + maxX.ToString() + " y=" + maxY.ToString() + "z=" + maxZ.ToString());
            axes.renderGrid((int)Math.Ceiling(maxX), (int)Math.Ceiling(maxY), (int)Math.Ceiling(maxZ));
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
            for (int i = 0; i < dataPoints.Count; i++)
            {
                dataPoints[i].GetComponent<DataPoint>().SetAxisScale(scaleFactorX, scaleFactorY, scaleFactorZ);
            }
        }

        //// Set the graph maximums for the current exploration data
        //private int[] GetGraphMaximums()
        //{
        //    float maxDimensions = {;
        //    for (int j = 0; j < dataPoints.Count; j++)
        //    {
        //        maxx += 1;
        //        if (!dataPoints[j].empty)
        //        {
        //            if (dataPoints[j].x > maxy)
        //            {
        //                maxy = dataPoints[j].x;
        //            }
        //            if (dataPoints[j].y > maxz)
        //            {
        //                maxz = dataPoints[j].y;
        //            }
        //        }
        //    }
        //    return 
        //}
    }

    [Serializable]
    public class ParticipantDatabase
    {
        public string dbName;
        public List<Participant> participants;
    }

    [Serializable]
    public class Participant
    {
        public string participantName;
        public List<Attribute> attributes;
    }

    [Serializable]
    public class Attribute
    {
        public string attributeName;
        public List<DateValuePair> attributeData;
    }

    [Serializable]
    public class DateValuePair
    {
        public string date;
        public string value;
    }
}

