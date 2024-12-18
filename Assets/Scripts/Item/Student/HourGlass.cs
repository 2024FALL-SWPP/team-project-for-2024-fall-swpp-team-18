using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HourGlass : StudentItem
{
    private GameObject player;
    private PlayerPositionController playerPositionController;

    public static GameObject FindInChildren(GameObject parent, string targetName)
    {
        // Parent의 이름이 일치하면 반환
        if (parent.name == targetName)
            return parent;

        // 자식들을 재귀적으로 검색
        foreach (Transform child in parent.transform)
        {
            GameObject result = FindInChildren(child.gameObject, targetName);
            if (result != null)
                return result;
        }

        // 찾지 못하면 null 반환
        return null;
    }

    protected override void Start()
    {
        base.Start();
        player = GameObject.Find("NewPlayer");
        playerPositionController = FindInChildren(player, "PlayerPosition")
            .GetComponent<PlayerPositionController>();
        if (playerPositionController == null)
            Debug.Log("fail");
    }

    // Update is called once per frame
    void Update() { }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.CompareTag("Player"))
        {
            playerPositionController.CallCoroutine(-2f, gameObject);
            Destroy(gameObject);
        }
    }
}
