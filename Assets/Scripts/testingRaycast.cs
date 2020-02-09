using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testingRaycast : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject found;
    void Start()
    {
        Debug.Log("setup good");
        found = FindAt(transform.position, Vector3.back);
        if(found != null){
            Debug.Log("back " + found.ToString());
        }
        found = FindAt(transform.position, Vector3.forward);
        if(found != null){
            Debug.Log("forward " + found.ToString());
        }
        found = FindAt(transform.position, Vector3.left);
        if(found != null){
            Debug.Log("left " + found.ToString());
        }
        found = FindAt(transform.position, Vector3.right);
        if(found != null){
            Debug.Log("right " + found.ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    GameObject FindAt(Vector3 pos, Vector3 direction) {
        RaycastHit hit;
        // cast a ray downwards with range = 0.5 which is always where wall will be
        if (Physics.Raycast(pos, direction, out hit, 0.5f)){
            return hit.collider.gameObject;
        } else {
            return null;
        }
    }
}
