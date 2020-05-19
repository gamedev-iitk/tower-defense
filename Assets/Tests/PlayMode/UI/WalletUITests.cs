using System.Collections;
using UnityEngine.TestTools;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tests.UI
{
    /// <summary>
    /// TODO: Tests for the WalletUI
    /// </summary>
    public class WalletUITests
    {
        [SetUp]
        public void Setup()
        {
            SceneManager.LoadScene("TestScene");
        }

        [UnityTest]
        public IEnumerator DisplaysCurrentCash()
        {
            GameObject walletUI = GameObject.Find("UIManager").transform.Find("WalletUI").gameObject;
            GameObject cashText = walletUI.transform.Find("WalletBG/Cash").gameObject;
            yield return null;
            Assert.AreEqual("$ " + GameState.CurrentCash, cashText.GetComponent<Text>().text);
        }

        [UnityTest]
        public IEnumerator CashUICanChange()
        {
            GameObject walletUI = GameObject.Find("UIManager").transform.Find("WalletUI").gameObject;
            GameObject cashText = walletUI.transform.Find("WalletBG/Cash").gameObject;
            string originalText = cashText.GetComponent<Text>().text;
            GameState.CurrentCash -= 1;
            yield return null;
            Assert.AreNotEqual(originalText, cashText.GetComponent<Text>().text);
        }

        [UnityTest]
        public IEnumerator CashDeductedUponAction()
        {
            GameObject walletUI = GameObject.Find("UIManager").transform.Find("WalletUI").gameObject;
            int originalCash = GameState.CurrentCash;
            TDEvent<ETowerType> showMoveDialog = EventRegistry.GetEvent<ETowerType>("showMoveDialog");
            showMoveDialog.Invoke(ETowerType.Base);
            walletUI.GetComponent<WalletUISystem>().OnOKClickMove();
            yield return null;
            Assert.AreNotEqual(originalCash, GameState.CurrentCash);
        }
    }
}
