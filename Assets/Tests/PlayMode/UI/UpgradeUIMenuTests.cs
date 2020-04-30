using System.Collections;
using UnityEngine.TestTools;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tests.UI
{
    /// <summary>
    /// Tests for the UpgradeMenuUISystem
    /// </summary>
    public class UpgradeUIMenuTests
    {
        [SetUp]
        public void Setup()
        {
            SceneManager.LoadScene("MainScene");
        }

        [UnityTest]
        public IEnumerator CanCreateUpgradeUIMenu()
        {
            // Create the upgrade menu UI
            GameObject baseTower = GameObject.Find("BaseTower");
            EventRegistry.Invoke("showMenu", baseTower, typeof(UpgradeMenuUISystem));
            GameObject upgradeMenu = GameObject.Find("UIManager/UpgradeUI");

            yield return null;
            Assert.True(upgradeMenu.activeSelf);
        }

        [UnityTest]
        public IEnumerator CanUpgradeToRedTower()
        {
            // Create the upgrade menu UI
            GameObject baseTower = GameObject.Find("BaseTower");
            EventRegistry.Invoke("showMenu", baseTower, typeof(UpgradeMenuUISystem));
            GameObject upgradeMenu = GameObject.Find("UIManager/UpgradeUI");
            UpgradeMenuUISystem component = upgradeMenu.GetComponent<UpgradeMenuUISystem>();

            // Create the red tower and store initial position for comparison
            Vector3 initial = baseTower.transform.position;
            component.CreateTower(ETowerType.Red);

            yield return new WaitForSeconds(0.5f);
            GameObject redTower = GameObject.Find("RedTower(Clone)");

            Assert.IsNotNull(redTower);
            Assert.AreEqual(initial, redTower.transform.position);
        }

        [UnityTest]
        public IEnumerator EscapeKeyMakesUIGoAway()
        {
            // Create the upgrade menu UI
            GameObject baseTower = GameObject.Find("BaseTower");
            EventRegistry.Invoke("showMenu", baseTower, typeof(UpgradeMenuUISystem));
            GameObject upgradeMenu = GameObject.Find("UIManager/UpgradeUI");

            // Hide active UI
            EventRegistry.Invoke("hideMenu");

            yield return null;
            Assert.False(upgradeMenu.activeSelf);
        }
    }
}
