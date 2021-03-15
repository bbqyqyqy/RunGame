using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManage : MonoBehaviour
{
    public Road road1;
    public Road road2;             //这里保留了两个路径
    public int roadCount = 2;      //生成的第几个路径用于确定位置
 
    public GameObject[] roads;    //供随机生成的不同路径Prefabs;
 
    public static RoadManage _instance;
    void Awake()
    {
        _instance = this;
        
        
    }
    
    public void GenerateRoad()
    {
        roadCount++;
        int type = Random.Range(0, roads.Length);
        GameObject newRoad = Instantiate(roads[type], new Vector3(-8, 1, roadCount * 115 - 90), Quaternion.identity) as GameObject;
        road1 = road2;
        road2 = newRoad.GetComponent<Road>();
        Debug.Log("road gener");
    }
}