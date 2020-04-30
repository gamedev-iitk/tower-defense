using UnityEngine;

public class WalletUISystem : MonoBehaviour
{
    private DialogSystem dialogSystem;

    // When upgrading towers, the type must be stored for the confirmation dialog,
    // or passed through events. I chose to save it.
    private ETowerType currentType;

    private TDEvent<ETowerType> createTower;
    private TDEvent cancelTowerCreation;

    void Start()
    {
        dialogSystem = transform.Find("Dialog")?.GetComponent<DialogSystem>();

        // Register events and callbacks
        createTower = EventRegistry.GetEvent<ETowerType>("createTower");
        cancelTowerCreation = EventRegistry.GetEvent("cancelTowerCreation");
        EventRegistry.RegisterAction<ETowerType>("showUpgradeDialog", ShowUpgradeDialog);
    }

    /// <summary>
    /// Callback for "showUpgradeDialog" event. Generates content for the upgrade confirmation
    /// dialog and enables it.
    /// </summary>
    /// <param name="type"></param>
    public void ShowUpgradeDialog(ETowerType type)
    {
        currentType = type;

        // TODO: Check balance, change message accordingly to "Can't purchase"
        int cost = 20;
        int cash = 1000;
        string messageString = "Upgrade to " + type.GetString() + "will cost $" + cost + ". You have $" + cash;

        DialogConfig config = new DialogConfig
        {
            Message = messageString,
            OKCallback = OnOKClick,
            CancelCallback = OnCancelClick
        };

        dialogSystem.Show(config);
    }

    /// <summary>
    /// Passed an a callback for the OK button on the upgrade dialog.
    /// </summary>
    public void OnOKClick()
    {
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
