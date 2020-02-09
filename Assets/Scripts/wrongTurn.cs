using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wrongTurn : MonoBehaviour
{
    // Start is called before the first frame update
    statisticsManager GMS;
    public bool isBadTurn;
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
            if(isBadTurn){
                GMS.numWrong++;
            }
            isBadTurn = !isBadTurn;
        }
    }
}
