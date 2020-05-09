using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests.Gameplay
{
    public class DetectionTests
    {
        [SetUp]
        public void SetUp()
        {
            SceneManager.LoadScene("TestScene");
        }

        [UnityTest]
        public IEnumerator TowersDetectsEnemies()
        {
            GameObject tower = GameObject.FindWithTag("Tower");
            GameObject enemy = GameObject.FindWithTag("Enemy");
            float distance = tower.GetComponent<AbstractBattle>().Range;

            // Place the enmey within tower's range
            enemy.transform.position = tower.transform.position + new Vector3(distance / 2, 0, distance / 2);

            yield return new WaitForSeconds(0.5f);
            Assert.AreEqual(true, tower.GetComponent<Detection>().IsOccupied);
        }
    }
}
