using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

namespace WitchOS
{
    public class CursorManager : Singleton<CursorManager>
    {
        public Texture2D NormalTexture, VerticalResizeTexture, HorizontalResizeTexture, DiagonalFromSoutheastResizeTexture, DiagonalFromSouthwestResizeTexture;

        [SerializeField]
        CursorState _currentCursorState;
        public CursorState CursorState
        {
            get => _currentCursorState;

            set
            {
                Vector2 hotspot = value == CursorState.Normal
                    ? Vector2.zero
                    : Vector2.one * 15;

                Texture2D texture;

                switch (value)
                {
                    case CursorState.Normal: texture = NormalTexture; break;
                    case CursorState.VerticalResize: texture = VerticalResizeTexture; break;
                    case CursorState.HorizontalResize: texture = HorizontalResizeTexture; break;
                    case CursorState.DiagonalFromSoutheastResize: texture = DiagonalFromSoutheastResizeTexture; break;
                    case CursorState.DiagonalFromSouthwestResize: texture = DiagonalFromSouthwestResizeTexture; break;

                    default: throw new ArgumentException($"unexpected CursorState value {value}");
                }

                Cursor.SetCursor(texture, hotspot, CursorMode.Auto);
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
        Normal, VerticalResize, HorizontalResize, DiagonalFromSoutheastResize, DiagonalFromSouthwestResize
    }
}
