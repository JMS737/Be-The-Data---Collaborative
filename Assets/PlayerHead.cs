using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataVis.Collaboration
{
	public class PlayerHead : MonoBehaviour {

		public static List<Color> playerColours = new List<Color>
		{
			Color.blue,
			Color.yellow,
			Color.magenta,
			Color.white,
		};

		private static int colourIndex = -1;

		public static int ColourIndex {
			get {
				colourIndex++;
				if (colourIndex >= playerColours.Count) {
					colourIndex = 0;
				}
				return colourIndex;
			}
		}

		// Use this for initialization
		void Start () {

			Material material = GetComponent<Renderer> ().material;
			material.color = playerColours [ColourIndex];
		}
	}
}
