using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabelManager : MonoBehaviour {

    public string xName;
    public string yName;
    public string zName;

    private GameObject xAxisLabel;
    private GameObject yLAxisLabel;
    private GameObject yRAxisLabel;
    private GameObject zLAxisLabel;
    private GameObject zRAxisLabel;

    // Use this for initialization
    void Start () {
        xAxisLabel = transform.GetChild(0).gameObject;
        yLAxisLabel = transform.GetChild(1).gameObject;
        yRAxisLabel = transform.GetChild(2).gameObject;
        zLAxisLabel = transform.GetChild(3).gameObject;
        zRAxisLabel = transform.GetChild(4).gameObject;

        xAxisLabel.GetComponent<TextMesh>().text = xName;
        yLAxisLabel.GetComponent<TextMesh>().text = yName;
        yRAxisLabel.GetComponent<TextMesh>().text = yName;
        zLAxisLabel.GetComponent<TextMesh>().text = zName;
        zRAxisLabel.GetComponent<TextMesh>().text = zName;
    }

    public void SetPositions(float maxX, float maxY, float maxZ)
    {
        xAxisLabel.transform.position = new Vector3(maxX / 2.0f, maxY + 3.0f, maxZ);
        yLAxisLabel.transform.position = new Vector3(0.0f, maxY / 2.0f, -3.0f);
        yRAxisLabel.transform.position = new Vector3(maxX, maxY / 2.0f, -3.0f);
        zLAxisLabel.transform.position = new Vector3(0.0f, maxY + 3, maxZ / 2.0f);
        zRAxisLabel.transform.position = new Vector3(maxX, maxY + 3, maxZ / 2.0f);
    }
}
