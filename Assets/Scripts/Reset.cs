using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Reset : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    
    public void OnPointerDown(PointerEventData pointerEventData)
    {



    }

    //Detect if clicks are no longer registering
    public void OnPointerUp(PointerEventData pointerEventData)
    {


        PlayerPrefs.SetInt("journeyProgress", 0);
        PlayerPrefs.SetInt("Found1", 0);
        PlayerPrefs.SetInt("Found2", 0);
        PlayerPrefs.SetInt("Found3", 0);
        PlayerPrefs.SetInt("Found4", 0);
        PlayerPrefs.SetInt("Found5", 0);
        PlayerPrefs.SetInt("Found6", 0);
        PlayerPrefs.SetInt("Found7", 0);
        PlayerPrefs.SetInt("Found8", 0);
        PlayerPrefs.SetInt("Found9", 0);
        PlayerPrefs.SetInt("Found10", 0);
        PlayerPrefs.SetInt("Found11", 0);
        PlayerPrefs.SetInt("Found12", 0);
        PlayerPrefs.SetInt("Found13", 0);
        PlayerPrefs.SetInt("Found14", 0);
        Debug.Log("Reset");
        SceneManager.LoadScene("Text");
        


    }
}

