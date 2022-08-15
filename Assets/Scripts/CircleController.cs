using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CircleController : MonoBehaviour
{


    
    private GameObject beastlyCircle;
    private GameObject beastHolder;
    private GameObject tooth;
    private SpriteRenderer hole;
    private GameObject toothShadow;
    private CanvasGroup page1;
    private CanvasGroup page2;
    private CanvasGroup page3;

    // Start is called before the first frame update
    void Start()
    {
        beastlyCircle = GameObject.Find("beastlyCircle");
        beastHolder = GameObject.Find("beastHolder");
        tooth = GameObject.Find("tooth");
        toothShadow = GameObject.Find("toothShadow");
        page1 = GameObject.Find("Page1").GetComponent<CanvasGroup>();
        page2 = GameObject.Find("Page2").GetComponent<CanvasGroup>();
        page3 = GameObject.Find("Page3").GetComponent<CanvasGroup>();

        hole = GameObject.Find("Hole").GetComponent<SpriteRenderer>();

        StartCoroutine(Clone());
        StartCoroutine(IntroScript());



    }

   
    IEnumerator IntroScript()


    {
        

        var zeroScale = new Vector3(0, 0, 0);
        
        tooth.transform.localScale = zeroScale;
        toothShadow.transform.localScale = zeroScale;
        beastHolder.transform.localScale = zeroScale;
        var tinyScale = new Vector3(0.1f, 0.1f, 0.1f);
        var scaleTo = new Vector3(0.34f,0.34f, 0.34f);
        var toothScale = new Vector3(0.2f, 0.2f, 0.2f);
        var finalToothScale = new Vector3(0.3f, 0.3f, 0.3f);
        StartCoroutine(ScaleOverSeconds(beastHolder, scaleTo, 14));
        yield return new WaitForSeconds(14);
        StartCoroutine(ScaleOverSeconds(toothShadow, toothScale, 10));
        StartCoroutine(ScaleOverSeconds(tooth, toothScale, 10));
        yield return new WaitForSeconds(10);
        StartCoroutine(ScaleOverSeconds(tooth, finalToothScale, 10));
        yield return new WaitForSeconds(10);
        StartCoroutine(FadeHole());
        yield return new WaitForSeconds(3);
        StartCoroutine(FadeIn(2f,page1));
        yield return new WaitForSeconds(8);
        StartCoroutine(FadeOut(2f, page1));
        yield return new WaitForSeconds(2);
        StartCoroutine(FadeIn(2f, page2));
        yield return new WaitForSeconds(8);
        StartCoroutine(FadeOut(2f, page2));
        yield return new WaitForSeconds(2);
        StartCoroutine(FadeIn(2f, page3));
        yield return new WaitForSeconds(2);
        StartCoroutine(LoadScene("Text"));



    }
    public IEnumerator LoadScene(string sceneName)
    {

        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneName);

    }
    IEnumerator Clone()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject myclone = Instantiate(beastlyCircle, new Vector3(0, 0, 0), Quaternion.identity, beastHolder.transform);
            myclone.transform.localScale = new Vector3(0.5f+(0.2f*i), 0.5f + (0.2f *i), 0);
            SpriteRenderer spriteRenderer  = myclone.GetComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = 5 - i;
            Animator cloneAnimator = myclone.GetComponent<Animator>();
            cloneAnimator.Play("beastlyCircle", 0, 0.1f*i);

        }
        yield return new WaitForEndOfFrame();
    }

    private IEnumerator FadeHole()
    {
        float elapsedTime = 0;
        float seconds = 5f;
        
        while (elapsedTime < seconds) {
            float t = elapsedTime / seconds;
            t = t * t * (3f - 2f * t);
            Color c = Color.black;
            c.a = Mathf.Lerp(0, 1, t);
            hole.color = c;// Color.Lerp(trans, black, t);
            Debug.Log(c);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator ScaleOverSeconds(GameObject objectToScale, Vector3 scaleTo, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingScale = objectToScale.transform.localScale;

        while (elapsedTime < seconds)
        {
            float t = elapsedTime / seconds;
            //ease out
            //t = Mathf.Sin(t * Mathf.PI * 0.5f);
            //exponential
            //t = t * t;

            //smoothstep

            t = t * t * (3f - 2f * t);
            objectToScale.transform.localScale = Vector3.Lerp(startingScale, scaleTo, t);

            
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        
    }

    public IEnumerator FadeIn(float t, CanvasGroup cg)
    {
        for (float f = 0; f <= t; f += Time.deltaTime)
        {
            cg.alpha = Mathf.Lerp(0f, 1f, f / 2);
            yield return null;
        }
        cg.alpha = 1;
    }
    public IEnumerator FadeOut(float t, CanvasGroup cg)
    {
        for (float f = t; f >= 0; f -= Time.deltaTime)
        {
            cg.alpha = Mathf.Lerp(0f, 1f, f / 2);
            yield return null;
        }
        cg.alpha = 0;
    }
}
