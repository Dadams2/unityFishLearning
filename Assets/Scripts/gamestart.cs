using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gamestart : MonoBehaviour
{
    // Start is called before the first frame update
    public Button but = null;
    public GameObject fish;

    public InputField ifield;
    void Start()
    {
        but.GetComponent<Button>().onClick.AddListener(delegate {makeplayer();});
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void makeplayer() {
        GameObject player = GameObject.Find("Fish1(Clone)");
        if(player == null){
            Instantiate(fish, new Vector3(-4.5f,0.5f,-4.5f), transform.rotation);
            double rate = float.Parse(ifield.text);
            GameObject curp = GameObject.Find("Fish1(Clone)");
            curp.GetComponent<learning>().a = rate;
        }
        else {
            Destroy(player);
        }
    }
}
