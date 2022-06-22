using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Twirly : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator[] catAnimator;
    private Animator middleCat;
    private GameObject cat;
    private GameObject holder;
    private GameObject outerTooth;
    private GameObject innerTooth;
    private GameObject innerToothHolder;
    private GameObject outerToothHolder;
    private GameObject catHolder;
    private GameObject longTooth;
    private GameObject longToothHolder;
    private GameObject molarTooth;
    private GameObject molarToothHolder;
    private GameObject middleCatHolder;

    private float rotationAmount = 4f;
    private Image button;
    private RectTransform buttonRectTransform;
    private AudioSource squeakPlayer;
    private AudioClip squeakClip;
    private float rotationSpeed;
    private float rate;
    private float buttonX;
    private float buttonY;
    private bool playTheSqueak;
    private int tempo = 96;
    private double bpm = 288.0F;
    private float friction = 0.3f;
    private readonly int speedLimit = 10;
    private float sliderValue;
    private bool running = false;

    private float gain = 0.2F;
    private int signatureHi = 0;
    private int signatureLo = 8;

    
    private double nextTick = 0.0F;
    private float amp = 0.0F;
    private float phase = 0.0F;
    private double sampleRate = 0.0F;
    private int accent;

    private Image disc;
    private GameObject discRect;
    private float r;

    void Start()
    {
        rate = 5f;
        sliderValue = 30;
        disc = GameObject.Find("CanvasDisc").GetComponent<Image>();
        discRect = GameObject.Find("CanvasDisc");
        if (PlayerPrefs.GetString("BeastName") == "Billy")
        {
            discRect.transform.localPosition = new Vector3(0, -350, 0);
        }
        double startTick = AudioSettings.dspTime;
        sampleRate = AudioSettings.outputSampleRate;
        nextTick = startTick * sampleRate;
        running = true;
        squeakPlayer = GameObject.Find("SqueakPlayer").GetComponent<AudioSource>();
        SwipeDetector.OnSwipe += SwipeDetector_OnSwipe;
        holder = GameObject.Find("Holder");
        cat = GameObject.Find("Cat");
       
        outerTooth = GameObject.Find("OuterTooth");
        innerTooth = GameObject.Find("InnerTooth");
        outerToothHolder = GameObject.Find("OuterToothHolder");
        innerToothHolder = GameObject.Find("InnerToothHolder");
        longToothHolder = GameObject.Find("LongToothHolder");
        longTooth = GameObject.Find("LongTooth");
        molarToothHolder = GameObject.Find("MolarToothHolder");
        molarTooth = GameObject.Find("MolarTooth");
        middleCatHolder = GameObject.Find("MiddleCat");
        catHolder = GameObject.Find("CatHolder");
        
        button = GameObject.Find("CanvasDisc").GetComponent<Image>();
        buttonX = button.transform.position.x;
        buttonY = button.transform.position.y;
        buttonRectTransform = button.rectTransform;
        Vector3 discPosition = Camera.main.ScreenToWorldPoint(disc.rectTransform.transform.position);
        cat.transform.SetPositionAndRotation(discPosition, Quaternion.Euler(0, 0, 0));
        outerTooth.transform.SetPositionAndRotation(Camera.main.ScreenToWorldPoint(disc.rectTransform.transform.position), Quaternion.Euler(0, 0, 0));
        innerTooth.transform.SetPositionAndRotation(Camera.main.ScreenToWorldPoint(disc.rectTransform.transform.position), Quaternion.Euler(0, 0, 0));
        longTooth.transform.SetPositionAndRotation(Camera.main.ScreenToWorldPoint(disc.rectTransform.transform.position), Quaternion.Euler(0, 0, 0));
        molarTooth.transform.SetPositionAndRotation(Camera.main.ScreenToWorldPoint(disc.rectTransform.transform.position), Quaternion.Euler(0, 0, 0));
        middleCatHolder.transform.SetPositionAndRotation(Camera.main.ScreenToWorldPoint(disc.rectTransform.transform.position), Quaternion.Euler(0, 0, 0));
        Debug.Log(Screen.height);
        Debug.Log(Screen.width);
        Debug.Log((float)Screen.height / (float)Screen.width);
        float aspect = (float)Screen.height / (float)Screen.width;
        Debug.Log(aspect);
        if (aspect < 1.4)
        {
            r = 1.3f;
        }
        else
        {
            r = 1.1f;
        }
        StartCoroutine(Clone());
        catAnimator = catHolder.GetComponentsInChildren<Animator>();
        middleCat = GameObject.Find("MiddleCat").GetComponent<Animator>();
        StartCoroutine(Friction(0.1f));

    }
    private void PlaySqueak()
    {
        Debug.Log("Playing the squeak");
        squeakClip = Resources.Load<AudioClip>("Audio/squeak");
        squeakPlayer.clip = squeakClip;
        squeakPlayer.PlayOneShot(squeakClip);
    }
    IEnumerator Clone()
    {

        for (int i = 0; i < 12; i++)
        {
            Color newColour = new Color(1 - (i * 0.2f), 0, 0, 1f);


            GameObject catClone = Instantiate(cat, new Vector3(Camera.main.ScreenToWorldPoint(disc.rectTransform.transform.position).x + (r*1.5f * Mathf.Cos(i * 30 * Mathf.PI / 180)), Camera.main.ScreenToWorldPoint(disc.rectTransform.transform.position).y + (r*1.5f * Mathf.Sin(i * 30 * Mathf.PI / 180)), -9), Quaternion.Euler(0, 0, cat.transform.rotation.z + (i * 30) + 90));

            catClone.name = "catClone-" + (i + 1);
            SpriteRenderer catSprite = catClone.GetComponent<SpriteRenderer>();
            //catSprite.color = newColour;
            // Debug.Log(catClone.name);
            catClone.transform.parent = catHolder.transform;
        }

        for (int i = 0; i < 16; i++)
        {
            GameObject innerToothClone = Instantiate(innerTooth,
                new Vector3(Camera.main.ScreenToWorldPoint(disc.rectTransform.transform.position).x
                + (r*0.8f * Mathf.Cos(i * 22.5f * Mathf.PI / 180)), Camera.main.ScreenToWorldPoint(disc.rectTransform.transform.position).y
                + (r*0.8f * Mathf.Sin(i * 22.5f * Mathf.PI / 180)), -9),
                Quaternion.Euler(0, 0, innerTooth.transform.rotation.z + (i * 22.5f) + 90));
            GameObject outerToothClone = Instantiate(outerTooth,
                new Vector3(Camera.main.ScreenToWorldPoint(disc.rectTransform.transform.position).x
                + (r * Mathf.Cos(i * 22.5f * Mathf.PI / 180)), Camera.main.ScreenToWorldPoint(disc.rectTransform.transform.position).y
                + (r * Mathf.Sin(i * 22.5f * Mathf.PI / 180)), -9),
                Quaternion.Euler(0, 0, outerTooth.transform.rotation.z + (i * 22.5f) + 90));
            outerToothClone.name = "outerToothClone-" + (i + 1);
            outerToothClone.transform.parent = outerToothHolder.transform;
            innerToothClone.name = "innerToothClone-" + (i + 1);
            innerToothClone.transform.parent = innerToothHolder.transform;

        }

        for (int i = 0; i < 4; i++)
        {
            GameObject longToothClone = Instantiate(longTooth,
                new Vector3(Camera.main.ScreenToWorldPoint(disc.rectTransform.transform.position).x
                + (r * 0.5f * Mathf.Cos(i * 90 * Mathf.PI / 180)),
                Camera.main.ScreenToWorldPoint(disc.rectTransform.transform.position).y
                + (r * 0.5f * Mathf.Sin(i * 90 * Mathf.PI / 180)), -9),
                Quaternion.Euler(0, 0, longTooth.transform.rotation.z + (i * 90) +45 ));
            GameObject molarToothClone = Instantiate(molarTooth,
                new Vector3(Camera.main.ScreenToWorldPoint(disc.rectTransform.transform.position).x
                + (r * 0.5f * Mathf.Cos(i * 90 * Mathf.PI / 180)),
                Camera.main.ScreenToWorldPoint(disc.rectTransform.transform.position).y
                + (r * 0.5f * Mathf.Sin(i * 90 * Mathf.PI / 180)), -9),
                Quaternion.Euler(0, 0, molarTooth.transform.rotation.z + (i * 90) +180));

            longTooth.name = "longToothClone-" + (i + 1);
            longToothClone.transform.parent = longToothHolder.transform;
            molarTooth.name = "molarToothClone-" + (i + 1);
            molarToothClone.transform.parent = molarToothHolder.transform;
        }
        molarToothHolder.transform.RotateAround(Camera.main.ScreenToWorldPoint(buttonRectTransform.transform.position), new Vector3(0, 0, 1f), 0);
        yield return new WaitForEndOfFrame();
    }

    // Update is called once per frame
    void Update()
    {
        if (playTheSqueak == true)
        {
            PlaySqueak();
            playTheSqueak = false;
        }
        if (rate > 0)
        {
            catHolder.transform.RotateAround(Camera.main.ScreenToWorldPoint(buttonRectTransform.transform.position), new Vector3(0, 0, 1f), rotationAmount * (rate / 2));
            outerToothHolder.transform.RotateAround(Camera.main.ScreenToWorldPoint(buttonRectTransform.transform.position), new Vector3(0, 0, 1f), -rotationAmount * (rate/1.75f));
            innerToothHolder.transform.RotateAround(Camera.main.ScreenToWorldPoint(buttonRectTransform.transform.position), new Vector3(0, 0, 1f), rotationAmount * (rate/1.5f));
            longToothHolder.transform.RotateAround(Camera.main.ScreenToWorldPoint(buttonRectTransform.transform.position), new Vector3(0, 0, 1f), -rotationAmount * (rate / 1.25f));
            molarToothHolder.transform.RotateAround(Camera.main.ScreenToWorldPoint(buttonRectTransform.transform.position), new Vector3(0, 0, 1f), -rotationAmount * (rate));
        }
        foreach (Animator anim in catAnimator)
        {
            anim.speed = rate;
        }
        middleCat.speed = rate;
    }
    void OnAudioFilterRead(float[] data, int channels)
    {
        if (!running)
            return;

        double samplesPerTick = sampleRate * 60.0F / bpm * 4.0F / signatureLo;
        double sample = AudioSettings.dspTime * sampleRate;
        int dataLen = data.Length / channels;

        int n = 0;
        while (n < dataLen)
        {
            float x = gain * amp * Mathf.Sin(phase);
            int i = 0;
            while (i < channels)
            {
                data[n * channels + i] += x;
                i++;
            }
            while (sample + n >= nextTick)
            {
                nextTick += samplesPerTick;
                amp = 1.0F;
                if (++accent > signatureHi)
                {
                    accent = 1;
                    amp *= 2.0F;
                }
                Debug.Log("Tick: " + accent + "/" + signatureHi);
            }
            phase += amp * 0.3F;
            amp *= 0.3F;
            n++;
        }
    }
    IEnumerator Friction(float time)
    {
        yield return new WaitForSeconds(time);
        while (true)
        {
            if (rate > 0)
            {
                running = true;
                rate -= friction;
                if (bpm > 100) bpm -= 1;
            }
            else
            {
                rate = 0;
                running = false;
            }
            yield return new WaitForSeconds(time);
        }
    }

    private void SwipeDetector_OnSwipe(SwipeData data)
    {


        if (data.FingerUp == true || data.FingerStationary == true)
        {
            //rate = 0;
            friction = 0.3f;


        }


        float speed = (data.EndPosition - data.StartPosition).magnitude / sliderValue;
        
        if (speed > speedLimit) speed = speedLimit;


        if (data.StartPosition.y < buttonY + 500)
        {

            if (data.StartPosition.x < buttonX && data.EndPosition.x > buttonX && data.StartPosition.y < buttonY && data.EndPosition.y < buttonY)
            {
                Debug.Log("Crossed Lower Midpoint X" + data.StartPosition.x.ToString() + ":" + data.EndPosition.x.ToString());
                Debug.Log("Crossed Lower Midpoint Y" + data.StartPosition.y.ToString() + ":" + data.EndPosition.y.ToString());
                rate = speed;
                bpm = speed * tempo;
            }
            if (data.StartPosition.x > buttonX && data.EndPosition.x < buttonX && data.StartPosition.y > buttonY && data.EndPosition.y > buttonY)
            {
                Debug.Log("Crossed Upper Midpoint X" + data.StartPosition.x.ToString() + ":" + data.EndPosition.x.ToString());
                Debug.Log("Crossed Upper Midpoint Y" + data.StartPosition.y.ToString() + ":" + data.EndPosition.y.ToString());
                playTheSqueak = true;
                rate = speed;
                bpm = speed * tempo;
            }
            if (data.StartPosition.y < buttonY && data.EndPosition.y > buttonY && data.StartPosition.x > buttonX && data.EndPosition.x > buttonX)
            {
                Debug.Log("Crossed Right Midpoint");
                rate = speed;
                bpm = speed * tempo;
            }
            if (data.StartPosition.y > buttonY && data.EndPosition.y < buttonY && data.StartPosition.x < buttonX && data.EndPosition.x < buttonX)
            {
                Debug.Log("Crossed Left Midpoint");
                rate = speed;
                bpm = speed * tempo;
            }

            if (data.StartPosition.x > buttonX && data.StartPosition.y < buttonY)
            {
                if (data.StartPosition.y < data.EndPosition.y && data.StartPosition.x < data.EndPosition.x)
                {

                    Debug.Log("Going AntiClock in the Bottom Right Quad");

                    rate = speed;
                    bpm = speed * tempo;

                }

            }



            if (data.StartPosition.x > buttonX && data.StartPosition.y > buttonY)
            {

                if (data.StartPosition.y < data.EndPosition.y && data.StartPosition.x > data.EndPosition.x)
                {
                    Debug.Log("Going AntiClock In Top Right Quad    ");
                    rate = speed;
                    bpm = speed * tempo;

                }

            }

            if (data.StartPosition.x < buttonX && data.StartPosition.y > buttonY)
            {

                if (data.StartPosition.y > data.EndPosition.y && data.StartPosition.x > data.EndPosition.x)
                {
                    Debug.Log("Going AntiClock In Top Left Quad ");
                    rate = speed;
                    bpm = speed * tempo;

                }
            }
            if (data.StartPosition.x < buttonX && data.StartPosition.y < buttonY)
            {

                if (data.StartPosition.y > data.EndPosition.y && data.StartPosition.x < data.EndPosition.x)
                {
                    Debug.Log("Going AntiClock In Bottom Left Quad ");
                    rate = speed;
                    bpm = speed * tempo;

                }
            }
        }
        Debug.Log("Speed Limit is" + speedLimit.ToString());
        Debug.Log("Rate is " + rate.ToString());
        Debug.Log("Speed is " + speed.ToString());

    }
}
