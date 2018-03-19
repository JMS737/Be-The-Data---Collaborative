using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataVis.Collaboration
{
	public class DataPoint : MonoBehaviour
    {

        public int id { get; set; }

        public Vector3 values { get; set; }

        public string Label
        {
            get
            {
                return xLabel + "\n" + yLabel + "\n" + zLabel;
            }
        }

        private HighlightGrid highlightGrid;

        private string xLabel = "", yLabel = "", zLabel = "";
        
        private HUDDataLabel hudLabel;

        private bool hasLocalHighlight;

        // Use this for initialization
        void Start()
        {
            hasLocalHighlight = false;
            StartCoroutine("WaitAndLoad");
            highlightGrid = FindObjectOfType<HighlightGrid>();
        }


        IEnumerator WaitAndLoad()
        {
            yield return new WaitForSeconds(0.1f);
            highlightGrid = FindObjectOfType<HighlightGrid>();
        }

            // Update is called once per frame
            void Update()
        {
			if (hudLabel == null && PlayerManager.LocalPlayerInstance != null) {
				hudLabel = PlayerManager.LocalPlayerInstance.GetComponentInChildren<HUDDataLabel>();
			}
        }

        public void SetAxisScale(float x, float y, float z)
        {
            transform.position = new Vector3(values.x * x, values.y * y, values.z * z);
        }

        public void SetLabels(string x = null, string y = null, string z = null)
        {
            if (x != null)
            {
                xLabel = x.Trim();
            }

            if (y != null)
            {
                yLabel = y.Trim();
            }

            if (z != null)
            {
                zLabel = z.Trim();
            }
        }

        public void OnPointerEnter()
        {
            hudLabel.SetDataLabel(Label);

            highlightGrid.EnableGrid(transform.position.x);
        }

        public void OnPointerExit()
        {
            hudLabel.SetDataLabel("");
            highlightGrid.DisableGrid();
        }

        // Add or remove a highlight object around the current data point.
        public void OnPointerClick()
        {
            HighlightManager HLManager = PlayerManager.LocalPlayerInstance.GetComponent<HighlightManager>();

            // If a highlight object has already been spawned, remove it.
            if (hasLocalHighlight)
            {
                hasLocalHighlight = false;
                HLManager.RemoveHighlight(transform.position);
            }
            // Else add a new highlight.
            else
            {
                hasLocalHighlight = true;
                HLManager.AddHighlight(transform.position);
            }

        }
    }
}

