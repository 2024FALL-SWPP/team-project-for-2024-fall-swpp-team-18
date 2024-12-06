using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    public GameObject Avalanche;
    public Image Img;
    private float T = 0.0f;
    private float Speed = 1.4f;
    // Start is called before the first frame update
    private bool stair = false, stop = false, run = false;
    void Start()
    {
        Avalanche.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        T += Time.deltaTime;

        if (T < 5.0f) {
            if (!stair) {
                GameObject.Find("Main Camera").GetComponent<IntroCamera>().Run_t(5.0f);
                stair = true;
            }
            if (T < 1.0f) {
                transform.Translate(Vector3.forward * Speed * T * Time.deltaTime);
            } else if (T < 4.0f) {
                transform.Translate(Vector3.forward * Speed * Time.deltaTime);
            } else {
                transform.Translate(Vector3.forward * Speed * (5-T) * Time.deltaTime);
            }
            
        } else if (T < 6.0f) {
            if (!stop) {
                GameObject.Find("Main Camera").GetComponent<IntroCamera>().Shake_t(10.0f);
                Avalanche.SetActive(true);
                stop = true;
            }
        } else if (T < 6.3f) {
            transform.Rotate(Vector3.up * -300 * Time.deltaTime);
        } else if (T < 8.0f) {

        } else if (T < 8.5f) {
            transform.Rotate(Vector3.up *  300 * Time.deltaTime);
        } else if (T < 9.0f) {

        } else if (T < 9.25f) {
            transform.Rotate(Vector3.up *  -300 * Time.deltaTime);
        } else if (T >= 10.0f && T< 13.0f) {
            if (!run) {
                GameObject.Find("Main Camera").GetComponent<IntroCamera>().Run_t(3.0f);
                Img.CrossFadeAlpha(255.0f, 2.0f, false);
                run = true;
            }
            transform.Translate(Vector3.forward * Speed * 2 * Time.deltaTime);
        }
    }
}
