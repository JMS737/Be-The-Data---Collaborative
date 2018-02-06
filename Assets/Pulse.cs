using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
		float emission = 0.5f + Mathf.PingPong (Time.time, 0.5f);

		Color newColour = baseColor * Mathf.LinearToGammaSpace (emission);
		material.SetColor ("_EmissionColor", newColour);
	}
}
