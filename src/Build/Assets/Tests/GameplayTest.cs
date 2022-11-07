using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class GameplayTest
{
    
    [UnityTest]
    public IEnumerator GameplayTestButtonTesting()
    {
        SceneManager.LoadScene("WorldSelectionScene");
        yield return null;
        GameObject.Find("WorldButton1").GetComponent<Button>().onClick.Invoke();
        yield return null;
        Assert.AreEqual(SceneManager.GetActiveScene().name, "GameplayScene");
    }

    [UnityTest]
    public IEnumerator GameplayTestSolution()
    {
        SceneManager.LoadScene("WorldSelectionScene");
        yield return null;
        GameObject.Find("WorldButton1").GetComponent<Button>().onClick.Invoke();
        yield return null;
        GameObject.Find("A1").GetComponent<Button>().onClick.Invoke();
        Assert.IsNotNull(GameObject.Find("NextQuestionButton"));
    }
}
