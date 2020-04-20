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

public class NewTestScript

{
   

    [SetUp]
    public void Setup()
    {
        SceneManager.LoadScene("MainScene");
       
    }


//test 1--
    [UnityTest] 

    public IEnumerator ToCheckIfElseConditionsInTowerMenu()
    {
        GameObject tower = GameObject.Find("BaseTower");



        var towermenu = GameObject.Find("UIManager/TowerMenuUI");
       

        bool check = towermenu.GetComponent<TowerMenuUISystem>().Create(tower);

        yield return null;
        Assert.True(check);
    }  
//test 2--
    [UnityTest]
    public IEnumerator tocheckifplayerismoving()
    {
       
        GameObject player = GameObject.Find("Player");
        Vector3 initialpos = player.transform.position;
       
      
      
        player.GetComponent<AIController>().MoveTo(initialpos);
       
        yield return new  WaitForSeconds(0.1f);
        Assert.AreEqual(player.transform.position, initialpos);
        Vector3 destination =new  Vector3(-1.4f, 0.3f, -3.87f);
        player.GetComponent<AIController>().MoveTo(destination);
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(player.transform.position.z, destination.z);
        Assert.AreEqual(player.transform.position.x, destination.x);
         

    } 
    //test 3-- 
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
     //test 4--
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

