using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchOS
{
[CreateAssetMenu(fileName = "NewPDF.asset", menuName = "PDF")]
public class TextPDF : ScriptableObject
{
    public string Title;

    [TextArea(5, 100)]
    public List<string> Pages;
}
}
