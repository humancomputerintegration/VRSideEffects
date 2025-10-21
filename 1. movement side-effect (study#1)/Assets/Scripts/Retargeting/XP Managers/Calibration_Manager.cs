using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calibration_Manager : Manager
{
    Vector3 target_Pos = Vector3.zero;
    Vector3 reset_Pos = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        gh = GetComponent<Global_Holder>();
        
        
    }

    // Update is called once per frame
    void Update()
    {

        if(!activated)return;

        // if(Input.GetKeyDown(KeyCode.Space)){
        //     gh.environment.transform.position = gh.environmentAttach.transform.position;
        //     gh.environment.transform.rotation = gh.environmentAttach.transform.rotation;
        // }

        // if(Input.GetKeyDown(KeyCode.A)){
        //     gh.targetPoint_r = gh.tip_r.transform.position;
        // }

        // if(Input.GetKeyDown(KeyCode.Z)){
        //     gh.resetPoint_r = gh.tip_r.transform.position;
        // }


        if(Input.GetKeyDown(KeyCode.A)){
            reset_Pos = gh.laser_Point.position;
            position_environment();

        }

        if(Input.GetKeyDown(KeyCode.Z)){
            target_Pos = gh.laser_Point.position;
            position_environment();
        }

        if(Input.GetKeyDown(KeyCode.Space)){
            Vector3 dist_reset_tip = gh.tip_r.transform.position - target_Pos;
            Vector3 dist_hand_tip = gh.tip_r.transform.position - gh.hand_readjustment.position;
            //gh.hand_readjustment.position = gh.resetPoint_v.transform.position + dist_reset_tip - dist_hand_tip; 
            gh.hand_readjustment.position = target_Pos - dist_hand_tip; 

            //Debug.Log();
            //gh.resetPoint_r = gh.tip_v.transform.position;
            //(TODO)
        }


        void position_environment(){
            if(reset_Pos != Vector3.zero && target_Pos != Vector3.zero){

                Vector3 dist_env_reset = gh.resetPoint_v.transform.position - gh.environment.transform.position;
                gh.environment.transform.position = reset_Pos - dist_env_reset ;

                GameObject go = new GameObject("Go");
                go.transform.position = reset_Pos;

                go.transform.forward = gh.resetPoint_v.transform.forward;
                gh.environment.transform.parent = go.transform;

                go.transform.LookAt(target_Pos);
                
                gh.environment.transform.parent = null;
                GameObject.Destroy(go);
                
                gh.targetPoint_v.transform.position = target_Pos;


            }
        }
        

    }
}
