using UnityEditor;
using UnityEngine;

#if UNITY_STANALONE
public class SeparatorAttribute : PropertyAttribute
{
    // Empty class, used only as a marker attribute
}

[CustomPropertyDrawer(typeof(SeparatorAttribute))]

public class SeparatorDrawer : DecoratorDrawer
{

    public override void OnGUI(Rect position)
    {
        GUIStyle style = new GUIStyle(GUI.skin.box);
        style.border = new RectOffset(2, 2, 2, 2);
        style.padding = new RectOffset(1, 1, 1, 1);
        GUI.Box(position, GUIContent.none, style);
    }

    public override float GetHeight()
    {
        return 8; // Adjust the height as needed
    }

} 
#endif