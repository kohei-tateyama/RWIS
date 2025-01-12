INCLUDE globals.ink

-> start

=== start ===
-> spaceport_scene

=== spaceport_scene ===
[You and Dad are at the spaceport, standing near a boarding gate.] #speaker:narrator

Final call for the outer moon loop. All passengers please come to gate ***teen. #speaker:speaker

What did they announce? #speaker:mimi #portrait:mimi #layout:right

That’s not for our flight. #speaker:dad #portrait:dad #layout:right

The shuttle flight to Aulus will begin boarding in 5 minutes. Please proceed to gate 15. #speaker:speaker

Our flight will board in 5 minutes we should go to the gate. #speaker:dad #portrait:dad #layout:right
+ [Yes]
    ~ isGoingToGate = 1
    -> finish

+ [No]
    -> finish

=== finish ===

-> END