using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

namespace BurnGames.DependencyInjection.Editor
{

    [CustomPropertyDrawer(typeof(Injectable), true)]
    public class InjectableDrawer : PropertyDrawer
    {

        bool error = false;

        float editorTotalHeight = 19;

        float labelOffsetY = 20;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {

            var propertyHeight = base.GetPropertyHeight(property, label);

            if (!error)
            {
                return propertyHeight + editorTotalHeight;
            }
            else
            {
                return propertyHeight;
            }

        }

        private void ShowError(string message, Rect position)
        {

            error = true;

            var labelStyle = new GUIStyle(EditorStyles.boldLabel);

            var infoPosition = position;

            infoPosition.y += 2;

            labelStyle.normal.textColor = Color.red;

            EditorGUI.LabelField(infoPosition, new GUIContent(message), labelStyle);

        }

        private void ShowInfo(string message, Rect position, bool active = false)
        {

            var labelStyle = new GUIStyle(EditorStyles.boldLabel);

            var infoPosition = position;

            infoPosition.y += labelOffsetY;

            if (active)
            {
                labelStyle.normal.textColor = new Color32(27, 137, 196, 255);
            }
            
            EditorGUI.LabelField(infoPosition, new GUIContent(message), labelStyle);

        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {

            Injectable injectable = attribute as Injectable;

            Object injectedReferenceValue = property.objectReferenceValue;

            if (property.propertyType == SerializedPropertyType.ObjectReference)
            {

                EditorGUI.BeginProperty(position, label, property);

                var fieldPosition = position;

                fieldPosition.height = 20;

                if (injectable.DisplayName != null)
                {
                    label.text = injectable.DisplayName;
                }

                injectedReferenceValue = EditorGUI.ObjectField(fieldPosition, label, property.objectReferenceValue, typeof(Object), true);

                if (injectedReferenceValue != null && injectedReferenceValue.GetType() == typeof(GameObject))
                {

                    GameObject injectedGameObject = (GameObject)injectedReferenceValue;

                    injectedReferenceValue = injectedGameObject.GetComponent(injectable.InjectedType);

                }

                else if (!injectable.IsValid(injectedReferenceValue))
                {
                    injectedReferenceValue = null;
                }

                error = false;

            }
            else
            {

                injectedReferenceValue = null;

                // Shows an error message if isn't an Object Reference
                ShowError("A reference type is needed, " + property.type + " isn't.", position);

            }

            if (!error)
            {

                if (injectedReferenceValue == null)
                {
                    ShowInfo("Must implement " + injectable.InjectedType.Name, position);
                }
                else
                {
                    ShowInfo("Injected [" + injectedReferenceValue.ToString() + "]", position, active: true);
                }

            }

            property.objectReferenceValue = injectedReferenceValue;

            EditorGUI.EndProperty();

        }

    }

}
