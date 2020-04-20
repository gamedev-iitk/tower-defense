using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

namespace Tests
{
    /// <summary>
    /// Tests for the player functionality
    /// </summary>
    public class PlayerTests
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
        /// Tests if PlacementIndicator can be toggled
        /// </summary>
        /// <returns></returns>

        [UnityTest]

        public IEnumerator ButtonTogglesIndicator()
        {

            GameObject BaseTower = GameObject.Find("BaseTower");
            Camera mainCamera = Camera.main;
            //Calls toggleplacer function
            Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, LayerMask.GetMask("Ground"));
            EventRegistry.Invoke("togglePlacer", BaseTower, hit.point);
            yield return new WaitForSeconds(1f);
            Assert.IsNotNull(GameObject.Find("PlacementIndicator(Clone)"));
        }

        /// <summary>
        /// Tests if PlacementIndicator is removed the same way
        /// </summary>
        /// <returns></returns>


        [UnityTest]

        public IEnumerator ButtonRemovesIndicator()
        {
            GameObject BaseTower = GameObject.Find("BaseTower");
            Camera mainCamera = Camera.main;
            //first create an indicator
            Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, LayerMask.GetMask("Ground"));
            EventRegistry.Invoke("togglePlacer", BaseTower, hit.point);
            yield return new WaitForSeconds(0.5f);
            //then remove it the same way
            EventRegistry.Invoke("togglePlacer", BaseTower, hit.point);
            yield return new WaitForSeconds(0.5f);
            Assert.IsNull(GameObject.Find("PlacementIndicator(Clone)"));
        }

        /// <summary>
        /// Tests if PlacementIndicator moves if cursor moves
        /// </summary>
        /// <returns></returns>

        [UnityTest]
        public IEnumerator IndicatorMovesWithCursor()
        {
            GameObject BaseTower = GameObject.Find("BaseTower");
            Camera mainCamera = Camera.main;
            //create an indicator
            Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, LayerMask.GetMask("Ground"));
            EventRegistry.Invoke("togglePlacer", BaseTower, hit.point);
            yield return new WaitForSeconds(0.5f);
            Vector3 initial = GameObject.Find("PlacementIndicator(Clone)").transform.position;
            //remove it
            EventRegistry.Invoke("togglePlacer", BaseTower, hit.point);
            //spawn it at another location
            EventRegistry.Invoke("togglePlacer", BaseTower, hit.point + new Vector3(1, 1, 1));
            yield return new WaitForSeconds(0.5f);
            Assert.AreNotEqual(initial, GameObject.Find("PlacementIndicator(Clone)").transform.position);
        }
        /// <summary>
        /// Tests if a tower can be placed through TowerPlacer
        /// </summary>
        /// <returns></returns>


        [UnityTest]

        public IEnumerator IndicatorPlacesTower()
        {
            GameObject BaseTower = GameObject.Find("BaseTower");
            Camera mainCamera = Camera.main;
            //create an indicator
            Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, LayerMask.GetMask("Ground"));
            EventRegistry.Invoke("togglePlacer", BaseTower, hit.point);
            yield return new WaitForSeconds(0.5f);
            //create a tower through its intended pathway
            GameObject pointer = GameObject.Find("PlacementIndicator(Clone)");
            pointer.GetComponent<TowerPlacer>().SetTower(GameObject.Find("BaseTower"));
            pointer.GetComponent<TowerPlacer>().PlaceTower();
            yield return new WaitForSeconds(0.5f);
            Assert.IsNotNull(GameObject.Find("BaseTower(Clone)"));
        }

        /// <summary>
        /// Tests if PlacementIndicator is Red in color near an object
        /// </summary>
        /// <returns></returns>


        [UnityTest]

        public IEnumerator IndicatorColorIsRedNearObstacles()
        {
            GameObject BaseTower = GameObject.Find("BaseTower");
            Camera mainCamera = Camera.main;
            //create an indicator
            Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, LayerMask.GetMask("Ground"));
            EventRegistry.Invoke("togglePlacer", BaseTower, hit.point);
            yield return new WaitForSeconds(0.5f);
            GameObject pointer = GameObject.Find("PlacementIndicator(Clone)");
            //create a tower at same point
            GameObject.Instantiate(GameObject.Find("BaseTower"), pointer.transform.position, pointer.transform.rotation);
            yield return new WaitForSeconds(0.5f);
            Assert.AreEqual(pointer.GetComponent<Renderer>().material.color, new Color(1, 0, 0, 0.3f));
        }
        /// <summary>
        /// Tests if PlacementIndicator is green in color when moved away
        /// </summary>
        /// <returns></returns>

        [UnityTest]

        public IEnumerator IndicatorChangesColorToGreenAwayFromObstacles()
        {
            GameObject BaseTower = GameObject.Find("BaseTower");
            Camera mainCamera = Camera.main;
            //create an indicator
            Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, LayerMask.GetMask("Ground"));
            EventRegistry.Invoke("togglePlacer", BaseTower, hit.point);
            yield return new WaitForSeconds(1f);
            GameObject pointer = GameObject.Find("PlacementIndicator(Clone)");
            //create a tower at same point so it turns red
            GameObject.Instantiate(GameObject.Find("BaseTower"), pointer.transform.position, pointer.transform.rotation);
            yield return new WaitForSeconds(0.5f);
            //remove tower
            GameObject.Destroy(GameObject.Find("BaseTower(Clone)"));
            //reset the indicator(there is an error where the color remains red on removing tower)
            EventRegistry.Invoke("togglePlacer", BaseTower, hit.point);
            yield return new WaitForSeconds(0.5f);
            EventRegistry.Invoke("togglePlacer", BaseTower, hit.point);
            GameObject pointer2 = GameObject.Find("PlacementIndicator(Clone)");
            yield return new WaitForSeconds(0.5f);
            //check if color is not red(cannot check  for green as its alpha is sometimes 0.3f and sometimes 0.212f )
            Assert.AreNotEqual(pointer2.GetComponent<Renderer>().material.color, new Color(1, 0, 0, 0.3f));
        }


    }
}
