Here is a list of the scripts included and their functions at a glance: 

# Backend

### DataLogging
*Logs data from each scene.*

One file per each block of data recording. These files reference the events within the scene 
(e.g., space pressed, car loops, etc.), changes their status in a static variable, then pipes
that information into a static class within "CombinedData.cs". These variables are directly 
referenced when updating the form data. To alter what gets recorded on the server, you must start here
then make changes along the whole pipeline.

The pipeline for recording data goes
[SceneSpecific_DataLogging].cs >> CombinedData.cs >> DBConnect

### Advance Scenes
*Loads the next scene (i.e. "block" in the experiement) for most scenes*.*

Here is the list of scenes in order: 
0. **Please Wait** - forces subjects to wait for the experimenter to be ready
1. **Informed Consent** 
2. **Registration** - where subjects log ID and condition #
3. **Narrative1** - first tutorial for how to press SPACE and wave at cars
4. **NeutralScene*** - familiarization test block
5. **Narrative2** - second tutorial for how to press B to pick up baby
6. **BabyScene*** - test block
7. **EndGame** - stops experiment and prompts sub to follow URL to survey link

*Scenes 4 & 6 automatically advance from 3 & 5 respectively and aren't referenced in this script.

### DBConnect
*Interfaces with server database to store data*

This script is written to interface with a MySQL database. There are strong syntax restrictions
so be careful about editting this file unless you know what you are doing. The file can be 
modified to redirect to any MySQL database given the server address, database, id, and password.


### Millisecond Timer
*converts internal clock into ms; allowing me to track RT for the whole game*

### Numerical Validation
*Validation for participant speed estimation responses*





# Gameplay

### Baby

#### Baby Wander 
*Sets baby to wander randomly within a designated range*

(The baby cannot run into toys or get hit by the car.)

#### Crossing Threshold 
*Triggers for when baby is on either the road, blanket, or grass*

#### Pick Up Baby
*coordinating player and baby animations when the baby needs to be picked up*

The baby is animated and has a series of animations that are triggered when the participant presses "B". 
However, the player is not truly animated. The player is just a camera that is set to orient and move towards
the baby object. There are a collection of object meshes that are attached to the player to make this work 
smoothly (acting as hands, arms, return points, etc.). 


### Car

#### Car Cycle
*Controls car speed, direction, and animation*

This script is chiefly in charge of determining the car loop mechanics. However, it also includes the 
triggers for seeing the participant input fields (where they enter their estimated speed), the 
hand waving animation, trigger score updating, and advancing to the next scene.

There are currently 10 car trials per test block (i.e., NeutralScene & BabyScene).

#### Tutorial Car Cycle 
*simplified "Car Cycle"*

### Dialogue

#### Dialogue [1-3] & Informed Consent
*instructions that participants see and read through*

### Player

#### Modded Standard Assets
*collection of scripts pertaining to the first-person camera from "Standard Assets" that I have slightly changed to modify looking direction when the player picks up the baby*

### Score

#### Score Keeper & Tutorial Score Keeper
*ticks up when the subject successfully waves down a car*

The tutorial score keeper is kept seperate because it references TutorialCarCycle instead of CarCycle.
