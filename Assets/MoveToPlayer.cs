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

        private void Start()
        {
            playerMovement = PlayerManager.LocalPlayerInstance.GetComponent<PlayerMovement>();
        }

        public void OnPlayerClicked()
        {
            //PlayerMovement playerMovement = PlayerManager.LocalPlayerInstance.GetComponent<PlayerMovement>();

            playerMovement.MoveTo(transform.position, false);
        }
    }
}

