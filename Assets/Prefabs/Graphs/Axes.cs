using UnityEngine;
using System.Collections;

namespace DataVis.Collaboration
{
    public class Axes : MonoBehaviour
    {

        private int xSize = 5;
        private int ySize = 5;
        private int zSize = 5;

        private GameObject xAxis;
        private GameObject yAxis;
        private GameObject zAxis_left;
        private GameObject zAxis_right;

        // Renders the grids to the parameterised dimensions
        public void renderGrid(int x, int y, int z)
        {

            // Set local sizes
            xSize = x;
            ySize = y;
            zSize = z;

            // Set the grid's gameobjects to manipulate
            xAxis = this.gameObject.transform.GetChild(0).gameObject;
            yAxis = this.gameObject.transform.GetChild(1).gameObject;
            zAxis_left = this.gameObject.transform.GetChild(2).gameObject;
            zAxis_right = this.gameObject.transform.GetChild(3).gameObject;

            // Scale each grid to the relevant dimensions
            xAxis.transform.localScale = new Vector3((float)xSize, (float)zSize, 0.01f);
            yAxis.transform.localScale = new Vector3((float)xSize, (float)ySize, 0.01f);
            zAxis_left.transform.localScale = new Vector3((float)zSize, (float)ySize, 0.01f);
            zAxis_right.transform.localScale = new Vector3((float)zSize, (float)ySize, 0.01f);

            // Position the grids
            if ((xSize == ySize) && (ySize == zSize))
            {
                // X, Y, Z components are the same
                xAxis.transform.position = new Vector3(xSize / 2.0f, 0.0f, xSize / 2.0f);
                yAxis.transform.position = new Vector3(ySize / 2.0f, ySize / 2.0f, ySize);
                zAxis_left.transform.position = new Vector3(0f, zSize / 2.0f, zSize / 2.0f);
                zAxis_right.transform.position = new Vector3(zSize, zSize / 2.0f, zSize / 2.0f);
            }
            else
            {
                // At least one component is different, so position differently
                xAxis.transform.position = new Vector3(xSize / 2.0f, 0.0f, zSize / 2.0f);
                yAxis.transform.position = new Vector3(xSize / 2.0f, ySize / 2.0f, zSize);
                zAxis_left.transform.position = new Vector3(0f, ySize / 2.0f, zSize / 2.0f);
                zAxis_right.transform.position = new Vector3(xSize, ySize / 2.0f, zSize / 2.0f);
            }

            // Adjust textures to spread evenly over the passed dimensions
            xAxis.GetComponent<Renderer>().material.mainTextureScale = new Vector2(xSize, zSize);
            yAxis.GetComponent<Renderer>().material.mainTextureScale = new Vector2(xSize, ySize);
            zAxis_left.GetComponent<Renderer>().material.mainTextureScale = new Vector2(zSize, ySize);
            zAxis_right.GetComponent<Renderer>().material.mainTextureScale = new Vector2(zSize, ySize);
        }
    }
}


