using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Handles the apprearance and disappearance of the associated dialog box.
/// </summary>
public class DialogSystem : MonoBehaviour
{
    private Animator animator;
    private Text messageField;
    private DialogConfig activeConfig;

    void Start()
    {
        animator = GetComponent<Animator>();
        messageField = transform.Find("Text")?.GetComponent<Text>();
    }

    public void OKClicked()
    {
        Debug.LogError("OKClicked");
        activeConfig.OKCallback();
        Hide();
    }

    public void CancelClicked()
    {
        activeConfig.CancelCallback();
        Hide();
    }

    /// <summary>
    /// Show the dialog
    /// </summary>
    /// <param name="config">Settings with which the dialog should be created.</param>
    public void Show(DialogConfig config)
    {
        messageField.text = config.Message;
        activeConfig = config;
        animator.SetBool("IsOpen", true);
    }

    /// <summary>
    /// Hide the dialog
    /// </summary>
    public void Hide()
    {
        animator.SetBool("IsOpen", false);
    }
}

/// <summary>
/// Configuration settings for a dialog.
/// </summary>
public struct DialogConfig
{
    /// <summary>
    /// Message to be displayed in the text field of the dialog box.
    /// </summary>
    public string Message;

    /// <summary>
    /// Callback for the OK button on the dialog box.
    /// </summary>
    public UnityAction OKCallback;

    /// <summary>
    /// Callback for the cancel button on the dialog box.
    /// </summary>
    public UnityAction CancelCallback;
}