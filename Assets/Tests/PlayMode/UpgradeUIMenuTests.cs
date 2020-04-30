using System.Collections;
using UnityEngine.TestTools;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tests
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
            GameObject tower = GameObject.Find("BaseTower");
            GameObject towerMenu = GameObject.Find("UIManager/UpgradeUI");
            towerMenu.GetComponent<UpgradeMenuUISystem>().Show(tower);

            yield return null;
            Assert.True(towerMenu.activeSelf);
        }

        [UnityTest]
        public IEnumerator CanUpgradeToRedTower()
        {
            UpgradeMenuUISystem ui = GameObject.Find("UpgradeUI").GetComponent<UpgradeMenuUISystem>();
            GameObject baseTower = GameObject.Find("BaseTower");
            Vector3 initial = baseTower.transform.position;
            ui.Show(baseTower);
            ui.OnClick("red");

            yield return new WaitForSeconds(1f);
            GameObject redTower = GameObject.Find("RedTower(Clone)");

            // Check if a new tower was created
            Assert.IsNotNull(redTower);

            // Check if it was created at the same location
            Assert.AreEqual(initial, redTower.transform.position);
        }

        [UnityTest]
        public IEnumerator EscapeKeyMakesUIGoAway()
        {
            GameObject towerui = GameObject.Find("TowerMenuUI");
            GameObject baseTower = GameObject.Find("BaseTower");

            // Bring up the UI
            EventRegistry.Invoke("showMenu", baseTower, typeof(UpgradeMenuUISystem));

            // Hide all UIs
            EventRegistry.Invoke("hideMenu");

            yield return new WaitForSeconds(0.5f);
            Assert.AreEqual(towerui.GetComponent<CanvasGroup>().alpha, 0);
        }
    }
}
