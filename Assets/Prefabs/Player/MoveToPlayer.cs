using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DataVis.Collaboration
{
    [RequireComponent(typeof(PlayerMovement))]
    public class MoveToPlayer : MonoBehaviour
    {
        private PlayerMovement playerMovement;

        private void Update()
        {
            if (playerMovement == null)
            {
                playerMovement = PlayerManager.LocalPlayerInstance.GetComponent<PlayerMovement>();
            }
        }

        public void OnPlayerClicked()
        {
            playerMovement.MoveTo(transform.position, false);
        }
    }
}

