using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainXP_Manager : Manager
{
    public enum State {Prep, Conditioning, Trials};
    public enum Redir {Bellow, Above,None}
    public enum Visib {EyesOpen, EyesClosed}
    public enum Record {Yes, No}
    //public enum Mitigation {None, Wind, Buttons}
    //public (Redir, Visib, State) current = (Redir.Bellow, Visib.EyesOpen, State.Prep);
    public Record current_record = Record.No; 
    public Redir current_redir = Redir.Bellow; 
    public Visib current_visib = Visib.EyesOpen; 
    public State current_state = State.Prep;
    //public Mitigation mitigation;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {    
            
        

        if(!activated) return;



        if(current_state == State.Prep){

            // gh.random_button.SetActive(false);
            // gh.wind.SetActive(true);

            if(Input.GetKeyDown(KeyCode.Space)){

                current_state = State.Conditioning;
                gh.conditioning_Routine.Beggin();

                if(current_redir == MainXP_Manager.Redir.Bellow){
                    gh.handRetargeting.angle = gh.ideal_retargeting_angle;
                }else if (current_redir == MainXP_Manager.Redir.Above){
                    gh.handRetargeting.angle = gh.ideal_retargeting_angle * 2;
                }else{
                    gh.handRetargeting.angle = 0;
                }                
            }
        }
        else if(current_state == State.Conditioning){
            

            bool change = gh.conditioning_Routine.Routine();
            
            if(change){
                current_state = State.Trials;
                gh.trial_Routine.Beggin();

                // if(mitigation == Mitigation.Buttons){
                //     gh.handRetargeting.angle = 0;
                //     gh.random_button.SetActive(true);
                // }
            }

        }else if(current_state == State.Trials){

            bool change = gh.trial_Routine.Routine();
            if(change){
                current_state = State.Prep;

                // if(current_visib == Visib.EyesOpen){
                //     current_visib = Visib.EyesClosed;
                
                // }
                // else if (current_redir == Redir.Bellow){
                //     current_redir = Redir.Above;
                //     current_visib = Visib.EyesOpen;
                // }
                // else {
                    activated = false;
                    current_state = State.Prep;
                // }
            }
        }
    }
}


