using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogs", menuName = "Dialog Collection", order = 1)]
public class DialogCollection : ScriptableObject {

    public List<Dialog> dialogs = new List<Dialog>();
    public int currentDialogIndex;

    public Dialog currentDialog
    {
        get
        {
            return dialogs.Count > 0 ? dialogs[currentDialogIndex] : null;
        }
    }

    /// <summary>
    /// Adds a new dialog to the collection.
    /// </summary>
    /// <returns>The new dialog</returns>
    public Dialog Add() 
    {
        var newDialog = new Dialog();
        dialogs.Add(newDialog);
        return newDialog;
    }

    /// <summary>
    /// Removes the dialog at the given index
    /// </summary>
    /// <param name="index">Index of the dialog to be OBLITERATED</param>
    public void RemoveDialog(int index) 
    {
        dialogs.RemoveAt(index);

        // Adjust the current index so it is not invalidated
        if (currentDialogIndex >= dialogs.Count && dialogs.Count > 0) {
            currentDialogIndex = dialogs.Count - 1;
        }
    }
}
