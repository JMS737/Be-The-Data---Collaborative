using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataVis.Collaboration
{
    public class PointHighlight : Photon.PunBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public override void OnPhotonInstantiate(PhotonMessageInfo info)
        {
            if (photonView.isMine)
            {
                Debug.Log("Setting reference and colour");
                PlayerManager.LocalPlayerInstance.GetComponent<HighlightManager>().AddReference(transform.position, this.gameObject);
                photonView.RPC("SetColour", PhotonTargets.AllBufferedViaServer, PlayerManager.LocalPlayerInstance.GetComponent<PlayerManager>().myColourIndex);
            }
            else{
                Debug.Log("Skipping...");
            }

        }

        [PunRPC]
        public void SetColour(int colourIx)
        {
            GetComponent<Pulse>().baseColor = PlayerManager.LocalPlayerInstance.GetComponent<PlayerManager>().playerColours[colourIx];

        }
    }
}

