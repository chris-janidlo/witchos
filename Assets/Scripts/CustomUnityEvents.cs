using System;
using UnityEngine.Events;

namespace WitchOS
{
    [Serializable]
    public class WindowEvent : UnityEvent<Window> { }

    [Serializable]
    public class IntEvent : UnityEvent<int> { }
}
