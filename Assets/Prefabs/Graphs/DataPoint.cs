﻿using System.Collections;
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

        // Use this for initialization
        void Start()
        {
			
        }

        // Update is called once per frame
        void Update()
        {
			if (hudLabel == null && PlayerManager.LocalPlayerInstance != null) {
				Debug.Log ("HUD Set...");
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
    }
}

