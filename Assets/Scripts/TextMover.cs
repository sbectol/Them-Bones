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

    private GameObject frame;
    private GameObject mainCamera;
    private CanvasGroup catBluePrint;
    private CanvasGroup[] parts = new CanvasGroup[15];
    private SpriteRenderer[] slide = new SpriteRenderer[15];
    private int slideIndex = 1;
    private string[] slideAnimations = new string[14];
    private Vector2[] coordsCB = new Vector2[13];
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
    private SpriteGlowEffect skull;
    private SpriteRenderer boxContents;
    private bool inRange = false;

    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;
    private bool fingerUp;
    private bool stationary;
    private bool fingerStationary;

    private bool checkingLocation = true;
    private Animator cameraAnimator;
    private Animator boxAnimator;
    private Animator slideHolder;
    
    public Sprite[] spriteArray;

    void Start()
    {
        Input.compass.enabled = true;
        if (Input.location.isEnabledByUser)
        {
            StartCoroutine(GetLocation());
        }
        boxContents = GameObject.Find("boxContents").GetComponent<SpriteRenderer>();
        
        boxContents.sprite = spriteArray[5];
        coordsCresswell[1] = new Vector2(53.26301182857125f, -1.193743944168091f);
        coordsCresswell[2] = new Vector2(53.26306958489932f, -1.1971020698547366f);
        coordsCresswell[3] = new Vector2(53.262569027464444f, -1.1982500553131106f);
        coordsCresswell[4] = new Vector2(53.26229307662967f, -1.198915243148804f);
        coordsCresswell[5] = new Vector2(53.26197861885664f, -1.2005996704101565f);
        coordsCresswell[6] = new Vector2(53.26175400474593f, -1.2015652656555178f);
        coordsCresswell[7] = new Vector2(53.261503719061544f, -1.20275616645813f);
        coordsCresswell[8] = new Vector2(53.26099672562242f, -1.202058792114258f);
        coordsCresswell[9] = new Vector2(53.26121492606713f, -1.2010395526885989f);
        coordsCresswell[10] = new Vector2(53.261574312608026f, -1.2000739574432375f);
        coordsCresswell[11] = new Vector2(53.2618053452181f, -1.19876503944397f);
        coordsCresswell[12] = new Vector2(53.26202995905914f, -1.19776725769043f);
        coordsCresswell[13] = new Vector2(53.26222248426966f, -1.1971557140350342f);
        coordsCresswell[14] = new Vector2(53.26242784353887f, -1.1964797973632812f);

        coordsSean[1] = new Vector2(52.827694f, -3.408333f);
        coordsSean[2] = new Vector2(52.827194f, -3.406417f);
        coordsSean[3] = new Vector2(52.826833f, -3.403361f);
        coordsSean[4] = new Vector2(52.827194f, -3.400417f);
        coordsSean[5] = new Vector2(52.828083f, -3.396639f);
        coordsSean[6] = new Vector2(52.828694f, -3.394556f);
        coordsSean[7] = new Vector2(52.830972f, -3.391333f);
        coordsSean[8] = new Vector2(52.829750f, -3.390194f);
        coordsSean[9] = new Vector2(52.828583f, -3.392967f);
        coordsSean[10] = new Vector2(52.823417f, -3.391722f);
        coordsSean[11] = new Vector2(52.823556f, -3.394611f);
        coordsSean[12] = new Vector2(52.822806f, -3.399861f);
        coordsSean[13] = new Vector2(52.824556f, -3.394611f);
        coordsSean[14] = new Vector2(52.823806f, -3.399861f);






        coordsCB[1] = new Vector2(53.291048960315216f, -3.7248343819867595f);  //Home
        coordsCB[2] = new Vector2(53.29135f, -3.71566f);  //Corner of Leisure Centre
        coordsCB[3] = new Vector2(53.29301f, -3.71410f);  //Stone Circle
        coordsCB[4] = new Vector2(53.29302f, -3.712158f); //Moel Eirias
        coordsCB[5] = new Vector2(53.29172f, -3.71059f); //Lego
        coordsCB[6] = new Vector2(53.28918f, -3.71349f);  //Lorry
        coordsCB[7] = new Vector2(53.29522328468637f, -3.716254282301825f);  //Porth Eirias Roof
        coordsCB[8] = new Vector2(53.29592243145316f, -3.7201209658271615f); //Crazy Seats

        //private Vector2 lionEiriasCoords = new Vector2(53.2968343881184f, -3.7229468536380628f);  //Pier
        coordsCB[9] = new Vector2(53.295990779479716f, -3.720529407297063f);  //Near Seats
        coordsCB[10] = new Vector2(53.297953888903734f, -3.727067838982684f);  //Colwyn Sign
        coordsCB[11] = new Vector2(53.29979923676437f, -3.7315745163052583f);  // The Toad
        coordsCB[12] = new Vector2(53.29641334528825f, -3.7318060268120283f);   //War Memorial
        frame = GameObject.Find("frame");
        cameraAnimator = GameObject.Find("Main Camera").GetComponent<Animator>();
        boxAnimator = GameObject.Find("BoxTop").GetComponent<Animator>();
      
        slideHolder = GameObject.Find("SlideHolder").GetComponent<Animator>();
        skull = GameObject.Find("frame").GetComponent<SpriteGlowEffect>();
        



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


        //PlayerPrefs.SetInt("journeyProgress", 0);
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
        AnimationClip[] animationClips = slideHolder.runtimeAnimatorController.animationClips;
        int j = 1;
        foreach (AnimationClip animClip in animationClips)
        {
            Debug.Log(animClip.name + ": " + j);
            slideAnimations[j] = animClip.name;
            j++;

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
        
        slideHolder.SetFloat("direction", 1);
        if (journeyProgress > 1)
        {
            slideHolder.Play(slideAnimations[journeyProgress-1]);
        }

        //StartCoroutine(MoveSomeText());
        //StartCoroutine(RotateDisc());

    }

    IEnumerator MoveSomeText()


    {

        yield return new WaitForSeconds(5);
        //




    }
    IEnumerator RotateDisc() {
        for(int i = 1; i < 14; i++)
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
    IEnumerator FlyIn()
    {
        catBluePrint.alpha = 0;
        boxContents.sprite = spriteArray[journeyProgress];
        cameraAnimator.Play("CameraMove3");
        yield return new WaitForSeconds(1);
        cameraAnimator.Play("CameraMove1");
        yield return new WaitForSeconds(5);
        
    }
    IEnumerator FlyOut()
    {
        
        messageText.text = "";
        cameraAnimator.Play("CameraMove1_Reversed");
        yield return new WaitForSeconds(2.5f);
        
        StartCoroutine(FadeIn(slide[journeyProgress], 0.5f));
        boxAnimator.Play("LidClose");
        
        parts[journeyProgress].alpha = 1;
        if (journeyProgress < 14)
        {
            journeyProgress++;
        }
        PlayerPrefs.SetInt("journeyProgress", journeyProgress);

        catBluePrint.alpha = 1;
        PlayerPrefs.SetString("AnimationToPlay", ( journeyProgress-1).ToString());
        StartCoroutine(LoadScene("Twirly"));
        yield return new WaitForSeconds(3);
        

        //StartCoroutine(FadeIn(slide[journeyProgress], 0.5f));

    }

    IEnumerator OpenBox()
    {
        boxAnimator.Play("Lid");
        yield return new WaitForSeconds(1);
        cameraAnimator.Play("IntoBox");
        messageText.text = "Inside the box you discover...";
        
    }
    public IEnumerator LoadScene(string sceneName)
    {

        yield return new WaitForSeconds(0f);
        SceneManager.LoadScene(sceneName);

    }
    void Update()
    {
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

                if (thing[0] == "cat")
                {
                    //cameraAnimator.Play("CameraMove2");
                    //catBluePrint.alpha = 1;
                }
                if (raycastHit.collider.name == "arts")
                {
                    cameraAnimator.Play("CameraMove3");
                    catBluePrint.alpha = 0;
                    //PlayerPrefs.SetInt("journeyProgress", 0);
                    //journeyProgress = 0;

                }
               

                if (thing[0] == "Slide")
                {
                    Color transparent = new(0, 0, 0, 0);
                    Debug.Log(raycastHit.collider.GetComponent<SpriteRenderer>().color);
                    if (raycastHit.collider.GetComponent<SpriteRenderer>().color != transparent)
                    {
                        PlayerPrefs.SetString("AnimationToPlay", number);
                        StartCoroutine(LoadScene("Twirly"));
                    }
                    
                   
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
                    if (journeyProgress > 0)
                    {
                        StartCoroutine(FlyIn());

                        
                    } else
                    {
                        StartCoroutine(CGFadeIn(catBluePrint, 1.0f));
                        messageText.text = "You find a page from an artist's sketch book\nThey have drawn a fearsome beast!";
                        journeyProgress = 1;
                        PlayerPrefs.SetInt("journeyProgress", 1);
                    }
                    

                    
                    


                   



                }






            }
        }

        if (checkingLocation)
        {
            latitude = Input.location.lastData.latitude;
            longitude = Input.location.lastData.longitude;
        } else
        {
            latitude = 0f;
            longitude = 0f;
        }
        //Debug.Log(Distance(latitude, longitude, coordsCB[1].x, coordsCB[1].y));
        //find closest
        
        for (int i = 1; i < 14; i++)
        {
           
        //if(Distance(latitude, longitude, coordsCresswell[i].x, coordsCresswell[i].y) <= minDistance)
        //    {
        //        closest = i;
        //    }
        if (Distance(latitude, longitude, coordsSean[i].x, coordsSean[i].y) <= minDistance)
            {
                closest = i;
            }
        }



        //minDistance = Distance(latitude, longitude, coordsCresswell[closest].x, coordsCresswell[closest].y);
        minDistance = Distance(latitude, longitude, coordsSean[closest].x, coordsSean[closest].y);
        debugText.text = "minDistance is " + minDistance.ToString() +"m, The closest is " + closest.ToString() + " and Journey Progress is " + journeyProgress.ToString();

        int closestisFound = 0;

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

    


        if (closestisFound ==1)
        {
            skull.GlowBrightness = 1f;
            skull.OutlineWidth = 0;
            messageText.text = "";
        }

        
        
        if (minDistance > 100f && checkingLocation)
        {
            skull.GlowBrightness = 1f;
            skull.OutlineWidth = 0;
            inRange = false;


        }

        if (minDistance < 100f && minDistance > 75f && checkingLocation && closestisFound == 0 )
        {
            skull.GlowBrightness = 2f;
            skull.OutlineWidth = 2;
            inRange = false;
        }
        if (minDistance < 75f && minDistance > 50f  && checkingLocation && closestisFound == 0)
        {
            skull.GlowBrightness = 2f;
            skull.OutlineWidth = 5;
            inRange = false;
        }
        if (minDistance < 50f && minDistance > 25f && checkingLocation && closestisFound == 0)
        {
            skull.GlowBrightness = 2f;
            skull.OutlineWidth = 7;
            inRange = false;
        }
        if (minDistance < 25f && minDistance > 10f && checkingLocation && closestisFound == 0)
        {   
            skull.GlowBrightness = 2f;
            skull.OutlineWidth = 8;
            inRange = false;
        }
        if (minDistance < 10f && checkingLocation && closestisFound == 0)
        {   
            skull.GlowBrightness = 2f;
            skull.OutlineWidth = 10;
            inRange = true;
            //cameraAnimator.Play("CameraMove3");  //Put up a message or just move camera?
            messageText.text = "Press the skull to discover it's secrets";
            //checkingLocation = false;
           

        }




    }

    private IEnumerator GetLocation()
    {
        Input.location.Start();
        while (Input.location.status == LocationServiceStatus.Initializing)
        {
            yield return new WaitForSeconds(0.5f);
        }
        checkingLocation = true;
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
                if (direction == SwipeDirection.Left)
                {
                    //sideIndex = journeyProgress+1;
                    Debug.Log(slideIndex);
                    slideHolder.SetFloat("direction", 1);
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
