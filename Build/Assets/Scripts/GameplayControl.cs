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
public class GameplayControl : MonoBehaviour
{
    string username = GlobalInfo.Usernameglobal;
    int userscore = GlobalInfo.Userscoreglobal;
    // Start is called before the first frame update
    public GameObject QuestionText;
    public GameObject AnswerButton1;
    public GameObject AnswerButton2;
    public GameObject AnswerButton3;
    public GameObject AnswerButton4;
    public GameObject CorrectAns;
    public GameObject WrongAns;
    public GameObject NextButton;

    [Serializable]
    public class Question
    {
        public int levelId;
        public string ans1;
        public string ans2;
        public string ans3;
        public string ans4;
        public int correctAnswer;
        public string question;
        public int points;
        public int worldId;
        
    }
    [Serializable]
    public class pointholder
    {
        public int totalPoints;
    }
    List<Question> questionlist;
    List<bool> correctanslist = new List<bool>();
    void Start()
    {
        if (GlobalInfo.Worldglobal > 4)
        {
            questionlist = GetQuestionsPlayer();
        }
        else
        {
            questionlist = GetQuestions();
        }
        GenerateQuestion();

    }

    List<Question> GetQuestions() {
        string url = "http://localhost:8080/api/levels/" + GlobalInfo.Worldglobal;
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd();
        List<Question> entryList = JsonConvert.DeserializeObject<List<Question>>(jsonResponse);
        return entryList;
    }
    List<Question> GetQuestionsPlayer()
    {
        string url = "http://localhost:8080/api/" + GlobalInfo.UserID2 + "/levels";
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd();
        List<Question> entryList = JsonConvert.DeserializeObject<List<Question>>(jsonResponse);
        return entryList;
    }

    public void GenerateQuestion()
    {
        // Change UI
        Debug.Log(correctanslist.Count);
        Debug.Log(questionlist.Count);
        Question qtogenerate = questionlist[correctanslist.Count];
        Debug.Log(qtogenerate.question);
        QuestionText.GetComponent<TextMeshProUGUI>().SetText(qtogenerate.question);
        AnswerButton1.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText(qtogenerate.ans1);
        AnswerButton2.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText(qtogenerate.ans2);
        AnswerButton3.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText(qtogenerate.ans3);
        AnswerButton4.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText(qtogenerate.ans4);

       

        AnswerButton1.GetComponent<Button>().onClick.RemoveAllListeners();
        AnswerButton2.GetComponent<Button>().onClick.RemoveAllListeners();
        AnswerButton3.GetComponent<Button>().onClick.RemoveAllListeners();
        AnswerButton4.GetComponent<Button>().onClick.RemoveAllListeners();
        switch (qtogenerate.correctAnswer)
        {
            case 1:
                AnswerButton1.GetComponent<Button>().onClick.AddListener(CorrectAnsEvent);
                AnswerButton2.GetComponent<Button>().onClick.AddListener(WrongAnsEvent);
                AnswerButton3.GetComponent<Button>().onClick.AddListener(WrongAnsEvent);
                AnswerButton4.GetComponent<Button>().onClick.AddListener(WrongAnsEvent);
                break;
            case 2:
                AnswerButton1.GetComponent<Button>().onClick.AddListener(WrongAnsEvent);
                AnswerButton2.GetComponent<Button>().onClick.AddListener(CorrectAnsEvent);
                AnswerButton3.GetComponent<Button>().onClick.AddListener(WrongAnsEvent);
                AnswerButton4.GetComponent<Button>().onClick.AddListener(WrongAnsEvent);
                break;
            case 3:
                AnswerButton1.GetComponent<Button>().onClick.AddListener(WrongAnsEvent);
                AnswerButton2.GetComponent<Button>().onClick.AddListener(WrongAnsEvent);
                AnswerButton3.GetComponent<Button>().onClick.AddListener(CorrectAnsEvent);
                AnswerButton4.GetComponent<Button>().onClick.AddListener(WrongAnsEvent);
                break;
            case 4:
                AnswerButton1.GetComponent<Button>().onClick.AddListener(WrongAnsEvent);
                AnswerButton2.GetComponent<Button>().onClick.AddListener(WrongAnsEvent);
                AnswerButton3.GetComponent<Button>().onClick.AddListener(WrongAnsEvent);
                AnswerButton4.GetComponent<Button>().onClick.AddListener(CorrectAnsEvent);
                break;
            default:
                AnswerButton1.GetComponent<Button>().onClick.AddListener(CorrectAnsEvent);
                AnswerButton2.GetComponent<Button>().onClick.AddListener(WrongAnsEvent);
                AnswerButton3.GetComponent<Button>().onClick.AddListener(WrongAnsEvent);
                AnswerButton4.GetComponent<Button>().onClick.AddListener(WrongAnsEvent);
                break;
        }

        //AnswerButton1.GetComponent<Button>().onClick.AddListener(WrongAnsEvent);
        //AnswerButton2.GetComponent<Button>().onClick.AddListener(WrongAnsEvent);
        //AnswerButton3.GetComponent<Button>().onClick.AddListener(WrongAnsEvent);
    }

    void WrongAnsEvent() {
        AnswerButton1.GetComponent<Button>().enabled = false;
        AnswerButton2.GetComponent<Button>().enabled = false;
        AnswerButton3.GetComponent<Button>().enabled = false;
        AnswerButton4.GetComponent<Button>().enabled = false;
        correctanslist.Add(false);
        if (correctanslist.Count >= questionlist.Count)
        {
            EndGame();
        }
        else
        {
            // Change UI
            
            WrongAns.SetActive(true);
            NextButton.SetActive(true);
            NextButton.GetComponent<Button>().onClick.AddListener(NextQuestion);
        }
    }

    void CorrectAnsEvent() {
        AnswerButton1.GetComponent<Button>().enabled = false;
        AnswerButton2.GetComponent<Button>().enabled = false;
        AnswerButton3.GetComponent<Button>().enabled = false;
        AnswerButton4.GetComponent<Button>().enabled = false;
        correctanslist.Add(true);
        if (correctanslist.Count >= questionlist.Count)
        {
            EndGame();
        }
        else
        {
            // Change UI
            CorrectAns.SetActive(true);
            NextButton.SetActive(true);
            NextButton.GetComponent<Button>().onClick.AddListener(NextQuestion);
        }
    }

    public void NextQuestion()
    {
        AnswerButton1.GetComponent<Button>().enabled = true;
        AnswerButton2.GetComponent<Button>().enabled = true;
        AnswerButton3.GetComponent<Button>().enabled = true;
        AnswerButton4.GetComponent<Button>().enabled = true;
        NextButton.GetComponent<Button>().onClick.RemoveAllListeners();
        WrongAns.SetActive(false);
        CorrectAns.SetActive(false);
        NextButton.SetActive(false);
        GenerateQuestion();
    }
    void EndGame()
    {
        for (int i = 0; i<correctanslist.Count; i++)
        {
            Question qtogenerate = questionlist[i];
            int qpoints = qtogenerate.points;
            bool d = correctanslist[i];
            userscore = EloRating(userscore, qpoints, d);
        }
        pointholder pointparam = new pointholder();
        pointparam.totalPoints = userscore;
        string pointJson = JsonUtility.ToJson(pointparam);
        GlobalInfo.Userscoreglobal = userscore;
        if (GlobalInfo.Worldglobal <= 4)
        {
            StartCoroutine(CalculatePlayerScore(pointJson));
        }
        else
        {
            NextButton.SetActive(true);
            NextButton.GetComponent<Button>().onClick.AddListener(returntoPreviousScene);
        }
        
    }
    // Function to calculate the Probability
    static float Probability(float rating1,
                                 float rating2)
    {
        return 1.0f * 1.0f / (1 + 1.0f *
               (float)(Math.Pow(10, 1.0f *
                 (rating1 - rating2) / 400)));
    }

    // Function to calculate Elo rating
    // K is a constant.
    // d determines whether Player A wins or
    // Player B.
    static int EloRating(int Ra, int Rb,
                                bool d)
    {
        if (Ra == 0)
        {
            if (d)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        int K = 3;

        // To calculate the Winning
        // Probability of Player A
        float Pa = Probability(Rb, Ra);

        // Case -1 When Player A wins
        // Updating the Elo Ratings
        if (d == true)
        {
            Ra = (int)(Ra + K * (1 - Pa));
        }

        // Case -2 When Player B wins
        // Updating the Elo Ratings
        else
        {
            Ra = (int)(Ra + K * (0 - Pa));
        }

        return Ra;
    }

    IEnumerator CalculatePlayerScore(string pointJson)
    {
        string url = "http://localhost:8080/api/user/points/" + GlobalInfo.UserID;
        using (UnityWebRequest request = UnityWebRequest.Put(url, pointJson))
        {
            request.method = UnityWebRequest.kHttpVerbPUT;
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Accept", "application/json");

            request.timeout = 2;
            yield return request.SendWebRequest();
            if (request.responseCode == 200)
            {
                Debug.Log("Data Send Success");
                NextButton.SetActive(true);
                NextButton.GetComponent<Button>().onClick.AddListener(returntoPreviousScene);
            }
            else
            {
                NextButton.SetActive(true);
                NextButton.GetComponent<Button>().onClick.AddListener(returntoPreviousScene);
            }
        }
    }

    void returntoPreviousScene() {
        SceneManager.LoadScene("WorldSelectionScene");
    }

}
