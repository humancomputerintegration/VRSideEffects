using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingSquarePositioning : MonoBehaviour
{
    public GameObject square;
    int nbH = 8; 
    int nbV = 12; 

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < nbH; i++){
            for (int j = 0; j < nbV; j++){
                
                GameObject go = GameObject.Instantiate(square);
                go.transform.rotation = square.transform.rotation;
                go.transform.localScale = square.transform.lossyScale;
                go.transform.parent = this.transform;
                go.transform.localPosition = square.transform.localPosition - new Vector3 (0,0,1) * (i* 0.53f) + new Vector3 (1,0,0) * (j * 0.53f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
