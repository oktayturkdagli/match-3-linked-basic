using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Match3Linked
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CustomButtonAttribute : PropertyAttribute
    {
        public string ButtonLabel { get; }

        public CustomButtonAttribute(string buttonLabel = null)
        {
            ButtonLabel = buttonLabel;
        }
    }

    [CustomEditor(typeof(MonoBehaviour), true)]
    public class CustomButtonDrawer : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var targetType = target.GetType();
            var methods = targetType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);

            foreach (var method in methods)
            {
                var attribute = method.GetCustomAttribute<CustomButtonAttribute>();
                if (attribute != null)
                {
                    string buttonLabel = string.IsNullOrEmpty(attribute.ButtonLabel) ? method.Name : attribute.ButtonLabel;

                    if (GUILayout.Button(buttonLabel))
                    {
                        method.Invoke(target, null);
                    }
                }
            }
        }
    }
}