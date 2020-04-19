using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;

public class EditModeSampleTest
{
    [Test]
    public void EditModeSampleTestSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    [UnityTest]
    public IEnumerator EditModeSampleTestWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
