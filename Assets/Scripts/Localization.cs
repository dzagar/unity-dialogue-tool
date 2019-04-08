using UnityEngine;
using UnityEngine.UI;

// ******* RESUBMISSION: Make it ultra easy for the designer to input localized strings! ***********
// This can be attached to any Text objects to localize them.
[ExecuteInEditMode]
public class Localization : MonoBehaviour
{
    private Text textComponent;

    public int currentIndex = 0;
    public string text;

    public void OnEnable()
    {
        textComponent = GetComponent<Text>();
    }

    public void OnGUI()
    {
        textComponent.text = text;
    }
}
