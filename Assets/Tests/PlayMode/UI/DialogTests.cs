using System.Collections;
using UnityEngine.TestTools;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tests.UI
{
    /// <summary>
    /// TODO: Tests for the dialog box
    /// </summary>
    public class DialogTests
    {
        [SetUp]
        public void Setup()
        {
            SceneManager.LoadScene("TestScene");
        }

        [UnityTest]
        public IEnumerator UIShowsUpOnMoveClick()
        {
            TDEvent<ETowerType> showMoveDialog = EventRegistry.GetEvent<ETowerType>("showMoveDialog");
            showMoveDialog.Invoke(ETowerType.Base);
            GameObject dialogBox = GameObject.Find("UIManager").transform.Find("WalletUI/Dialog").gameObject;
            yield return null;
            Assert.True(dialogBox.activeSelf);
        }

        [UnityTest]
        public IEnumerator UIShowsUpOnUpgradeClick()
        {
            TDEvent<ETowerType> showUpgradeDialog = EventRegistry.GetEvent<ETowerType>("showUpgradeDialog");
            showUpgradeDialog.Invoke(ETowerType.Base);
            GameObject dialogBox = GameObject.Find("UIManager").transform.Find("WalletUI/Dialog").gameObject;
            yield return null;
            Assert.True(dialogBox.activeSelf);
        }

        [UnityTest]
        public IEnumerator UIGoesAwayOnOKClick()
        {
            TDEvent<ETowerType> showMoveDialog = EventRegistry.GetEvent<ETowerType>("showMoveDialog");
            showMoveDialog.Invoke(ETowerType.Base);
            GameObject dialogBox = GameObject.Find("UIManager").transform.Find("WalletUI/Dialog").gameObject;
            yield return null;
            dialogBox.GetComponent<DialogSystem>().OKClicked();
            yield return null;
            Assert.False(dialogBox.activeSelf);
        }

        [UnityTest]
        public IEnumerator UIGoesAwayOnCancelClick()
        {
            TDEvent<ETowerType> showMoveDialog = EventRegistry.GetEvent<ETowerType>("showMoveDialog");
            showMoveDialog.Invoke(ETowerType.Base);
            GameObject dialogBox = GameObject.Find("UIManager").transform.Find("WalletUI/Dialog").gameObject;
            yield return null;
            dialogBox.GetComponent<DialogSystem>().CancelClicked();
            yield return null;
            Assert.False(dialogBox.activeSelf);
        }

        [UnityTest]
        public IEnumerator UIButtonTextDependsOnConfig()
        {
            TDEvent<ETowerType> showMoveDialog = EventRegistry.GetEvent<ETowerType>("showMoveDialog");
            showMoveDialog.Invoke(ETowerType.Base);
            GameObject dialogBox = GameObject.Find("UIManager").transform.Find("WalletUI/Dialog").gameObject;
            GameObject okButtonText = dialogBox.transform.Find("OKButton/Text").gameObject;
            yield return null;
            Assert.AreEqual("Move", okButtonText.GetComponent<Text>().text);
        }
    }
}
