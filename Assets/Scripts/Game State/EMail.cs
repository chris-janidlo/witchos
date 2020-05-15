using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEMail.asset", menuName = "EMail")]
public class EMail : ScriptableObject
{
    public string FromAddress, Subject;
    [TextArea(5, 100)]
    public string Body;

    public bool Read { get; set; }
}
