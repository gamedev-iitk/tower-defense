using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests.Gameplay
{
    /// <summary>
    /// Tests for the wave Spawner
    /// </summary>

    public class WaveTests
    {
        [SetUp]
        public void SetUp()
        {
            SceneManager.LoadScene("TestScene");
        }

        [UnityTest]

        public IEnumerator SpawnerSpawnsEnemies()
        {
            Spawner spawner=GameObject.Find("GameManager").GetComponent<Spawner>();
            spawner.StartWave(); //Starts Wave
            yield return new WaitForSeconds(spawner.spawnRate);
            Assert.IsNotNull(GameObject.Find("Enemy(Clone)"));
        }

        [UnityTest]

        public IEnumerator SpawnerSpawnsAtRegularIntervals()
        {
            Spawner spawner=GameObject.Find("GameManager").GetComponent<Spawner>();
            spawner.StartWave(); //Starts Wave
            yield return new WaitForSeconds(spawner.spawnRate);
            GameObject.Destroy(GameObject.Find("Enemy(Clone)"));  // destroy first spawned enemy
            yield return new WaitForSeconds(spawner.spawnRate);
            Assert.IsNotNull(GameObject.Find("Enemy(Clone)"));
        }

        [UnityTest]

        public IEnumerator SpawnerCanTriggerBonusWave()
        {
            Spawner spawner=GameObject.Find("GameManager").GetComponent<Spawner>();
            GameState.WaveNumber=spawner.bonusWave-1;
            spawner.StartWave();
            yield return new WaitForSeconds(spawner.spawnRate);
            Assert.IsNotNull(GameObject.Find("BonusEnemy(Clone)"));
        }
    }
}