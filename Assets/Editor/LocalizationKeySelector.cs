using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

// ******** RESUBMISSION: Make it ultra easy for the designer to input localized strings! ********
[CustomEditor(typeof(Localization))]
public class LocalizationKeySelector : Editor
{
    LanguageCollection languageCollection;
    Language currentLanguage;
    List<string> availableKeys = new List<string>();
    string currentLangName;

    SerializedProperty currentIndex;
    SerializedProperty text;

    private void OnEnable()
    {
        languageCollection = LanguageLoader.languageCollection;
        currentIndex = serializedObject.FindProperty("currentIndex");
        text = serializedObject.FindProperty("text");
        RefreshCurrentLanguage();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // Before we clear the popup list, let's quickly store the current key in case we
        // need to do a lookup (on language change).
        var lastKey = availableKeys[currentIndex.intValue];

        RefreshCurrentLanguage();

        // If we switcharooed the language, let's try to find the same key that we had selected
        // before. Otherwise, reset to 0.
        var newIndex = availableKeys.FindIndex(str => str.Equals(lastKey));
        currentIndex.intValue = newIndex >= 0 ? newIndex : 0;

        // Display the current language that is chosen.
        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Current language:");
        EditorGUILayout.LabelField(currentLangName, EditorStyles.boldLabel);
        GUILayout.EndHorizontal();

        // Display a popup for the user to select a valid string in the current language.
        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Localized key to use: ");
        currentIndex.intValue = EditorGUILayout.Popup(currentIndex.intValue, availableKeys.ToArray());
        GUILayout.EndHorizontal();

        // Set the string value of the text component to the localized string.
        text.stringValue = LanguageLoader.GetLocalizedString(availableKeys[currentIndex.intValue]);

        serializedObject.ApplyModifiedProperties();
    }

    private void RefreshCurrentLanguage()
    {
        // Clear the popup list
        availableKeys.Clear();

        // Get the current language info
        currentLanguage = languageCollection.languages[languageCollection.currentLanguageIndex];
        currentLangName = currentLanguage.langName;

        // Populate the popup list
        foreach (var kvp in currentLanguage.langStrings)
        {
            availableKeys.Add(kvp.Key);
        }
    }
}
