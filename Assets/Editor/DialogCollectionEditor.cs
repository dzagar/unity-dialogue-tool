using UnityEngine;
using UnityEditor;
using System.Linq;

public class DialogCollectionEditor : EditorWindow
{
    Vector2 scrollPosition;
    DialogCollection dialogCollection;
    LanguageCollection languageCollection;
    string[] dialogKeys;
    // ********** RESUBMISSION: Allow for easy localization key selection.
    string[] localizationKeys;

    [MenuItem("Window/Dialog Editor")]
    static void Init() {
        EditorWindow.GetWindow(typeof(DialogCollectionEditor));
    }

    void OnEnable()
    {
        dialogCollection = Resources.Load("Dialogs") as DialogCollection;
        languageCollection = Resources.Load("Localization") as LanguageCollection;

        // If one hasn't been created yet, build one.
        if (dialogCollection == null)
        {
            dialogCollection = DialogCollectionBuilder.Build();
        }

        // If one hasn't been created yet, build one.
        if (languageCollection == null)
        {
            languageCollection = LanguageCollectionBuilder.Build();
        }
    }

    void OnGUI()
    {
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);
        dialogKeys = dialogCollection.dialogs.Select(d => d.localizationKey).ToArray();
        localizationKeys = languageCollection.languages[languageCollection.currentLanguageIndex].langStrings.Select(s => s.Key).ToArray();

        GUILayout.Label("Dialog Collection Editor!!", EditorStyles.boldLabel);

        if (dialogCollection != null) {

            // Add new dialog
            GUILayout.BeginHorizontal();
            GUILayout.Label("Add some dialogue.", GUILayout.ExpandWidth(false));
            if (GUILayout.Button("+", GUILayout.ExpandWidth(false)))
            {
                var newDialog = dialogCollection.Add();
                dialogCollection.currentDialogIndex = dialogCollection.dialogs.IndexOf(newDialog);
            }
            GUILayout.EndHorizontal();

            // Modify active dialog / remove active dialog
            GUILayout.BeginHorizontal();
            dialogCollection.currentDialogIndex = EditorGUILayout.Popup("Active dialogue: ", dialogCollection.currentDialogIndex, dialogKeys);
            if (GUILayout.Button("x"))
            {
                dialogCollection.RemoveDialog(dialogCollection.currentDialogIndex);
            }
            GUILayout.EndHorizontal();

            // Display active dialog
            if (dialogCollection.currentDialog != null)
            {
                ShowDialog(dialogCollection.currentDialog);
            }
        }

        if (GUI.changed) {
            // SAVE ALL THE THINGS!
            EditorUtility.SetDirty(dialogCollection);
        }
        GUILayout.EndScrollView();
    }

    void ShowDialog(Dialog dialog) {
        EditorGUILayout.Space();

        // NPC Statement
        // ********** RESUBMISSION: Allow for easy localization key selection.
        dialog.localizationKeyIndex = EditorGUILayout.Popup("Statement key string: ", dialog.localizationKeyIndex, localizationKeys);
        EditorGUILayout.LabelField("Statement preview: ", dialog.localizedVal);

        // Add response(s)
        GUILayout.BeginHorizontal();
        GUILayout.Label("Add a potential response.", GUILayout.ExpandWidth(false));
        if (GUILayout.Button("+", GUILayout.ExpandWidth(false))) {
            dialog.responses.Add(new Response());
        }
        GUILayout.EndHorizontal();

        // List response(s)
        EditorGUILayout.Space();
        for (var i = 0; i < dialog.responses.Count; i++) {
            // I tried to make it look cool but now it just looks like gross code and a mediocre UI.
            // Oh well.
            EditorGUILayout.Space();
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical(GUILayout.ExpandWidth(true));

            // ********** RESUBMISSION: Allow for easy localization key selection.
            dialog.responses[i].localizationKeyIndex = EditorGUILayout.Popup("Response key string: ", dialog.responses[i].localizationKeyIndex, localizationKeys);
            GUILayout.BeginHorizontal();
            dialog.responses[i].continues = EditorGUILayout.Toggle("Continue dialog?", dialog.responses[i].continues);
            if (dialog.responses[i].continues) {
                // ********* RESUBMISSION: Don't allow dialog to select itself in continuing dialogs.
                var validDialogs = dialogKeys.Where(d => !d.Equals(dialogKeys.ElementAt(dialogCollection.currentDialogIndex))).ToArray();
                if (dialogCollection.currentDialogIndex <= dialog.responses[i].editorDialogKeyIndex) {
                    dialog.responses[i].continuingDialogKeyIndex = dialog.responses[i].editorDialogKeyIndex + 1;
                }
                dialog.responses[i].editorDialogKeyIndex = EditorGUILayout.Popup(string.Empty, dialog.responses[i].editorDialogKeyIndex, validDialogs);
            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
            EditorGUILayout.Space();
            GUILayout.BeginVertical(GUILayout.ExpandWidth(false));
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Reply preview: ", dialog.responses[i].localizedVal);
            if (GUILayout.Button("x", GUILayout.MaxWidth(50.0f))) {
                dialog.RemoveResponse(i);
            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }
    }
}
