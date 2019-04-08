using System;
using System.Collections.Generic;

[Serializable]
public class Language 
{
    /// <summary>
    /// The name of the language.
    /// </summary>
    public string langName;

    /// <summary>
    /// The localized strings associated with this language.
    /// </summary>
    public List<LocalizedString> langStrings;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Language"/> class.
    /// </summary>
    /// <param name="name">Name of the language.</param>
    public Language(string name)
    {
        langName = name;
        langStrings = new List<LocalizedString>();
    }

    /// <summary>
    /// Adds a new, empty localized string to the language.
    /// </summary>
    public void AddString()
    {
        langStrings.Add(new LocalizedString());
    }

    /// <summary>
    /// Removes the localized string at the given index.
    /// </summary>
    /// <param name="index">Index of string to remove</param>
    public void RemoveString(int index)
    {
        langStrings.RemoveAt(index);
    }
}
