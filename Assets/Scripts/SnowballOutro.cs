using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnowballOutro : MonoBehaviour
{
    public GameObject Snowball;
    public Image Img;
    private Rigidbody SnowballRb;
    private float T = 0.0f;
    private Vector3 origin;
    private bool fadein = false;
    // Start is called before the first frame update
    void Start()
    {
        origin = transform.localPosition;
        SnowballRb = Snowball.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        T += Time.deltaTime;

        if (T < 2.0f) {
            Img.CrossFadeAlpha(0.0f, 1.0f, false);
        } else if(T < 4.0f) {
            if (T > 3.0f) {
                SnowballRb.AddForce(Vector3.forward * -100);
            }
            transform.localPosition = origin + 0.05f * (T-2) * (Vector3)(Random.insideUnitCircle);
        } else if (T < 5.0f) {
            transform.localPosition = origin + 0.1f * (Vector3)(Random.insideUnitCircle);
        } else if (T < 7.0f) {
            transform.localPosition = origin + 0.05f * (7-T) * (Vector3)(Random.insideUnitCircle);
        }
    }
}
