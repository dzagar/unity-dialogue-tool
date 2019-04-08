using UnityEngine;
using UnityEditor;

public static class DialogCollectionBuilder 
{
    /// <summary>
    /// Builds an instance of <see cref="DialogCollection"/>. Saves a new dialog collection as an asset.
    /// </summary>
    /// <returns>The saved dialog asset containing the dialog collection</returns>
    public static DialogCollection Build()
    {
        // Create an instance of a language collection as an asset, save, and return.
        DialogCollection dialogsAsset = ScriptableObject.CreateInstance<DialogCollection>();
        AssetDatabase.CreateAsset(dialogsAsset, "Assets/Resources/Dialogs.asset");
        AssetDatabase.SaveAssets();
        return dialogsAsset;
    }
}
