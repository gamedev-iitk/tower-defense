using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tests
{
    public class BattleTests
    {
        [SetUp]
        public void SetUp()
        {
            SceneManager.LoadScene("MainScene");
        }

         /// <summary>
        /// Tests if the tower and enemy are in combat when detected
        /// </summary>
        /// <returns></returns>
        [UnityTest]
        public IEnumerator TowersAttacksEnemies()
        {
            GameObject tower = GameObject.FindWithTag("Tower");
            GameObject enemy = GameObject.FindWithTag("Enemy");
            tower.GetComponent<TowerBattle>().OnDetect(enemy);
            tower.GetComponent<TowerBattle>().FireRate = 0.25f; // increase so that health changes fast
            float health = enemy.GetComponent<Damageable>().GetHealth();
            yield return new WaitForSeconds(0.5f);
            Assert.AreNotEqual(health, enemy.GetComponent<Damageable>().GetHealth());
        }
    }
}