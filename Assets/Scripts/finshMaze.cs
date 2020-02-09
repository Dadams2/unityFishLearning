using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finshMaze : MonoBehaviour
{
    // Start is called before the first frame update
    statisticsManager GMS;
    void Awake()
    {
        GMS = GameObject.Find("statisticsManager").GetComponent<statisticsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player"){
            Destroy(other.gameObject);
            GMS.interationNum++;
        }
    }
}
