using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSelectorController : MonoBehaviour
{
    public int WorldNum;
    public void SetWorld()
    {
        GlobalInfo.Worldglobal = WorldNum;
        SceneManager.LoadScene("GameplayScene");
    }


}
