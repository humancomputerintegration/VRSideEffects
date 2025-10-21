using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandRetargeting : MonoBehaviour
{
    public enum Retargeting_mode {Hand, Environment};
    // public enum RetargetingTypes {Forward, Left, Up, None};
    // public RetargetingTypes currentRetargeting;
    public float angle = 5;  
    float last_world_angle = 0;  
    public Global_Holder gh;
    public Retargeting_mode retargeting_mode = Retargeting_mode.Hand;

    // Update is called once per frame
    void Update()
    {  
        // float retargetingProportion = Mathf.Clamp(Vector3.Distance(realTip.transform.position,resetPosition.transform.position)/distResetProp, 0, 1);
        
        // Vector3 retargetingVector = retargetingProportion * retargetingMagnitude * getVector(currentRetargeting);
        // this.transform.localPosition = Vector3.zero;
        // this.transform.position += retargetingVector;    

        if(retargeting_mode == Retargeting_mode.Hand){
            
            Vector3 heightVector = gh.resetPoint_v.transform.up.normalized;

            Vector3 distToOrigin = gh.tip_r.transform.position - gh.resetPoint_v.transform.position;
            Vector3 handHeight = Vector3.Project(distToOrigin,heightVector);
            
            Vector3 handProjectedPlane = distToOrigin - handHeight;

            Vector3 forwardVector = handProjectedPlane.normalized;
            Vector3 leftVector = Vector3.Cross(forwardVector,heightVector).normalized;

            float distMagnitude = handProjectedPlane.magnitude;
            Vector3 dist_hand_tip = gh.tip_r.position - gh.hand_r.transform.position;

            this.transform.position = gh.resetPoint_v.transform.position - dist_hand_tip + handHeight + distMagnitude * forwardVector * Mathf.Cos(angle * Mathf.Deg2Rad) + distMagnitude * leftVector * Mathf.Sin(angle * Mathf.Deg2Rad);

        }else{

            gh.environment.transform.RotateAround(gh.resetPoint_v.transform.position, Vector3.up, angle - last_world_angle);
            last_world_angle = angle;
        }
        
        //this.transform.position = gh.hand_r.transform.position -distMagnitude * forwardVector * Mathf.Cos(angle * Mathf.Deg2Rad) - distMagnitude * leftVector * Mathf.Sin(angle * Mathf.Deg2Rad);
    }


    // Vector3 getVector(RetargetingTypes rt){
        
    //     if(rt == RetargetingTypes.Up){ return -resetPosition.transform.up;}
    //     else if(rt == RetargetingTypes.Left){ return resetPosition.transform.right}
    //     else if(rt == RetargetingTypes.Forward){ return -resetPosition.transform.forward;}
    //     else { return Vector3.zero;}

    // }
}
