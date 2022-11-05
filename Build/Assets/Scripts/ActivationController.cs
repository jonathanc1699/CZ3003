using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationController : MonoBehaviour
{
    public GameObject hiddengameObject;
    public void disableObject(GameObject objecttodisable)
    {
        objecttodisable.SetActive(false);
    }

    public void enableObject(GameObject objecttoenable)
    {
        objecttoenable.SetActive(true);
    }

    public void enablehidden()
    {
        hiddengameObject.SetActive(true);
    }
}
