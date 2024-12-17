using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvalancheController : MonoBehaviour
{
    [SerializeField]
    public float speed = 1f;

    [SerializeField]
    public int level = 1;
    private float diminishingTime = 10f;
    private float diminishingRate = 0.05f;
    public ParticleSystem fog;

    // Start is called before the first frame update
    void Start()
    {
        StartAvalanche();
    }

    // Update is called once per frame
    void Update() { }

    void OnEnable() 
    {
        StartAvalanche();
    }
    public void StartAvalanche()
    {
        speed += (float)level * 2f;
        StartCoroutine(AvalancheSlide());
    }

    private IEnumerator AvalancheSlide()
    {
        float time = 0;
        while (true)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            time += Time.deltaTime;
            //if (time > 4f) {
            //    StopAvalanche();
            //}
            yield return null;
        }
    }

    public void StopAvalanche()
    {
        StartCoroutine(StopAvalancheCoroutine());
    }

    private IEnumerator StopAvalancheCoroutine()
    {
        float time = 0;
        ParticleSystem.EmissionModule emissionModule = fog.emission;
        float currentRate = emissionModule.rateOverTime.constant;
        while (time < diminishingTime)
        {
            time += Time.deltaTime;
            if (currentRate > 0)
            {
                emissionModule.rateOverTime = currentRate - diminishingRate * Time.deltaTime;
            }
            gameObject.transform.position -= new Vector3(0, diminishingRate * Time.deltaTime, 0);
            yield return null;
        }
        Destroy(gameObject);
    }
}
