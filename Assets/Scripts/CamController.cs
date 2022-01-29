using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{

    public Transform player;
    public ArenaController arena;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }


    void FollowPlayer()
    {
        Vector3 pos = new Vector3(player.position.x/2, player.position.y/2, transform.position.z);
        transform.position = pos;
    }
}
