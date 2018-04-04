using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DataVis.Collaboration
{
    [RequireComponent(typeof(PlayerMovement_Daydream))]
    public class MoveToPlayer : MonoBehaviour
    {

        private PlayerMovement_Daydream playerMovement;

        // Use this for initialization
        void Start()
        {
            playerMovement = GetComponent<PlayerMovement_Daydream>();
        }

        public void OnPlayerClicked(PointerEventData data)
        {
            GameObject selectedPlayer = data.selectedObject;
            Debug.Log(selectedPlayer);

            playerMovement.SetPosition(selectedPlayer.transform.position);
        }
    }
}

