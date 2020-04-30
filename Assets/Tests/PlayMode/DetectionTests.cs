using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        public IEnumerator TowersDetecEnemies()
        {
            yield return null;
        }
    }
}