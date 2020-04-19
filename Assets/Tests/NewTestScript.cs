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

public class NewTestScript
{

    [SetUp]
    public void Setup()
    {
        SceneManager.LoadScene("MainScene");
    }



    [UnityTest]

    public IEnumerator Test()
    {


        GameObject tower = GameObject.Find("BaseTower");
        var towermenu = GameObject.Find("UIManager/TowerMenuUI");
        var upgradeui = GameObject.Find("UIManager/UpgradeUI");

        bool check = towermenu.GetComponent<TowerMenuUISystem>().Create(tower);

        yield return null;
        Assert.True(check);









    }


}

