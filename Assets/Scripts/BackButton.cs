using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public string scene;
    public void OnPointerDown(PointerEventData pointerEventData)
    {

       

    }

    //Detect if clicks are no longer registering
    public void OnPointerUp(PointerEventData pointerEventData)
    {


        if (PlayerPrefs.GetInt("journeyProgress") == 14)
        {
            PlayerPrefs.SetString("anmationToPlay", "14");
            SceneManager.LoadScene("Twirly");
        } else
        {
            SceneManager.LoadScene(scene);
        }
        
        
    }
}
