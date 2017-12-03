using UnityEngine;
using System.Collections;

public class MovementScript : Photon.PunBehaviour {

	private bool cameraMoving;
	public bool playerMoving;
	private IEnumerator coroutine;
	private enum Direction {Forward, Back, Left, Right};
	public float moveStrength = 0.5f;
	//private CreateSphereGrid createSphereGrid;
	private GameObject selectedDataPoint;

	// Collider used when moving only, otherwise disabled to let ray tracer out
	BoxCollider playerCollider;

	// Daydream controller variables
	private bool isScrolling;
	Vector2 prevTouch;
	float prevTouchtime;
	Vector2 currentTouch;
	Vector2 touchDelta;
	Vector2 overallVelocity;

	//--- CONSTANTS TAKEN FROM GOOGLE SDK 1.2 ---
	// Source: gvr-unity-sdk/GoogleVR/Demos/Scripts/ScrollingUIDemo/PaginatedScrolling/PagedScrollRect.cs

	/// Values used for low-pass-filter to improve the accuracy of
	/// our tracked velocity. 
	private const float kCuttoffHz = 10.0f;
	private const float kRc = (float) (1.0 / (2.0 * Mathf.PI * kCuttoffHz));
	/// Touch Delta is required to be higher than
	/// the click threshold to avoid detecting clicks as swipes.
	private const float kClickThreshold = 0.125f;
	//--- END ---

	// Initialise variables
	void Start () {
		//createSphereGrid = GameObject.FindGameObjectWithTag ("GridObject").GetComponent<CreateSphereGrid>();
		playerCollider = this.gameObject.GetComponentInParent<BoxCollider> ();
	}
	
	// Update every frame
	void Update () {

		// -- DAYDREAM INPUT -- 
        if (!photonView.isMine)
        {
            return;
        }
		// Record the user's first touch position and timestamp
		if (!isScrolling && GvrController.IsTouching) {
			isScrolling = true;
			prevTouch = GvrController.TouchPos;
			prevTouchtime = Time.time;
		}

		// Record the user's latest touch position
		if (GvrController.IsTouching) {
			currentTouch = GvrController.TouchPos;
		}

		// Move around the world using the initial and final touch positions
		if (GvrController.TouchUp) {
			// User has lifted off the touchpad
			isScrolling = false;
			touchDelta = prevTouch - currentTouch;

			// Calculate velocity of touch movement
			float elapsedTime = (Time.time - prevTouchtime);
			Vector2 velocity = touchDelta / elapsedTime;

			// From Google SDK (as above) to improve the accuracy of the recorded velocity
			float weight = elapsedTime / (kRc + elapsedTime);
			overallVelocity = Vector2.Lerp(overallVelocity, velocity, weight);

			// Get the significant direction and move in that direction
			if (Mathf.Abs (touchDelta.x) > Mathf.Abs (touchDelta.y)) {

				if (touchDelta.x > kClickThreshold) {
					//Debug.Log ("Swipe left");
					daydreamWorldPositionHandler (Direction.Left, overallVelocity);
				} else if (touchDelta.x < -kClickThreshold) {
					//Debug.Log ("Swipe right");
					daydreamWorldPositionHandler (Direction.Right, overallVelocity);
				}

			} else {
				
				if (touchDelta.y > kClickThreshold) {
					//Debug.Log ("Swipe up");
					daydreamWorldPositionHandler (Direction.Forward, overallVelocity);
				} else if (touchDelta.y < -kClickThreshold) {
					//Debug.Log ("Swipe down");
					daydreamWorldPositionHandler (Direction.Back, overallVelocity);
				}
			}

			// Reset variables to record the next touches
			prevTouch = Vector2.zero;
			currentTouch = Vector2.zero;
			touchDelta = Vector2.zero;
		}

		// -- KEYBOARD INPUT -- 

		if (Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) 
			|| Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)) {
			cameraMoving = true;
		} else {
			cameraMoving = false; 	//reset every frame if false
		}

		if (cameraMoving) {
			if (Input.GetKey (KeyCode.UpArrow)) {
				keyboardWorldPositionHandler (Direction.Forward);
			}else if (Input.GetKey (KeyCode.DownArrow)) {
				keyboardWorldPositionHandler (Direction.Back);
			}else if (Input.GetKey (KeyCode.LeftArrow)) {
				keyboardWorldPositionHandler (Direction.Left);
			}else if (Input.GetKey (KeyCode.RightArrow)) {
				keyboardWorldPositionHandler (Direction.Right);
			}
		}
	
	}

	// Move to a singular GameObject, usually representative of a data point in our graph
	public void moveToGameObject(GameObject go){

		// Move from current position to the target GameObject
		// This coroutine also disables the GameObject
		coroutine = moveToGameObjectCaller(1.5f, this.transform.parent.transform.position, go.transform.position);
		StartCoroutine (coroutine);

		// Rotate X labels if required
		//createSphereGrid.rotateXLabels(go.transform.position.z);

		
	}

	// Move to any position in the world
	public void moveToWorldPosition(Vector3 endPos){

		float time = 0.4f;
		// Reduce movement time if a small distance to endPos
		if ((endPos - this.transform.parent.transform.position).magnitude <= 1) {
			time = 0.25f;
		}

		// Move from current position to the target position, with deceleration enabled
		coroutine = moveToWorldPositionCaller(time, this.transform.parent.transform.position, endPos);
		StartCoroutine (coroutine);

		// Rotate X labels	
		//createSphereGrid.rotateXLabels(endPos.z);
	}

	// Calculate world position from keyboard input
	private void keyboardWorldPositionHandler(Direction dir){

		Vector3 startPos = this.transform.position;
		Vector3 endPos = this.transform.position; //default

		// Set end vector 
		if (dir == Direction.Forward) {
			endPos = startPos + (this.transform.forward * moveStrength);
		} else if (dir == Direction.Back) {
			endPos = startPos + (-this.transform.forward * moveStrength);
		} else if (dir == Direction.Left) {
			endPos = startPos + (-this.transform.right * moveStrength);
		} else if (dir == Direction.Right) {
			endPos = startPos + (this.transform.right * moveStrength);
		}

		moveToWorldPosition(endPos);

	}

	// Calculate world position from the user's touchpoints on the Daydream Controller
	private void daydreamWorldPositionHandler(Direction dir, Vector2 velocity){

		Vector3 startPos = this.transform.position;
		Vector3 endPos = this.transform.position; //default

		// Set end vector 
		if (dir == Direction.Forward) {
			endPos = startPos + (this.transform.forward * Mathf.Abs(velocity.y));
		} else if (dir == Direction.Back) {
			endPos = startPos + (-this.transform.forward * Mathf.Abs(velocity.y));
		} else if (dir == Direction.Left) {
			endPos = startPos + (-this.transform.right * Mathf.Abs(velocity.x));
		} else if (dir == Direction.Right) {
			endPos = startPos + (this.transform.right * Mathf.Abs(velocity.x));
		}

		moveToWorldPosition (endPos);
		
	}

	// Allows for synchronousity with asynchronous child methods
	// Disable the gameobject once reaching it to enable raycaster to get out of the data point
	private IEnumerator moveToGameObjectCaller(float movementTime, Vector3 startPosition, Vector3 endPosition){
		enableColliding();
		yield return interpolatePositions(movementTime, startPosition, endPosition);
		disableColliding();

	}

	// Allows for synchronousity with asynchronous child methods + deceleration
	// Disable the gameobject once reaching it to enable raycaster to get out of the data point
	private IEnumerator moveToWorldPositionCaller(float movementTime, Vector3 startPosition, Vector3 endPosition){

		enableColliding ();
		yield return interpolatePositions(movementTime, startPosition, endPosition, true);
		disableColliding ();

	}
		
	// Coroutine to yield new camera positions over time
	private IEnumerator interpolatePositions(float movementTime, Vector3 startPosition, Vector3 endPosition, bool decelerationEnabled = false) {
		float i = 0.0f; //starting fraction
		float rate = 1.0f / movementTime; //rate at which i progresses from 0 to 1
		while (i < 1.0f) {
			i += Time.deltaTime * rate; //incrementally add to movement fraction: time of last frame rate production * rate of i
			// New camera positions based on chosen interpolation method
			if (decelerationEnabled) {
				yield return this.transform.parent.transform.position = Sinerp (startPosition, endPosition, i);
			} else {
				yield return this.transform.parent.transform.position = Vector3.Lerp (startPosition, endPosition, i);
			}
		}
	}

	// Enables non-linear interpolation. Sinerp is a curve with a small deceleration.
	public static Vector3 Sinerp(Vector3 start, Vector3 end, float percentage){
		// Clamp to the range 0-1
		percentage = Mathf.Clamp01(percentage);
		// Follow sinerp instead of standard lerp for deceleration
		percentage = Mathf.Sin(percentage * Mathf.PI * 0.5f);
		return start + (end - start) * percentage;
	}


	// Should be called when entering the proximity of a data point
	public void enterGameObject(GameObject go){

		if (selectedDataPoint == null) {

			// If there is no data point set, set it to this one and set it to inactive
			// This allows the raycast to escape
			selectedDataPoint = go;
			selectedDataPoint.SetActive (false);

		} else {

			// If reentering the current selection, ignore it
			// This can happen when within a small proximity of a data point and leaveGameObject() has not yet been called
			if (selectedDataPoint == go) {
				return; 
			}

			// If we are moving to a new data point
			if (selectedDataPoint != null) {
				leaveGameObject (); // leave the current point
				selectedDataPoint = go; //update to the new one
				selectedDataPoint.SetActive(false);
			}
		}
	}

	// Should be called when leaving the proximity of a data point
	public void leaveGameObject(){
		selectedDataPoint.SetActive (true);
		selectedDataPoint = null;
	}

	// Should be called when the player is moving around the world space
	// This enables the colliders to detect when the player moves into a data point
	private void enableColliding(){
		playerMoving = true;
		playerCollider.enabled = true;
		if (selectedDataPoint != null) {
			selectedDataPoint.SetActive (true); //enable while moving so ontriggerexit can be called
		}

	}

	// Should be called when the player has completed their move around the world space
	private void disableColliding(){

		if (selectedDataPoint != null) {

			// Round the vector magnitudes of the player's position and the position of the selected data point
			float boundsMagnitude = selectedDataPoint.GetComponent<SphereCollider>().bounds.center.magnitude;
			float positionManitude = this.transform.position.magnitude;

			// Currently rounding to 0dp - 1-2 dp was too inaccurate on Android
			boundsMagnitude = Mathf.Round(boundsMagnitude);
			positionManitude = Mathf.Round(positionManitude);

			// Bound.contains() has a bug on android (it works in the Unity editor), so use approx vector magnitudes instead
			// See if in the tested acceptable range of mod2
			if (boundsMagnitude % positionManitude < 2 || positionManitude % boundsMagnitude < 2){

				// If here, the player is inside the data point, so enter it.
				enterGameObject(selectedDataPoint);

			}else{
				// Otherwise the player is outside, so we should leave it.
				leaveGameObject();
			}
		}

		// Finally, set conditions for when the player is stationary
		playerMoving = false;
		playerCollider.enabled = false;
	}
}
