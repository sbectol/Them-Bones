using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class TextMover : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject textObject1;
    private GameObject textObject2;
    private GameObject textObject3;
    private GameObject tooth;
    private GameObject mainCamera;
    private CanvasGroup[] parts = new CanvasGroup[14];
    private Vector2[] coordsCB = new Vector2[13];
    private Transform endMarker1;
    private Transform startMarker1;
    private float speed;
    private float latitude;
    private float longitude;
    private int journeyProgress;
    void Start()
    {
        Input.compass.enabled = true;
        if (Input.location.isEnabledByUser)
        {
            StartCoroutine(GetLocation());
        }
        coordsCB[1] = new Vector2(53.291048960315216f, -3.7248343819867595f);  //Home
        coordsCB[2] = new Vector2(53.29135f, -3.71566f);  //Corner of Leisure Centre
        coordsCB[3] = new Vector2(53.29301f, -3.71410f);  //Stone Circle
        coordsCB[4] = new Vector2(53.29302f, -3.712158f); //Moel Eirias
        coordsCB[5]= new Vector2(53.29172f, -3.71059f); //Lego
        coordsCB[6] = new Vector2(53.28918f, -3.71349f);  //Lorry
        coordsCB[7] = new Vector2(53.29522328468637f, -3.716254282301825f);  //Porth Eirias Roof
        coordsCB[8] = new Vector2(53.29592243145316f, -3.7201209658271615f); //Crazy Seats

        //private Vector2 lionEiriasCoords = new Vector2(53.2968343881184f, -3.7229468536380628f);  //Pier
        coordsCB[9] = new Vector2(53.295990779479716f, -3.720529407297063f);  //Near Seats
        coordsCB[10] = new Vector2(53.297953888903734f, -3.727067838982684f);  //Colwyn Sign
        coordsCB[11] = new Vector2(53.29979923676437f, -3.7315745163052583f);  // The Toad
        coordsCB[12] = new Vector2(53.29641334528825f, -3.7318060268120283f);   //War Memorial
        textObject1 = GameObject.Find("Target1");
        textObject2= GameObject.Find("Target2");
        textObject3 = GameObject.Find("Target3");
        tooth = GameObject.Find("Target4");
        mainCamera = GameObject.Find("Main Camera");
        mainCamera.transform.LookAt(textObject1.GetComponent<Transform>());
       
        startMarker1 = mainCamera.GetComponent<Transform>();
        endMarker1 = startMarker1;
        speed = 25f;

        parts[1] = GameObject.Find("Part1").GetComponent<CanvasGroup>();
        parts[2] = GameObject.Find("Part2").GetComponent<CanvasGroup>();
        parts[3] = GameObject.Find("Part3").GetComponent<CanvasGroup>();
        parts[4] = GameObject.Find("Part4").GetComponent<CanvasGroup>();
        parts[5] = GameObject.Find("Part5").GetComponent<CanvasGroup>();
        parts[6] = GameObject.Find("Part6").GetComponent<CanvasGroup>();
        parts[7] = GameObject.Find("Part7").GetComponent<CanvasGroup>();
        parts[8] = GameObject.Find("Part8").GetComponent<CanvasGroup>();
        parts[9] = GameObject.Find("Part9").GetComponent<CanvasGroup>();
        parts[10] = GameObject.Find("Part10").GetComponent<CanvasGroup>();
        parts[11] = GameObject.Find("Part11").GetComponent<CanvasGroup>();
        parts[12] = GameObject.Find("Part12").GetComponent<CanvasGroup>();
        parts[13] = GameObject.Find("Part13").GetComponent<CanvasGroup>();

        

        for (int i = 1; i < 14; i++)
        {
            parts[i].alpha = 0;
        }
        journeyProgress = PlayerPrefs.GetInt("journeyProgress");

        StartCoroutine(MoveSomeText());
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
        //StartCoroutine(LoadScene("Twirly"));




    }
    public IEnumerator LoadScene(string sceneName)
    {

        yield return new WaitForSeconds(0f);
        SceneManager.LoadScene(sceneName);

    }
    void Update()
    {
        latitude = Input.location.lastData.latitude;
        longitude = Input.location.lastData.longitude;
        Debug.Log(Distance(latitude, longitude, coordsCB[1].x, coordsCB[1].y));

        mainCamera.transform.Rotate(Vector3.forward, 10.0f * Time.deltaTime);
        mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, endMarker1.position, speed * Time.deltaTime);
    }

    private IEnumerator GetLocation()
    {
        Input.location.Start();
        while (Input.location.status == LocationServiceStatus.Initializing)
        {
            yield return new WaitForSeconds(0.5f);
        }
        latitude = Input.location.lastData.latitude;
        longitude = Input.location.lastData.longitude;
        yield break;
    }
    private float Distance(float lat1, float lon1, float lat2, float lon2)
    {

        var R = 6378.137f; // Radius of earth in KM
        var dLat = lat2 * Mathf.PI / 180 - lat1 * Mathf.PI / 180;
        var dLon = lon2 * Mathf.PI / 180 - lon1 * Mathf.PI / 180;
        float a = Mathf.Sin(dLat / 2) * Mathf.Sin(dLat / 2) +
        Mathf.Cos(lat1 * Mathf.PI / 180) * Mathf.Cos(lat2 * Mathf.PI / 180) *
        Mathf.Sin(dLon / 2) * Mathf.Sin(dLon / 2);
        var c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));
        var d = R * c;
        d = d * 1000f; // meters
        return d;


    }
}
