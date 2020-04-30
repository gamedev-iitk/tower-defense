using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class DetectionTests
    {
        [SetUp]
        public void SetUp()
        {
            SceneManager.LoadScene("MainScene");
        }

        [UnityTest]
        public IEnumerator TowersDetectsEnemies()
        {
            GameObject tower = GameObject.FindWithTag("Tower");
            GameObject enemy = GameObject.FindWithTag("Enemy");
            float distance = tower.GetComponent<TowerBattle>().Range;

            // Place the enmey within tower's range
            enemy.transform.position = tower.transform.position + new Vector3(distance / 2, 0, distance / 2);

            yield return new WaitForSeconds(0.5f);
            Assert.AreEqual(true, tower.GetComponent<Detection>().isOccupied);
        }

        [UnityTest]
        public IEnumerator TowerStopsAttackingWhenEnemyIsOutOfRange()
        {
            GameObject tower = GameObject.FindWithTag("Tower");
            GameObject enemy = GameObject.FindWithTag("Enemy");
            float distance = tower.GetComponent<TowerBattle>().Range;

            // Place the enemy within towers's range
            enemy.transform.position = tower.transform.position + new Vector3(distance / 2, 0, distance / 2);
            yield return new WaitForSeconds(0.5f);

            // Place it outside the range
            enemy.transform.position = tower.transform.position + new Vector3(distance, 0, distance);
            yield return new WaitForSeconds(0.5f);

            Assert.AreEqual(false, tower.GetComponent<Detection>().isOccupied);
        }

        [UnityTest]
        public IEnumerator TowersStartsRaycastingWhenEnemyIsDead()
        {
            GameObject tower = GameObject.FindWithTag("Tower");
            GameObject enemy = GameObject.FindWithTag("Enemy");
            float distance = tower.GetComponent<TowerBattle>().Range;

            // Place the enemy
            enemy.transform.position = tower.transform.position + new Vector3(distance / 2, 0, distance / 2);
            yield return new WaitForSeconds(0.5f);
            // Destroy it
            GameObject.Destroy(enemy);

            yield return new WaitForSeconds(0.5f);
            Assert.AreEqual(false, tower.GetComponent<Detection>().isOccupied);
        }
    }
}