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
        /// Tests if a new tower can be created
        /// </summary>
        /// <returns></returns>
        [UnityTest]
        public IEnumerator CreateNewTower()
        {
            GameObject tower = GameObject.Find("BaseTower");
            GameObject towermenu = GameObject.Find("UIManager/TowerMenuUI");
            bool check = towermenu.GetComponent<TowerMenuUISystem>().Create(tower);

            // Advance one frame
            yield return null;

            // Check condition
            Assert.True(check);
        }

           /// <summary>
        /// Tests if clicking on a tower brings up UI
        /// </summary>
        /// <returns></returns>

        [UnityTest]

        public IEnumerator ClickingOnTowerBringsUpUI()
        {
            UIManager  uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
            Camera mainCamera=Camera.main;
            GameObject towerui=GameObject.Find("TowerMenuUI");
            GameObject BaseTower=GameObject.Find("BaseTower");
            //create an indicator
            Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, LayerMask.GetMask("Ground"));
            EventRegistry.Invoke("togglePlacer", BaseTower, hit.point);
            yield return new WaitForSeconds(0.5f);
            GameObject pointer=GameObject.Find("PlacementIndicator(Clone)");
            //create tower and remove indicator
            GameObject.Instantiate(GameObject.Find("BaseTower"),pointer.transform.position,pointer.transform.rotation);
            GameObject.Destroy(pointer);
            yield return new WaitForSeconds(0.5f);
            //create ui through its intended pathway
            if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit2))
            {
                GameObject hitObject = hit2.transform.parent.gameObject;
                if (hitObject.CompareTag("Tower"))
                {
                    uiManager.ShowTowerMenu(hitObject);
                    yield return new WaitForSeconds(0.5f);
                }
            }
            Assert.AreEqual(towerui.GetComponent<CanvasGroup>().alpha,1);
        }
          /// <summary>
        /// Tests if pressing esc makes UI go away
        /// </summary>
        /// <returns></returns>

        [UnityTest]

        public IEnumerator EscapeKeyMakesUIGoAway()
        {
            UIManager  uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
            GameObject towerui=GameObject.Find("TowerMenuUI");
             GameObject hitObject = GameObject.Find("BaseTower");
             //show ui then hide it
            if (hitObject.CompareTag("Tower"))
            {
              uiManager.ShowTowerMenu(hitObject);
              yield return new WaitForSeconds(0.5f);
            }
            uiManager.HideAll();
            yield return new WaitForSeconds(0.5f);
            Assert.AreEqual(towerui.GetComponent<CanvasGroup>().alpha,0);
        }
          /// <summary>
        /// Tests if upgrade UI changes tower
        /// </summary>
        /// <returns></returns>

        [UnityTest]

        public IEnumerator UpgradeChangesTowerColor()
        {
            UpgradeMenuUISystem ui=GameObject.Find("UpgradeUI").GetComponent<UpgradeMenuUISystem>();
            ui.Create(GameObject.Find("BaseTower"));
            ui.OnClick("red");
            yield return new WaitForSeconds(1f);
            Assert.IsNotNull(GameObject.Find("RedTower(Clone)"));
        }
    }
}
