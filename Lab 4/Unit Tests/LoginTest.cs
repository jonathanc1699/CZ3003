using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginTest
{
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator LoginTestWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        SceneManager.LoadScene("LoginScene");
        yield return null;
        GameObject.Find("UsernameInput").GetComponent<TMP_InputField>().text = "";
        GameObject.Find("LoginButton").GetComponent<Button>().onClick.Invoke();
        Assert.AreEqual("PLEASE ENTER YOUR USERNAME/PASSWORD", GameObject.Find("ErrorText").GetComponent<TextMeshProUGUI>().text);
    }
}
