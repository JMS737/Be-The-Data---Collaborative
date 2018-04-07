using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataVis.Collaboration
{
    public class HighlightManager : Photon.PunBehaviour
    {
        public GameObject highlightPrefab;

        private Dictionary<Vector3, GameObject> highlightedPoints = new Dictionary<Vector3, GameObject>();


        // Instantiates a highlight object on all client machines.
        //
        // Note: Colour and a reference to the gameobject are dealt with in the
        // PointHighlight script, once the object has been created.
        public void AddHighlight(Vector3 point)
        {
            PhotonNetwork.Instantiate(highlightPrefab.name, point, Quaternion.identity, 0);
        }

        // If a highlight exists for the given point, destroy the game object on
        // all clients.
        public void RemoveHighlight(Vector3 point)
        {
            if (highlightedPoints.ContainsKey(point))
            {
                PhotonNetwork.Destroy(highlightedPoints[point]);
                highlightedPoints.Remove(point);
            }
        }

        // Provides the ability to add a reference to a highlight gameobject from
        // the PointHighlight script.
        // This allows us to destroy them at a later date.
        public void AddReference(Vector3 point, GameObject highlight)
        {
            highlightedPoints.Add(point, highlight);
        }
    }
}

