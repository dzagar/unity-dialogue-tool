using System.Collections.Generic;
using System;

[Serializable]
public class Dialog {

    public List<Response> responses = new List<Response>();

    // ********** RESUBMISSION: Allow for easy localization key selection.
    public string localizationKey {
        get
        {
            return LanguageLoader.languageCollection.languages[LanguageLoader.languageCollection.currentLanguageIndex].langStrings[localizationKeyIndex].Key;
        }
    }

    public int localizationKeyIndex;

    public string localizedVal {
        get 
        {
            return LanguageLoader.GetLocalizedString(localizationKey);
        }
    }

    /// <summary>
    /// Removes the response.
    /// </summary>
    /// <param name="index">Index of the response to be obliterated.</param>
    public void RemoveResponse(int index) 
    {
        responses.RemoveAt(index);
    }
}
