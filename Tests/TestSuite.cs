using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
    public class TestSuite
    {
        
        [SetUp]
        public void Setup()
        {
           
           SceneManager.LoadScene("MainScene");
        }
        [TearDown]

        public void Teardown()
        {
        }

        [UnityTest]
        public IEnumerator ControllerMovesPlayer()
        {
            GameObject X=GameObject.Find("Player");
            Vector3 initial=X.transform.position;
            X.GetComponent<AIController>().MoveTo(initial+new Vector3(-1,0,-1));
           
            yield return new WaitForSeconds(2f);
            Assert.AreNotEqual(initial,X.transform.position);
        }
      
        
       
    }

