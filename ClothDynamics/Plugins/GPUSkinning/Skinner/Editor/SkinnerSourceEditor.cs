using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Rendering;

namespace ClothDynamics
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(SkinnerSource))]
    public class SkinnerSourceEditor : Editor
    {
        SerializedProperty _model;
        static bool _advancedMode = false;

        void OnEnable()
        {
            _model = serializedObject.FindProperty("_model");
        }

        public override void OnInspectorGUI()
        {
            if (GraphicsSettings.currentRenderPipeline)
            {
                if (GraphicsSettings.currentRenderPipeline.GetType().ToString().Contains("HighDefinition"))
                {
                    GUILayout.Label("HDRP active: This script only works with the Built-in RP!");
                }
                else // assuming here we only have HDRP or URP options here
                {
                    GUILayout.Label("URP active: This script only works with the Built-in RP!");
                }
            }
            else
            {
                if (_advancedMode)
                {
                    DrawDefaultInspector();
                }
                else
                {
                    serializedObject.Update();
                    EditorGUILayout.PropertyField(_model);
                    serializedObject.ApplyModifiedProperties();
                }

                if (GUILayout.Button(_advancedMode ? "Normal Mode" : "Advanced Mode"))
                {
                    _advancedMode = _advancedMode == false;
                }
            }
        }
    }
}
