# VR side-effects

This repository contains the source code used in the research publication "VR Side-Effects" from the [Human-Computer Integration Lab](https://lab.plopes.org/) at the University of Chicago.

The Unity code for each study is respectively placed in the "1. movement side-effect (study#1)" and "2. memory side-effect (study#2)"


# Movement side-effect (study#1)

The Unity code can be found inside the "1. movement side-effect (study#1)" folder. 

The project runs with Unity 2020.3.25f1, and Steam VR.

## Apparatus

This project requires a HTC vive headset, at least 2 HTC Vive base stations, and two HTC Vive hand trackers (one for calibration, one attached to the user's palm). 

We also used a table covered by an anti-infrared material, and two laser pointers directed toward the table, replicated in the virtual environment. You might have to change the virtual environment to adapt to a different table dimension or laser point placement. 

## Code organization

You will need to run the scene placed in "Assets/Scenes/Scene".

## Calibration

Run the Unity scene. 

First, you will need to calibrate the virtual environment to align it with the real environment. To do so, you will register the position of both laser points by placing the light of the calibration HTC Vive tracker under each of the laser points, and pressing either A or Z key on the keyboard (respectively for the closer or further dot). 

Then, you will need to calibrate the position of the user's hand in the environment. To do so, ask the participant (wearing the hand-tracking HTC Vive tracker) to place their right index tip on the farther laser dot, and then press space. Then you will automatically leave the calibration mode.

## Additional controls

You can switch between staircase and measurement modes simply by clicking on the "stair" button in the Unity game view. 
In measurement mode, you can select the condition (redirection and eye-openness variables), from "ExperimentScripsHolder/MainXPManager" script. 


# Memory side-effect (study#2)

This project being essentially based on the reconstruction of a specific room, it can most likely only be used for reference, and you might have to reconstruct a replication of your own target environment. 

The Unity code can be found inside the "2. memory side-effect (study#2)" folder. 

The project runs with Unity 2020.3.25f1.

## Apparatus

This project requires an Oculus headset and two controllers. 

## Code organization

You will need to run the scene placed in "Assets/Scenes/Scene".

## Calibration

Two empty game objects (SetPointLeft and SetPointRight) are located in the virtual scene under "Environment/Table". Their corresponding positions must be identified with markers in the real environment, in order to align virtual and real spaces.

To do so, as you run the scene, you will have to place the flat circular part of the controller on the table. Then, you will have to align each marker (first left, then right) to the end of the line present on the backside of the controller, and press the A controller button.



# Citing

When using or building upon this device in an academic publication, please consider citing as follows:

ACM Reference Format: Cheymol, A. and Lopes, P. 2025. VR Side-Effects: Memory & Proprioceptive Discrepancies After Leaving Virtual Reality. Proceedings of the 38th Annual ACM Symposium on User Interface Software and Technology (New York, NY, USA, 2025). 1-9. DOI:https://doi.org/10.1145/3746059.3747596

# Contact

For any questions about this repository, please contact antonin.cheymol@gmail.com


