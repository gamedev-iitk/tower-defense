using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tests
{
    public class TowerTests
    {
        [SetUp]
        public void SetUp()
        {
            SceneManager.LoadScene("MainScene");
        }

        [UnityTest]
        public IEnumerator CanDamageTowersAndChangeHealthBar()
        {
            Damageable damageable = GameObject.Find("BaseTower").GetComponent<Damageable>();
            float initialHealth = damageable.GetHealth();

            Image image = GameObject.Find("BaseTower/Canvas/HealthBG/ProgressBar").GetComponent<Image>();
            float initialFill = image.fillAmount;

            damageable.ApplyDamage(20);

            yield return null;
            Assert.AreEqual(initialHealth - 20, damageable.GetHealth());
            Assert.AreEqual(initialFill - 0.2f, image.fillAmount);
        }
        /// <summary>
        /// tests  if focusedTower is destroyed
        /// </summary>
        /// <returns></returns>
        [UnityTest]
        public IEnumerator CheckingDestroyMethodInTowerMenu()
        {
            GameObject tower = GameObject.Find("BaseTower");

            GameObject towermenu = GameObject.Find("TowerMenuUI");
            bool check = towermenu.GetComponent<TowerMenuUISystem>().Create(tower);
            yield return null;
            Assert.IsTrue(check);
            check = false;
            towermenu.GetComponent<TowerMenuUISystem>().Destroy();
            check = towermenu.GetComponent<TowerMenuUISystem>().Create(tower);
            yield return null;
            Assert.IsTrue(check);



        }
    }
}