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
        //public TextAsset data;
        //public Axes axes;
        //public int participantIndex = 0;
        //public int attributeIndexForY = 9;
        //public int attributeIndexForZ = 11;

        //private ParticipantDatabase database;
        private List<GameObject> dataPoints;

        private float scale = 1;
        private bool down = true;

        private Vector3 maxValues = new Vector3();
        public Vector3 MaxValues { get; set; }

        // Use this for initialization
        void Start()
        {
            
        }

        private void Update()
        {
            
        }

        public void LoadData(ParticipantDatabase database, int participantIndex, int attributeIndexForY, int attributeIndexForZ)
        {
            //database = JsonUtility.FromJson<ParticipantDatabase>(data.text);
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
                    newPoint.GetComponent<DataPoint>().SetLabels(pairY.date, pairY.value + " Hours", pairZ.value + " Hours");
                    newPoint.GetComponent<DataPoint>().values = position;
                    dataPoints.Add(newPoint);
                }
            }
            Debug.Log("x=" + maxX.ToString() + " y=" + maxY.ToString() + "z=" + maxZ.ToString());
            maxValues.x = maxX;
            maxValues.y = maxY;
            maxValues.z = maxZ;
            //axes.renderGrid((int)Math.Ceiling(maxX), (int)Math.Ceiling(maxY), (int)Math.Ceiling(maxZ));
        }
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

