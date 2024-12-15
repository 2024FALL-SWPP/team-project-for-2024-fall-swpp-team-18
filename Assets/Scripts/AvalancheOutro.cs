using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvalancheOutro : MonoBehaviour
{
    public Image Img;
    // Start is called before the first frame update
    void Start()
    {
        Img.CrossFadeAlpha(0.0f, 1.0f, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
