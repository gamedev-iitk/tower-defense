using UnityEngine;
using UnityEngine.UI;

public class WalletUISystem : MonoBehaviour
{
    // When upgrading towers, the type must be stored for the confirmation dialog,
    // or passed through events. I chose to save it.
    private ETowerType currentType;
    private DialogSystem dialogSystem;
    private Text cashDisplay;

    private TDEvent<ETowerType> createTower;
    private TDEvent cancelTowerCreation;

    void Start()
    {
        dialogSystem = transform.Find("Dialog")?.GetComponent<DialogSystem>();
        cashDisplay = transform.Find("WalletBG/Cash")?.GetComponent<Text>();

        // Register events and callbacks
        createTower = EventRegistry.GetEvent<ETowerType>("createTower");
        cancelTowerCreation = EventRegistry.GetEvent("cancelTowerCreation");
        EventRegistry.RegisterAction<ETowerType>("showUpgradeDialog", ShowUpgradeDialog);
    }

    void Update()
    {
        cashDisplay.text = "$ " + GameState.CurrentCash;
    }

    /// <summary>
    /// Callback for "showUpgradeDialog" event. Generates content for the upgrade confirmation
    /// dialog and enables it.
    /// </summary>
    /// <param name="type"></param>
    public void ShowUpgradeDialog(ETowerType type)
    {
        currentType = type;
        DialogConfig config = new DialogConfig();

        // TODO: Check balance, change message accordingly to "Can't purchase"
        int cost = currentType.GetCost();
        string messageString;
        if (cost > GameState.CurrentCash)
        {
            messageString = "You don't have enough cash. Required: " + cost + ". You have $" + GameState.CurrentCash;

            // As both buttons won't do anything special in this case, you could leave the callback
            config.OK = new DialogButton(
                interactable: false,
                text: "Upgrade"
            );
            config.Cancel = new DialogButton(
                text: "Cancel"
            );
        }
        else
        {
            messageString = "Upgrade to " + type.GetString() + "will cost $" + cost + ". You have $" + GameState.CurrentCash;
            config.OK = new DialogButton(
                onClick: OnOKClick,
                text: "Upgrade"
            );
            config.Cancel = new DialogButton(
                onClick: OnCancelClick,
                text: "Cancel"
            );
        }

        config.Message = messageString;
        dialogSystem.Show(config);
    }

    /// <summary>
    /// Passed an a callback for the OK button on the upgrade dialog.
    /// </summary>
    public void OnOKClick()
    {
        GameState.CurrentCash -= currentType.GetCost();
        createTower.Invoke(currentType);
    }

    /// <summary>
    /// Passed as a callback for the Cancel button on the upgrade dialog.
    /// </summary>
    public void OnCancelClick()
    {
        cancelTowerCreation.Invoke();
    }
}
