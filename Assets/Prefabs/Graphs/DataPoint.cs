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

        private string xLabel = "", yLabel = "", zLabel = "";
        
        private HUDDataLabel hudLabel;

        private bool hasLocalHighlight;

        // Use this for initialization
        void Start()
        {
            hasLocalHighlight = false;
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
        }

        public void OnPointerExit()
        {
            hudLabel.SetDataLabel("");
        }

        public void OnPointerClick()
        {
            if (hasLocalHighlight)
            {
                Debug.Log("Removing Highlight on point");
                hasLocalHighlight = false;
                PlayerManager.LocalPlayerInstance.GetComponentInChildren<HighlightManager>().RemoveHighlight(transform.position);
            }
            else
            {
                Debug.Log("Highlighting point");

                hasLocalHighlight = true;
                PlayerManager.LocalPlayerInstance.GetComponentInChildren<HighlightManager>().AddHighlight(transform.position);
            }

        }
    }
}

