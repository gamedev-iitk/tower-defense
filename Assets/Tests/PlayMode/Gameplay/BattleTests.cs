using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests.Gameplay
{

    // TODO: FIX
    public class BattleTests : MonoBehaviour
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
            tower.GetComponent<AbstractBattle>().OnDetect(new[] { enemy });

            float health = enemy.GetComponent<Damageable>().GetHealth();
            yield return new WaitForSeconds(1.0f);
            Assert.AreNotEqual(health, enemy.GetComponent<Damageable>().GetHealth());
        }

        [UnityTest]
        public IEnumerator TowerAttacksNearestEnemy()
        {
            GameObject tower = GameObject.FindWithTag("Tower");
            GameObject enemy1 = GameObject.FindWithTag("Enemy");
            tower.GetComponent<AbstractBattle>().FireRate = 0.25f;

            float distance = tower.GetComponent<AbstractBattle>().Range;
            GameObject enemy2 = Instantiate(enemy1, tower.transform.position + new Vector3(distance / 3, 0, distance / 3), Quaternion.identity);

            // Both enemies should be in attacking range
            enemy1.transform.position = tower.transform.position + new Vector3(distance / 2, 0, distance / 2);

            GameObject[] targets = new GameObject[2];
            targets[0] = enemy1;
            targets[1] = enemy2;
            tower.GetComponent<AbstractBattle>().OnDetect(targets);

            float health1 = enemy1.GetComponent<Damageable>().GetHealth();
            float health2 = enemy2.GetComponent<Damageable>().GetHealth();

            yield return new WaitForSeconds(0.5f);
            Assert.AreNotEqual(health2, enemy2.GetComponent<Damageable>().GetHealth());
            Assert.AreEqual(health1, enemy1.GetComponent<Damageable>().GetHealth());
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