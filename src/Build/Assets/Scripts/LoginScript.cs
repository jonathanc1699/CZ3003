using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginScript : MonoBehaviour
{
   
    public GameObject userinput;
    public GameObject passinput;
    public GameObject errormsg;

    [Serializable]
    public class User
    {
        public string email;
        public string password;
    }
    [Serializable]
    public class FullUser
    {
        public string id;
        public string username;
        public string email;
        public string role;
        public int totalPoints;
        
    }
    public void Loginvalidation()
    {
        string username = userinput.GetComponent<TMP_InputField>().text;
        string password = passinput.GetComponent<TMP_InputField>().text;
        if (username == "" || password == "")
        {
            errormsg.GetComponent<TextMeshProUGUI>().SetText("PLEASE ENTER YOUR USERNAME/PASSWORD");
            return;
        }
        User userinfo = new User();
        userinfo.email = username;
        userinfo.password = password;
        string UserJson = JsonUtility.ToJson(userinfo);

        StartCoroutine(PostLogin(UserJson));
        
        
    }

    public IEnumerator PostLogin(string UserJson)
    {
        

        string url = "http://localhost:8080/api/login";
        using (UnityWebRequest request = UnityWebRequest.Put(url, UserJson))
        {
            request.method = UnityWebRequest.kHttpVerbPOST;
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Accept", "application/json");

            yield return request.SendWebRequest();
            if (request.responseCode == 200) {
                try
                {
                    string responsejson = request.downloadHandler.text;
                    FullUser fulluser = JsonUtility.FromJson<FullUser>(responsejson);
                    if (fulluser.role == "admin")
                    {
                        SceneManager.LoadScene("AdminScene");
                    }
                    else
                    {
                        GlobalInfo.Usernameglobal = fulluser.email;
                        GlobalInfo.Userscoreglobal = fulluser.totalPoints;
                        GlobalInfo.UserID = fulluser.id;
                        SceneManager.LoadScene("WorldSelectionScene");
                    }
                }
                catch
                {
                    errormsg.GetComponent<TextMeshProUGUI>().SetText("INVALID USERNAME/PASSWORD");
                }
            }
            else
            {
                errormsg.GetComponent<TextMeshProUGUI>().SetText("CONNECTION FAILURE");
            }
        }
    }
}
