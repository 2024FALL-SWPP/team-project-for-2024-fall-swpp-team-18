using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Utils
{
    public static void Swap<T>(ref T lhs, ref T rhs)
    {
        T temp;
        temp = lhs;
        lhs = rhs;
        rhs = temp;
    }
}

public class NewTestScript
{
    // A Test behaves as an ordinary method
    [Test]
    public void TestUtilsSwap()
    {
        // Use the Assert class to test conditions
        int a = 10,
            b = 20;
        int a_value = a,
            b_value = b;
        Utils.Swap<int>(ref a, ref b);

        Assert.AreEqual(a, b_value);
        Assert.AreEqual(b, a_value);
    }
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    /* [UnityTest]
    IEnumerator NewTestScriptWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    } */
}
