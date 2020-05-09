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

    /// <summary>
    /// Called when the OK button is clicked on the UI.
    /// </summary>
    public void OKClicked()
    {
        activeConfig.OK.OnClick();
        Hide();
    }

    /// <summary>
    /// Called when the Cancel button is clicked on the UI.
    /// </summary>
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

    /// <summary>
    /// Settings for the OK button.
    /// </summary>
    public DialogButton OK;

    /// <summary>
    /// Settings for the Cancel button.
    /// </summary>
    public DialogButton Cancel;
}

public class DialogButton
{
    /// <summary>
    /// Callback for the button.
    /// </summary>
    /// <value>A void function with zero arguments.</value>
    public UnityAction OnClick { get; }

    /// <summary>
    /// Indicates that the button is interactable.
    /// </summary>
    /// <value>True if is interactable.</value>
    public bool Interactable { get; }

    /// <summary>
    /// Text to be shown on the button.
    /// </summary>
    /// <value>String</value>
    public string Text { get; }

    /// <summary>
    /// Constructor for this settings class to allow optional parameters.
    /// </summary>
    /// <param name="interactable">(Optional) Is the button interactable. Default is true.</param>
    /// <param name="text">(Optional) The text to be shown on the button. Default is "Button".</param>
    /// <param name="onClick">(Optional) Function to be called on click. Default is an empty function.</param>
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