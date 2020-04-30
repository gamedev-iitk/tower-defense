using System.Collections;
using UnityEngine.TestTools;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            SceneManager.LoadScene("MainScene");
        }

        [UnityTest]
        public IEnumerator CanCreateUpgradeUIMenu()
        {
            yield return null;
        }
    }
}
