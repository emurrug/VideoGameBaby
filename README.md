Emma's notes! (Updated 02.11.2021)

*****************************
***Point of "Virtual Baby"***
*****************************
Virtual baby was designed as a way to tap into how parenthood changes
perception of threat in the environment. It was a way to threaten infants 
and see how parents perceive those threats (without endangering or
stressing anybody too much). In the game, subjects will see a crawling infant 
near an active road with cars. The goal of parents is twofold: 

1. make sure their infant doesn't get hit 
2. find someone to help repair their car

These goals are intentionally at odds with one another to also see
how parents are dividing their attention. The main DVs in this experiment 
are car-detection RT and estimated mph. The IV is parenthood vs. no-children adults.


The P.I. on this project is Emma Murrugarra (eam422@cornell.edu) working under the supervision
of Michael Goldstein (mhg26@cornell.edu) in the B.A.B.Y. Lab at Cornell University. 

**NOTE:** As of 07.15.21, construction for new versions of Virtual Baby is underway. The new versions will feature immature speech and locomotion stimuli, as well as integrate electrophysiological sensors. See branching file directory for more information. 

****************
***File Navigation***
****************
Here is some important information about the relevant files for this project.

#### **Publicly Available Builds**
This game is being hosted on itch.io (free game-sharing platform). There is a MAC and Windows
build ready to install at the following link: 
https://cornell-baby-lab.itch.io/

This link can be used to see what the game looks like in practice or to run test subjects. 
As of 01.18.2021, the game hosted at this link is released (~40 subjects have been run so far), but patches are still coming out

#### **Complete Packaged Game File (must be opened in Unity)**
In this folder, there is a single Unity package file of the most recent game version in development. 
When opened in Unity, it will automatically import all the necessary assets (models, animations, 
scripts, dependencies) to recreate the game. Please note that you won't be able to recreate the game 
in Unity from the objects/scripts files below, and you must use this exported package. I have intentionally
left out all unity metadata and many of the scripts/objects included "standard assets". 

This game was designed in Unity Version 2018.4.23f1 (free). Please install and run the game on this version.

#### **Game Objects (includes model and animation files)**
For reference on some of the assets that I used or models that were created (e.g., the virtual baby).
All assets are free and publicly available, or were otherwise crafted by Emma. All crafted
models/animations were done in Blender (free; recommended). 

#### **Game Scripts**
Other than what I have done in Unity, I don't really know any C#. So I do my best to notate my methods
within the script files themselves. If you are already familiar with C#....please forgive me...

Here is a brief overview of the script contents:
* Gameplay trigger mechanics 
* Dialogue Scripts (what participants read)
* Data Logging (through an AWS-hosted server) 

I have written a "Scripts at a Glance" markdown file. ***This may be a helpful starting place for those
who want to recycle the code for their own use.***


*************************************
***Bits and Boops (E.M.'s dev habits)***
*************************************
If things seem wonky, it's because I'm a noob to both C# and Unity. Here is a record of some 
notes I thought to include when it comes to making changes within the game files. 

#### **"Under Construction" Progress Updates**

*02.11.21: changed the compensation in the informed consent to reflect SONA credits OR Amazon gift card

* 01.27.21 - Running the study online (where timing events are based on fps) is causing some real time variance in carspeeds. This may warrant increasing the distance between stimulus speeds in the future. For now, just make sure to analyze based on true speed (using ms timing).

* 01.14.21 - Three problems: 
  * The car speeds are not being presented at the same rate. The slowest car speed is being presented twice as often as any other speed. UPDATE 01.19.21: The speed was being internally randomized twice per trial- once during the RandomWaitDelay function when determining the speed, and again when selecting which speed was getting removed from the sample list. Additionally, the speed was being initialized twice on start, although this should have no impact in presentation (but it may have messed with which trial was being recorded first). The additional specifications of speeds and speedslist indices have been removed.
	
  * Data logging misses the speed estimate on the 10th trial of block 1 and 2 (it seems like the button isn't acting as a data submission trigger like normal). UPDATE 01.25.21: Changed CarCycle so that there was a time delay (1s) between submitting speed estimate on 10th/20th/30th trial and proceeding to the next scene.

  * Participants are really bad at guessing mph (and some don't even know mph or can't drive), so we decided to change units to a likert scale (very slow - very fast) instead of a free text speed option. UPDATE 01.25.21: the scale object replaced the inputfield object (that way the code didn't need to change much). This impacted code in CarCycle, TutorialCarCycle, and Dialogue (to reset the slider), Narrative 1, NeutralScene, and BabyScene (to record estimated speed). I will add a couple questions into the debrief section to ask about mph/km estimates.

  * Need to add "this ends the tutorial" instructions, so they don't continue. UPDATE 01.25.21: just added "click to continue" as an explicit instruction.


* 10.14.20 - Changes:
  * added code to lock camera (in whatever position it was in) when answering mph prompt (note: do not adjust to reset camera, as this conflicts with "pick up baby" animation)
  * adjusted the instructions to make it ultra clear to use the mouse to move and advance
  * made sure the informed consent was contingent on subject condition (press "n" for nonparent" or "p" for parent" in the second line)
  * added 10 more neutral trials to the end of the baby trials for 30 total trials

NOTE: scripts folders have not yet been updated as of 01.18.21

* 10.13.20 - Something is wrong where the car does not appear as fast as 30-70 mph. Need to reconfigure the speeds to match the appropriate timing. Found out that maxDeltaTime (in the MoveToward() method) corresponds to the units on the X scale (gauged by position). So the car travels 258 units (distance between origin and destination) in 1 sec. Each unit corresponds roughly to one foot; speed at 258 = ~176 mph. BUG FIX UPDATE 10.14.20: new list is updated with appropriate speed conversions. Everything looks so fast now o_o. Also increased the range by 5 sec for timed delay between car loops to make it appear more random.

* 10.13.20 I found out that my datalog is lagging still, such that the given event is a row behind each timestamp. BUG FIX UPDATE 10.14.20: fixed by adding second delay to event outputs. Should not impact statistical analysis, but does mean there is a consistent 1sec delay on every row.

* Mike was able to trigger hand-waving in tutorial before clicking. This locked him out of progression out of the tutorial. BUG-FIX UPDATE 09.06.20: added condition to prevent waving until tutorial dialogue prompts it.

* 08.25.20 - Dynamic linking (DLL imports) are currently not supported by WebAssembly (this is a Unity-end bug). This is a problem for WebGL and creates the JavaScript error "To use dlopen, you need to use Emscripten's linking support..." in (at least) the chrome browser. When I compile the plugins, they "should be authored to link statically to the project instead"

I went through the plugins and tested for problematic DLLS, and found out it was MySql.Data.dll. This is a problem because it is fundamentally a .NET class that cannot be converted into JavaScript (which makes it playable within a web browser). I would have to rewrite my DBconnect code, likely to point to a JS file which is readable by WebGL. I will consult Mike, but I would rather persue the MacOS and Windows platforms only at this time (but I must find a way to get around the Mac security concerns)


#### **Compiling***
* For the Windows platform, Inno Setup Compiler software was used so that I could distribute the game installer (rather than the .exe and data files as a .zip).

* For the Mac platform, it is necessary to adjust the permissions while in the developing Mac desktop. Otherwise the file contents associated with the app won't be recognized. To do this, I entered the executed the following from the cmd terminal: "chmod a+x CarWavingGame.app/Contents/MacOS/*"


* A reoccuring problem whenever I try to build across platforms is that the "System.Windows.Forms.dll is not allowed to be included or could not be found". I originally fixed this by switching the Scripting Runtime Version and API Compatibility Level to .NET 4.x within the Player Build Settings.

* When compiling for a WebGL platform, I needed to also copy the Mono.Posix and Mono.WebBrowser DLL from
"C:\Program Files\Unity\Hub\Editor\2018.4.23f1\Editor\Data\MonoBleedingEdge\lib\mono\gac"



#### **Asset Organization** 
* I uploaded the "standard assets" package because it simplified some of the elements that I wanted to use. 
However, there are a lot of things in this package that I did NOT use. This *should* be cleaned out, but I have done this yet.

* In the export package, there are many assets that are no longer being used. These are from previous
drafts of the project. These also need to be carefully cleaned out. When in doubt, see the "Game Scripts"
and "Game Objects" file for the most up-to-date assets in use. 

* Whenever I add script to scenes or objects, I add it to a folder in the hierarchy called "Functions"
(found in the "Investigater" panel -- default left side)

* I tried to keep the hierarchies (default left panel) as similar as possible across the scenes. 
Changing them (e.g., unnesting or adding new parents) can have pretty big effects on how 
game objects interact and if they are accessible, so please be careful here.


#### **If objects look strange...**
* Please note that this game is best ran on a single device to ensure best accuracy in stimulus presentation and speed recording.

* How to change the baby's crawl speed: you will find this in the Nav Mesh Inspector

* Remember to make most objects (e.g., the player, the baby, the car) kinematic and affected by gravity

#### **Misc. considerations for the future**
* The baby object is equipped with a fairly detailed skeleton, and can move
in more complex ways than is currently being used. This may be useful for future projects.

* Wind, shadowing, textures, car colors etc. are all controlled for because they may impact perception or judgmenets of threat.

* Participants lock onto one position, so it may be useful to have cars coming from both directions to increase attentional demands

* It is worth thinking about non-baby objects that might also move around and demand attention to add in as a control

