/*Emma's notes! (Logged 06.01.20; Copied to .md 07.21.20)

*****************************
***Point of "Virtual Baby"***
*****************************
Virtual baby was designed as a way to tap into how parenthood changes
perception of threat in the world. It was a way to threaten infants 
and see how parents perceive those threats, without endangering or
stressing anybody too much. In the game, subs will see a crawling infant 
near an active road with cars. The goal of parents is twofold: 

1. make sure their infant doesn't get hit 
2. find someone to help repair their car

These goals are intentionally at odds with one another to also see
how parents are dividing their attention. The main DVs in this experiment 
are car-detection RT and estimated mph. The IV is parenthood vs. no-children adults.



****************
***Navigation***
****************
Here is some important information about the relevant files for this project.

'DBConnect': 
Connects to the web-hosted database and logs data collection
This is the *most important* script for collecting data, so please read the details in the file



*************************************
***Bits and Boops (EMs dev habits)***
*************************************

-Other than what I have done in Unity, I don't really know any C#. So I do my best to notate my methods
If you are already familiar with C#....please forgive me.

-Whenever I add script to scenes or objects, I add it to a folder in the hierarchy called "functions"
(found in the default left panel)

-The baby object is equipped with a fairly detailed skeleton, and can move
in more complex ways than is currently being used

-I uploaded the "standard assets" package (free and available from Unity Store) because
it simplified some of the elements that I wanted to use. However, there are a lot of things
in this package that I did NOT use. I need to clean this stuff out.

-How to change the baby's crawl speed: you will find this in the Nav Mesh Inspector

-Remember to make most objects (e.g., the player, the baby, the car) kinematic and affected by gravity

-I tried to keep the hierarchies (default left panel) as similar as possible across the scenes. 
Changing them (e.g., unnesting or adding new parents) can have pretty big effects on how 
game objects interact and if they are accessible, so please be careful here.



**********************
***Progress Updates***
**********************
06.01.20    X continue working on listing all variables and logging them in 'DBConnect'
                    X keystrokes (car waving, baby pickup)
                    X mph entries
                    X pickup baby location
                    X car sequence (beginning, in-transit, end)
                    X car speed
                    X trial number
            X assign SceneManager values so 'AdvanceScene' is accurate
            X Create main menu scenes (before actual gameplay)
            X add animation to pick up baby
            X add narrative scene to explain instructions
            X add a "try it now {space]" instructions with counter for the tutorial

06.09.20    X add a millisecond timer at some point (add script at beginning to start timer, then every other occasion samples)
            X add restriction so score only increases if car is visible
            X add qualtrics survey to end of game
            X add second narrative to instruct for babyscene
            X add broken car prop on the side of the road

06.12.20    X add baby pick up animation
            X add script to trigger pick up animation (has to include transforming position/rotation to baby object)
            X fix the car loop
            X add mph question after each car
            X go back and find way to make it WITHOUT replacement on the random speed list
            X refresh the mph after each loop
            X find way to have idling player without an animation
            X add explanation for blanket and chair in the narrative
            X add delay timer to the car loop

06.13.20    X the NavigatonAI and the PickUpBaby script look at odds with one another, but baby cant crawl without NavAI?
            X add "move to baby" to the pickupbaby script
            X replace car loop in the narrative introductions
            X make the panel objects uniform across scenes (including functions!)
            X fix animator for baby with new animations

06.17.20    X adjust pickup animation rate and timing 
            X maybe turn off the pickup bool once baby is seated
            X add "clear scorevalue to advance scenes and just use the scorekeeper script for both car tutorial and car scene
            X need to adjust the score keeper so that they cant press space when car is at origin
            - add halo (instead of box) to parentPOV object (to prevent them from awkwardly rotating around baby) 
            X (dialogue.cs) adjust script so that there isnt a need to specify two repeat/continue buttons (using if/thens in the starting class)

06.18.20    X figure out why mouse is not available in the narrative instructions
            X in the workbench add another colomn for trial and speed
            X info from neutralscenes (data logging) is not moving over to datalogging
            X need to figure out why dbconnect is inserting a million times
06.19.20
            X DBConnect is recording the correct response/events now, but it wont insert it 
            X I think there is a trial lag between actual input and recorded input
            X something about the block names mess with the SQL query. Should try removing INT  within string

06.21.20    - add beginning/end of picking up baby eventtimes (like for how it is with cars)
            X adjust the trees so that it is properly blocking cars starting/stopping path
            X for some reason it is not recording the final trial estimated speed (does it need a new db.insert()?)
            X the pick up baby animation doesnt complete after the initial starting sitting position
            
06.22.20    X double check toy objects in the scene and their correct placement
            X add in informed consent
            X make the informed consent actually readable!
            X add in final scene with hyperlink and thanks
            X Make sure "Do not consent" actually exits the study
            - figure out why mouse is not visible in last scene
            X adjust display logic in qualtrics survey
            X tree problem needs adjustment
            X start new block code on each scene
            - it doesnt move the 1st randomized trial over to list 2 (instead it moves the first randomized one)
06.23.20    X after talking with mike
                    X "experimenter"
                    X hand that pops up to indicate waving someone down
                    X cars >> cars flagged
                    X make mph number validated (and above 0)
                    X qualtrics SES ("occupation outside the home...")
                    X birth date (instead of age)
                    X qualtrics get more standards Qs from steven (or mike)
                    X add events for when the baby is past the road and past the blanket
                    X thorough debrief script in qualtrics ("would you like results" at the end; Tamar has moderated zoom before)
                    X look into thesis to see how they compensated adults (T-shirt and bib)


06.30.20    X add additional column to track baby location then modofy "Cross threshold script"
            X dbconnect.Insert() is not recording the last item because it is happening at the exact same 
                time (maybe earlier) than the change it is written next to. this is bad bc it means timing is 
                off (by 1) on everything

07.03.20    - go back and figure out how to put mphinputfield to 0 when subs repeat instructions
            X if baby is stuck, change directions

07.07.20    X dbconnect is not registering B presses on narrative 2 and baby scene
            - edit IRB and send APPENDICES to mike

07.08.20    X narrative 2 datalogging isnt working bc CrossingThreshold conflicts



not integral notes: 
            - clean up the standard asset junk
            - remind mike later about the "international repository of family volunteers" that he mentioned
            - eventually add scripts to github
            - review advancescenes script for clarity
            - figure out scale of road using car model
*/

