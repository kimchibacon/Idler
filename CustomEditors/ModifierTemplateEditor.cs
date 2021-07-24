//=============================================================================
// ModifierTemplateEditor.cs
//
// Custom Unity editor for types that inherit from ModifierTemplateBase. 
//=============================================================================

using System;
using Idler.Modifiers.Templates;
using UnityEngine;
using UnityEditor;

namespace Idler.CustomEditors
{
    [CustomEditor(typeof(ModifierTemplateBase), true)]
    public class ModifierTemplateEditor : Editor
    {
        private ModifierTemplateBase _targetItem;
        private SerializedProperty _resourceIdProperty;
        private string[] _resourceIds;
        private int _idIndex;
        public void OnEnable()
        {
            _targetItem = (ModifierTemplateBase) target;
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

            var myTexture = AssetPreview.GetAssetPreview(_targetItem.icon);
            GUILayout.Label(myTexture);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
