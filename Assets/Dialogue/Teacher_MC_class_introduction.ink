VAR social_meter = 0
VAR classmate_name = "Alex"
VAR classmate_name_capital = "ALEX"
VAR player_input = ""

-> classroom

=== classroom ===
Teacher: We have a new student here today. Please introduce yourself.

MC: Hello, I'm MC. I just moved here from Mars. It's nice to meet you.

+ [Mention your hearing impairment]
    ~ social_meter = social_meter - 1
    I have hearing loss so please be patient if I can't understand what you are saying.
    -> teacher_response

+ [Do not say anything]
    -> teacher_response

=== teacher_response ===
Teacher: Thank you, you can sit there.

-> classmate_interaction

=== classmate_interaction ===
CM (whisper): Hi, I'm {classmate_name}.

MC: (You try to recall the classmate's name.) #input_required:name

{ player_input == classmate_name:
    MC: Nice to meet you, {classmate_name}.
    -> lesson

- else:
    MC: Sorry, could you repeat that?
    ~ social_meter = social_meter - 1
    -> classmate_repeating1
}

=== classmate_repeating1 ===
CM: My name is {classmate_name}.

MC: (You try to recall the classmate's name.) #input_required:name
{ player_input == classmate_name:
    MC: Okay! Nice to meet you, {classmate_name}.
    -> lesson

- else:
    MC: I'm so sorry, could you repeat that one more time?
    ~ social_meter = social_meter - 1
    -> classmate_repeating2
}

=== classmate_repeating2 ===
CM: MY NAME IS {classmate_name_capital}! What is your problem??

+ [Try to say the name again]
    -> say_name

+ [Fake a smile and pretend to have understood the name]
    -> akward_silence

=== say_name ===
MC: Sorry, now I understood. Nice to meet you {classmate_name}.
-> lesson

=== akward_silence ===
MC: Okay.. nice to meet you..
-> lesson

=== lesson ===
Teacher: Today I will...

Teacher: Here are the permission slips for next week's field trip to the mines. Please have your parents sign them and bring them back before the trip.

~ playEmote("exclamation")

-> END