using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global_Holder : Manager
{
    
    [Header("Parameters")]
    public string participant_name = "rand";
    public float ideal_retargeting_angle;

    [Header("Points")]
    public GameObject targetPoint_v;
    public GameObject resetPoint_v;

    [Header("Hand")]
    public Transform tip_r;
    public Transform hand_readjustment;

    public GameObject hand_v;
    public GameObject hand_r;

    [Header("Environment")]
    public GameObject environment;
    public Transform environmentAttach;

    //[Header("For Mitigating")]
    //public GameObject wind;
    //public GameObject random_button;

    [Header("Tracker")]
    public Transform laser_Point;

    [Header("Audio")]
    public AudioSource regularBeep;
    public AudioSource longBeep;

    [Header("Scripts")]
    public HandRetargeting handRetargeting;
    public EightShape eightShape;
    [HideInInspector] public Trial_Routine trial_Routine;
    [HideInInspector] public Conditioning_Routine conditioning_Routine;
    [HideInInspector] public MainXP_Manager mainXP_Manager;
    [HideInInspector] public Calibration_Manager calibration_Manager;
    [HideInInspector] public Staircase_Manager staircase_Manager;

    List<Manager> managers;

    //HIDDEN
    //[HideInInspector] public Vector3 targetPoint_r;
    //[HideInInspector] public Vector3 resetPoint_r;

    // Start is called before the first frame update
    void Start()
    {
        trial_Routine = GetComponent<Trial_Routine>();
        conditioning_Routine = GetComponent<Conditioning_Routine>();
        calibration_Manager = GetComponent<Calibration_Manager>();
        staircase_Manager = GetComponent<Staircase_Manager>();
        mainXP_Manager = GetComponent<MainXP_Manager>();

        managers = new List<Manager>(){calibration_Manager, staircase_Manager};
        activateCalib();
    }

    // Update is called once per frame
    public void activateCalib() {activateManager(calibration_Manager); }
    public void activateStairCase() {activateManager(staircase_Manager); }
    public void activateMainXP() {activateManager(mainXP_Manager); }

    void activateManager(Manager manager){
        foreach(Manager m in managers) {
            if(m != null) m.activated = false;
        }
        manager.activated = true;
    }
}

public class Manager : MonoBehaviour {

    [HideInInspector]
    public bool activated; 

    [HideInInspector]
    public Global_Holder gh;

    void Awake()
    {
        gh = GetComponent<Global_Holder>();
    }
}
