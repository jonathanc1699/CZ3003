using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class CreateNewWorldControl : MonoBehaviour
{
    public GameObject NameInput;
    public GameObject DescriptionInput;
    public GameObject errormsg;

    [Serializable]
    public class WorldParams
    {
        public string name;
        public string description;
    }

    public void CreateWorld()
    {
        WorldParams worldparameters = new WorldParams();
        worldparameters.name = NameInput.GetComponent<TMP_InputField>().text;
        worldparameters.description = DescriptionInput.GetComponent<TMP_InputField>().text;
        
        if (worldparameters.name == "" || worldparameters.description == "")
        {
            errormsg.GetComponent<TextMeshProUGUI>().SetText("PLEASE FILL IN ALL FIELDS");
            return;
        }

        string worldJson = JsonUtility.ToJson(worldparameters);
        StartCoroutine(PostWorld(worldJson));
    }

    public IEnumerator PostWorld(string worldJson)
    {


        string url = "http://localhost:8080/"+ GlobalInfo.UserID +"/world";
        using (UnityWebRequest request = UnityWebRequest.Put(url, worldJson))
        {
            request.method = UnityWebRequest.kHttpVerbPOST;
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Accept", "application/json");

            request.timeout = 2;
            yield return request.SendWebRequest();
            if (request.responseCode == 200)
            {
                SceneManager.LoadScene("CreateUserQuestions");
            }
            else
            {
                errormsg.GetComponent<TextMeshProUGUI>().SetText("CONNECTION FAILURE");
            }
        }
    }
}
