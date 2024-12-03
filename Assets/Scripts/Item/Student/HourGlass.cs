using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HourGlass : StudentItem
{
    public PlayerPositionController playerPositionController;

    // Update is called once per frame
    void Update() { }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        playerPositionController.CallCoroutine(-2f, gameObject);
        Destroy(gameObject);
    }
}
