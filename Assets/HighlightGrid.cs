using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightGrid : MonoBehaviour {

    private MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        DisableGrid();
    }

    public void EnableGrid(float x)
    {
        Vector3 newPosition = transform.position;
        newPosition.x = x;

        transform.position = newPosition;
        meshRenderer.enabled = true;
        //gameObject.SetActive(true);
    }

    public void DisableGrid()
    {
        meshRenderer.enabled = false;
        //gameObject.SetActive(false);
    }
    
}
