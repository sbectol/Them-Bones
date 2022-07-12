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
    private SpriteRenderer[] slide = new SpriteRenderer[14];
    private int slideIndex = 1;
    private string[] slideAnimations = new string[14];
    private Vector2[] coordsCB = new Vector2[13];
    private Vector2[] coordsCresswell = new Vector2[15];
    private Transform endMarker1;
    private Transform startMarker1;
    private TMP_Text debugText;
    private float speed;
    private float latitude;
    private float longitude;
    private int journeyProgress;
    private int closest;
    public float minDistance = 1000;
    private SpriteGlowEffect skull;

    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;
    private bool fingerUp;
    private bool stationary;
    private bool fingerStationary;

    private bool checkingLocation = true;
    private Animator cameraAnimator;
    private Animator boxAnimator;
    private Animator slideHolder;
    void Start()
    {
        Input.compass.enabled = true;
        if (Input.location.isEnabledByUser)
        {
            StartCoroutine(GetLocation());
        }

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
        //mainCamera.transform.LookAt(textObject1.GetComponent<Transform>());

        startMarker1 = mainCamera.GetComponent<Transform>();
        endMarker1 = startMarker1;
        speed = 25f;
        catBluePrint = GameObject.Find("CatBluePrint").GetComponent<CanvasGroup>();
        catBluePrint.alpha = 0;
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

        
        PlayerPrefs.SetInt("journeyProgress", 0);

        //slide[1] = GameObject.Find("Slide1").GetComponent<SpriteRenderer>();
        //slide[2] = GameObject.Find("Slide2").GetComponent<SpriteRenderer>();
        //slide[3] = GameObject.Find("Slide3").GetComponent<SpriteRenderer>();
        //slide[4] = GameObject.Find("Slide4").GetComponent<SpriteRenderer>();
        //slide[5] = GameObject.Find("Slide5").GetComponent<SpriteRenderer>();
        //slide[6] = GameObject.Find("Slide6").GetComponent<SpriteRenderer>();
        //slide[7] = GameObject.Find("Slide7").GetComponent<SpriteRenderer>();
        //slide[8] = GameObject.Find("Slide8").GetComponent<SpriteRenderer>();
        //slide[9] = GameObject.Find("Slide9").GetComponent<SpriteRenderer>();
        //slide[10] = GameObject.Find("Slide10").GetComponent<SpriteRenderer>();
        //slide[11] = GameObject.Find("Slide11").GetComponent<SpriteRenderer>();
        //slide[12] = GameObject.Find("Slide12").GetComponent<SpriteRenderer>();
        //slide[13] = GameObject.Find("Slide13").GetComponent<SpriteRenderer>();
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
            slide[i] = GameObject.Find("Slide" + i).GetComponent<SpriteRenderer>();
            //slide[i].color = transparent;
            parts[i].alpha = 0;
        }
        journeyProgress = PlayerPrefs.GetInt("journeyProgress");
        checkingLocation = true;
        Debug.Log(journeyProgress);

        //StartCoroutine(MoveSomeText());
        //StartCoroutine(RotateDisc());
        
    }

    IEnumerator MoveSomeText()


    {

        yield return new WaitForSeconds(5);
        //StartCoroutine(LoadScene("Twirly"));




    }
    IEnumerator RotateDisc() {
        for(int i = 1; i < 14; i++)
        {
            yield return new WaitForSeconds(1f);
            slideIndex = i;
            slideHolder.Play(slideAnimations[i]);
            //StartCoroutine(FadeOut(slide[i], 0.5f));
        }
        for (int i = 1; i < 14; i++)
        {
            yield return new WaitForSeconds(1f);
            //StartCoroutine(FadeIn(slide[i], 0.5f));
            slideIndex = i;
            slideHolder.Play(slideAnimations[i]);
            
        }

       
    }
    IEnumerator FireSequence(object[] parms)
    {
        

        
        Animator animator = (Animator)parms[0];
        string sequence = (string)parms[1];
        float delay = (float)parms[2];
        animator.Play(sequence);
        //yield return new WaitForSeconds(delay);
        //CanvasGroup cg = (CanvasGroup)parms[8];
        //animator.Play(sequence);
        //yield return new WaitForSeconds(delay);
        //cg.alpha = 1;
        //cg = (CanvasGroup)parms[9];
        //cg.alpha = 1;
        yield return new WaitForSeconds(delay);
        
        

        checkingLocation = true;
        Debug.Log("Checking again");
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
                if (raycastHit.collider.name == "cat1")
                {
                    
                    object[] parms = new object[3] { cameraAnimator, "CameraMove1", 5f};
                    StartCoroutine(FadeOut(slide[1], 0.5f));
                    StartCoroutine(FireSequence(parms));



                }
                if (raycastHit.collider.name == "Slide1")
                {
                    Debug.Log("Slide 1");
                    object[] parms = new object[3] { cameraAnimator, "CameraMove2", 5f };
                    StartCoroutine(FireSequence(parms));
                }
                    if (raycastHit.collider.name == "Box")
                {
                    Debug.Log("Clicked box");
                    
                }
                if (raycastHit.collider.name == "BoxTop")
                {
                    object[] parms = new object[3] { boxAnimator, "Lid", 5f };
                    StartCoroutine(FireSequence(parms));
                }



              
            }
        }

        if (checkingLocation)
        {
            latitude = Input.location.lastData.latitude;
            longitude = Input.location.lastData.longitude;
        }
        //Debug.Log(Distance(latitude, longitude, coordsCB[1].x, coordsCB[1].y));
        //find closest
        
        for (int i = 1; i < 15; i++)
        {
           
        if(Distance(latitude, longitude, coordsCresswell[i].x, coordsCresswell[i].y) <= minDistance)
            {
                closest = i;
                
                

            }
        }

        minDistance = Distance(latitude, longitude, coordsCresswell[closest].x, coordsCresswell[closest].y);
        debugText.text = "minDistance is " + minDistance.ToString() +" The closest is " + closest.ToString();


        
        if (minDistance > 100f && checkingLocation)
        {
            Debug.Log("Over 100m from" + closest);
            skull.GlowBrightness = 1f;
            skull.OutlineWidth = 0;
        }

        if (minDistance < 100f && minDistance > 75f && checkingLocation)
        {
                Debug.Log("100m from" + closest);
                skull.GlowBrightness = 2f;
                skull.OutlineWidth = 3;
        }
        if (minDistance < 75f && minDistance > 50f  && checkingLocation)
        {
            Debug.Log("75m from" + closest);
            skull.GlowBrightness = 3f;
            skull.OutlineWidth = 3;
        }
        if (minDistance < 50f && minDistance > 0f && checkingLocation)
        {
            Debug.Log("50m from" + closest);
            skull.GlowBrightness = 4f;
            skull.OutlineWidth = 3;
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

                    slideHolder.SetFloat("direction", 1);
                    slideHolder.Play(slideAnimations[slideIndex]);
                    slideIndex++;
                    if(slideIndex>13)
                    {
                        slideIndex = 1;
                    }
                }
                if (direction == SwipeDirection.Right)
                {

                    if (slideIndex < 1)
                    {
                        slideIndex = 13;
                    }

                    slideHolder.SetFloat("direction", -1);
                    slideHolder.Play(slideAnimations[slideIndex]);
                    slideIndex--;
                    
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
