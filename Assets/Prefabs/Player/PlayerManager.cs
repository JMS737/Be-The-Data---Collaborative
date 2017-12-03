using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Photon.PunBehaviour {

    public static GameObject LocalPlayerInstance;

    public GameObject Reticle;


    private void Awake()
    {
        if (photonView.isMine)
        {
            PlayerManager.LocalPlayerInstance = this.gameObject;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // Use this for initialization
    void Start () {
        if (photonView.isMine)
        {
            // Enable scripts on the camera.
            GetComponentInChildren<Camera>().enabled = true;
            GetComponentInChildren<FlareLayer>().enabled = true;
            GetComponentInChildren<AudioListener>().enabled = true;
            GetComponentInChildren<GvrPointerPhysicsRaycaster>().enabled = true;
            GetComponentInChildren<MovementScript>().enabled = true;

            // Enable scripts on the controller.

            GetComponentInChildren<GvrArmModel>().enabled = true;
            GetComponentInChildren<GvrTrackedController>().enabled = true;

            Reticle.SetActive(true);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
