using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Redirection : MonoBehaviour
{
    public enum RetrackMode {left, Right};

    [Header("Parameters")]
    public RetrackMode retrackMode;
    public float retargetingBound;

    [Header("GameObjects")]
    [Space(15)]
    public GameObject indexReal;
    public GameObject indexVirtual;

    [Space(15)]
    public GameObject handVirtual;
    public GameObject handReal;

    [Space(15)]
    public GameObject objVirtual;
    public GameObject objLeft;
    public GameObject objRight;

    [Space(15)]
    public Mesh mesh;
    


    // Start is called before the first frame update
    void Start()
    {
        
        
    }


    // Update is called once per frame
    void Update()
    {
        GameObject objReal = ((retrackMode == RetrackMode.left)? objLeft : objRight);

        Vector3 distToReal = (objReal.transform.position - indexReal.transform.position) ;
        Vector3 distToRealNormal = new Vector3 (
            distToReal.x / objReal.transform.lossyScale.x,
            distToReal.y / objReal.transform.lossyScale.y,
            distToReal.z / objReal.transform.lossyScale.z
        );

        Vector3 virtualIdealTipPos = new Vector3 (
                objVirtual.transform.position.x - distToRealNormal.x * objVirtual.transform.lossyScale.x,
                objVirtual.transform.position.y - distToRealNormal.y * objVirtual.transform.lossyScale.y,
                objVirtual.transform.position.z - distToRealNormal.z * objVirtual.transform.lossyScale.z
        );
            
        float ratio = 1 - Mathf.Clamp01(distToGO(indexReal.transform.position, objReal)/retargetingBound);

        Vector3 redirection = (indexReal.transform.position -  virtualIdealTipPos) * ratio;
        Vector3 virtualTipPos = handReal.transform.position - redirection;

        handVirtual.transform.position = virtualTipPos - (handVirtual.transform.position - indexVirtual.transform.position);
    
    }

    float distToGO(Vector3 pos, GameObject go){
        return go.GetComponent<Collider>().bounds.SqrDistance(pos);
    }
    
   
}
