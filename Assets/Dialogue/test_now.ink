INCLUDE globals.ink

=== outer_moon_loop ===
Announcement: Final call for the outer moon loop. All passengers please come to gate 12. #speaker:Announcement #audio:staticy_announcement #layout:center
-> conversation

=== conversation ===
Mimi: What did they announce? #speaker:Mimi #portrait:mimi_neutral #layout:left
+ [What did they announce?]
    Dad: That’s not for our flight. #speaker:Dad #portrait:dad_neutral #layout:right
    -> boarding_announcement

=== boarding_announcement ===
Announcement: The shuttle flight to Aulus will begin boarding in 5 minutes. Please proceed to gate 15. #speaker:Announcement #audio:boarding_announcement #layout:center
Dad: Our flight will board in 5 minutes. We should go to the gate. #speaker:Dad #portrait:dad_neutral #layout:right
-> END
