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
            bool enemyFound = false;
            Spawner spawner = GameObject.Find("GameManager").GetComponent<Spawner>();
            spawner.StartWave(); //Starts Wave
            yield return new WaitForSeconds(spawner.spawnRate + 1);
            if (GameObject.Find("Runner(Clone)") != null || GameObject.Find("Berzerker(Clone)") != null)
            {
                enemyFound = true;
            }
            Assert.AreEqual(true, enemyFound);
        }

        [UnityTest]

        public IEnumerator SpawnerSpawnsAtRegularIntervals()
        {
            bool enemyFound = false;
            GameObject firstEnemy;
            Spawner spawner = GameObject.Find("GameManager").GetComponent<Spawner>();
            spawner.StartWave(); //Starts Wave
            yield return new WaitForSeconds(spawner.spawnRate);
            firstEnemy = GameObject.Find("Runner(Clone)");
            if (firstEnemy == null)
            {
                firstEnemy = GameObject.Find("Berzerker(Clone)");
            }
            GameObject.Destroy(firstEnemy);  // destroy first spawned enemy
            yield return new WaitForSeconds(spawner.spawnRate);
            if (GameObject.Find("Runner(Clone)") != null || GameObject.Find("Berzerker(Clone)") != null)
            {
                enemyFound = true;
            }
            Assert.AreEqual(true, enemyFound);
        }

        [UnityTest]

        public IEnumerator SpawnerCanTriggerBonusWave()
        {
            bool enemyFound = false;
            Spawner spawner = GameObject.Find("GameManager").GetComponent<Spawner>();
            GameState.WaveNumber = spawner.bonusWave - 1;
            spawner.StartWave();
            yield return new WaitForSeconds(spawner.spawnRate);
            Assert.IsNotNull(GameObject.Find("Runner(Clone)"));
        }
    }
}