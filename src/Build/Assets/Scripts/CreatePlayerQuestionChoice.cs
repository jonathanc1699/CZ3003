using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreatePlayerQuestionChoice : MonoBehaviour
{
    public GameObject questionbuttoncontainer;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            Button qnbutton = questionbuttoncontainer.transform.GetChild(i).gameObject.GetComponent<Button>();
            qnbutton.onClick.RemoveAllListeners();
            int qnno = i + 1;
            qnbutton.onClick.AddListener(delegate { ChooseQuestion(qnno); });

        }
    }

    public void ChooseQuestion(int questionid)
    {

        GlobalInfo.Questionglobal = questionid;
        GlobalInfo.LevelID = int.Parse(questionid.ToString());

        SceneManager.LoadScene("PlayerEditQuestion");
    }
}
