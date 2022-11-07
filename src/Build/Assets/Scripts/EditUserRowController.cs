using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EditUserRowController : MonoBehaviour
{
    // Start is called before the first frame update
    
    
    void Start()
    {
        GameObject deleteconfirmation = GameObject.Find("Canvas").transform.GetChild(4).gameObject;
        GameObject[] objectlist = GameObject.FindGameObjectsWithTag("DeleteUser");
        foreach (GameObject buttongameobject in objectlist){
            buttongameobject.GetComponent<Button>().onClick.AddListener(() => activateTab(deleteconfirmation));
        }
    }

    // Update is called once per frame
    void activateTab(GameObject deleteconfirmation)
    {
        deleteconfirmation.SetActive(true);
    }
}
