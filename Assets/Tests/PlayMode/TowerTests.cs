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
    }
}
