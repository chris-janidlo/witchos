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
        
        public TimeState TimeState;

        void Start ()
        {
            Yes.onClick.AddListener(TimeState.EndDay);
            No.onClick.AddListener(Minimizer.Minimize);
        }
    }
}
