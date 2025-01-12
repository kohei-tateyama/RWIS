INCLUDE globals.ink

VAR isFirstTime = true

Hello there! #speaker:Dr. Green #portrait:dr_green_neutral #layout:left #audio:yoroshiku
-> main

=== main ===
{isFirstTime: **** | How} are you {isFirstTime: **** | feeling} today?
{isFirstTime:
    + [Huh?]
        I couldn't hear you clearly
        Can you say that again?#portrait:dr_green_happy
        ~ isFirstTime = false
        -> main
    + [Yes...?]
        It seems you don't hear me #portrait:dr_green_sad
        Good bye. #speaker:Dr. Green #portrait:dr_green_neutral
        -> END
- else:
    + [Happy]
        I’m glad you’re still happy! #portrait:dr_green_happy
        -> next_section
    + [Sad]
        That’s unfortunate to hear. I hope things get better soon. #portrait:dr_green_sad
        -> next_section
}

=== next_section ===
- Don't trust him, he's <b><color=\#FF1E35>not</color></b> a real doctor! #speaker:Ms. Yellow #portrait:ms_yellow_neutral #layout:right #audio:yoroshikugirl

Well, do you have any more questions? #speaker:Dr. Green #portrait:dr_green_neutral #layout:left #audio:yoroshiku
+ [Yes]
    -> main
+ [No]
    Goodbye then!
    -> END
