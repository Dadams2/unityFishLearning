using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    
    public float moveSpeed = 100f;
    // Update is called once per frame
    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        //Actual Movement
        Vector3 movement = new Vector3(moveX, 0f, moveZ);
        GetComponent<Rigidbody>().velocity = movement * moveSpeed * Time.deltaTime;
    }
}