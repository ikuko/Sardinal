using HoshinoLabs.Sardinal.Udon;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace HoshinoLabs.Sardinal {
    [CustomPropertyDrawer(typeof(SignalId))]
    internal sealed class SignalIdDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            EditorGUI.BeginProperty(position, label, property);

            var bindToProperty = property.FindPropertyRelative("_serialized");
            EditorGUI.PropertyField(position, bindToProperty, label);
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            return EditorGUIUtility.singleLineHeight;
        }
    }
}
