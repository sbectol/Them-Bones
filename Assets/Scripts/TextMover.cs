using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TextMover : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject textObject1;
    private GameObject textObject2;
    private GameObject textObject3;
    private GameObject tooth;
    private GameObject mainCamera;
    private Transform endMarker1;
    private Transform startMarker1;
    private float speed;
    void Start()
    {
        textObject1 = GameObject.Find("Target1");
        textObject2= GameObject.Find("Target2");
        textObject3 = GameObject.Find("Target3");
        tooth = GameObject.Find("Target4");
        mainCamera = GameObject.Find("Main Camera");
        mainCamera.transform.LookAt(textObject1.GetComponent<Transform>());
       
        startMarker1 = mainCamera.GetComponent<Transform>();
        endMarker1 = startMarker1;
        speed = 25f;

        StartCoroutine("MoveSomeText");
    }

    IEnumerator MoveSomeText()

        
    {
        yield return new WaitForSeconds(2);
        endMarker1 = textObject1.GetComponent<Transform>();
        yield return new WaitForSeconds(5);
        endMarker1 = textObject2.GetComponent<Transform>();
        yield return new WaitForSeconds(5);
        endMarker1 = textObject3.GetComponent<Transform>();
        yield return new WaitForSeconds(5);

        endMarker1 = tooth.GetComponent<Transform>();
        yield return new WaitForSeconds(5);
        StartCoroutine(LoadScene("Twrily"));




    }
    public IEnumerator LoadScene(string sceneName)
    {

        yield return new WaitForSeconds(0f);
        SceneManager.LoadScene(sceneName);

    }
    void Update()
    {
        mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, endMarker1.position, speed * Time.deltaTime);
    }
}
