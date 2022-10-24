using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class GameplayIntegrationTest
{
    [UnityTest]
    public IEnumerator GameplayIntegrationTestWithEnumeratorPasses()
    {
        SceneManager.LoadScene("LoginScene");
        yield return null;
        GameObject.Find("UsernameInput").GetComponent<TMP_InputField>().text = "usertwo@test.com";
        GameObject.Find("PasswordInput").GetComponent<TMP_InputField>().text = "123456";
        GameObject.Find("LoginButton").GetComponent<Button>().onClick.Invoke();
        yield return null;
        yield return null;
        Assert.AreEqual(SceneManager.GetActiveScene().name, "WorldSelectionScene");
        yield return null;
        yield return null;
        yield return null;
        GameObject.Find("WorldButton1").GetComponent<Button>().onClick.Invoke();
        yield return null;
        yield return null;
        yield return null;
        Assert.AreEqual(SceneManager.GetActiveScene().name, "GameplayScene");
        while (GameObject.Find("A1") != null)
        {
            GameObject.Find("A1").GetComponent<Button>().onClick.Invoke();
            yield return null;
            yield return null;
            GameObject.Find("NextQuestionButton").GetComponent<Button>().onClick.Invoke();
            yield return null;
            yield return null;
        }
        Assert.AreEqual(SceneManager.GetActiveScene().name, "WorldSelectionScene");
    }
}
