using System.Linq;
using UnityEditor;
using UnityEngine;

namespace HoshinoLabs.Sardinal {
    [CustomPropertyDrawer(typeof(Signal<>))]
    internal sealed class SignalDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            EditorGUI.BeginProperty(position, label, property);
            using (new EditorGUI.DisabledGroupScope(true)) {
                var topic = fieldInfo.FieldType.GetGenericArguments().First();
                EditorGUI.LabelField(position, label, new GUIContent($"<{topic.FullName}>"));
            }
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            return EditorGUIUtility.singleLineHeight;
        }
    }
}
