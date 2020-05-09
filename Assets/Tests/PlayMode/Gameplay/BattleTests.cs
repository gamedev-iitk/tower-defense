using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests.Gameplay
{

    // TODO: FIX
    public class BattleTests
    {
        [SetUp]
        public void SetUp()
        {
            SceneManager.LoadScene("TestScene");
        }

        [UnityTest]
        public IEnumerator TowersAttacksEnemies()
        {
            GameObject tower = GameObject.FindWithTag("Tower");
            GameObject enemy = GameObject.FindWithTag("Enemy");
            tower.GetComponent<AbstractBattle>().FireRate = 0.25f; // increase so that health changes fast
            tower.GetComponent<AbstractBattle>().OnDetect(new[] {enemy});

            float health = enemy.GetComponent<Damageable>().GetHealth();
            yield return new WaitForSeconds(0.5f);
            Assert.AreNotEqual(health, enemy.GetComponent<Damageable>().GetHealth());
        }

        [UnityTest]
        public IEnumerator TowerRevertsToIdleOnTargetLose()
        {
            GameObject tower = GameObject.FindWithTag("Tower");
            GameObject enemy = GameObject.FindWithTag("Enemy");

            // Destroy enemy fast
            // tower.GetComponent<AbstractBattle>().Attack = 1000;

            // Place the enemy
            float distance = tower.GetComponent<AbstractBattle>().Range;
            enemy.transform.position = tower.transform.position + new Vector3(distance / 2, 0, distance / 2);
            yield return new WaitForSeconds(1f);

            GameObject.Destroy(enemy);
            yield return new WaitForSeconds(1f);
            Assert.AreEqual(false, tower.GetComponent<Detection>().IsOccupied);
        }

        [UnityTest]
        public IEnumerator TowerStopsAttackingWhenEnemyIsOutOfRange()
        {
            GameObject tower = GameObject.FindWithTag("Tower");
            GameObject enemy = GameObject.FindWithTag("Enemy");
            float distance = tower.GetComponent<AbstractBattle>().Range;

            // Place the enemy within towers's range
            enemy.transform.position = tower.transform.position + new Vector3(distance / 2, 0, distance / 2);
            yield return new WaitForSeconds(0.5f);

            // Place it outside the range
            enemy.transform.position = tower.transform.position + new Vector3(distance, 0, distance);
            yield return new WaitForSeconds(0.5f);

            Assert.AreEqual(false, tower.GetComponent<Detection>().IsOccupied);
        }
    }
}