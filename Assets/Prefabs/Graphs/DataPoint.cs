using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPoint : MonoBehaviour {

    public int id { get; set; }

    public Vector3 values { get; set; }

    public string xLabel { get; set; }
    public string yLabel { get; set; }
    public string zLabel { get; set; }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
}
