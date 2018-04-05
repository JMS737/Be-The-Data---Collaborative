using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_Daydream : MonoBehaviour {

    [Range(1, 10)]
    public float moveSpeed = 3.0f;

    public float swipeStrength = 0.2f;

    private Vector3 newPosition;
    private float moveNullzone = 0.1f;

    // Input variables
    private Vector2 firstTouch, secondTouch, deltaTouch;
    private float firstTime, secondTime, deltaTime;

    private Transform cameraTransform;

    private bool inTransit = false;

    // Use this for initialization
    void Start () {
        newPosition = transform.position;
        cameraTransform = GetComponentInChildren<Camera>().transform;
    }
	
	// Update is called once per frame
	void Update () {
        HandleInput();
        HandleMovementForFrame();
    }

    private void HandleMovementForFrame()
    {
        Vector3 distanceTo = newPosition - transform.position;

        Vector3 change = distanceTo * Time.deltaTime * moveSpeed / distanceTo.magnitude;

        if (newPosition != transform.position)
        {
            transform.position += change;
            if (distanceTo.magnitude < moveNullzone)
            {
                transform.position = newPosition;
                inTransit = false;
            }
        }
    }

    private void HandleInput()
    {
        if (GvrControllerInput.TouchDown)
        {
            firstTouch = GvrControllerInput.TouchPos;
            firstTime = Time.time;
        }

        if (GvrControllerInput.TouchUp && !inTransit)
        {
            secondTouch = GvrControllerInput.TouchPos;
            secondTime = Time.time;

            deltaTouch = secondTouch - firstTouch;
            deltaTime = secondTime - firstTime;

            CalculateNewPosition(deltaTouch, deltaTime);
        }
    }

    private void CalculateNewPosition(Vector2 deltaTouch, float deltaTime)
    {
        Vector3 deltaMove;
        if (Mathf.Abs(deltaTouch.x) > Mathf.Abs(deltaTouch.y))
        {
            deltaMove = new Vector3(deltaTouch.x, 0.0f, 0.0f);
        }
        else
        {
            deltaMove = new Vector3(0.0f, 0.0f, deltaTouch.y);
        }
        deltaMove = deltaMove * swipeStrength / deltaTime;

        Vector3 forwardTranslate = cameraTransform.forward * -deltaMove.z;
        Vector3 rightTranslate = cameraTransform.right * deltaMove.x;

        deltaMove = forwardTranslate + rightTranslate;

        newPosition = transform.position + deltaMove;
    }

    public void SetPosition(Vector3 position)
    {
        inTransit = true;
        newPosition = position;
    }
}
