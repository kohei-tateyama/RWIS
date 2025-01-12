INCLUDE globals.ink
VAR pause = 2
VAR social_meter = 0
-> outer_moon_loop

=== outer_moon_loop ===
Announcement: Final call for the outer moon loop. All passengers please come to gate 12. #speaker:Announcement #audio:staticy_announcement #layout:center

//=== conversation ===
Mimi: What did they announce? #speaker:Mimi #portrait:mimi_neutral #layout:left
Dad: That’s not for our flight. #speaker:Dad #portrait:dad_neutral #layout:right

Announcement: The shuttle flight to Aulus will begin boarding in 5 minutes. Please proceed to gate 15. #speaker:Announcement #audio:boarding_announcement #layout:center
~ pause(2)

Dad: Our flight will board in 5 minutes. We should go to the gate. #speaker:Dad #portrait:dad_neutral #layout:right

-> END