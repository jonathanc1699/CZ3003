using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class CreateQuestionController : MonoBehaviour
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
        public int levelId;
        public int worldId;
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
        public int levelId;
        public int worldId;
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
        questioninfo.worldId = GlobalInfo.Worldglobal;
        questioninfo.questionNumber = GlobalInfo.Questionglobal;

        questioninfo.ans1 = answerinput1.GetComponent<TMP_InputField>().text;
        questioninfo.ans2 = answerinput2.GetComponent<TMP_InputField>().text;
        questioninfo.ans3 = answerinput3.GetComponent<TMP_InputField>().text;
        questioninfo.ans4 = answerinput4.GetComponent<TMP_InputField>().text;
        questioninfo.question = questioninput.GetComponent<TMP_InputField>().text;
        if(!int.TryParse(correctAnsinput.GetComponent<TMP_InputField>().text, out int value)){
            errormsg.GetComponent<TextMeshProUGUI>().SetText("FILL IN with INT");
            return;
        }

        questioninfo.correctAnswer = int.Parse(correctAnsinput.GetComponent<TMP_InputField>().text);

        if (questioninfo.ans1 == ""|| questioninfo.ans2 == "" || questioninfo.ans3 == ""|| questioninfo.ans4 == ""|| questioninfo.question == ""|| questioninfo.correctAnswer > 4 || questioninfo.correctAnswer < 1)
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
            questioninfo.levelId = GlobalInfo.LevelID;
            string QuestionJson = JsonUtility.ToJson(questioninfo);
            StartCoroutine(PostNewQuestion(QuestionJson));
        }
    }

    Question GetQuestion() {
        try {
            string url = "http://localhost:8080/api/level/" + GlobalInfo.Worldglobal + "/" + GlobalInfo.Questionglobal;
            Debug.Log(url);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string jsonResponse = reader.ReadToEnd();
            Question entryList = JsonConvert.DeserializeObject<Question>(jsonResponse);
            return entryList;
        }
        catch
        {
            errormsg.GetComponent<TextMeshProUGUI>().SetText("CONNECTION FAILURE");
            return null;
        }
    }


    IEnumerator PostNewQuestion(string QuestionJson){
        string url = "http://localhost:8080/api/levels";
        using (UnityWebRequest request = UnityWebRequest.Put(url, QuestionJson))
        {
            request.method = UnityWebRequest.kHttpVerbPOST;
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Accept", "application/json");

            request.timeout = 2;
            yield return request.SendWebRequest();
            if (request.responseCode == 200)
            {
                SceneManager.LoadScene("AdminEditQuestionScene1");
            }
            else
            {
                errormsg.GetComponent<TextMeshProUGUI>().SetText("CONNECTION FAILURE");
            }
        }
    }

    IEnumerator PostEditQuestion(string QuestionJson)
    {
        string url = "http://localhost:8080/api/level/" + GlobalInfo.Worldglobal + " / " + GlobalInfo.Questionglobal;
        using (UnityWebRequest request = UnityWebRequest.Put(url, QuestionJson))
        {
            request.method = UnityWebRequest.kHttpVerbPUT;
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Accept", "application/json");

            request.timeout = 2;
            yield return request.SendWebRequest();
            if (request.responseCode == 200)
            {
                SceneManager.LoadScene("AdminEditQuestionScene1");
            }
            else
            {
                errormsg.GetComponent<TextMeshProUGUI>().SetText("CONNECTION FAILURE");
            }
        }
    }
}
