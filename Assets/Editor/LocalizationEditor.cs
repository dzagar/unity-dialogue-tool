using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LocalizationEditor : EditorWindow {

    private LanguageCollection languageCollection;

    Vector2 scrollPosition;

    [MenuItem("Window/Localization")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(LocalizationEditor));
    }

    private void OnEnable()
    {
        // Load the language collection.
        languageCollection = Resources.Load("Localization") as LanguageCollection;

        // If one hasn't been created yet, build one.
        if (languageCollection == null)
        {
            languageCollection = LanguageCollectionBuilder.Build();
        }
    }

    private void OnGUI()
    {
        // Window title.
        GUILayout.BeginHorizontal();
        GUILayout.Label("Localization Editor!!!", EditorStyles.boldLabel); // !!! for major excitement. L10N SUCH EXCITE.
        GUILayout.EndHorizontal();

        // Build list of language names.
        List<string> langs = new List<string>();

        if (languageCollection.languages.Count > 0)
        {
            foreach (var lang in languageCollection.languages)
            {
                langs.Add(lang.langName);
            }
        }

        // Enable localization toggle.
        languageCollection.isLocalizationEnabled = EditorGUILayout.Toggle("Enable localization", languageCollection.isLocalizationEnabled);

        // Don't render anything else, since localization is not enabled.
        if (!languageCollection.isLocalizationEnabled) return;

        // Add a language.
        GUILayout.BeginHorizontal();
        int latestIndex = languageCollection.languages.Count - 1;
        if (latestIndex < 0)
        {
            languageCollection.AddNewLanguage(string.Empty);
            latestIndex = 0;
        }

        languageCollection.languages[latestIndex].langName = EditorGUILayout.TextField("New language name", languageCollection.languages[latestIndex].langName);

        if (GUILayout.Button("Add language") && !languageCollection.languages[latestIndex].langName.Equals(string.Empty))
        {
            languageCollection.AddNewLanguage(string.Empty);
        }

        GUILayout.EndHorizontal();

        EditorGUILayout.Space();

        // Set active language, and add/remove strings to/from it.
        GUILayout.BeginHorizontal();
        languageCollection.currentLanguageIndex = EditorGUILayout.Popup("Active language", languageCollection.currentLanguageIndex, langs.ToArray());
        var currentLang = languageCollection.languages[languageCollection.currentLanguageIndex];

        // ******** RESUBMISSION: Allow for removal of current language. ************
        if (GUILayout.Button("Remove language", GUILayout.ExpandWidth(false)))
        {
            languageCollection.RemoveLanguage();
        }

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label(string.Format("Add localized string to {0}", currentLang.langName), GUILayout.ExpandWidth(false));
        if (GUILayout.Button("+", GUILayout.ExpandWidth(false)))
        {
            currentLang.AddString();
        }
        GUILayout.EndHorizontal();

        // Show each editable localized string in the current language.
        // *********** RESUBMISSION: Added a scroll view. Doh. ************
        GUILayout.BeginHorizontal();
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);
        foreach (var str in currentLang.langStrings.ToArray())
        {
            EditorGUILayout.Space();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Key", GUILayout.ExpandWidth(false));
            str.Key = EditorGUILayout.TextField(str.Key);
            GUILayout.Label("Localized", GUILayout.ExpandWidth(false));
            str.LocalizedValue = EditorGUILayout.TextField(str.LocalizedValue);

            // Allow for removal of specific localized string.
            if (GUILayout.Button("x", GUILayout.ExpandWidth(false)))
            {
                currentLang.RemoveString(currentLang.langStrings.FindIndex(s => s.Equals(str)));
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndScrollView();
        GUILayout.EndHorizontal();

        // Save the asset because without The Save we are NOTHING. NOOTHINGG
        if (GUI.changed)
        {
            EditorUtility.SetDirty(languageCollection);
            AssetDatabase.Refresh();
        }
    }
}
