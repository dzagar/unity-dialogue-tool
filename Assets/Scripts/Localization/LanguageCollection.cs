using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Localization", menuName = "Language Collection", order = 1)]
public class LanguageCollection : ScriptableObject 
{
    /// <summary>
    /// True if localization is turned on, false otherwise.
    /// </summary>
    public bool isLocalizationEnabled = false;

    /// <summary>
    /// The index of the current language.
    /// </summary>
    public int currentLanguageIndex = 0;

    /// <summary>
    /// A list of all the supported languages.
    /// </summary>
    public List<Language> languages = new List<Language>();

    /// <summary>
    /// Adds the new language.
    /// </summary>
    /// <param name="langName">The language's name.</param>
    public void AddNewLanguage(string langName)
    {
        languages.Add(new Language(langName));
    }

    /// <summary>
    /// Removes the language at the current index.
    /// </summary>
    public void RemoveLanguage()
    {
        languages.RemoveAt(currentLanguageIndex);
        currentLanguageIndex = 0;
    }
}
