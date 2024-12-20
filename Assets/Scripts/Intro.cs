using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    private Animator animator;
    public GameObject Camera1, Camera2, Camera3;
    public GameObject Avalanche;
    public Image Img;
    public Button SkipButton;
    private float T = 0.0f;
    private float Speed = 1.4f;
    private Vector3 InitPos;
    private Quaternion InitRot;
    // Start is called before the first frame update
    private bool stair = false, camera1 = false, camera3 = false, shake = false, run = false;
    void Start()
    {
        InitPos = Camera2.transform.position;
        InitRot = Camera2.transform.GetChild(0).rotation;
        animator = GetComponentInChildren<Animator>();
        Camera2.SetActive(false);
        Camera3.SetActive(false);
        Avalanche.SetActive(false);

        Img.color = new Color(0, 0, 0, 1);
        SkipButton.gameObject.SetActive(false);
        StartCoroutine(FadeIn());
    }

    // Update is called once per frame
    void Update()
    {
        T += Time.deltaTime;
        
        if (T < 1.0f) {
            animator.SetBool("isWalking", true);
        } else if (T < 2.0f) {
            SkipButton.gameObject.SetActive(true);
        } else if (T < 6.0f) {
            if (!stair) {
                GameObject.Find("Main Camera").GetComponent<IntroCamera>().Walk_t(5.0f);
                stair = true;
            }
            if (T < 2.0f) {
                transform.Translate(Vector3.forward * Speed * 0.5f * (T-1) * Time.deltaTime);
            } else if (T < 5.0f) {
                transform.Translate(Vector3.forward * Speed * 0.5f * Time.deltaTime);
            } else {
                animator.SetBool("isWalking", false);
                transform.Translate(Vector3.forward * Speed * (6-T) * Time.deltaTime);
            }
            
        } else if (T < 7.5f) {
            if (!camera1) {
                Camera1.SetActive(false);
                Camera2.SetActive(true);
                Avalanche.SetActive(true);
                camera1 = true;
            }
        } else if (T < 8.0f) {
            if (!shake) {
                GameObject.Find("Main Camera (1)").GetComponent<IntroCamera>().Shake_t(10.0f);
            }
            shake = true;
        } else if (T < 8.2f) {
            transform.GetChild(1).transform.GetChild(0).Rotate(Vector3.up * -300 * Time.deltaTime);
        }else if (T < 8.8f) {

        } else if (T < 9.0f) {
            Camera2.transform.GetChild(0).Rotate(Vector3.up * 300 * Time.deltaTime);
        } else if (T < 10.0f) {
            
        } else if (T < 10.2f) {
            Camera2.transform.Translate(Vector3.forward * 200 * Time.deltaTime);
        } else if (T < 11.2f) {

        } else if (T < 11.5f) {
            Camera2.transform.position = InitPos;
            Camera2.transform.GetChild(0).rotation = InitRot;
        } else if (T < 12.5f) {

        } else if (T < 12.8f) {
            transform.GetChild(1).GetChild(0).Rotate(Vector3.up *  300 * Time.deltaTime);
        } else if (T < 13.5f) {

        } else if (T < 13.75f) {
            transform.GetChild(1).GetChild(0).Rotate(Vector3.up *  -200 * Time.deltaTime);
        } else if (T >= 14.5f && T< 17.5f) {
            if (!run) {
                animator.SetBool("isRunning", true);
                run = true;
            }
            if (T > 15.0f && !camera3) {
                StartCoroutine(FadeOut());
                SkipButton.gameObject.SetActive(false);
                transform.GetChild(1).position = transform.GetChild(1).position + new Vector3(0, 0.3f, 0);
                Camera2.SetActive(false);
                Camera3.SetActive(true);
                camera3 = true;
            }
            transform.Translate(Vector3.forward * Speed * 2 * Time.deltaTime);
        } else if (T > 17.5f) {
            SceneManager.LoadScene("MainScene");
        }
    }

    public void SkipIntro() {
        StartCoroutine(FadeOut());
        SkipButton.gameObject.SetActive(false);
        Invoke("LoadMainScene", 1.0f);
    }

    private void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0.0f;
        float fade = 0.0f;

        while (elapsedTime < 1.0f) {
            elapsedTime += Time.deltaTime;
            fade = Mathf.Lerp(1, 0, elapsedTime);
            Img.color = new Color(0, 0, 0, fade);
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        float elapsedTime = 0.0f;
        float fade = 0.0f;

        while (elapsedTime < 1.0f) {
            elapsedTime += Time.deltaTime;
            fade = Mathf.Lerp(0, 1, elapsedTime);
            Img.color = new Color(0, 0, 0, fade);
            yield return null;
        }
    }
}
