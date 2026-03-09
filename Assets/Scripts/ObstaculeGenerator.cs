
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ObstaculeGenerator : MonoBehaviour
{
    public GameObject[] Obstacules;
    List<GameObject> activeObstacules = new();
    int cuantityOfObstacules = 10;
    public Transform player;
    Vector3 pInit = new(30,0,0.9f);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    float lastObst, intervalo = 2;
    bool spawning = true;
    void Start()
    {
        ListBase();
    }

    // Update is called once per frame
    void Update()
    {
    }
    void FixedUpdate()
    {
        if(spawning){
            lastObst +=Time.fixedDeltaTime;
            SpawnObs();
        }
    }
    
    void SpawnObs()
    {
        if(lastObst<=intervalo)return;
        lastObst=0;
        var obs = LastRandomObstacule();
        obs.transform.position = pInit;
        obs.SetActive(true);
    }
    GameObject NewObs()
    {
        Vector3 direction = player.position - pInit;
        direction.y = 0f;
        int actualObs = Random.Range(0,Obstacules.Length);
        var newOb = Instantiate(Obstacules[actualObs],pInit,new Quaternion());
        activeObstacules.Add(newOb);
        return newOb;
        
    }
    
    void ListBase()
    {
        for(int i = 0; i < Obstacules.Length; i++)
        {
            var newOb = Instantiate(Obstacules[i],pInit,new Quaternion());
            newOb.SetActive(false);
            activeObstacules.Add(newOb);
        }
        
    }
    public void SetSpawning(bool newVal)
    {
        spawning = newVal;
    }
    GameObject LastRandomObstacule()
    {
        GameObject obs = null;
        var listObs = activeObstacules.Where(x=>!x.activeInHierarchy);
        if(listObs.Count()>0)
            obs = activeObstacules[Random.Range(0,listObs.Count())];
        if (obs == null)
        {
            obs = NewObs();
        }
        return obs;
    }
}
