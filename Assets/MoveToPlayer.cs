using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DataVis.Collaboration
{
    [RequireComponent(typeof(PlayerMovement_Cardboard))]
    public class MoveToPlayer : MonoBehaviour
    {
        public void OnPlayerClicked()
        {
            PlayerMovement_Cardboard playerMovement = PlayerManager.LocalPlayerInstance.GetComponent<PlayerMovement_Cardboard>();

            playerMovement.SetPosition(transform.position);
        }
    }
}

