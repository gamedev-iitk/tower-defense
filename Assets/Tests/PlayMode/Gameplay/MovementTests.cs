using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests.Gameplay
{
    /// <summary>
    /// Tests for player movement systems
    /// </summary>
    public class MovementTests
    {
        [SetUp]
        public void Setup()
        {
            SceneManager.LoadScene("TestScene");
        }

        [UnityTest]
        public IEnumerator AIMovePlayer()
        {
            GameObject player = GameObject.Find("Player");
            Vector3 initial = player.transform.position;
            player.GetComponent<AIController>().MoveTo(initial + new Vector3(-1, 0, -1));

            yield return new WaitForSeconds(1f);
            Assert.AreNotEqual(initial, player.transform.position);
        }
    }
}
