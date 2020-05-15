using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the wallet UI on screen.
/// </summary>
public class WalletUISystem : MonoBehaviour
{
    // When upgrading towers, the type must be stored for the confirmation dialog,
    // or passed through events. I chose to save it.
    private ETowerType currentType;
    private DialogSystem dialogSystem;
    private Text cashDisplay;

    private TDEvent<ETowerType> createTower;

    private TDEvent<bool> moveTransaction;

    private TDEvent cancelTowerCreation;

    void Start()
    {
        dialogSystem = transform.Find("Dialog")?.GetComponent<DialogSystem>();
        cashDisplay = transform.Find("WalletBG/Cash")?.GetComponent<Text>();

        // Register events and callbacks
        createTower = EventRegistry.GetEvent<ETowerType>("createTower");
        cancelTowerCreation = EventRegistry.GetEvent("cancelTowerCreation");
        moveTransaction = EventRegistry.GetEvent<bool>("moveTransaction");
        EventRegistry.RegisterAction<ETowerType>("showUpgradeDialog", ShowUpgradeDialog);
        EventRegistry.RegisterAction<ETowerType>("showMoveDialog", ShowMoveDialog);
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
            messageString = "Upgrade to " + type.GetString() + " will cost $" + cost + ". You have $" + GameState.CurrentCash;
            config.OK = new DialogButton(
                onClick: OnOKClickUpgrade,
                text: "Upgrade"
            );
            config.Cancel = new DialogButton(
                onClick: OnCancelClickUpgrade,
                text: "Cancel"
            );
        }

        config.Message = messageString;
        dialogSystem.Show(config);
    }

    /// <summary>
    /// Callback for "showMoveDialog" event. Generates content for the move confirmation
    /// dialog and enables it.
    /// </summary>
    /// <param name="type"></param>
    public void ShowMoveDialog(ETowerType type)
    {
        currentType = type;
        DialogConfig config = new DialogConfig();

        int cost = currentType.GetMoveCost();
        string messageString;
        if (cost > GameState.CurrentCash)
        {
            messageString = "You don't have enough cash. Required: " + cost + ". You have $" + GameState.CurrentCash;

            // As both buttons won't do anything special in this case, you could leave the callback
            config.OK = new DialogButton(
                interactable: false,
                text: "Move"
            );
            config.Cancel = new DialogButton(
                onClick: OnCancelClickMove,
                text: "Cancel"
            );
        }
        else
        {
            messageString = "Moving " + type.GetString() + " will cost $" + cost + ". You have $" + GameState.CurrentCash;
            config.OK = new DialogButton(
                onClick: OnOKClickMove,
                text: "Move"
            );
            config.Cancel = new DialogButton(
                onClick: OnCancelClickMove,
                text: "Cancel"
            );
        }

        config.Message = messageString;
        dialogSystem.Show(config);
    }

    /// <summary>
    /// Passed as a callback for the OK button on the upgrade dialog.
    /// </summary>
    public void OnOKClickUpgrade()
    {
        GameState.CurrentCash -= currentType.GetCost();
        createTower.Invoke(currentType);
    }

    /// <summary>
    /// Passed as a callback for the Cancel button on the upgrade dialog.
    /// </summary>
    public void OnCancelClickUpgrade()
    {
        cancelTowerCreation.Invoke();
    }

    /// <summary>
    /// Passed as a callback for the OK button on the move dialog.
    /// </summary>
    public void OnOKClickMove()
    {
        GameState.CurrentCash -= currentType.GetMoveCost();
        moveTransaction.Invoke(true);
    }

    /// <summary>
    /// Passed as a callback for the Cancel button on the move dialog.
    /// </summary>
    public void OnCancelClickMove()
    {
        moveTransaction.Invoke(false);
    }
}
