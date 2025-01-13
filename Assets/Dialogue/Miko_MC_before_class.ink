INCLUDE globals.ink

VAR classmate_name = "Miko"
VAR classmate_name_capital = "MIKO"
VAR player_input = ""

-> classroom

=== classroom ===

-> classmate_interaction

=== classmate_interaction ===
#speaker:Mimi #portrait:mimi #layout:right #audio:Class_Miko_2
MIMI: Hi, I’m Mimi.

CM (whisper): Hi, I'm ****. #speaker:classmate    #portrait:default  #layout:right #audio:Class_Miko_1

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
CM: My name is ****. #audio:Class_Teacher_9

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
#speaker:Teacher #portrait:teacher #layout:left #audio:Class_Miko_3
Teacher: All right, before we get started I just want to remind you that our field trip to the Exterra mine will be happening next week.
#speaker:Teacher #portrait:teacher #layout:left #audio:Class_Miko_4
Teacher: I will hand out permission slips at the end of class so please have your guardian sign them by Friday. 

~goal = "go_home"

-> END