using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadUsers : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject UserRow;

    public GameObject emailinput;
    public GameObject userinput;
    public GameObject passinput;

    public GameObject AddSuccessDisplay;
    public GameObject DeleteSuccessDisplay;
    public GameObject ConnectionError;
    [Serializable]
    public class FullUser
    {
        public string id;
        public string username;
        public string email;
        public string role;
        public int totalPoints;

    }
    [Serializable]
    public class AddFullUser
    {
        public string email;
        public string username;
        public string password;
        public string role;
    }

    void Start()
    {
        try
        {
            GameObject deleteconfirmation = GameObject.Find("Canvas").transform.GetChild(4).gameObject;
            List<FullUser> entrylist = GetUsers();
            GameObject Content = GameObject.FindGameObjectWithTag("ScrollContent");
            float yposition = 0f;
            int userlen = entrylist.Count;
            Content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, userlen * 20f + 5f);
            for (int i = 0; i < userlen; i++)
            {
                GameObject row = Instantiate(UserRow, Content.transform, false);
                row.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText((i + 1).ToString());
                row.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().SetText(entrylist[i].username);
                row.transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(() => activateTab(deleteconfirmation, entrylist[i].id));
                row.transform.position += new Vector3(0f, yposition);
                yposition += -20f;
            }
        }
        catch{
            ConnectionError.SetActive(true);
        }
    }

    List<FullUser> GetUsers()
    {
        string url = "http://localhost:8080/api/users";
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd();
        List<FullUser> entryList = JsonConvert.DeserializeObject<List<FullUser>>(jsonResponse);
        return entryList;
    }

    void activateTab(GameObject deleteconfirmation, string id)
    {
        deleteconfirmation.SetActive(true);
        deleteconfirmation.transform.GetChild(0).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        deleteconfirmation.transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(() => DeleteUser(id));
    }

    void DeleteUser(string id)
    {
        StartCoroutine(PostDelete(id));
    }

    public void AddUser()
    {
        string email = emailinput.GetComponent<TMP_InputField>().text;
        string username = userinput.GetComponent<TMP_InputField>().text;
        string password = passinput.GetComponent<TMP_InputField>().text;
        if (username == "" || password == "" || email == "")
        {
            Debug.Log("Null value not accepted");
            return;
        }
        AddFullUser userinfo = new AddFullUser();
        userinfo.email = email;
        userinfo.username = username;
        userinfo.password = password;
        userinfo.role = "student";
        string UserJson = JsonUtility.ToJson(userinfo);
        StartCoroutine(PostAdd(UserJson));
    }

    IEnumerator PostDelete(string id)
    {
        string url = "http://localhost:8080/api/user/" + id;
        UnityWebRequest www = UnityWebRequest.Delete(url);
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            DeleteSuccessDisplay.SetActive(true);
        }
        else
        {
            DeleteSuccessDisplay.SetActive(true);
            DeleteSuccessDisplay.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("FAILED TO DELETE");
        }
    }

    IEnumerator PostAdd(string UserJson)
    {
        string url = "http://localhost:8080/api/signup";
        using (UnityWebRequest request = UnityWebRequest.Put(url, UserJson))
        {
            request.method = UnityWebRequest.kHttpVerbPOST;
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Accept", "application/json");

            request.timeout = 2;
            yield return request.SendWebRequest();
            if (request.responseCode == 200)
            {
                AddSuccessDisplay.SetActive(true);
            }
            else
            {
                AddSuccessDisplay.SetActive(true);
                AddSuccessDisplay.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("FAILED TO ADD");
            }



        }
    }
}