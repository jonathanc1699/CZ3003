using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreateQuestionChoice : MonoBehaviour
{
    public GameObject questionbuttoncontainer;
    public void ChooseWorld(int worldid)
    {
        questionbuttoncontainer.SetActive(true);
        for (int i = 0; i < 10; i++)
        {
            Button qnbutton = questionbuttoncontainer.transform.GetChild(i).gameObject.GetComponent<Button>();
            Debug.Log(qnbutton.transform.position);
            qnbutton.onClick.RemoveAllListeners();
            int world = worldid;
            int qnno = i + 1;
            qnbutton.onClick.AddListener(delegate { ChooseQuestion(world, qnno); });
          
        }
    }

    public void ChooseQuestion(int worldid, int questionid)
    {
        
        GlobalInfo.Worldglobal = worldid;
        GlobalInfo.Questionglobal = questionid;
        GlobalInfo.LevelID = int.Parse((worldid - 1).ToString() + questionid.ToString());
        
        SceneManager.LoadScene("AdminEditQuestionScene2");
    }
}