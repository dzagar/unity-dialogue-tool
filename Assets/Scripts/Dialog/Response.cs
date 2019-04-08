using System;
using UnityEngine;

[Serializable]
public class Response {

    public bool continues = false;
    public int editorDialogKeyIndex;
    public int continuingDialogKeyIndex;

    // ********** RESUBMISSION: Allow for easy localization key selection.
    public string localizationKey
    {
        get
        {
            return LanguageLoader.languageCollection.languages[LanguageLoader.languageCollection.currentLanguageIndex].langStrings[localizationKeyIndex].Key;
        }
    }

    public int localizationKeyIndex;

    public string localizedVal 
    {
        get 
        {
            return LanguageLoader.GetLocalizedString(localizationKey);
        }
    }

    /// <summary>
    /// Gets the next dialog based on the continuing dialog key.
    /// </summary>
    /// <returns>The next dialog.</returns>
    /// <param name="dialogCollection">Dialog collection.</param>
    public Dialog GetNextDialog(DialogCollection dialogCollection) 
    {
        /*int lookUpIndex = continuingDialogKeyIndex;
        if (dialogCollection.currentDialogIndex <= continuingDialogKeyIndex) {
            lookUpIndex += 1;
        }*/
        return continues ? dialogCollection.dialogs[continuingDialogKeyIndex] : null;
    }
}
