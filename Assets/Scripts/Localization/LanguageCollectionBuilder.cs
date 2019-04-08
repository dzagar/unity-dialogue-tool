using UnityEngine;
using UnityEditor;

public static class LanguageCollectionBuilder
{
    /// <summary>
    /// Builds an instance of <see cref="LanguageCollection"/>. Saves a new language collection as an asset.
    /// </summary>
    /// <returns>The saved localization asset containing the language collection</returns>
    public static LanguageCollection Build()
    {
        // Create an instance of a language collection as an asset, save, and return.
        LanguageCollection languagesAsset = ScriptableObject.CreateInstance<LanguageCollection>();
        AssetDatabase.CreateAsset(languagesAsset, "Assets/Resources/Localization.asset");
        AssetDatabase.SaveAssets();
        return languagesAsset;
    }
}
