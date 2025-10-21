using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Place_mismatch_experiment_manager : MonoBehaviour
{
    enum Phase {align, sphere, select}
    public GameObject sphere; 
    public GameObject controller;
    public GameObject environment;
    public List<GameObject> spheres; 
    List<GameObject> sphereOrder = new List<GameObject>();
    int current_sphere = 0;
    
    Phase phase = Phase.align;

    bool last_sphere = false;
    bool done = false;

    //int sphereNb = 30;
    //int sphereCpt = 0;

    public Text text;
    public bool end_displayed = false;

    float maxDistSphere = 0.2f;

    int indexLastSphere = 0;

    Vector3 alignPointLeft = Vector3.zero;
    Vector3 alignPointRight = Vector3.zero;
    public GameObject controllerRefPoint;
    public GameObject deskLeftRefPoint;
    public GameObject deskRightRefPoint;


    // Start is called before the first frame update
    void Start()
    {
        //newSpherePosition();
        int total = spheres.Count;


        for(int i = 0; i < total; i++)
        {
            int index = Random.Range(0, spheres.Count);
            sphereOrder.Add(spheres[index]);
            spheres.RemoveAt(index);
        }


    }

    // Update is called once per frame
    void Update()
    {

        if(end_displayed){
                text.text = "Please come close to the door and wait for the experimenter\n" +  
                "(you should NOT removemove the headset for the moment)";
        }else{
                text.text = "";
        }

            
            
        if(phase == Phase.align){
            if(OVRInput.GetDown(OVRInput.Button.One) || Input.GetKeyDown(KeyCode.Space)){

                if(alignPointLeft == Vector3.zero){
                    alignPointLeft = controllerRefPoint.transform.position;

                    // GameObject test  = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    // test.transform.position = alignPointLeft;
                    // test.transform.localScale = new Vector3(0.1f,0.1f,0.1f);


                }else{

                    alignPointRight = controllerRefPoint.transform.position; 
                    
                    // GameObject test  = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    // test.transform.position = alignPointRight;
                    // test.transform.localScale = new Vector3(0.1f,0.1f,0.1f);

                    Vector3 dist_env_right = deskRightRefPoint.transform.position - environment.transform.position;
                    environment.transform.position = alignPointRight - dist_env_right;

                    GameObject go = new GameObject("Go");
                    go.transform.position = alignPointRight;

                    go.transform.forward = deskRightRefPoint.transform.right;
                    environment.transform.parent = go.transform;

                    go.transform.LookAt(alignPointLeft);
                    
                    environment.transform.parent = null;
                    GameObject.Destroy(go);

                    phase = Phase.sphere;
                    newSpherePosition();
                    
                }
            }

        }else if(phase == Phase.sphere){

            if(Input.GetKeyDown(KeyCode.Space)){
                end_displayed = !end_displayed;
            }


            if(Input.GetKeyDown(KeyCode.Tab)){
                newSpherePosition();
            }

            if(maxDistSphere > Vector2.Distance( new Vector2(controller.transform.position.x, controller.transform.position.z), 
                                                    new Vector2(sphere.transform.position.x, sphere.transform.position.z))) {
                
                newSpherePosition();
                
                

                // if(last_sphere){

                //     if(!done){
                //         sphere.transform.localPosition = positionSet[indexLastSphere];
                //     }else{
                //         newSpherePosition();
                //     }
                //     //
                    
            
                    
                // }else{
                    //hideSphere();
                    //phase = Phase.select;

                //}    
            }
        }else{
            
        }
    }

    void newSpherePosition(){
        
        
        sphere.transform.position = sphereOrder[current_sphere].transform.position; 
        current_sphere = (current_sphere + 1)%sphereOrder.Count;
        
    }

    void hideSphere (){
        sphere.transform.position = new Vector3 (-100,-100,-100);
    }
}
