using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staircase_Manager : Manager
{

    public enum Direction {Ascending, Descending}
    enum State {Prep, Drawing, Reach, Choose, End}


    public int min_Angle_Threshold = 4;
    public int max_Angle_Threshold = 12;

    public int current_ascending;
    public int current_descending;

    int maxRound = 10;
    int roundToTakeIntoAccount = 5;

    (int,int) round = (0,0);

    (bool,int)[] ascending_history ;
    (bool,int)[] descending_history;

    public Direction startDirection;
    public Direction endDirection;

    bool end;

    public bool restart_trigger;

    State currentState = State.Prep;
    float drawing_time = 0;
    float drawing_duration = 7;

    float reach_time = 0; 
    float reach_duration = 1f;
    int round_trip_nb = 5; //aller-retours * 2
    int reach_cpt = 0;

    // Start is called before the first frame update
    void Start()
    {
        endDirection = startDirection == Direction.Descending ?  Direction.Ascending : Direction.Descending;

        ascending_history = new (bool,int)[maxRound];
        descending_history = new (bool,int)[maxRound];

        current_ascending = min_Angle_Threshold;
        current_descending = max_Angle_Threshold;

        round = (0,0);
    }

    // Update is called once per frame
    void Update()
    {   

        if(!activated){
            gh.eightShape.deactivate();
            return;
        }

        if(restart_trigger) {
            restart_trigger = false;
            Start();
        }

        gh.handRetargeting.angle = getCurrentAngle();

        if(currentState == State.Prep){
            if(Input.GetKeyDown(KeyCode.Space)) {
                currentState = State.Drawing;
                drawing_time = 0;
                gh.eightShape.activate();
            }
        }
        else if(currentState == State.Drawing){
            drawing_time += Time.deltaTime;

            if(drawing_time > drawing_duration) {
                gh.eightShape.deactivate();
                currentState = State.Reach;
                reach_time = 0;
                reach_cpt = -1;
            }

        }else if (currentState == State.Reach){
            
            reach_time += Time.deltaTime;
            
            if(reach_cpt < reach_time /  reach_duration ){
                gh.regularBeep.Play();
                reach_cpt++;
            }

            if(reach_cpt == round_trip_nb * 2){
                currentState = State.Choose;
            }
        
        }else if (currentState == State.Choose){
            
                if((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.Z))){
                    Debug.Log(round.Item1 + " " + round.Item2 + " " + getCurrentAngle());


                //similar
                    if(Input.GetKeyDown(KeyCode.A)){
                        getCurrentHistory()[round.Item1] = (true,getCurrentAngle());
                        update_current_angle(Mathf.Min(getCurrentAngle() +1, max_Angle_Threshold));

                    }

                    //different
                    if(Input.GetKeyDown(KeyCode.Z)){
                        getCurrentHistory()[round.Item1] = (false,getCurrentAngle());

                        update_current_angle(Mathf.Max(getCurrentAngle() -1, min_Angle_Threshold));
                        
                    }

                    if(round.Item2 == 0) {
                        round.Item2 = 1;
                        currentState = State.Drawing;
                        drawing_time = 0;
                        gh.eightShape.activate();

                    }else{
                        if(round == (maxRound -1, 1)){
                            print_res();
                            currentState = State.End;
                            
                        }else{
                            round = (Mathf.Min(maxRound-1,round.Item1+1),0);
                            currentState = State.Drawing;
                            drawing_time = 0;
                            gh.eightShape.activate();

                        }                
                    } 
                }
        }
    }

    int getCurrentAngle(){

        return (getCurrentDirection() == Direction.Ascending)? current_ascending : current_descending;
    }

    (bool,int)[] getCurrentHistory(){

        return (getCurrentDirection() == Direction.Ascending)? ascending_history : descending_history;
    }

    Direction getCurrentDirection(){
        return (round.Item2 == 0) ? startDirection : endDirection;  
    }

    void print_res(){
        Debug.Log("Ascending results: ");
        print_table_res(ascending_history);
        Debug.Log("Descending results: ");
        print_table_res(descending_history);

        float avg = 0;
        for(int i = maxRound - roundToTakeIntoAccount + 1; i < maxRound; i++){
            avg += ascending_history[i].Item2;
            avg += descending_history[i].Item2;
        }

        foreach((bool,int) item in new (bool,int)[]{ascending_history[maxRound-1], descending_history[maxRound-1]}  ) 
            if(item.Item1)
                avg += Mathf.Min(item.Item2 +1, max_Angle_Threshold);
            else
                avg += Mathf.Max(item.Item2 +2, min_Angle_Threshold);

        avg /= 10;
        Debug.Log("Average = " + avg);
        gh.ideal_retargeting_angle = avg;
    }

    void print_table_res((bool,int)[] res){
        string s = "| ";
        foreach((bool, int) items in res){
           s += items.Item2 + " : " + ((items.Item1 == true) ? "sim" : "dif") + " | ";
        }
        Debug.Log(s);
    }

    void update_current_angle(int angle){
        if(getCurrentDirection() == Direction.Ascending){
            current_ascending = angle;
            
        }else{
            current_descending = angle;
        }
    }
}
