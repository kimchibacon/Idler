//=============================================================================
// AttributeTemplateEditor.cs
//
// Custom Unity editor for AttributeTemplate types. 
//=============================================================================

using System;
using Idler.Attributes.Templates;
using UnityEditor;

namespace Idler.CustomEditors
{
    [CustomEditor(typeof(AttributeTemplate), true)]
    public class AttributeTemplateEditor : Editor
    {
        private SerializedProperty _resourceIdProperty;
        private string[] _resourceIds;
        private int _idIndex;
        public void OnEnable()
        {
            _resourceIds = Resources.ResourceIds.GetAllResourceIds();
            _idIndex = 0;
            
            _resourceIdProperty = serializedObject.FindProperty("resourceId");
            _idIndex = Array.IndexOf(_resourceIds, _resourceIdProperty.stringValue);
        }
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();

            _idIndex = EditorGUILayout.Popup("Resource Id", _idIndex, _resourceIds);
            if (_idIndex < 0) _idIndex = 0;
            _resourceIdProperty.stringValue = _resourceIds[_idIndex];

            serializedObject.ApplyModifiedProperties();
        }
    }
}