using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class QuestionChoiceTest
{
    
    [UnityTest]
    public IEnumerator QuestionChoiceTestActivity()
    {
        SceneManager.LoadScene("AdminEditQuestionScene1");
        yield return null;
        GameObject.Find("World 1").GetComponent<Button>().onClick.Invoke();
        GameObject Questionparent = GameObject.Find("QuestionParent");
        Assert.AreEqual(true, Questionparent.activeSelf);
    }

    [UnityTest]
    public IEnumerator QuestionChoiceTestSceneChange()
    {
        SceneManager.LoadScene("AdminEditQuestionScene1");
        yield return null;
        GameObject.Find("World 1").GetComponent<Button>().onClick.Invoke();
        GameObject Questionparent = GameObject.Find("QuestionParent");
        Assert.AreEqual(true, Questionparent.activeSelf);
        yield return null;
        Questionparent.transform.GetChild(0).gameObject.GetComponent<Button>().onClick.Invoke();
        
        yield return null;
        yield return null;
        Assert.AreEqual(1, GlobalInfo.Questionglobal);
        Assert.AreEqual(1, GlobalInfo.Worldglobal);
        Assert.AreEqual("AdminEditQuestionScene2", SceneManager.GetActiveScene().name);
    }
}
