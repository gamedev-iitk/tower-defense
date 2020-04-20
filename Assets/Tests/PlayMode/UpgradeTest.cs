using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using UnityEngine.TestTools;
using NUnit.Framework;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System.Reflection;
using System.CodeDom;
using System.Runtime.Versioning;
using System.Security.Permissions;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System;


namespace Tests
{
 public class UpgradeTest 

    {
        [SetUp]
        public void Setup()
        {
            SceneManager.LoadScene("MainScene");
        }
        /// <summary>
        /// Tests if new coloured tower formed at same position as that of reference tower 
        /// </summary>
        /// <returns></returns> 
        /// 
        //test 1
        [UnityTest]
        public IEnumerator IfNewTowerisFormingAtTheSamePosition()
        {
            GameObject tower = GameObject.Find("BaseTower");

            Vector3 towerpos = tower.transform.position;
            GameObject upgradeui = GameObject.Find("UpgradeUI");
            upgradeui.GetComponent<UpgradeMenuUISystem>().Create(tower);
            upgradeui.GetComponent<UpgradeMenuUISystem>().OnClick("red");



            GameObject redtower = GameObject.Find("RedTower(Clone)");
            Vector3 rpos = redtower.transform.position;
            yield return null;
            Assert.AreEqual(rpos, towerpos);
        }
        /// <summary>
        /// tests for tower placement at different position when move button is clicked
        /// </summary>
        /// <returns></returns>
        /// 

         //test2


        [UnityTest]
        public IEnumerator CheckingIfTowerPlacementOccursOnFunctionCall()
        {
            GameObject tower = GameObject.Find("BaseTower");

            GameObject towermenu = GameObject.Find("TowerMenuUI");
            towermenu.GetComponent<TowerMenuUISystem>().Create(tower);
            towermenu.GetComponent<TowerMenuUISystem>().OnMoveClick();
            GameObject placementind = GameObject.Find("PlacementIndicator(Clone)");
            placementind.GetComponent<TowerPlacer>().SetTower(tower);
            bool check = placementind.GetComponent<TowerPlacer>().PlaceTower();
            yield return null;
            Assert.IsTrue(check);

        }


    }





}