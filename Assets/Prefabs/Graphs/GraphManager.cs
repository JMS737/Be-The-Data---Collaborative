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
        [Tooltip("Reference to the DataSet prefab. This is used to instantiate datasets at runtime.")]
        public GameObject dataSetPrefab;

        [Tooltip("Prefabs to be used for datasets.\nNote: Prefabs will be reused if the number of datasets exceeds number of prefabs specified.")]
        public List<GameObject> dataPointPrefabs;

        public static float maxX = 0;
        public static float maxY = 0;
        public static float maxZ = 0;

        private GameObject highlightGridPrefab;
        private HighlightGrid highlightGrid;

        private List<DataSet> dataSets = new List<DataSet>();

        private Vector3 spawnPoint;

        private Axes axes;
        private LabelManager labelManager;
        private LabelValueManager labelValueManager;

		private DateTime minDate, maxDate;

//        private float scaleFactorX = 1;
//        private float scaleFactorY = 1;
//        private float scaleFactorZ = 1;
        
        // Use this for initialization
        void Start()
        {
            StartCoroutine("WaitAndLoad");
            highlightGridPrefab = Resources.Load<GameObject>("HighlightGrid");
            Debug.Log(highlightGridPrefab);
            highlightGrid = Instantiate(highlightGridPrefab, transform).GetComponent<HighlightGrid>();
        }

        IEnumerator WaitAndLoad()
        {
            yield return new WaitForSeconds(0.01f);

            axes = GetComponentInChildren<Axes>();
            labelManager = GetComponentInChildren<LabelManager>();
            labelValueManager = GetComponentInChildren<LabelValueManager>();

            //            AddDataSet("data", 0, 9, 11);
            //         AddDataSet("data", 7, 9, 11);
            //         AddDataSet("data", 10, 9, 11);
            //AddDataSet("data", 14, 9, 11);
            //         AddDataSet("data", 16, 9, 11);

            int participantStart = GameState.ParticipantSet * 4;

            AddDataSet("data", participantStart + 0, 0, 1);
            AddDataSet("data", participantStart + 1, 0, 1);
            AddDataSet("data", participantStart + 2, 0, 1);
            AddDataSet("data", participantStart + 3, 0, 1);


            SetMaxGridSize();
			AlignDataSets ();
        }


        public void AddDataSet(string dataAssetName, int participant, int indexY, int indexZ)
        {
            GameObject nextDataPointPrefab = GetNextDataPointPrefab();

            GameObject newDataSetObj = Instantiate(dataSetPrefab, this.transform);
            DataSet newDataSet = newDataSetObj.GetComponent<DataSet>();
            newDataSet.LoadData(dataAssetName, participant, indexY, indexZ, nextDataPointPrefab);

            dataSets.Add(newDataSet);
        }

        private GameObject GetNextDataPointPrefab()
        {
            return dataPointPrefabs[dataSets.Count % dataPointPrefabs.Count];
        }

		// Uses the min and max dates to align datasets along the graphs x axis.
		private void AlignDataSets()
		{
			for (int i = 0; i < dataSets.Count; i++)
			{
				dataSets[i].transform.position = new Vector3((float)(dataSets[i].StartDate - minDate).TotalDays, 0.0f, 0.0f);
			}
		}

        private void SetMaxGridSize()
        {
			if (!(dataSets.Count > 0)) 
			{
				return;
			}

			DateTime minDate = dataSets[0].StartDate, maxDate = dataSets[0].EndDate;
            Vector3 dataSetMaxValues;

            for (int i = 0; i < dataSets.Count; i++)
            {
                dataSetMaxValues = dataSets[i].MaxValues;

				minDate = DateTimeHelper.Min (minDate, dataSets[i].StartDate);
				maxDate = DateTimeHelper.Max (maxDate, dataSets[i].EndDate);

				maxX = (float)(maxDate - minDate).TotalDays;
                maxY = (float)Math.Ceiling(Math.Max(maxY, dataSetMaxValues.y));
                maxZ = (float)Math.Ceiling(Math.Max(maxZ, dataSetMaxValues.z));
            }
            axes.renderGrid(maxX, maxY, maxZ);

            this.minDate = minDate;
			this.maxDate = maxDate;

            labelManager.SetPositions(maxX, maxY, maxZ);
            labelValueManager.SetupLabels(minDate, maxDate, maxY, maxZ);
            highlightGrid.SetupGrid(maxY, maxZ);

            UpdateSpawnPoint(maxX / 2.0f, maxY / 2.0f, -3f);
        }

        private void Update()
        {

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

//        public void SetScale(float? x = null, float? y = null, float? z = null)
//        {
//            if (x.HasValue)
//            {
//                scaleFactorX = x.Value;
//            }
//            if (y.HasValue)
//            {
//                scaleFactorY = y.Value;
//            }
//            if (z.HasValue)
//            {
//                scaleFactorZ = z.Value;
//            }
//            UpdateScale();
//        }

//        private void UpdateScale()
//        {
//            axes.ScaleAxes(scaleFactorX, scaleFactorY, scaleFactorZ);
//            for (int i = 0; i < dataSets.Count; i++)
//            {
//                dataSets[i].ScaleData(scaleFactorX, scaleFactorY, scaleFactorZ);
//            }
//            //transform.localScale = new Vector3(scaleFactorX, scaleFactorY, scaleFactorZ);
//        }
    }

	public static class DateTimeHelper
	{
		public static DateTime Min(DateTime a, DateTime b)
		{
			return a < b ? a : b;
		}

		public static DateTime Max(DateTime a, DateTime b)
		{
			return a > b ? a : b;
		}
	}

}

