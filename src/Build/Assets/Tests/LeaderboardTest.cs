using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class LeaderboardTest
{
    
    [UnityTest]
    public IEnumerator LeaderboardTestLoad()
    {
        SceneManager.LoadScene("LeaderboardScene");
        yield return null;
        if (GameObject.Find("ConnectionPopup") != null)
        {
            GameObject.Find("ConnectionPopup").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.Invoke();
            yield return null;
            Assert.AreEqual("LoginScene", SceneManager.GetActiveScene().name);
            Assert.AreEqual(GameObject.FindGameObjectWithTag("ScrollContent").transform.childCount, 0);
        }
        else
        {
            yield return null;
            Assert.GreaterOrEqual(GameObject.FindGameObjectWithTag("ScrollContent").transform.childCount, 0);
        }
        
    }
}
