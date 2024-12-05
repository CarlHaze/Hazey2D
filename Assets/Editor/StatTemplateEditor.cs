using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StatTemplate))]
public class StatTemplateEditor : UnityEditor.Editor
{
    SerializedProperty baseStats;

    private void OnEnable()
    {
        baseStats = serializedObject.FindProperty("baseStats");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        EditorGUILayout.LabelField("Base Character Stats", EditorStyles.boldLabel);
        EditorGUILayout.Space(5);

        // Create stats if they don't exist
        if (baseStats.arraySize == 0)
        {
            InitializeStats();
        }

        // Draw stat fields
        for (int i = 0; i < baseStats.arraySize; i++)
        {
            SerializedProperty statProperty = baseStats.GetArrayElementAtIndex(i);
            SerializedProperty typeProperty = statProperty.FindPropertyRelative("type");
            SerializedProperty valueProperty = statProperty.FindPropertyRelative("baseValue");

            EditorGUILayout.BeginHorizontal();
            
            // Draw stat name with bold label
            GUIStyle statLabelStyle = new GUIStyle(EditorStyles.label)
            {
                fontStyle = FontStyle.Bold
            };
            
            // Use fixed width layout options instead of GUIStyle.width
            EditorGUILayout.LabelField(typeProperty.enumDisplayNames[typeProperty.enumValueIndex], 
                                     statLabelStyle, 
                                     GUILayout.Width(100));

            // Draw value field
            EditorGUI.BeginChangeCheck();
            float newValue = EditorGUILayout.FloatField(valueProperty.floatValue);
            if (EditorGUI.EndChangeCheck())
            {
                valueProperty.floatValue = newValue;
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(2);
        }

        // Summary box
        EditorGUILayout.Space(10);
        DrawStatsSummary();

        serializedObject.ApplyModifiedProperties();
    }

    private void InitializeStats()
    {
        baseStats.ClearArray();
        foreach (StatType statType in System.Enum.GetValues(typeof(StatType)))
        {
            baseStats.InsertArrayElementAtIndex(baseStats.arraySize);
            var element = baseStats.GetArrayElementAtIndex(baseStats.arraySize - 1);
            var typeProperty = element.FindPropertyRelative("type");
            var valueProperty = element.FindPropertyRelative("baseValue");
            
            typeProperty.enumValueIndex = (int)statType;
            valueProperty.floatValue = 10f; // Default value
        }
    }

    private void DrawStatsSummary()
    {
        string summary = "Stats Summary:\n";
        float total = 0;

        for (int i = 0; i < baseStats.arraySize; i++)
        {
            var stat = baseStats.GetArrayElementAtIndex(i);
            var type = stat.FindPropertyRelative("type");
            var value = stat.FindPropertyRelative("baseValue");
            
            total += value.floatValue;
            summary += $"{type.enumDisplayNames[type.enumValueIndex]}: {value.floatValue}\n";
        }

        summary += $"\nTotal Stats: {total}";
        EditorGUILayout.HelpBox(summary, MessageType.Info);
    }
}