using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataVis.Collaboration
{
    // A global space to store values across scenes.
    public static class GameState
    {
        // Defines which participant set will be loaded in the 'main' scene.
        public static int ParticipantSet = 0;
    }
}

