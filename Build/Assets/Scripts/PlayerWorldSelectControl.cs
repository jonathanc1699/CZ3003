using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerWorldSelectControl : MonoBehaviour
{
    public GameObject RowPrefab;
    public GameObject ErrorMsg;
    [Serializable]
    public class FullUser
    {
        public string id;
        public string username;
        public string email;
        public string role;
        public int totalPoints;

    }
    void Start()
    {
        try
        {
            List<FullUser> entrylist = GetUsers();
            entrylist.RemoveAll(x => x.role == "admin");
            GameObject Content = GameObject.FindGameObjectWithTag("ScrollContent");
            float yposition = 0f;
            int userlen = entrylist.Count;
            Content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, userlen * 20f + 5f);
            for (int i = 0; i < userlen; i++)
            {
                GameObject row = Instantiate(RowPrefab, Content.transform, false);
                int worldnum = i + 5;
                string userid2 = entrylist[i].id;
                row.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText(entrylist[i].username);
                row.transform.GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(() => SetWorld(userid2));
                row.transform.position += new Vector3(0f, yposition);
                yposition += -20f;
            }
        }
        catch
        {
            ErrorMsg.SetActive(true);
            return;
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


    public void SetWorld(string userid)
    {
        GlobalInfo.Worldglobal = 5;
        GlobalInfo.UserID2 = userid;
        SceneManager.LoadScene("GameplayScene");
    }

}
