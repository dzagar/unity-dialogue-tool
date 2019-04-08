using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the localized data .
/// </summary>
public static class LanguageLoader 
{
    /// <summary>
    /// The static localization object.
    /// </summary>
    public static LanguageCollection languageCollection;

    /// <summary>
    /// Initializes the <see cref="T:LanguageLoader"/> class.
    /// </summary>
    static LanguageLoader()
    {
        languageCollection = Resources.Load("Localization") as LanguageCollection;
    }

    /// <summary>
    /// Gets the localized string.
    /// </summary>
    /// <returns>The localized string.</returns>
    /// <param name="friendlyValue">Friendly value.</param>
    public static string GetLocalizedString(string key)
    {
        if (!languageCollection.isLocalizationEnabled) return key;

        var language = languageCollection.languages[languageCollection.currentLanguageIndex];

        string outputString;

        if (language.langStrings != null && TryMapLocalizedString(key, 
                                                                  language.langStrings, 
                                                                  out outputString))
        {
            return outputString;
        }

        return key;
    }

    /// <summary>
    /// Tries to find the localized string mapping.
    /// </summary>
    /// <returns>
    /// <c>true</c>, if mapped localized string was found, 
    /// <c>false</c> otherwise.
    /// </returns>
    /// <param name="friendlyValue">Friendly value.</param>
    /// <param name="entries">The language entries.</param>
    /// <param name="localizedString">Localized string.</param>
    private static bool TryMapLocalizedString(string key, 
                                              List<LocalizedString> entries, 
                                              out string localizedString)
    {
        var localizedStringObj = entries.Find(str => str.Key.Equals(key));
        if (localizedStringObj == null)
        {
            localizedString = string.Empty;
            return false;
        }

        localizedString = localizedStringObj.LocalizedValue;
        return true;
    }
}
