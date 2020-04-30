using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests.Gameplay
{
    public class BattleTests
    {
        [SetUp]
        public void SetUp()
        {
            SceneManager.LoadScene("MainScene");
        }

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