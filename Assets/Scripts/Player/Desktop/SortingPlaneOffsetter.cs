using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchOS
{
    public class SortingPlaneOffsetter : MonoBehaviour
    {
        public int Offset;
        public Renderer Renderer;

        public void UpdateSortingOrder (int sortingPlaneIndex)
        {
            Renderer.sortingOrder = sortingPlaneIndex + Offset;
        }
    }
}
