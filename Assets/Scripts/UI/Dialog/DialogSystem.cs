using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Handles the apprearance and disappearance of the associated dialog box.
/// </summary>
public class DialogSystem : MonoBehaviour
{
    // TODO: Add animations back to the dialog if required. The animator component on this is disabled through the editor.
    private Animator animator;
    private Text messageField;
    private GameObject okButton;
    private GameObject cancelButton;
    private DialogConfig activeConfig;

    void Start()
    {
        gameObject.SetActive(false);
        animator = GetComponent<Animator>();
        messageField = transform.Find("Text")?.GetComponent<Text>();
        okButton = transform.Find("OKButton").gameObject;
        cancelButton = transform.Find("CancelButton").gameObject;
    }

    public void OKClicked()
    {
        activeConfig.OK.OnClick();
        Hide();
    }

    public void CancelClicked()
    {
        activeConfig.Cancel.OnClick();
        Hide();
    }

    /// <summary>
    /// Show the dialog
    /// </summary>
    /// <param name="config">Settings with which the dialog should be created.</param>
    public void Show(DialogConfig config)
    {
        activeConfig = config;

        messageField.text = config.Message;
        okButton.GetComponent<Button>().interactable = config.OK.Interactable;
        okButton.transform.Find("Text").GetComponent<Text>().text = config.OK.Text;
        cancelButton.GetComponent<Button>().interactable = config.Cancel.Interactable;
        cancelButton.transform.Find("Text").GetComponent<Text>().text = config.Cancel.Text;

        animator.SetBool("IsOpen", true);
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Hide the dialog
    /// </summary>
    public void Hide()
    {
        animator.SetBool("IsOpen", false);
        gameObject.SetActive(false);
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
    public DialogButton OK;
    public DialogButton Cancel;
}

public class DialogButton
{
    public UnityAction OnClick { get; }
    public bool Interactable { get; }
    public string Text { get; }

    public DialogButton(bool interactable = true, string text = "Button", UnityAction onClick = default)
    {
        if (onClick == default)
        {
            OnClick = () => { };
        }
        else
        {
            OnClick = onClick;
        }

        Interactable = interactable;
        Text = text;
    }
}