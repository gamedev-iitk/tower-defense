using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    /// <summary>
    /// Tests for player movement systems
    /// </summary>
    public class MovementTests
    {
        [SetUp]
        public void Setup()
        {
            SceneManager.LoadScene("MainScene");
        }

        /// <summary>
        /// Tests if the player can move
        /// </summary>
        /// <returns></returns>
        [UnityTest]
        public IEnumerator AIMovePlayer()
        {
            GameObject player = GameObject.Find("Player");
            Vector3 initial = player.transform.position;
            player.GetComponent<AIController>().MoveTo(initial + new Vector3(-1, 0, -1));

            yield return new WaitForSeconds(1f);
            Assert.AreNotEqual(initial, player.transform.position);
        }
        /// <summary>
        /// checking calculations of aimovement
        /// </summary>
        /// <returns></returns>
        [UnityTest]
        public IEnumerator tocheckifplayerismoving()
        {

            GameObject player = GameObject.Find("Player");
            Vector3 initialpos = player.transform.position;



            player.GetComponent<AIController>().MoveTo(initialpos);

            yield return new WaitForSeconds(0.1f);
            Assert.AreEqual(player.transform.position, initialpos);
            Vector3 destination = new Vector3(-1.4f, 0.3f, -3.87f);
            player.GetComponent<AIController>().MoveTo(destination);
            yield return new WaitForSeconds(0.5f);
            Assert.AreEqual(player.transform.position.z, destination.z);
            Assert.AreEqual(player.transform.position.x, destination.x);


        }
    }
}
