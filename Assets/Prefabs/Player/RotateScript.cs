using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScript : MonoBehaviour {

    public float RPM = 100;
	
	// Update is called once per frame
	void Update () {
        float rotateAmount = RPM * 360 / 60 * Time.deltaTime;
        transform.Rotate(0f, rotateAmount, 0f);
	}
}
