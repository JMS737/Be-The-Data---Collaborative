using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataVis.Collaboration
{
	public class Pulse : MonoBehaviour {

		Material material;
		public Color baseColor;

		// Use this for initialization
		void Start () {
			material = GetComponent<Renderer> ().material;
	//		baseColor = material.color;

		}
		
		// Update is called once per frame
		void Update () {
			float emission = 0.05f + Mathf.PingPong (Time.time, 0.25f);

			Color newColour = baseColor * Mathf.LinearToGammaSpace (emission);
			material.SetColor ("_EmissionColor", newColour);
		}
	}
}
