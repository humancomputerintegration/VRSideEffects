using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandConstantRedirection : MonoBehaviour
{

    public GameObject hand;
    public GameObject hmd;
    float armMaxLength = 0.3f;
    public Vector3 maxRedirection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = hand.transform.position + maxRedirection *  Vector3.Distance (hand.transform.position, hmd.transform.position)/armMaxLength;
    }
}
