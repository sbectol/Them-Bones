using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using SpriteGlow;

public class TextMover : MonoBehaviour
{
    // Start is called before the first frame update
    private BoxCollider slideBoxCollider;
    private GameObject frame;
    private GameObject mainCamera;
    private CanvasGroup fader;
    private CanvasGroup catBluePrint;
    private CanvasGroup[] parts = new CanvasGroup[15];
    private SpriteRenderer[] slide = new SpriteRenderer[15];
    private int slideIndex = 1;
    private string[] slideAnimations = new string[16];
    private Vector2[] coordsCB = new Vector2[15];
    private Vector2[] coordsCresswell = new Vector2[15];
    private Vector2[] coordsSean = new Vector2[15];
    private Transform endMarker1;
    private Transform startMarker1;
    private TMP_Text debugText;
    private TMP_Text messageText;
    private float speed;
    private float latitude;
    private float longitude;
    private int journeyProgress;
    private int closest;
    public float minDistance = 1000;
    //private SpriteGlowEffect skull;
    private SpriteRenderer boxContents;
    private SpriteRenderer skull;
    private bool inRange = false;
    private bool SlideHolderZoomed = false;
  


    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;
    private bool fingerUp;
    private bool stationary;
    private bool fingerStationary;

    private bool checkingLocation = false;
    private Animator cameraAnimator;
    private Animator boxAnimator;
    private Animator slideHolder;
    private int frameCounter;

    public Sprite[] spriteArray;
    private int closestisFound = 0;

   

    void Start()
    {
        //Input.compass.enabled = true;
        //if (Input.location.isEnabledByUser)
        //{
        //    StartCoroutine(GetLocation());
        //}
        checkingLocation = true;
        slideBoxCollider = GameObject.Find("slide disc").GetComponent<BoxCollider>();
        boxContents = GameObject.Find("boxContents").GetComponent<SpriteRenderer>();
        skull = GameObject.Find("frame").GetComponent<SpriteRenderer>();
        fader = GameObject.Find("Fader").GetComponent<CanvasGroup>();
        
        boxContents.sprite = spriteArray[5];
        coordsCresswell[1] = new Vector2(53.26290105395153f, -1.1966601837793833f);
        coordsCresswell[2] = new Vector2(53.26245825170634f, -1.1971322525337256f);
        coordsCresswell[3] = new Vector2(53.26207320254887f, -1.198569916467404f);
        coordsCresswell[4] = new Vector2(53.26188709421333f, -1.1991063582336894f);
        coordsCresswell[5] = new Vector2(53.261797248520075f, -1.1999432073891145f);
        coordsCresswell[6] = new Vector2(53.261573642314296f, -1.2006772584858334f);
        coordsCresswell[7] = new Vector2(53.26136833495697f, -1.2017225470920287f);
        coordsCresswell[8] = new Vector2(53.261178580310904f, -1.2026534260000337f);
        coordsCresswell[9] = new Vector2(53.261502095922864f, -1.2021281814765226f);
        coordsCresswell[10] = new Vector2(53.262047532481795f, -1.200629852849976f); 
        coordsCresswell[11] = new Vector2(53.26227043569321f, -1.1993199434300275f);
        coordsCresswell[12] = new Vector2(53.26231087424608f, -1.1988155006698238f);
        coordsCresswell[13] = new Vector2(53.26277747016736f, -1.1981186415990268f);
        coordsCresswell[14] = new Vector2(53.26310719488181f, -1.1972345666584634f);

        coordsSean[1] = new Vector2(52.824895f, -3.406880f);
        coordsSean[2] = new Vector2(52.822625f, -3.401272f);
        coordsSean[3] = new Vector2(52.822806f, -3.399861f);
        coordsSean[4] = new Vector2(52.823556f, -3.394611f);
        coordsSean[5] = new Vector2(52.823417f, -3.391722f);
        coordsSean[6] = new Vector2(52.828583f, -3.392967f);
        coordsSean[7] = new Vector2(52.829750f, -3.390194f);
        coordsSean[8] = new Vector2(52.830972f, -3.391333f);
        coordsSean[9] = new Vector2(52.828694f, -3.394556f);
        coordsSean[10] = new Vector2(52.828083f, -3.396639f);
        coordsSean[11] = new Vector2(52.827194f, -3.400417f);
        coordsSean[12] = new Vector2(52.826833f, -3.403361f);
        coordsSean[13] = new Vector2(52.827194f, -3.406417f);
        coordsSean[14] = new Vector2(52.827694f, -3.408333f);

        coordsCB[1] = new Vector2(53.2909471f, -3.7244838f);
        coordsCB[2] = new Vector2(53.2912213f, -3.7255084f);
        coordsCB[3] = new Vector2(53.2914826f, -3.7263854f);
        coordsCB[4] = new Vector2(53.2918001f, -3.7274959f);
        coordsCB[5] = new Vector2(53.2920084f, -3.7281906f);
        coordsCB[6] = new Vector2(53.2925759f, -3.7283703f);
        coordsCB[7] = new Vector2(53.2922299f, -3.7269906f);
        coordsCB[8] = new Vector2(53.2918889f, -3.7259538f);
        coordsCB[9] = new Vector2(53.2915442f, -3.724905f);
        coordsCB[10] = new Vector2(53.2913301f, -3.7236626f);
        coordsCB[11] = new Vector2(53.2915127f, -3.722724f);
        coordsCB[12] = new Vector2(53.2909933f, -3.7229611f);
        coordsCB[13] = new Vector2(53.2907288f, -3.7232991f);
        coordsCB[14] = new Vector2(53.2908202f, -3.7237819f);
        frame = GameObject.Find("frame");
        cameraAnimator = GameObject.Find("Main Camera").GetComponent<Animator>();
        boxAnimator = GameObject.Find("BoxTop").GetComponent<Animator>();

        slideHolder = GameObject.Find("SlideHolder").GetComponent<Animator>();
        //skull = GameObject.Find("frame").GetComponent<SpriteGlowEffect>();




        mainCamera = GameObject.Find("Main Camera");
        debugText = GameObject.Find("Debug").GetComponent<TextMeshProUGUI>();
        messageText = GameObject.Find("MessageText").GetComponent<TextMeshProUGUI>();
        //mainCamera.transform.LookAt(textObject1.GetComponent<Transform>());

        startMarker1 = mainCamera.GetComponent<Transform>();
        endMarker1 = startMarker1;
        speed = 25f;
        catBluePrint = GameObject.Find("CatBluePrint").GetComponent<CanvasGroup>();

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


        //PlayerPrefs.SetInt("journeyProgress", 14);
        //PlayerPrefs.SetInt("Found1", 0);
        //PlayerPrefs.SetInt("Found2", 0);
        //PlayerPrefs.SetInt("Found3", 0);
        //PlayerPrefs.SetInt("Found4", 0);
        //PlayerPrefs.SetInt("Found5", 0);
        //PlayerPrefs.SetInt("Found6", 0);
        //PlayerPrefs.SetInt("Found7", 0);
        //PlayerPrefs.SetInt("Found8", 0);
        //PlayerPrefs.SetInt("Found9", 0);
        //PlayerPrefs.SetInt("Found10", 0);
        //PlayerPrefs.SetInt("Found11", 0);
        //PlayerPrefs.SetInt("Found12", 0);
        //PlayerPrefs.SetInt("Found13", 0);
        //PlayerPrefs.SetInt("Found14", 0);

        //PlayerPrefs.SetInt("journeyProgress", 0);

        journeyProgress = PlayerPrefs.GetInt("journeyProgress");
        //slideIndex = journeyProgress;
        AnimationClip[] animationClips = slideHolder.runtimeAnimatorController.animationClips;
        int j = 1;
        foreach (AnimationClip animClip in animationClips)
        {

            if (animClip.name != "SlideHolderZoomIn" && animClip.name != "SlideHolderZoomOut")
            {
                //Debug.Log(animClip.name + ": " + j);
                slideAnimations[j] = animClip.name;
                j++;
            }

        }
        Color transparent = new(0, 0, 0, 0);
        for (int i = 1; i < 14; i++)
        {
            slide[i] = GameObject.Find("Slide_" + i).GetComponent<SpriteRenderer>();
            if (i >= journeyProgress)
            {
                slide[i].color = transparent;
                parts[i].alpha = 0;
            }
        }

        if (journeyProgress > 0)
        {
            catBluePrint.alpha = 1;
        }
        else
        {
            catBluePrint.alpha = 0;
        }
        checkingLocation = true;
        Debug.Log(journeyProgress);
        StartCoroutine(GPSLoop());

        //slideHolder.SetFloat("direction", 1);
        //if (journeyProgress > 1)
        //{
        //    slideHolder.Play(slideAnimations[journeyProgress-1]);
        //}

        //StartCoroutine(MoveSomeText());
        //StartCoroutine(RotateDisc());

    }


    IEnumerator RotateDisc() {
        for (int i = 1; i < 14; i++)
        {
            yield return new WaitForSeconds(1f);
            slideIndex = i;
            slideHolder.Play(slideAnimations[i]);

        }
        for (int i = 1; i < 14; i++)
        {
            yield return new WaitForSeconds(1f);
            slideIndex = i;
            slideHolder.Play(slideAnimations[i]);

        }


    }

    IEnumerator FlyOut()
    {





        if (journeyProgress < 15)

        {
            StartCoroutine(FadeIn(slide[journeyProgress], 0.5f));
            journeyProgress++;
        }
        PlayerPrefs.SetInt("journeyProgress", journeyProgress);


        Debug.Log("JouneyProgress " + journeyProgress.ToString());
        PlayerPrefs.SetString("AnimationToPlay", (journeyProgress - 1).ToString());

        StartCoroutine(LoadScene("Twirly"));
        yield return new WaitForSeconds(0);




    }
    IEnumerator ZoomSlideHolder()
    {

        catBluePrint.alpha = 0;
        slideHolder.Play("SlideHolderZoomIn");
        slideBoxCollider.enabled = false;

        yield return new WaitForSeconds(1);



    }

    IEnumerator GPSLoop()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.5f);
            CheckClosest();
            DisplayLocationData();
            UseLocation();
        }
    }
    IEnumerator OpenBox()
    {
        boxAnimator.Play("Lid");
        yield return new WaitForSeconds(1);
        cameraAnimator.Play("IntoBox");
        //messageText.text = "Inside the box you discover...";

    }
    public IEnumerator LoadScene(string sceneName)
    {

        StartCoroutine(CGFadeIn(fader, 2f));
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneName);

    }
    private void CheckClosest()
    {
        for (int i = 1; i < 15; i++)
        {

            if (Distance(latitude, longitude, coordsCresswell[i].x, coordsCresswell[i].y) <= minDistance)
            {
                closest = i;
            }
            //if (Distance(latitude, longitude, coordsSean[i].x, coordsSean[i].y) <= minDistance)
            //    {
            //        closest = i;
            //    }
            //}
            //if (Distance(latitude, longitude, coordsCB[i].x, coordsCB[i].y) <= minDistance)
            //{
            //    closest = i;


            //}
            //minDistance = Distance(latitude, longitude, coordsCB[closest].x, coordsCB[closest].y);
            minDistance = Distance(latitude, longitude, coordsCresswell[closest].x, coordsCresswell[closest].y);
            //minDistance = Distance(latitude, longitude, coordsSean[closest].x, coordsSean[closest].y);
        }





        switch (closest)
        {
            case 1:
                closestisFound = PlayerPrefs.GetInt("Found1");
                break;
            case 2:
                closestisFound = PlayerPrefs.GetInt("Found2");
                break;
            case 3:
                closestisFound = PlayerPrefs.GetInt("Found3");
                break;
            case 4:
                closestisFound = PlayerPrefs.GetInt("Found4");
                break;
            case 5:
                closestisFound = PlayerPrefs.GetInt("Found5");
                break;
            case 6:
                closestisFound = PlayerPrefs.GetInt("Found6");
                break;
            case 7:
                closestisFound = PlayerPrefs.GetInt("Found7");
                break;
            case 8:
                closestisFound = PlayerPrefs.GetInt("Found8");
                break;
            case 9:
                closestisFound = PlayerPrefs.GetInt("Found9");
                break;
            case 10:
                closestisFound = PlayerPrefs.GetInt("Found10");
                break;
            case 11:
                closestisFound = PlayerPrefs.GetInt("Found11");
                break;
            case 12:
                closestisFound = PlayerPrefs.GetInt("Found12");
                break;
            case 13:
                closestisFound = PlayerPrefs.GetInt("Found13");
                break;
            case 14:
                closestisFound = PlayerPrefs.GetInt("Found14");
                break;

        }

    }
    void Update()
    {

        frameCounter++;
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fingerUpPosition = touch.position;
                fingerDownPosition = touch.position;
                fingerUp = false;

            }

            //if (touch.phase == TouchPhase.Moved)
            //{
            //    fingerDownPosition = touch.position;
            //    DetectSwipe();
            //}

            if (touch.phase == TouchPhase.Ended)
            {
                fingerDownPosition = touch.position;
                fingerUp = true;
                DetectSwipe();
            }

            if (touch.phase == TouchPhase.Stationary)
            {
                fingerStationary = true;
            }
        }

        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Debug.Log("Touch");
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit))
            {
                Debug.Log("Something Hit " + raycastHit.collider.name);

                int found = raycastHit.collider.name.IndexOf("_");
                string number = raycastHit.collider.name.Substring(found + 1);
                string[] thing = raycastHit.collider.name.Split("_");
                Debug.Log("The cat is " + number);



                if (thing[0] == "Slide" && SlideHolderZoomed)
                {
                    Color transparent = new(0, 0, 0, 0);
                    Debug.Log(raycastHit.collider.GetComponent<SpriteRenderer>().color);
                    if (raycastHit.collider.GetComponent<SpriteRenderer>().color != transparent)
                    {
                        PlayerPrefs.SetString("AnimationToPlay", number);
                        StartCoroutine(LoadScene("Twirly"));
                    }


                }
                if (raycastHit.collider.name == "slide disc" && SlideHolderZoomed == false)
                {
                    SlideHolderZoomed = true;
                    StartCoroutine("ZoomSlideHolder");

                }

                if (raycastHit.collider.name == "boxContents")
                {
                    Debug.Log("Clicked box");

                    StartCoroutine(FlyOut());



                    switch (closest)
                    {
                        case 1:
                            PlayerPrefs.SetInt("Found1", 1);
                            break;
                        case 2:
                            PlayerPrefs.SetInt("Found2", 1);
                            break;
                        case 3:
                            PlayerPrefs.SetInt("Found3", 1);
                            break;
                        case 4:
                            PlayerPrefs.SetInt("Found4", 1);
                            break;
                        case 5:
                            PlayerPrefs.SetInt("Found5", 1);
                            break;
                        case 6:
                            PlayerPrefs.SetInt("Found6", 1);
                            break;
                        case 7:
                            PlayerPrefs.SetInt("Found7", 1);
                            break;
                        case 8:
                            PlayerPrefs.SetInt("Found8", 1);
                            break;
                        case 9:
                            PlayerPrefs.SetInt("Found9", 1);
                            break;
                        case 10:
                            PlayerPrefs.SetInt("Found10", 1);
                            break;
                        case 11:
                            PlayerPrefs.SetInt("Found11", 1);
                            break;
                        case 12:
                            PlayerPrefs.SetInt("Found12", 1);
                            break;
                        case 13:
                            PlayerPrefs.SetInt("Found13", 1);
                            break;
                        case 14:
                            PlayerPrefs.SetInt("Found14", 1);
                            break;

                    }
                    checkingLocation = true;
                }
                if (raycastHit.collider.name == "BoxTop")
                {

                    StartCoroutine(OpenBox());
                }
                if (raycastHit.collider.name == "frame")
                {
                    //we'd better look at the wheel

                    Debug.Log("You touched the skull");
                    if (inRange == true || journeyProgress == 15)
                    {

                        if (journeyProgress > 0)
                        {
                            StartCoroutine(FlyOut());


                        }
                        else
                        {
                            StartCoroutine(CGFadeIn(catBluePrint, 1.0f));
                            // messageText.text = "You find a page from an artist's sketch book\nThey have drawn a fearsome beast!";
                            journeyProgress = 1;
                            PlayerPrefs.SetInt("journeyProgress", 1);
                        }
                    }










                }






            }
        }
        if (checkingLocation == true)
        {
            latitude = Input.location.lastData.latitude;
            longitude = Input.location.lastData.longitude;
        }


        

    }

    //private IEnumerator GetLocation()
    //{

    //    //if (Input.location.status != LocationServiceStatus.Running)
    //    //{
    //    //    debugText.text = "GPS Not Running Startng it";
    //        Input.location.Start();
    //    //}
    //    while (Input.location.status == LocationServiceStatus.Initializing)
    //    {
    //        yield return new WaitForSeconds(0.5f);
    //    }
    //    checkingLocation = true;
    //    //latitude = Input.location.lastData.latitude;
    //    //longitude = Input.location.lastData.longitude;

    //    yield break;
    //}
    private void DisplayLocationData()
    {
        string isFound = " not been found";
        if (closestisFound == 1)
        {
            isFound = " has been found";
        }
        else
        {
            isFound = " has not been found";
        }
        debugText.text = "The closest is " + minDistance.ToString("F1") + "m away, it is Point " + closest.ToString() + "\nIt" + isFound + " and you have found " + journeyProgress.ToString() + " locations";
        debugText.text += "\nHorizontal Accuracy is " + Input.location.lastData.horizontalAccuracy.ToString();
        //messageText.text = "Latttude: " + latitude.ToString() + " Longitude:" + longitude.ToString();

        //messageText.text += "\n";
        //messageText.text += "Horizontal Accuracy is " + Input.location.lastData.horizontalAccuracy.ToString();
        //messageText.text += "Found 1:" + PlayerPrefs.GetInt("Found1").ToString() + " Distance: " + Distance(latitude, longitude, coordsCB[1].x, coordsCB[1].y);
        //messageText.text += "\n";
        //messageText.text += "Found 2:" + PlayerPrefs.GetInt("Found2").ToString() + " Distance: " + Distance(latitude, longitude, coordsCB[2].x, coordsCB[2].y);
        //messageText.text += "\n";
        //messageText.text += "Found 3:" + PlayerPrefs.GetInt("Found3").ToString() + " Distance: " + Distance(latitude, longitude, coordsCB[3].x, coordsCB[3].y);
        //messageText.text += "\n";
        //messageText.text += "Found 4:" + PlayerPrefs.GetInt("Found4").ToString() + " Distance: " + Distance(latitude, longitude, coordsCB[4].x, coordsCB[4].y);
        //messageText.text += "\n";
        //messageText.text += "Found 5:" + PlayerPrefs.GetInt("Found5").ToString() + " Distance: " + Distance(latitude, longitude, coordsCB[5].x, coordsCB[5].y);
        //messageText.text += "\n";
        //messageText.text += "Found 6:" + PlayerPrefs.GetInt("Found6").ToString() + " Distance: " + Distance(latitude, longitude, coordsCB[6].x, coordsCB[6].y);
        //messageText.text += "\n";
        //messageText.text += "Found 7:" + PlayerPrefs.GetInt("Found7").ToString() + " Distance: " + Distance(latitude, longitude, coordsCB[7].x, coordsCB[7].y);
        //messageText.text += "\n";
        //messageText.text += "Found 8:" + PlayerPrefs.GetInt("Found8").ToString() + " Distance: " + Distance(latitude, longitude, coordsCB[8].x, coordsCB[8].y);
        //messageText.text += "\n";
        //messageText.text += "Found 9:" + PlayerPrefs.GetInt("Found9").ToString() + " Distance: " + Distance(latitude, longitude, coordsCB[9].x, coordsCB[9].y);
        //messageText.text += "\n";
        //messageText.text += "Found 10:" + PlayerPrefs.GetInt("Found10").ToString() + " Distance: " + Distance(latitude, longitude, coordsCB[10].x, coordsCB[10].y);
        //messageText.text += "\n";
        //messageText.text += "Found 11:" + PlayerPrefs.GetInt("Found11").ToString() + " Distance: " + Distance(latitude, longitude, coordsCB[11].x, coordsCB[11].y);
        //messageText.text += "\n";
        //messageText.text += "Found 12:" + PlayerPrefs.GetInt("Found12").ToString() + " Distance: " + Distance(latitude, longitude, coordsCB[12].x, coordsCB[12].y);
        //messageText.text += "\n";
        //messageText.text += "Found 13:" + PlayerPrefs.GetInt("Found13").ToString() + " Distance: " + Distance(latitude, longitude, coordsCB[13].x, coordsCB[13].y);
        //messageText.text += "\n";
        //messageText.text += "Found 14:" + PlayerPrefs.GetInt("Found14").ToString() + " Distance: " + Distance(latitude, longitude, coordsCB[14].x, coordsCB[14].y);
    }

    private void UseLocation()
    {
        if (closest != 0)
        {

            if (closestisFound == 1)
            {
                skull.color = new Color32(220, 220, 220, 220);

            }

            if (minDistance > 100f && checkingLocation)
            {
                skull.color = new Color32(220, 220, 220, 220);
                inRange = false;
            }
            if (minDistance < 100f && minDistance > 75f && checkingLocation && closestisFound == 0)
            {
                skull.color = new Color32(230, 225, 225, 225);
                inRange = false;
            }
            if (minDistance < 75f && minDistance > 50f && checkingLocation && closestisFound == 0)
            {
                skull.color = new Color32(230, 230, 230, 230);
                inRange = false;
            }
            if (minDistance < 50f && minDistance > 30f && checkingLocation && closestisFound == 0)
            {
                skull.color = new Color32(235, 235, 235, 235);
                inRange = false;
            }
            if (minDistance < 30f && minDistance > 20f && checkingLocation && closestisFound == 0)
            {
                skull.color = new Color32(240, 240, 240, 240);
                inRange = false;
            }
            if (minDistance < 20f && checkingLocation && closestisFound == 0)
            {
                skull.color = new Color32(255, 255, 255, 255);
                inRange = true;
                switch (closest)
                {
                    case 1:
                        PlayerPrefs.SetInt("Found1", 1);
                        break;
                    case 2:
                        PlayerPrefs.SetInt("Found2", 1);
                        break;
                    case 3:
                        PlayerPrefs.SetInt("Found3", 1);
                        break;
                    case 4:
                        PlayerPrefs.SetInt("Found4", 1);
                        break;
                    case 5:
                        PlayerPrefs.SetInt("Found5", 1);
                        break;
                    case 6:
                        PlayerPrefs.SetInt("Found6", 1);
                        break;
                    case 7:
                        PlayerPrefs.SetInt("Found7", 1);
                        break;
                    case 8:
                        PlayerPrefs.SetInt("Found8", 1);
                        break;
                    case 9:
                        PlayerPrefs.SetInt("Found9", 1);
                        break;
                    case 10:
                        PlayerPrefs.SetInt("Found10", 1);
                        break;
                    case 11:
                        PlayerPrefs.SetInt("Found11", 1);
                        break;
                    case 12:
                        PlayerPrefs.SetInt("Found12", 1);
                        break;
                    case 13:
                        PlayerPrefs.SetInt("Found13", 1);
                        break;
                    case 14:
                        PlayerPrefs.SetInt("Found14", 1);
                        break;

                }
                if (journeyProgress > 0)
                {
                    StartCoroutine(FlyOut());
                }
                else
                {
                    StartCoroutine(CGFadeIn(catBluePrint, 1.0f));
                    journeyProgress = 1;
                    PlayerPrefs.SetInt("journeyProgress", 1);
                }
            }
        }
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

    IEnumerator FadeIn(SpriteRenderer MyRenderer, float duration)
    {
        float counter = 0;
        //Get current color
        Color spriteColor = MyRenderer.material.color;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            //Fade from 1 to 0
            float alpha = Mathf.Lerp(0, 1, counter / duration);


            //Change alpha only
            MyRenderer.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
            //Wait for a frame
            yield return null;
        }
    }
    IEnumerator FadeOut(SpriteRenderer MyRenderer, float duration)
    {
        float counter = 0;
        //Get current color
        Color spriteColor = MyRenderer.material.color;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            //Fade from 1 to 0
            float alpha = Mathf.Lerp(1, 0, counter / duration);


            //Change alpha only
            MyRenderer.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
            //Wait for a frame
            yield return null;
        }
    }
    public IEnumerator CGFadeIn(CanvasGroup cg, float t)
    {
        for (float f = 0; f <= t; f += Time.deltaTime)
        {
            cg.alpha = Mathf.Lerp(0f, 1f, f / 2);
            yield return null;
        }
        cg.alpha = 1;
    }
    private void DetectSwipe()
    {
        if (SwipeDistanceCheckMet())
        {
            if (IsVerticalSwipe())
            {
                var direction = fingerDownPosition.y - fingerUpPosition.y > 0 ? SwipeDirection.Up : SwipeDirection.Down;
                Debug.Log(direction);
                
            }
            else
            {
                var direction = fingerDownPosition.x - fingerUpPosition.x > 0 ? SwipeDirection.Right : SwipeDirection.Left;
                Debug.Log(direction);
                if (direction == SwipeDirection.Left && SlideHolderZoomed == true)
                {
                    //sideIndex = journeyProgress+1;
                    Debug.Log(slideAnimations[slideIndex]);
                    slideHolder.SetFloat("direction", 1);
                    if (slideIndex == 0) slideIndex = 1;
                    slideHolder.Play(slideAnimations[slideIndex]);
                    slideIndex++;
                    if(slideIndex>13)
                    {
                        slideIndex = 1;
                    }
                }
               
            }
            fingerUpPosition = fingerDownPosition;
        }
    }

    private bool IsVerticalSwipe()
    {
        return VerticalMovementDistance() > HorizontalMovementDistance();
    }

    private bool SwipeDistanceCheckMet()
    {
        return VerticalMovementDistance() > 10 || HorizontalMovementDistance() > 10;
    }

    private float VerticalMovementDistance()
    {
        return Mathf.Abs(fingerDownPosition.y - fingerUpPosition.y);
    }

    private float HorizontalMovementDistance()
    {
        return Mathf.Abs(fingerDownPosition.x - fingerUpPosition.x);
    }


}
