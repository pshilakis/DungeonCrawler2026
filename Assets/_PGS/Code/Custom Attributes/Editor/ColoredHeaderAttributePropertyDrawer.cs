using UnityEngine;
using UnityEditor;

namespace PGS
{
    [CustomPropertyDrawer(typeof(ColoredHeaderAttribute))]
    public class ColoredHeaderAttributePropertyDrawer : PropertyDrawer
    {
        /// <summary>
        /// The default Light Mode text color for the editor
        /// </summary>
        private const string LIGHT_MODE_TEXT_COLOR = "000000";

        /// <summary>
        /// The default Dark Mode (ProSkin) text color for the editor
        /// </summary>
        private const string DARK_MODE_TEXT_COLOR = "ffffff";

        /// <summary>
        /// The default text size for labels in the inspector
        /// </summary>
        private const int DEFAULT_INSPECTOR_TEXT_SIZE = 12;
        private const int MAX_INSPECTOR_TEXT_SIZE = int.MaxValue;   //in case we ever want to set a max size, but for now shouldn't be a problem

		private const float DIVIDER_THICKNESS = 2f;

		private ColoredHeaderAttribute Header { get { return (ColoredHeaderAttribute)attribute; } }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            //Debug.Log(EditorGUI.indentLevel);
            GUIStyle style = new GUIStyle(Header.useBold ? EditorStyles.boldLabel : EditorStyles.whiteLabel);
            style.richText = true;
            style.fontSize = Mathf.Clamp(Header.textSize, DEFAULT_INSPECTOR_TEXT_SIZE, MAX_INSPECTOR_TEXT_SIZE);

            string color = Header.textColor; //Copy the current color string value since we can't directly set Header.textColor outside of the constructor
            if (string.IsNullOrEmpty(color)) //If there's no color value given for the header text, then use the default color based on the Light/Dark skin set for the editor
            {
                color = EditorGUIUtility.isProSkin ? DARK_MODE_TEXT_COLOR : LIGHT_MODE_TEXT_COLOR;
            }

            string text = string.Concat("<color=#", color, ">", Header.header, "</color>");
			EditorGUILayout.LabelField(text, style); //Draw the header
            DrawHorizontalDivider(position); //Draw horizontal divider
			EditorGUILayout.PropertyField(property); //Draw the property underneath the header
            EditorGUI.EndProperty();
        }

        private void DrawHorizontalDivider(Rect rect)
        {
            Vector2 pos = new Vector2(rect.position.x, rect.position.y + EditorGUIUtility.singleLineHeight / 2);
            Vector2 size = new Vector2(rect.width, DIVIDER_THICKNESS);
			Rect r = new Rect(pos, size); //The rect of the divider
			Color c = new Color(0, 0, 0, 0.25f); //The color of the divider
			EditorGUI.DrawRect(r, c);
		}
    }
}
