using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  Conditioning_Routine : MonoBehaviour
{   

    public float max_interval_duration;
    public float min_interval_duration;
    int trip_cpt;
    public int round_trip_total = 10;
    float last_beep;
    Global_Holder gh;
    bool await = true;


    // Start is called before the first frame update
    public void Beggin()
    {
        await = true;
        trip_cpt = 0;
        last_beep = Time.time;
        gh = GetComponent<Global_Holder>();

    }

    // Update is called once per frame
    public bool Routine()
    {  
        if(await){
            if(Input.GetKeyDown(KeyCode.Space)){
                await = false;
            }
        }else{
            if(nextBeep()){
            
            trip_cpt++;
            
            if(trip_cpt  == round_trip_total * 2 - 1){
                
                return true;

            }else{
                gh.regularBeep.Play();
                last_beep = Time.time;  
            }
            } 
        }
           
        return false;
    }

    bool nextBeep(){
        float decreaseFactor = Mathf.Pow (min_interval_duration/max_interval_duration, ((float)1/(float)((round_trip_total - 1)*2)));
        float currentDuration = max_interval_duration*Mathf.Pow(decreaseFactor, trip_cpt);
        return Time.time > last_beep + currentDuration;
    } 


}
