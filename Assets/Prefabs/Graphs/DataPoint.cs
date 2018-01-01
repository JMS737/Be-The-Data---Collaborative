using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPoint : MonoBehaviour {

    public int id { get; set; }

    public Vector3 values { get; set; }

    private string xLabel = "", yLabel = "", zLabel = "";

    private GameObject labelObj;

	// Use this for initialization
	void Start ()
    {
        labelObj = transform.GetChild(0).gameObject;
	}
	
	// Update is called once per frame
	void Update ()
    {
        
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

        StartCoroutine("WaitAndAssign");
    }

    // Gives the label object a chance to load and then assigns values.
    // This prevents a null reference exception when the scene is first loaded.
    IEnumerator WaitAndAssign()
    {
        yield return new WaitForSeconds(0.01f);

        labelObj.GetComponent<TextMesh>().text = xLabel + "\n" + yLabel + "\n" + zLabel + "\n";
    }

    public void OnPointerEnter()
    {
        Debug.Log("Setting active");
        labelObj.SetActive(true);
    }

    public void OnPointerExit()
    {
        labelObj.SetActive(false);
    }
}
