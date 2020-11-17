using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WitchOS
{
    public class ShutdownConfirmationBox : MonoBehaviour
    {
        public Button Yes, No;
        public Minimizer Minimizer;

        void Start ()
        {
            Yes.onClick.AddListener(TimeState.Instance.EndDay);
            No.onClick.AddListener(Minimizer.Minimize);
        }
    }
}
