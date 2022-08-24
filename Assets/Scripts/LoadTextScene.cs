using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadTextScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Loading");
        Input.compass.enabled = true;
        if (Input.location.isEnabledByUser)
        {
            StartCoroutine(GetLocation());
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public IEnumerator LoadScene(string sceneName)
    {

        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);

    }

    private IEnumerator GetLocation()
    {
        Debug.Log("Getting Location");
  
        Input.location.Start();
  
        while (Input.location.status == LocationServiceStatus.Initializing)
        {
            yield return new WaitForSeconds(0.5f);
        }

        Debug.Log("Got Location");
        StartCoroutine(LoadScene("Text"));

        yield break;
    }

}
