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

        /// <summary>
        /// Tests if a new tower menu UI can be created
        /// </summary>
        /// <returns></returns>
        [UnityTest]
        public IEnumerator CreateNewTowerMenuUI()
        {
            GameObject tower = GameObject.Find("BaseTower");
            GameObject towerMenu = GameObject.Find("UIManager/TowerMenuUI");
            bool check = towerMenu.GetComponent<TowerMenuUISystem>().Create(tower);

            yield return null;
            Assert.True(check);
        }

        /// <summary>
        /// Tests if clicking on a tower brings up UI
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Tests if pressing esc makes UI go away
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Tests if upgrade UI changes tower
        /// </summary>
        /// <returns></returns>
        [UnityTest]
        public IEnumerator UpgradeChangesTowerColor()
        {
            UpgradeMenuUISystem ui = GameObject.Find("UpgradeUI").GetComponent<UpgradeMenuUISystem>();
            ui.Create(GameObject.Find("BaseTower"));
            ui.OnClick("red");

            yield return new WaitForSeconds(1f);
            Assert.IsNotNull(GameObject.Find("RedTower(Clone)"));
        }
    }
}
