INCLUDE globals.ink
VAR social_meter = 0
VAR classmate_name = "Alex"
VAR classmate_name_capital = "ALEX"
VAR player_input = ""
VAR isGoingToGate = 0
-> classroom

=== classroom ===
Teacher: We have a new student here today. Please introduce yourself.  #speaker:Teacher #portrait:teacher #layout:left

MIMI: Hello, I'm MIMI. I just moved here from Mars. It's nice to meet you. #speaker:Mimi    #portrait:mimi  #layout:right

+ [Mention your hearing impairment]
    ~ social_meter = social_meter - 1
    I have hearing loss so please be patient if I can't understand what you are saying.
    -> teacher_response

+ [Do not say anything]
    -> teacher_response

=== teacher_response ===
Teacher: Thank you, you can sit there.  #speaker:Teacher #portrait:teacher #layout:left

-> classmate_interaction

=== classmate_interaction ===
CM (whisper): Hi, I'm {classmate_name}. #speaker:classmate    #portrait:default  #layout:right

MIMI: (You try to recall the classmate's name.) #input_required:name #speaker:Mimi    #portrait:mimi  #layout:right

{ player_input == classmate_name:
    MIMI: Nice to meet you, {classmate_name}. #input_required:name #speaker:Mimi    #portrait:mimi  #layout:right
    -> lesson

- else:
    MIMI: Sorry, could you repeat that? #input_required:name #speaker:Mimi    #portrait:mimi  #layout:right
    ~ social_meter = social_meter - 1
    -> classmate_repeating1
}

=== classmate_repeating1 ===
CM: My name is {classmate_name}.

MIMI: (You try to recall the classmate's name.) #input_required:name  #speaker:Mimi    #portrait:mimi  #layout:right
{ player_input == classmate_name:
    MIMI: Okay! Nice to meet you, {classmate_name}.
    -> lesson

- else:
    MIMI: I'm so sorry, could you repeat that one more time?    #speaker:Mimi    #portrait:mimi  #layout:right
    ~ social_meter = social_meter - 1
    -> classmate_repeating2
}

=== classmate_repeating2 ===
CM: MY NAME IS {classmate_name_capital}! What is your problem?? #speaker:classmate    #portrait:default  #layout:right

+ [Try to say the name again]
    -> say_name

+ [Fake a smile and pretend to have understood the name]
    -> akward_silence

=== say_name ===
MIMI: Sorry, now I understood. Nice to meet you {classmate_name}. #speaker:Mimi    #portrait:mimi
-> lesson

=== akward_silence ===
MIMI: Okay.. nice to meet you..#speaker:Mimi    #portrait:mimi
-> lesson

=== lesson ===
Teacher: Today I will... #speaker:Teacher #portrait:teacher

Teacher: Here are the permission slips for next week's field trip to the mines. Please have your parents sign them and bring them back before the trip.

~ playEmote("exclamation")

-> END