using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDDataLabel : MonoBehaviour {

    Text textObj;

	// Use this for initialization
	void Start () {
        textObj = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetDataLabel(string label)
    {
        textObj.text = label;
    }
}
