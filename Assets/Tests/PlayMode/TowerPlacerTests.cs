using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    /// <summary>
    /// Tests for tower placement indicator
    /// </summary>
    public class TowerPlacerTests
    {
        [SetUp]
        public void Setup()
        {
            SceneManager.LoadScene("MainScene");
        }

        /// <summary>
        /// Tests if the placement indicator can be toggled
        /// </summary>
        /// <returns></returns>
        [UnityTest]
        public IEnumerator IndicatorCanToggle()
        {
            GameObject baseTower = GameObject.Find("BaseTower");
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, LayerMask.GetMask("Ground"));
            EventRegistry.Invoke("togglePlacer", baseTower, hit.point);

            yield return new WaitForSeconds(1f);
            Assert.IsNotNull(GameObject.Find("PlacementIndicator(Clone)"));

            // Call it again to remove the placer
            // This call doesn't use the parameters so nulls should work too
            EventRegistry.Invoke<GameObject, Vector3>("togglePlacer", null, Vector3.zero);
            yield return new WaitForSeconds(0.5f);
            Assert.IsNull(GameObject.Find("PlacementIndicator(Clone)"));
        }

        /// <summary>
        /// Tests if the placement indicator moves if cursor moves
        /// </summary>
        /// <returns></returns>
        [UnityTest]
        public IEnumerator IndicatorMovesWithCursor()
        {
            GameObject baseTower = GameObject.Find("BaseTower");
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, LayerMask.GetMask("Ground"));
            EventRegistry.Invoke("togglePlacer", baseTower, hit.point);
            yield return new WaitForSeconds(0.5f);

            Vector3 initial = GameObject.Find("PlacementIndicator(Clone)").transform.position;

            // TODO: Find a better way to move here. UI testing? Ideally I should be able to move the cursor
            // remove it
            EventRegistry.Invoke("togglePlacer", baseTower, hit.point);
            // spawn it at another location
            EventRegistry.Invoke("togglePlacer", baseTower, hit.point + new Vector3(1, 1, 1));

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
            GameObject baseTower = GameObject.Find("BaseTower");
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, LayerMask.GetMask("Ground"));
            EventRegistry.Invoke("togglePlacer", baseTower, hit.point);
            yield return new WaitForSeconds(0.5f);

            // Create a tower through its intended pathway
            TowerPlacer placer = GameObject.Find("PlacementIndicator(Clone)").GetComponent<TowerPlacer>();
            placer.SetTower(GameObject.Find("BaseTower"));
            placer.PlaceTower();

            yield return new WaitForSeconds(0.5f);
            Assert.IsNotNull(GameObject.Find("BaseTower(Clone)"));
        }

        /// <summary>
        /// Tests if the placement indicator turns red near an object
        /// </summary>
        /// <returns></returns>
        [UnityTest]
        public IEnumerator IndicatorColorIsRedNearObstacles()
        {
            GameObject baseTower = GameObject.Find("BaseTower");
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, LayerMask.GetMask("Ground"));
            EventRegistry.Invoke("togglePlacer", baseTower, hit.point);
            yield return new WaitForSeconds(0.5f);

            GameObject pointer = GameObject.Find("PlacementIndicator(Clone)");

            // Create a tower at same point to work as an obstacle
            GameObject.Instantiate(GameObject.Find("BaseTower"), pointer.transform.position, pointer.transform.rotation);

            yield return new WaitForSeconds(0.5f);
            Assert.AreEqual(new Color(1, 0, 0, 0.3f), pointer.GetComponent<Renderer>().material.color);
        }


        // TODO: the test below wouldn't work. Setting pointer.transform doesn't work because TowerPlacer.Update overrides it
        // with the mouse position. We need to mock that.

        // [UnityTest]
        // public IEnumerator IndicatorChangesColorToGreenAwayFromObstacles()
        // {
        //     GameObject baseTower = GameObject.Find("BaseTower");
        //     Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, LayerMask.GetMask("Ground"));
        //     EventRegistry.Invoke("togglePlacer", baseTower, hit.point);
        //     yield return new WaitForSeconds(1f);

        //     GameObject pointer = GameObject.Find("PlacementIndicator(Clone)");

        //     // Turn the placer red
        //     Vector3 currentPos = pointer.transform.position;
        //     GameObject.Instantiate(GameObject.Find("BaseTower"), currentPos, Quaternion.identity);
        //     yield return new WaitForSeconds(0.5f);

        //     // Move the placer away from the obstacle
        //     // pointer.transform.position = new Vector3(currentPos.x + 10f, currentPos.y, currentPos.z + 10f);

        //     yield return new WaitForSeconds(2f);

        //     Assert.AreNotEqual(new Color(1, 0, 0, 0.3f), pointer.GetComponent<Renderer>().material.color);
        // }
    }
}
