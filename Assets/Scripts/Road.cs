using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    
    private Transform player;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (player.position.z > transform.position.z + 100){
            RoadManage._instance.GenerateRoad();
            Destroy(this.gameObject);
        }
            
        
    }
}
