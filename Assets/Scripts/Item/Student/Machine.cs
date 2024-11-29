using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : StudentItem
{
    // Update is called once per frame
    void Update() { }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        Destroy(gameObject);
    }
}
