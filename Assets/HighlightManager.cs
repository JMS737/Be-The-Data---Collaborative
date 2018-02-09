using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataVis.Collaboration
{
    public class HighlightManager : Photon.PunBehaviour
    {
        public GameObject highlightPrefab;

        private Dictionary<Vector3, GameObject> highlightedPoints;

        // Use this for initialization
        void Start()
        {
            highlightedPoints = new Dictionary<Vector3, GameObject>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void AddHighlight(Vector3 point)
        {
            PhotonNetwork.Instantiate(highlightPrefab.name, point, Quaternion.identity, 0);
            Debug.Log("Instantiated highlight");


            //photonView.RPC("PUNAddHighlight", PhotonTargets.AllBufferedViaServer, point);

        }

        public void RemoveHighlight(Vector3 point)
        {
            //photonView.RPC("PUNRemoveHighlight", PhotonTargets.AllBufferedViaServer, point);
            if (highlightedPoints.ContainsKey(point))
            {
                PhotonNetwork.Destroy(highlightedPoints[point]);
                highlightedPoints.Remove(point);
            }
        }

        public void AddReference(Vector3 point, GameObject highlight)
        {
            highlightedPoints.Add(point, highlight);
        }

        //[PunRPC]
        //public void PUNAddHighlight(Vector3 point)
        //{
        //    GameObject newHighlight = Instantiate(highlightPrefab, point, Quaternion.identity);
        //    newHighlight.GetComponent<Pulse>().baseColor = GetComponent<PlayerManager>().PlayerColour;

        //    Debug.Log("Highlight Object: " + newHighlight);

        //    highlightedPoints.Add(point, newHighlight);
        //}

        //[PunRPC]
        //public void PUNRemoveHighlight(Vector3 point)
        //{
        //    if (highlightedPoints.ContainsKey(point))
        //    {
        //        Destroy(highlightedPoints[point]);
        //        highlightedPoints.Remove(point);
        //    }
        //}
    }
}

