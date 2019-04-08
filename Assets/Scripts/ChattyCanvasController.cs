using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Chatty canvas controller! Alliterations always!
/// </summary>
public class ChattyCanvasController : MonoBehaviour {

    public GameObject textPrefab;
    public GameObject buttonPrefab;

    Dialog currentDialog;
    DialogCollection dialogCollection;
    Vector3 pos;
    bool updateDisplay = false;

    void OnEnable()
    {
        dialogCollection = Resources.Load("Dialogs") as DialogCollection;

        if (dialogCollection.dialogs.Count == 0)
        {
            // Goofed.
            Application.Quit();
            return;
        }

        // Start with the active dialog. 
        currentDialog = dialogCollection.currentDialog;
        ShowDialog(currentDialog);
    }

    void Update()
    {
        if (updateDisplay) {
            // Reload ALL the buttons
            for (var i = 0; i < transform.childCount; i++)
            {
                var go = gameObject.transform.GetChild(i).gameObject;
                Destroy(go);
            }

            // Quits if the continuing dialog key is empty.
            if (currentDialog == null)
            {
                Application.Quit();
            }
            else
            {
                ShowDialog(currentDialog);
            }
            updateDisplay = false;
        }
    }

    /// <summary>
    /// Shows the dialog.
    /// </summary>
    /// <param name="dialog">Dialog.</param>
    public void ShowDialog(Dialog dialog) {
        pos = Vector3.zero;
        // Display the statement from the NPC
        SpawnText(dialog.localizedVal, pos);

        // Display all available responses in a list
        for (var i = 0; i < dialog.responses.Count; i++) {
            pos.y -= 40;
            SpawnResponse(dialog.responses[i], pos);
        }
    }

    /// <summary>
    /// Spawns the text.
    /// </summary>
    /// <param name="text">Text.</param>
    /// <param name="pos">Position.</param>
    public void SpawnText(string text, Vector3 pos) {
        var textObj = Instantiate(textPrefab, pos, Quaternion.identity);
        // SET THE PARENT TO THE CANVAS TRANSFORM OR ELSE EVIL OCCURS
        textObj.transform.SetParent(gameObject.transform, false);
        textObj.GetComponent<Text>().text = text;
    }

    public void SpawnResponse(Response response, Vector3 pos) {
        var buttonObj = Instantiate(buttonPrefab, pos, Quaternion.identity);
        // SET THE PARENT TO THE CANVAS TRANSFORM OR ELSE EVIL OCCURS
        buttonObj.transform.SetParent(gameObject.transform, false);
        buttonObj.GetComponentInChildren<Text>().text = response.localizedVal;
        buttonObj.GetComponent<Button>().onClick.AddListener(() => OnResponseClick(response, dialogCollection));
    }

    public void OnResponseClick(Response response, DialogCollection dialogCollection) {
        updateDisplay = true;
        currentDialog = response.GetNextDialog(dialogCollection);
        //dialogCollection.currentDialogIndex = dialogCollection.dialogs.IndexOf(currentDialog);
    }
}
