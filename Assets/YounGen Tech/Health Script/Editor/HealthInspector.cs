using UnityEngine;
using UnityEditor;
using System.Collections;

namespace YounGenTech.HealthScript {
    [CustomEditor(typeof(Health))]
    public class HealthInspector : Editor {

        SerializedProperty clampHealth;
        SerializedProperty disableHealthChange;
        SerializedProperty incurable;
        SerializedProperty invincible;
        SerializedProperty maxValue;
        SerializedProperty value;

        SerializedProperty OnChangedHealth;
        SerializedProperty OnChangedMaxHealth;

        SerializedProperty OnHeal;
        SerializedProperty OnFullyRestored;
        SerializedProperty OnRevived;
        SerializedProperty OnDamaged;
        SerializedProperty OnDeath;
        SerializedProperty OnFirstDamaged;

        SerializedProperty OnCausedHeal;
        SerializedProperty OnCausedFullRestoration;
        SerializedProperty OnCausedRevival;
        SerializedProperty OnCausedDamage;
        SerializedProperty OnCausedDeath;
        SerializedProperty OnCausedFirstDamage;

        void OnEnable() {
            clampHealth = serializedObject.FindProperty("_clampHealth");
            disableHealthChange = serializedObject.FindProperty("_disableHealthChange");
            incurable = serializedObject.FindProperty("_incurable");
            invincible = serializedObject.FindProperty("_invincible");
            maxValue = serializedObject.FindProperty("_maxValue");
            value = serializedObject.FindProperty("_value");

            OnChangedHealth = serializedObject.FindProperty("OnChangedHealth");
            OnChangedMaxHealth = serializedObject.FindProperty("OnChangedMaxHealth");

            OnHeal = serializedObject.FindProperty("OnHeal");
            OnFullyRestored = serializedObject.FindProperty("OnFullyRestored");
            OnRevived = serializedObject.FindProperty("OnRevived");
            OnDamaged = serializedObject.FindProperty("OnDamaged");
            OnDeath = serializedObject.FindProperty("OnDeath");
            OnFirstDamaged = serializedObject.FindProperty("OnFirstDamaged");

            OnCausedHeal = serializedObject.FindProperty("OnCausedHeal");
            OnCausedFullRestoration = serializedObject.FindProperty("OnCausedFullRestoration");
            OnCausedRevival = serializedObject.FindProperty("OnCausedRevival");
            OnCausedDamage = serializedObject.FindProperty("OnCausedDamage");
            OnCausedDeath = serializedObject.FindProperty("OnCausedDeath");
            OnCausedFirstDamage = serializedObject.FindProperty("OnCausedFirstDamage");
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                EditorGUILayout.PropertyField(disableHealthChange);
                EditorGUILayout.PropertyField(clampHealth);

                GUI.enabled = !disableHealthChange.boolValue;

                if(clampHealth.boolValue)
                    EditorGUILayout.Slider(value, 0, maxValue.floatValue, GUILayout.ExpandWidth(true));
                else
                    EditorGUILayout.PropertyField(value, GUILayout.ExpandWidth(false));

                GUI.enabled = true;

                EditorGUILayout.PropertyField(maxValue, GUILayout.ExpandWidth(false));
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginHorizontal(GUI.skin.box);
            {
                GUI.backgroundColor = Color.green;
                EditorGUILayout.PropertyField(incurable);

                GUI.backgroundColor = Color.red;
                EditorGUILayout.PropertyField(invincible);

                GUI.backgroundColor = Color.white;
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Separator();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                EditorGUILayout.PropertyField(OnChangedHealth);
                EditorGUILayout.PropertyField(OnChangedMaxHealth);
            }
            EditorGUILayout.EndVertical();

            EditorGUI.indentLevel++;
            {
                OnHeal.isExpanded = EditorGUILayout.Foldout(OnHeal.isExpanded, "Received Health Events");

                if(OnHeal.isExpanded) {
                    EditorGUILayout.BeginVertical(GUI.skin.box);
                    {
                        EditorGUILayout.PropertyField(OnRevived);
                        EditorGUILayout.PropertyField(OnFirstDamaged);
                        EditorGUILayout.PropertyField(OnHeal);
                        EditorGUILayout.PropertyField(OnDamaged);
                        EditorGUILayout.PropertyField(OnFullyRestored);
                        EditorGUILayout.PropertyField(OnDeath);
                    }
                    EditorGUILayout.EndVertical();
                }
            }
            EditorGUI.indentLevel--;

            EditorGUI.indentLevel++;
            {
                OnCausedHeal.isExpanded = EditorGUILayout.Foldout(OnCausedHeal.isExpanded, "Caused Health Events");

                if(OnCausedHeal.isExpanded) {
                    EditorGUILayout.BeginVertical(GUI.skin.box);
                    {
                        EditorGUILayout.PropertyField(OnCausedRevival);
                        EditorGUILayout.PropertyField(OnCausedFirstDamage);
                        EditorGUILayout.PropertyField(OnCausedHeal);
                        EditorGUILayout.PropertyField(OnCausedDamage);
                        EditorGUILayout.PropertyField(OnCausedFullRestoration);
                        EditorGUILayout.PropertyField(OnCausedDeath);
                    }
                    EditorGUILayout.EndVertical();
                }
            }
            EditorGUI.indentLevel--;

            serializedObject.ApplyModifiedProperties();
        }
    }
}