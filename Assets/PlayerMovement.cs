using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float MovementStep = 1.0f;

    public float moveTime = 1.0f;

    private Vector3 newPosition;

    private void Start()
    {
        newPosition = transform.position;
    }

    public void MoveForward()
    {
        Vector3 forward = GetComponentInChildren<Camera>().transform.forward;
        newPosition = transform.position + (forward * MovementStep);
    }

    private void Update()
    {
        Vector3 change = (newPosition - transform.position) * Time.deltaTime / moveTime;
        //Debug.Log(newPosition - transform.position);
        transform.position += change;
    }
}
