using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorRotation : MonoBehaviour
{
    SkinnedMeshRenderer skinnedMesh;  
    public Transform environmentTracker;  
    // Start is called before the first frame update
    void Start()
    {
        skinnedMesh = GetComponent<SkinnedMeshRenderer>();
    }


    // Update is called once per frame
    void Update()
    {
        float angle = Vector3.Angle(transform.forward, environmentTracker.up); 
        float ratio = Mathf.Clamp(angle/90,0,1);
        Material m = skinnedMesh.material;
        m.color = new Color(ratio,0,0);
        skinnedMesh.material = m;
    }
}