INCLUDE globals.ink

VAR classmate_name = "Alex"
VAR classmate_name_capital = "ALEX"
VAR player_input = ""

-> classroom

=== classroom ===
Teacher: We have a new student here today. Please introduce yourself.  #speaker:Teacher #portrait:teacher #layout:left #audio:Class_Teacher_1

MIMI: Hello, I'm MIMI. I just moved here from Earth. It's nice to meet you. #speaker:Mimi    #portrait:mimi  #layout:right #audio:Class_Teacher_2

+ [Mention your hearing impairment]
    ~ social_meter = social_meter - social_meter_decrease
    I have hearing loss so please be patient if I can't understand what you are saying. #audio:Class_Teacher_3
    -> teacher_response

+ [Do not say anything]
    -> teacher_response

=== teacher_response ===
Teacher: Thank you, you can sit there.  #speaker:Teacher #portrait:teacher #layout:left #audio:Class_Teacher_4

-> classmate_interaction

=== classmate_interaction ===
CM (whisper): Hi, I'm {classmate_name}. #speaker:classmate    #portrait:default  #layout:right #audio:Class_Teacher_6

MIMI: (You try to recall the classmate's name.) #input_required:name #speaker:Mimi    #portrait:mimi 

{ player_input == classmate_name:
    MIMI: Nice to meet you, {classmate_name}. #input_required:name #speaker:Mimi    #portrait:mimi  #layout:right #audio:Class_Teacher_7
    -> lesson

- else:
    MIMI: Sorry, could you repeat that? #input_required:name #speaker:Mimi    #portrait:mimi  #layout:right #audio:Class_Teacher_8
    ~ social_meter = social_meter - social_meter_decrease
    MIMI: Sorry, could you repeat that?
    
    -> classmate_repeating1
}

=== classmate_repeating1 ===
CM: My name is {classmate_name}. #audio:Class_Teacher_9

MIMI: (You try to recall the classmate's name.) #input_required:name  #speaker:Mimi    #portrait:mimi  #layout:right
(You try to recall the classmate's name.)
{ player_input == classmate_name:
    MIMI: Okay! Nice to meet you, {classmate_name}. #audio:Class_Teacher_10
    -> lesson

- else:
    MIMI: I'm so sorry, could you repeat that one more time?    #speaker:Mimi    #portrait:mimi  #layout:right #audio:Class_Teacher_11
    ~ social_meter = social_meter - social_meter_decrease
    -> classmate_repeating2
}

=== classmate_repeating2 ===
CM: MY NAME IS {classmate_name_capital}! What is your problem?? #speaker:classmate    #portrait:default  #layout:right #audio:Class_Teacher_12

+ [Try to say the name again]
    -> say_name

+ [Fake a smile and pretend to have understood the name]
    -> akward_silence

=== say_name ===
MIMI: Sorry, now I understood. Nice to meet you {classmate_name}. #speaker:Mimi    #portrait:mimi #audio:Class_Teacher_13
-> lesson

=== akward_silence ===
MIMI: Okay.. nice to meet you..#speaker:Mimi    #portrait:mimi #audio:Class_Teacher_14
-> lesson

=== lesson ===
Teacher: Today I will... #speaker:Teacher #portrait:teacher #audio:Class_Teacher_15

Teacher: Here are the permission slips for next week's field trip to the mines. Please have your parents sign them and bring them back before the trip. #audio:Class_Teacher_16

~ goal = "talk_to_miko"

-> END






