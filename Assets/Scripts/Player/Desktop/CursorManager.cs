using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

namespace WitchOS
{
    public class CursorManager : Singleton<CursorManager>
    {
        [Serializable]
        public class CursorData
        {
            public Texture2D Cursor;
            public Vector2 Hotspot;
        }

        public List<CursorData> Data;

        [SerializeField]
        CursorState _currentCursorState;
        public CursorState CursorState
        {
            get => _currentCursorState;

            set
            {
                var data = Data[(int) value];
                Cursor.SetCursor(data.Cursor, data.Hotspot, CursorMode.Auto);
                
                _currentCursorState = value;
            }
        }

        void Awake ()
        {
            SingletonOverwriteInstance(this);
        }

        void Start ()
        {
            CursorState = CursorState.Normal;
        }
    }

    public enum CursorState
    {
        Normal, VerticalResize, HorizontalResize, DiagonalFromSoutheastResize, DiagonalFromSouthwestResize, HammerPrimed, HammerSwung
    }
}
