using System.Collections;
using UnityEngine.TestTools;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tests.UI
{
    /// <summary>
    /// Tests for the wave indicator UI
    /// </summary>
    public class WaveUITests
    {
        [SetUp]
        public void Setup()
        {
            SceneManager.LoadScene("TestScene");
        }

        [UnityTest]

        public IEnumerator WaveUIChangesWhenWaveStarts()
        {
            Spawner spawner=GameObject.Find("GameManager").GetComponent<Spawner>();
            spawner.StartWave();
            yield return null;
            GameObject waveUi=GameObject.Find("UIManager/WaveTimerUI/StateName");
            yield return null;
            Assert.AreEqual("Wave "+GameState.WaveNumber,waveUi.GetComponent<Text>().text);
        }

        [UnityTest]

        public IEnumerator WaveUICanShowBonusWave()
        {
            Spawner spawner=GameObject.Find("GameManager").GetComponent<Spawner>();
            GameState.WaveNumber=spawner.bonusWave-1;
            spawner.StartWave();
            GameObject waveUi=GameObject.Find("UIManager/WaveTimerUI/StateName");
            yield return null;
            Assert.AreEqual("Bonus Wave",waveUi.GetComponent<Text>().text);
        }

        [UnityTest]

        public IEnumerator WaveUITimerChanges()
        {
            GameObject waveUi=GameObject.Find("UIManager/WaveTimerUI/Timer");
            string initial=waveUi.GetComponent<Text>().text;
            yield return new WaitForSeconds(1f);
            Assert.AreNotEqual(initial,waveUi.GetComponent<Text>().text);
        }
    }
}
