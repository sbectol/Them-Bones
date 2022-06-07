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
        StartCoroutine(LoadScene("Text"));
    }

    // Update is called once per frame
    void Update()
    {

    }
    public IEnumerator LoadScene(string sceneName)
    {

        yield return new WaitForSeconds(30f);
        SceneManager.LoadScene(sceneName);

    }
}
