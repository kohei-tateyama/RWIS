INCLUDE globals.ink
VAR isGoingToGate = 0
VAR social_meter = 0
-> start

=== start ===
-> spaceport_scene

=== spaceport_scene ===
#speaker:Narration
[You and Dad are at the spaceport, standing near a boarding gate.]

// ~ PlaySound("announcement")
#speaker:Announcement
Final call for the outer moon loop. All passengers please come to gate ***teen.

#speaker:Mimi    #portrait:Mimi  #layout:right
What did they announce?

#speaker:Dad    #portrait:Dad  #layout:right
That’s not for our flight

#speaker:Announcement
The shuttle flight to Aulus will begin boarding in 5 minutes.  Please proceed to gate 15.

#speaker:Dad    #portrait:Dad  #layout:right
Our flight will board in 5 minutes we should go to the gate. 
+ [Yes]
    ~ isGoingToGate = 1
    -> finish

+ [No]
    -> finish
=== finish ===

-> END