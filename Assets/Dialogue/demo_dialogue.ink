-> start

=== start ===
-> spaceport_scene

=== spaceport_scene ===
[You and Dad are at the spaceport, standing near a boarding gate.]

// ~ PlaySound("announcement")
Announcement: All passengers for flight 393 to Ceres please come to boarding gate ***teen.

Dad (sign): What did they say?

MC (sign): I think they said to go to the gate? I’m not sure which one though.

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

// ~ PlaySound("announcement")
Agent 2: Oh, are you here for the Ceres flight? We’ve been calling you for the last five minutes.

MC: Sorry, we–

Agent 2: Just get on board.

* [Apologize.] -> boarding_scene
* [Say nothing and move on.] -> boarding_scene

=== boarding_scene ===
[You and Dad go through the gate and board the flight. A travel montage shows you arriving at your new home.]

// ~ PlayMontage("travel_montage")
// ~ EndScene

-> END