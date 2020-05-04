using System.Collections;
using UnityEngine.TestTools;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        public IEnumerator CanCreateUpgradeUIMenu()
        {
            yield return null;
        }
    }
}
