INCLUDE globals.ink
VAR social_meter = 0
VAR classmate_name = "Alex"
VAR classmate_name_capital = "ALEX"
VAR player_input = ""

-> classroom

=== classroom ===
Head Teacher: Hello Mimi, I need to get you registered. Normally we would have a parent here to help you.  #speaker:Teacher #portrait:teacher #layout:left #audio:Class_Teacher_1

MIMI: My Dad has to work early. #speaker:Mimi    #portrait:mimi  #layout:right #audio:Class_Teacher_2

Head Teacher: Oh what does he do for work?  #speaker:Teacher #portrait:teacher #layout:left #audio:Class_Teacher_1

MIMI: He’s a lab tech at the mine. #speaker:Mimi    #portrait:mimi  #layout:right #audio:Class_Teacher_2

Head Teacher: Ok and I see in your registration that you have hearing loss. Do you have a doctor’s note?  #speaker:Teacher #portrait:teacher #layout:left #audio:Class_Teacher_1

MIMI: (You pass over the hearing chart)

Head Teacher: Ok I know we’re on Aulus but you’ll find that this is like any other school. 

Bell sounds: #audio:Class_Teacher_2

Head Teacher: Oh it’s late, I’ll walk you to your classroom. If you need anything, ask me or you can ask Ara, the school president. She’s not here today but you’ll meet her soon. #speaker:Mimi    #portrait:mimi  #layout:right #audio:Class_Teacher_2

//~ playEmote("exclamation")

-> END