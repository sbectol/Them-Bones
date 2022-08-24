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
            PlayerPrefs.SetString("AnimationToPlay", "14");
            PlayerPrefs.SetInt("journeyProgress", 15);
            SceneManager.LoadScene("Twirly");
        } else
        {
            SceneManager.LoadScene(scene);
        }
        
        
    }
}
