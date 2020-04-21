using System.Collections;
using UnityEngine.TestTools;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tests
{
    /// <summary>
    /// Tests for the tower menu UI functions
    /// </summary>
    public class TowerUIMenuTests
    {
        [SetUp]
        public void Setup()
        {
            SceneManager.LoadScene("MainScene");
        }

        [UnityTest]
        public IEnumerator CreateNewTowerMenuUI()
        {
            GameObject tower = GameObject.Find("BaseTower");
            GameObject towerMenu = GameObject.Find("UIManager/TowerMenuUI");
            bool check = towerMenu.GetComponent<TowerMenuUISystem>().Create(tower);

            yield return null;
            Assert.True(check);
        }

        [UnityTest]
        public IEnumerator ClickingOnTowerBringsUpUI()
        {
            UIManager uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
            GameObject towerUI = GameObject.Find("TowerMenuUI");
            GameObject baseTower = GameObject.Find("BaseTower");

            // Create the tower menu UI
            uiManager.ShowTowerMenu(baseTower);

            yield return new WaitForSeconds(0.5f);
            Assert.AreEqual(towerUI.GetComponent<CanvasGroup>().alpha, 1);
        }

        [UnityTest]
        public IEnumerator StartTowerUpgradeFromTowerMenuUI()
        {
            GameObject baseTower = GameObject.Find("BaseTower");
            GameObject towerMenu = GameObject.Find("TowerMenuUI");
            towerMenu.GetComponent<TowerMenuUISystem>().Create(baseTower);

            towerMenu.GetComponent<TowerMenuUISystem>().OnUpgradeClick();
            CanvasGroup upgradeMenuCanvasGroup = GameObject.Find("UpgradeUI").GetComponent<CanvasGroup>();

            yield return null;
            Assert.AreEqual(1, upgradeMenuCanvasGroup.alpha);
        }

        [UnityTest]
        public IEnumerator StartTowerMoveFromTowerUI()
        {
            GameObject baseTower = GameObject.Find("BaseTower");
            GameObject towerMenu = GameObject.Find("TowerMenuUI");
            towerMenu.GetComponent<TowerMenuUISystem>().Create(baseTower);

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
            UIManager uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
            GameObject towerui = GameObject.Find("TowerMenuUI");
            GameObject baseTower = GameObject.Find("BaseTower");

            // Bring up the UI
            uiManager.ShowTowerMenu(baseTower);

            // Hide all UIs
            uiManager.HideAll();

            yield return new WaitForSeconds(0.5f);
            Assert.AreEqual(towerui.GetComponent<CanvasGroup>().alpha, 0);
        }
    }
}
