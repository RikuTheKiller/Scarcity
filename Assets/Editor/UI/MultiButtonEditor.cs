using UnityEditor.UI;
using UnityEditor;
using UnityEngine;

namespace Scarcity
{
    [CustomEditor(typeof(MultiButton), true)]
    [CanEditMultipleObjects]
    public class MultiButtonEditor : ButtonEditor
    {
        private SerializedProperty m_TargetGraphicsProperty;

        protected override void OnEnable()
        {
            base.OnEnable();
            m_TargetGraphicsProperty = serializedObject.FindProperty("m_TargetGraphics");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(m_TargetGraphicsProperty);
            serializedObject.ApplyModifiedProperties();
            base.OnInspectorGUI();
        }
    }
}