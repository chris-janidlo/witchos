using UnityEngine;
using UnityEditor;

namespace WitchOS.Editor
{
    [CustomPropertyDrawer(typeof(BaseSaveableScriptableObjectReference), useForChildren: true)]
    public class SSORDrawer : PropertyDrawer
    {
        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(position, property.FindPropertyRelative("Value"), label);
        }
    }
}
