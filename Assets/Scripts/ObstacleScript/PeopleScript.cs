using UnityEngine;

public class PeopleScript : MonoBehaviour
{

    public float lifetime = 10f; // 사람이 자동으로 삭제되기까지의 시간



    private void Start()
    {   
        
        // 일정 시간이 지나면 사람을 제거
        Destroy(gameObject, lifetime);
    }


}
