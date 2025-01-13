INCLUDE globals.ink

-> classroom

=== classroom ===
#speaker:Teacher #portrait:teacher #layout:left #audio:Class_Teacher_1
Teacher: We have a new student here today. Please introduce yourself.  

#speaker:Mimi #portrait:mimi  #layout:right #audio:Class_Teacher_2
MIMI: Hello, I'm MIMI. I just moved here from Earth. It's nice to meet you. 

+ [Mention your hearing impairment]
    ~ social_meter = social_meter - social_meter_decrease
    #audio:Class_Teacher_3
    I have hearing loss so please be patient if I can't understand what you are saying. 
    -> teacher_response

+ [Do not say anything]
    -> teacher_response

=== teacher_response ===
#speaker:Teacher #portrait:teacher #layout:left #audio:Class_Teacher_4
Teacher: Thank you, you can sit there.  

~goal = "talk_to_Miko"

-> END
