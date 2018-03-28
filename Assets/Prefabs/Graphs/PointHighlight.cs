using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataVis.Collaboration
{
    public class PointHighlight : Photon.PunBehaviour
    {
        // When a highlight object has been instantiated, if the local player spawned it
        // store a reference of it in the highlight manager (so it can be destroyed later on),
        // and set the colour to that of the local player.
        public override void OnPhotonInstantiate(PhotonMessageInfo info)
        {
            if (photonView.isMine)
            {
                PlayerManager.LocalPlayerInstance.GetComponent<HighlightManager>().AddReference(transform.position, this.gameObject);

                // Set the colour via a RPC so it is synchronised between clients.
                photonView.RPC("SetColour", PhotonTargets.AllBufferedViaServer, PlayerManager.LocalPlayerInstance.GetComponent<PlayerManager>().playerColourIndex);
            }

        }

        // Sets the colour of the highlight object (via the pulse script) to that
        // of the colour associated with the colour index.
        // Note: This uses an index rather than the Color type because Color is not serializable.
        [PunRPC]
        public void SetColour(int colourIx)
        {
            GetComponent<Pulse>().baseColor = PlayerManager.LocalPlayerInstance.GetComponent<PlayerManager>().playerColours[colourIx];

        }
    }
}