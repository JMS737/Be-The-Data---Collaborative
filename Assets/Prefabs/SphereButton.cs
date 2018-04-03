using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereButton : MonoBehaviour {

    public float enlargeFactor = 1.25f;

    Vector3 originalScale;

	// Use this for initialization
	void Start () {
        originalScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Highlight(bool isHighlighted)
    {
        if (isHighlighted)
        {
            transform.localScale = originalScale * enlargeFactor;
        }
        else
        {
            transform.localScale = originalScale;
        }
    }

    public void OnPointerEnter()
    {
        Highlight(true);
    }

    public void OnPointerExit()
    {
        Highlight(false);
    }
}
