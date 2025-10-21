using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
using System.IO;
using System.Globalization;

public class Trial_Routine : MonoBehaviour
{   
    enum State {ComeBack, InReach}
    public float reach_duration = 3;
    public float back_duration = 2;
    float timer = 0;
    State state = State.InReach;
    float near_threshold = 0.02f;


    //int required_in_row = 2;
    //int current_row = 0;
    int max_trials = 10;
    int current_trials;
    bool await;

    List<List<(float, Vector2)>> recordings;
    List<(float, Vector2)> current_recording;
    Global_Holder gh;
 

    // Start is called before the first frame update
    public void Beggin()
    {
        
        current_trials = 0;
        recordings = new List<List<(float, Vector2)>>();
        gh = GetComponent<Global_Holder>();

        await = true;
        
    }


    // Update is called once per frame
    public bool Routine()
    {   
        //Debug.Log(state + " " + timer);

        if(await){
            if(Input.GetKeyDown(KeyCode.Space)){
                await = false;
                startReach();
            }
        }
        

        else if(state == State.InReach){

            timer += Time.deltaTime;
            current_recording.Add((timer,projected_tip_dot()));

            if(timer > reach_duration){
                            
                current_trials++;
                gh.regularBeep.Play();
                recordings.Add(current_recording);

                Debug.Log("Current trial: " + current_trials);

                if(current_trials == max_trials){
                    print_results();
                    return true;
                }else{
                    startBack();
                }
            }
        }else{

            timer += Time.deltaTime;

            if(timer > back_duration){
                startReach();
            }

        }
        
        return false;
    }

    void startReach(){
        state = State.InReach;
        timer = 0;
        gh.longBeep.Play();

        current_recording = new List<(float, Vector2)>();
        current_recording.Add((timer,projected_tip_dot()));

    }

    void startBack(){
        state = State.ComeBack;
        timer = 0;
        gh.regularBeep.Play();
    }

    float dist_tip_dot(){

        return projected_tip_dot().magnitude;
    }

    Vector2 projected_tip_dot(){
        Vector3 dist_tip_dot = gh.tip_r.transform.position - gh.targetPoint_v.transform.position;

        Vector2 project_dist = new Vector2(Vector3.Project(dist_tip_dot, gh.resetPoint_v.transform.forward).magnitude, 
                                            Vector3.Project(dist_tip_dot, gh.resetPoint_v.transform.right).magnitude);

        return project_dist;
    }

    void print_results(){
        string result = "ParticipantNb; Angle; Visibility; Redirection Type; Trial number; Time; Position.forward; Position.right\n";
        if(gh.mainXP_Manager.current_record == MainXP_Manager.Record.No) result += "No record\n";
        for (int trial = 0; trial < 10; trial++){
            if(trial < recordings.Count){
                foreach((float, Vector2) point in recordings[trial]){
                    result += gh.participant_name + ";" + gh.ideal_retargeting_angle  + ";" + gh.mainXP_Manager.current_visib + ";" + gh.mainXP_Manager.current_redir + ";" + trial + ";" + point.Item1 + ";" + point.Item2.x + ";" + point.Item2.y + "\n";
                }
            }
        }

        result = result.Replace(',','.');
        Debug.Log(result);

        //string path = "Assets/Data Analysis/" + gh.participant_name + "/" + gh.mainXP_Manager.current_visib + "X" + gh.mainXP_Manager.current_redir + " " + System.DateTime.Now.ToString().Replace('/','_') + ".txt";
        string path = "Assets/Data Analysis/" + gh.participant_name + "/" + gh.mainXP_Manager.current_visib + "_" + gh.mainXP_Manager.current_redir + " " + System.DateTime.Now.Month + "_" + System.DateTime.Now.Day + "_" + System.DateTime.Now.Hour + "_" + System.DateTime.Now.Minute + "_" +System.DateTime.Now.Second + ".txt";

        DirectoryInfo d = Directory.CreateDirectory ("Assets/Data Analysis/" + gh.participant_name);
        System.IO.File.WriteAllText(path, result);


    }
}
