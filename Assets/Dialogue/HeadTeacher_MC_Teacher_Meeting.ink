INCLUDE globals.ink

VAR classmate_name = "Alex"
VAR classmate_name_capital = "ALEX"
VAR player_input = ""

-> classroom

=== classroom ===
#speaker:Headteacher #portrait:headteacher #layout:left #audio:Dean_Meeting_1
Head Teacher: Hello Mimi, I need to get you registered. Normally we would have a parent here to help you.

#speaker:Mimi #portrait:mimi #layout:right #audio:Dean_Meeting_2
MIMI: My Dad has to work early. 

#speaker:Headteacher #portrait:headteacher #layout:left #audio:Dean_Meeting_3
Head Teacher: Oh what does he do for work?

#speaker:Mimi #portrait:mimi #layout:right #audio:Dean_Meeting_4
MIMI: He’s a lab tech at the mine.

#speaker:Headteacher #portrait:headteacher #layout:left #audio:Dean_Meeting_5
Head Teacher: Ok and I see in your registration that you have hearing loss. Do you have a doctor’s note?

#speaker:Mimi #portrait:mimi #layout:left 
MIMI: (You pass over the hearing chart)

#speaker:Headteacher #portrait:headteacher #layout:left #audio:Dean_Meeting_6
Head Teacher: Ok I know we’re on Aulus but you’ll find that this is like any other school. 

#audio:Dean_Meeting_7
(Bell sound rings)

#speaker:Headteacher #portrait:headteacher #layout:right #audio:Dean_Meeting_8
Head Teacher: Oh it’s late, I’ll walk you to your classroom. 

#audio:Dean_Meeting_9
Head: If you need anything, ask me or you can ask Ara, the school president. She’s not here today but you’ll meet her soon. 
~ goal = "go_to_class"
-> END