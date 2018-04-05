using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DataVis.Collaboration
{
    [RequireComponent(typeof(PlayerMovement_Daydream))]
    public class MoveToPlayer : MonoBehaviour
    {
        public void OnPlayerClicked()
        {
            PlayerMovement_Daydream playerMovement = PlayerManager.LocalPlayerInstance.GetComponent<PlayerMovement_Daydream>();

            playerMovement.SetPosition(transform.position);
        }
    }
}

