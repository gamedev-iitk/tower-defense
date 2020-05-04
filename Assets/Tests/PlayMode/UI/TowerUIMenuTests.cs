using System.Collections;
using UnityEngine.TestTools;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tests.UI
{
    /// <summary>
    /// Tests for the tower menu UI functions
    /// </summary>
    public class TowerUIMenuTests
    {
        [SetUp]
        public void Setup()
        {
            SceneManager.LoadScene("TestScene");
        }

        [UnityTest]
        public IEnumerator ClickingOnTowerBringsUpUI()
        {
            // Create the tower menu UI
            GameObject baseTower = GameObject.Find("BaseTower");
            EventRegistry.Invoke("showMenu", baseTower, typeof(TowerMenuUISystem));
            GameObject towerUI = GameObject.Find("TowerMenuUI");

            yield return null;
            Assert.True(towerUI.activeSelf);
        }

        [UnityTest]
        public IEnumerator StartTowerUpgradeFromTowerMenuUI()
        {
            // Create the tower menu UI
            GameObject baseTower = GameObject.Find("BaseTower");
            EventRegistry.Invoke("showMenu", baseTower, typeof(TowerMenuUISystem));
            GameObject towerMenu = GameObject.Find("TowerMenuUI");

            // Bring up the upgrade UI
            towerMenu.GetComponent<TowerMenuUISystem>().OnUpgradeClick();
            GameObject upgradeMenu = GameObject.Find("UpgradeUI");

            yield return null;
            Assert.True(upgradeMenu.activeSelf);
            Assert.False(towerMenu.activeSelf);
        }

        [UnityTest]
        public IEnumerator StartTowerMoveFromTowerUI()
        {
            // Create the tower menu UI
            GameObject baseTower = GameObject.Find("BaseTower");
            EventRegistry.Invoke("showMenu", baseTower, typeof(TowerMenuUISystem));
            GameObject towerMenu = GameObject.Find("TowerMenuUI");

            // Bring up the move tool
            towerMenu.GetComponent<TowerMenuUISystem>().OnMoveClick();
            GameObject indicator = GameObject.Find("PlacementIndicator(Clone)");
            bool check = indicator.GetComponent<TowerPlacer>().PlaceTower();

            yield return null;
            Assert.IsTrue(check);
            Assert.IsNotNull(GameObject.Find("BaseTower(Clone)"));
        }

        [UnityTest]
        public IEnumerator EscapeKeyMakesUIGoAway()
        {
            // Create the tower menu UI
            GameObject baseTower = GameObject.Find("BaseTower");
            EventRegistry.Invoke("showMenu", baseTower, typeof(TowerMenuUISystem));
            GameObject towerMenu = GameObject.Find("TowerMenuUI");

            // Hide active UI
            EventRegistry.Invoke("hideMenu");

            yield return new WaitForSeconds(0.5f);
            Assert.False(towerMenu.activeSelf);
        }
    }
}
