using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldEditView : MonoBehaviour
{
    public GameObject errormsg;
    // Update is called once per frame
    public void CheckWorldExist()
    {
        try
        {
            string url = "http://localhost:8080/api/" + GlobalInfo.UserID + "/levels";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if((int)response.StatusCode == 200)
            {
                SceneManager.LoadScene("EditPlayerWorld");
            }
            else if((int)response.StatusCode == 400)
            {
                SceneManager.LoadScene("CreateNewWorldScene");
            }
            else
            {
                throw new Exception();
            }
            
        }
        catch
        {
            errormsg.SetActive(true);
        }
    }

}
