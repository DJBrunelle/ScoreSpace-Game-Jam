using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaController : MonoBehaviour
{
    public int xBounds;
    public int yBounds;

    public GameObject wall;

    void Awake()
    {
        transform.localScale = new Vector2(xBounds*4, yBounds*4);

        Instantiate(wall, new Vector3(xBounds,0,0),Quaternion.identity, transform).transform.localScale = new Vector3(0.1f,1,1);
        Instantiate(wall, new Vector3(-xBounds,0,0),Quaternion.identity, transform).transform.localScale = new Vector3(0.1f,1,1);
        Instantiate(wall, new Vector3(0,yBounds,0),Quaternion.identity, transform).transform.localScale = new Vector3(1,0.1f,1);
        Instantiate(wall, new Vector3(0,-yBounds,0),Quaternion.identity, transform).transform.localScale = new Vector3(1,0.1f,1);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnyBoi()
    {
        
    }
}
