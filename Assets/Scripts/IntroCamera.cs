using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCamera : MonoBehaviour
{
    private float rotateSpeed = 400f; 
    private Vector3 origin;
    // Start is called before the first frame update
    void Start()
    {
        origin = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Shake(float t) {
        float T = 0.0f;
        while(T < t) {
            T += Time.deltaTime;
            transform.localPosition = origin + 0.02f * T * (Vector3)(Random.insideUnitCircle);
            yield return null;
        }
        transform.localPosition = origin;
    }

    public void Shake_t(float t) {
        StartCoroutine(Shake(t));
    }

    IEnumerator Run(float t) {
        float T = 0.0f;
        while(T < t) {
            T += Time.deltaTime;
            transform.Translate(Vector3.up * (T%0.4f - 0.2f) * 0.1f);
            yield return null;
        }
        transform.localPosition = origin;
    }

    public void Run_t(float t) {
        StartCoroutine(Run(t));
    }
}
