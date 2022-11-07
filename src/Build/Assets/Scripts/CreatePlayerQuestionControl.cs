using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;

public class CreatePlayerQuestionControl : MonoBehaviour
{
    public GameObject questioninput;
    public GameObject answerinput1;
    public GameObject answerinput2;
    public GameObject answerinput3;
    public GameObject answerinput4;
    public GameObject correctAnsinput;
    public GameObject errormsg;
    bool QuestionExists;
    [Serializable]
    public class QuestionBody
    {
        public string ans1;
        public string ans2;
        public string ans3;
        public string ans4;
        public int correctAnswer;
        public string question;
        public int points;
        public int questionNumber;
    }

    public class Question
    {
        public string ans1;
        public string ans2;
        public string ans3;
        public string ans4;
        public int correctAnswer;
        public string question;
        public int points;
        public int questionNumber;
    }
    int questionpts;

    public void Start()
    {
        Question question = GetQuestion();
        if (question is null)
        {
            QuestionExists = false;
            return;
        }
        else
        {
            QuestionExists = true;
        }
        answerinput1.GetComponent<TMP_InputField>().text = question.ans1;
        answerinput2.GetComponent<TMP_InputField>().text = question.ans2;
        answerinput3.GetComponent<TMP_InputField>().text = question.ans3;
        answerinput4.GetComponent<TMP_InputField>().text = question.ans4;
        questioninput.GetComponent<TMP_InputField>().text = question.question;
        correctAnsinput.GetComponent<TMP_InputField>().text = question.correctAnswer.ToString();
        questionpts = question.points;
    }
    public void UpdateQuestion()
    {
        QuestionBody questioninfo = new QuestionBody();
        questioninfo.ans1 = answerinput1.GetComponent<TMP_InputField>().text;
        questioninfo.ans2 = answerinput2.GetComponent<TMP_InputField>().text;
        questioninfo.ans3 = answerinput3.GetComponent<TMP_InputField>().text;
        questioninfo.ans4 = answerinput4.GetComponent<TMP_InputField>().text;
        questioninfo.question = questioninput.GetComponent<TMP_InputField>().text;
        questioninfo.questionNumber = GlobalInfo.Questionglobal;
        if (!int.TryParse(correctAnsinput.GetComponent<TMP_InputField>().text, out int value))
        {
            errormsg.GetComponent<TextMeshProUGUI>().SetText("FILL IN with INT");
            return;
        }

        questioninfo.correctAnswer = int.Parse(correctAnsinput.GetComponent<TMP_InputField>().text);

        if (questioninfo.ans1 == "" || questioninfo.ans2 == "" || questioninfo.ans3 == "" || questioninfo.ans4 == "" || questioninfo.question == "" || questioninfo.correctAnswer > 4 || questioninfo.correctAnswer < 1)
        {
            errormsg.GetComponent<TextMeshProUGUI>().SetText("FILL IN ALL FIELDS");
            return;
        }


        if (QuestionExists == true)
        {
            questioninfo.points = questionpts;
            string QuestionJson = JsonUtility.ToJson(questioninfo);
            StartCoroutine(PostEditQuestion(QuestionJson));
        }
        else
        {
            questioninfo.points = 10;
            string QuestionJson = JsonUtility.ToJson(questioninfo);
            StartCoroutine(PostNewQuestion(QuestionJson));
        }
    }

    Question GetQuestion()
    {
        try
        {
            string url = "http://localhost:8080/api/" + GlobalInfo.UserID + "/level/" + GlobalInfo.Questionglobal;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string jsonResponse = reader.ReadToEnd();
            Question entryList = JsonConvert.DeserializeObject<Question>(jsonResponse);
            return entryList;
            
        }
        catch (WebException e)
        {
            HttpWebResponse response = (HttpWebResponse)e.Response;
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string errorResponse = reader.ReadToEnd();
            if (errorResponse == "This question number does not exist for this world. Please create one.")
            {
                return null;
            }

            errormsg.GetComponent<TextMeshProUGUI>().SetText("CONNECTION FAILURE");
            return null;
        }
        catch
        {
            errormsg.GetComponent<TextMeshProUGUI>().SetText("CONNECTION FAILURE");
            return null;
        }
    }


    IEnumerator PostNewQuestion(string QuestionJson)
    {
        string url = "http://localhost:8080/api/" + GlobalInfo.UserID + "/level";
        using (UnityWebRequest request = UnityWebRequest.Put(url, QuestionJson))
        {
            request.method = UnityWebRequest.kHttpVerbPOST;
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Accept", "application/json");

            request.timeout = 2;
            yield return request.SendWebRequest();
            if (request.responseCode == 200)
            {
                SceneManager.LoadScene("EditPlayerWorld");
            }
            else
            {
                errormsg.GetComponent<TextMeshProUGUI>().SetText("CONNECTION FAILURE");
            }
        }
    }

    IEnumerator PostEditQuestion(string QuestionJson)
    {
        string url = "http://localhost:8080/api/" + GlobalInfo.UserID + "/level/ " + GlobalInfo.Questionglobal;
        Debug.Log(url);
        using (UnityWebRequest request = UnityWebRequest.Put(url, QuestionJson))
        {
            request.method = UnityWebRequest.kHttpVerbPUT;
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Accept", "application/json");

            request.timeout = 4;
            yield return request.SendWebRequest();
            if (request.responseCode == 200)
            {
                SceneManager.LoadScene("EditPlayerWorld");
            }
            else
            {
                errormsg.GetComponent<TextMeshProUGUI>().SetText("CONNECTION FAILURE");
            }
        }
    }
}
