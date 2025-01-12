
-> start

=== start ===
-> spaceport_scene

=== spaceport_scene ===
[You and Dad are at the spaceport, standing near a boarding gate.]

Announcement: Final call for the outer moon loop. All passengers please come to gate ***teen.
Mimi: What did they announce?
#speaker:Mimi    #portrait:Mimi  #layout:right
Dad: That’s not for our flight
#speaker:Dad    #portrait:Dad  #layout:right
Announcement: The shuttle flight to Aulus will begin boarding in 5 minutes.  Please proceed to gate 15.

Dad: Our flight will board in 5 minutes we should go to the gate. 
#speaker:Dad    #portrait:Dad  #layout:right

* [Go to the first gate.] -> first_gate
// * [Wait and try to hear the announcement again.] -> missed_announcement

=== first_gate ===
[You and Dad go to the first gate.]

MC (spoken): Excuse me, is this the gate for Ceres?

Agent: No, the Ceres gate is gate 16. You should pay more attention to the announcements. Are you deaf or what?

MC: Actually, yeah.

* [Ignore the comment and leave.] -> to_ceres_gate
* [Respond to the comment.] -> confront_agent

=== confront_agent ===
MC: That’s a bit rude. You should be more considerate.

Agent: Oh, uh... sorry. Gate 16 is that way.

-> to_ceres_gate

=== to_ceres_gate ===
[You and Dad are walking to gate 16. Another announcement plays.]

Agent 2: Oh, are you here for the Ceres flight? We’ve been calling you for the last five minutes.

MC: Sorry, we–

Agent 2: Just get on board.

* [Apologize.] -> boarding_scene
* [Say nothing and move on.] -> boarding_scene

=== boarding_scene ===
[You and Dad go through the gate and board the flight. A travel montage shows you arriving at your new home.]


-> END