using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchOS
{
    public static class MagicColorRamp
    {
        // this class just works in raw colors; the color palette shader will transform them to the actual colors players will see. check the shader code for an explanation on why these specific colors were chosen.
        public static readonly Color StartColor = new Color(0, 1, 0);
        public static readonly Color EndColor = new Color(1, 1, 0);

        public static Color GetValue (float i)
        {
            return Color.Lerp(StartColor, EndColor, i);
        }
    }
}
