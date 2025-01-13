INCLUDE globals.ink

-> start

=== start ===
-> spaceport_scene

=== spaceport_scene ===
[You and Dad are at the spaceport, standing near a boarding gate.] #speaker:narrator 

Announce: Final call for the outer moon loop. All passengers please come to gate ***teen. #speaker:speaker #audio:Spaceport_Dialogue_1

Mimi: What did they announce? #speaker:mimi #portrait:mimi #layout:right #audio:Spaceport_Dialogue_2

Dad: That’s not for our flight. #speaker:dad #portrait:dad #layout:right #audio:Spaceport_Dialogue_3

Announce: The shuttle flight to Aulus will begin boarding in 5 minutes. Please proceed to gate 15. #speaker:speaker #audio:Spaceport_Dialogue_4

Dad: Our flight will board in 5 minutes we should go to the gate. #speaker:dad #portrait:dad #layout:right #audio:Spaceport_Dialogue_5
+ [Yes]
    ~ isGoingToGate = 1
    -> finish

+ [No]
    -> finish

=== finish ===
~class_session="go_to_gate"

-> END

