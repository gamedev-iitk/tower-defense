using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tests
{
    public class DetectionTests
    {
        [SetUp]
        public void SetUp()
        {
            SceneManager.LoadScene("MainScene");
        }

         /// <summary>
        /// Tests if the tower can detect an enemy in Range
        /// </summary>
        /// <returns></returns>
        [UnityTest]
        public IEnumerator TowersDetectsEnemies()
        {
            GameObject tower = GameObject.FindWithTag("Tower");
            GameObject enemy = GameObject.FindWithTag("Enemy");
            float distance = tower.GetComponent<TowerBattle>().Range;
            //places the enmey within tower's range
            enemy.transform.position = tower.transform.position + new Vector3(distance / 2, 0, distance / 2);
            yield return new WaitForSeconds(0.5f);
            Assert.AreEqual(tower.GetComponent<Detection>().isOccupied, true);
        }

         /// <summary>
        /// Tests if the tower stops attcaking when enemy goes out of range
        /// </summary>
        /// <returns></returns>
        [UnityTest]

        public IEnumerator TowerStopAttackingWhenEnemyIsOutOfRange()
        {
            GameObject tower = GameObject.FindWithTag("Tower");
            GameObject enemy = GameObject.FindWithTag("Enemy");
            float distance = tower.GetComponent<TowerBattle>().Range;
            //place the enemy within towers's range
            enemy.transform.position = tower.transform.position + new Vector3(distance / 2, 0, distance / 2);
            yield return new WaitForSeconds(0.5f);
            //place it outside the range
            enemy.transform.position = tower.transform.position + new Vector3(distance, 0, distance);
            yield return null;
            Assert.AreEqual(tower.GetComponent<Detection>().isOccupied, false);
        }

         /// <summary>
        /// Tests if the tower stops attacking when tower is destroyed
        /// </summary>
        /// <returns></returns>
        [UnityTest]

        public IEnumerator TowersStartsRaycastingWhenEnemyIsDead()
        {
            GameObject tower = GameObject.FindWithTag("Tower");
            GameObject enemy = GameObject.FindWithTag("Enemy");
            float distance = tower.GetComponent<TowerBattle>().Range;
            //place the enemy
            enemy.transform.position = tower.transform.position + new Vector3(distance / 2, 0, distance / 2);
            yield return new WaitForSeconds(0.5f);
            //destroy it
            GameObject.Destroy(enemy);
            yield return null;
            Assert.AreEqual(tower.GetComponent<Detection>().isOccupied, false);
        }
    }
}