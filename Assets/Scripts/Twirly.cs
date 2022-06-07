using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Twirly : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject cat;
    private GameObject holder;
    private GameObject outerTooth;
    private GameObject innerTooth;
    private GameObject innerToothHolder;
    private GameObject outerToothHolder;
    private GameObject catHolder;
    private GameObject longTeeth;
    private float rotationSpeed;


    private Image disc;
    private GameObject discRect;
    private float r;

    void Start()
    {
        rotationSpeed = 20f;
        disc = GameObject.Find("CanvasDisc").GetComponent<Image>();
        discRect = GameObject.Find("CanvasDisc");
        if (PlayerPrefs.GetString("BeastName") == "Billy")
        {
            discRect.transform.localPosition = new Vector3(0, -350, 0);
        }
        holder = GameObject.Find("Holder");
        cat = GameObject.Find("Cat");
        outerTooth = GameObject.Find("OuterTooth");
        innerTooth = GameObject.Find("InnerTooth");
        outerToothHolder = GameObject.Find("OuterToothHolder");
        innerToothHolder = GameObject.Find("InnerToothHolder");
        longTeeth = GameObject.Find("LongTeeth");
        catHolder = GameObject.Find("CatHolder");
        cat.transform.SetPositionAndRotation(Camera.main.ScreenToWorldPoint(disc.rectTransform.transform.position), Quaternion.Euler(0, 0, 0));
        outerTooth.transform.SetPositionAndRotation(Camera.main.ScreenToWorldPoint(disc.rectTransform.transform.position), Quaternion.Euler(0, 0, 0));
        innerTooth.transform.SetPositionAndRotation(Camera.main.ScreenToWorldPoint(disc.rectTransform.transform.position), Quaternion.Euler(0, 0, 0));
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

        yield return new WaitForEndOfFrame();
    }

    // Update is called once per frame
    void Update()
    {
        catHolder.transform.Rotate(Vector3.forward * (rotationSpeed * Time.deltaTime));
        outerToothHolder.transform.Rotate(Vector3.back * (rotationSpeed*1.4f * Time.deltaTime));
        innerToothHolder.transform.Rotate(Vector3.forward * (rotationSpeed*1.8f * Time.deltaTime));
        longTeeth.transform.Rotate(Vector3.back * (rotationSpeed*2f * Time.deltaTime));



    }
}
