using UnityEditor;
using UnityEngine;

namespace Items.Weapons.Editor
{
    [CustomEditor(typeof(WeaponTemplate))]
    public class WeaponTemplateEditor : UnityEditor.Editor
    {
        SerializedProperty weaponName;
        SerializedProperty weaponSprite;
        SerializedProperty weaponPrefab;
        SerializedProperty baseStats;
        SerializedProperty weaponCategory;
        SerializedProperty subcategoryIndex;

        private void OnEnable()
        {
            // Get the serialized properties
            weaponName = serializedObject.FindProperty("weaponName");
            weaponSprite = serializedObject.FindProperty("weaponSprite");
            weaponPrefab = serializedObject.FindProperty("weaponPrefab");
            baseStats = serializedObject.FindProperty("baseStats");
            weaponCategory = serializedObject.FindProperty("weaponCategory");
            subcategoryIndex = serializedObject.FindProperty("subcategoryIndex");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            WeaponTemplate weapon = (WeaponTemplate)target;

            // Draw main properties
            EditorGUILayout.PropertyField(weaponName);
            EditorGUILayout.PropertyField(weaponSprite);
            EditorGUILayout.PropertyField(weaponPrefab);
            EditorGUILayout.PropertyField(baseStats);

            // Draw weapon category with custom styling
            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("Weapon Classification", EditorStyles.boldLabel);
            
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(weaponCategory);
            if (EditorGUI.EndChangeCheck())
            {
                // Reset subcategory index when category changes
                subcategoryIndex.intValue = 0;
            }

            // Draw subcategory dropdown
            System.Type enumType = weapon.GetSubcategoryEnumType();
            if (enumType != null)
            {
                string[] names = System.Enum.GetNames(enumType);
                int currentIndex = subcategoryIndex.intValue;

                EditorGUI.BeginChangeCheck();
                currentIndex = EditorGUILayout.Popup("Subcategory", currentIndex, names);
                if (EditorGUI.EndChangeCheck())
                {
                    subcategoryIndex.intValue = currentIndex;
                }
            }

            serializedObject.ApplyModifiedProperties();

            // Draw a help box with weapon type information
            EditorGUILayout.Space(10);
            string categoryInfo = weapon.weaponCategory.ToString();
            string subcategoryInfo = weapon.Subcategory.ToString();
            EditorGUILayout.HelpBox(
                $"Selected Weapon Type:\n" +
                $"Category: {categoryInfo}\n" +
                $"Type: {subcategoryInfo}", 
                MessageType.Info
            );
        }
    }
}