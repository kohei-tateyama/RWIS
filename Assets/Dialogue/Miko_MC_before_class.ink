INCLUDE globals.ink

VAR classmate_name = "Alex"
VAR classmate_name_capital = "ALEX"
VAR player_input = ""

-> classroom

=== classroom ===

#speaker:Miko #portrait:miko #layout:right #audio:Class_Miko_1
Miko: Hi I’m Miko. 
  
#speaker:Mimi #portrait:mimi #layout:right #audio:Class_Miko_2
MIMI: Hi, I’m Mimi.

#speaker:Teacher #portrait:teacher #layout:left #audio:Class_Miko_3
Teacher: All right, before we get started I just want to remind you that our field trip to the Exterra mine will be happening next week.
#speaker:Teacher #portrait:teacher #layout:left #audio:Class_Miko_4
Teacher: I will hand out permission slips at the end of class so please have your guardian sign them by Friday. 

~goal = "go_home"
-> END