using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class MovementTests
{
    [SetUp]
    public void Setup()
    {
        SceneManager.LoadScene("MainScene");
    }

    [TearDown]
    public void Teardown() { }

    [UnityTest]
    public IEnumerator ControllerMovesPlayer()
    {
        GameObject player = GameObject.Find("Player");
        Vector3 initial = player.transform.position;
        player.GetComponent<AIController>().MoveTo(initial + new Vector3(-1, 0, -1));

        yield return new WaitForSeconds(2f);
        Assert.AreNotEqual(initial, player.transform.position);
    }
}

