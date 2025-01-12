INCLUDE globals.ink
VAR social_meter = 0
VAR classmate_name = "Alex"
VAR classmate_name_capital = "ALEX"
VAR player_input = ""

-> home

=== home ===
Dad: Welcome home. How was school? #speaker:Dad  #portrait:dad  #layout:right #audio:Class_Miko_1
  

Mimi: Good enough. How was work?  #speaker:Mimi    #portrait:mimi  #layout:right #audio:Class_Miko_2

Dad: We just did safety and orientation stuff today. Tomorrow I’ll start working in the lab properly. It looks like I’ll have to be on the early shift for the next few months so you’ll have to take care of your own breakfast.
 #speaker:Dad  #portrait:dad  #layout:right #audio:Class_Miko_1
Mimi: That’s fine, I’m a big girl.  #speaker:Mimi    #portrait:mimi  #layout:right #audio:Class_Miko_4

Dad: Do you want anything in particular? We have to put in our grocery order tonight. #speaker:Dad  #portrait:dad  #layout:right #audio:Class_Miko_4

+ [Could you get some baking powder?]
    Mimi: Could you get some baking powder?  #speaker:Mimi    #portrait:mimi  #layout:right #audio:Class_After_3
    -> Dad_answer

+ [Could you get some baking soda?]
    Mimi: Could you get some baking soda? #speaker:Mimi    #portrait:mimi  #layout:right #audio:Class_Miko_4
    -> Dad_answer

=== Dad_answer ===
Dad: Are you going to take up baking? #speaker:Dad  #portrait:dad  #layout:right #audio:Class_Miko_4

Mimi: No one of my classmates missed her order. She’s trying to make a birthday cake. #speaker:Mimi    #portrait:mimi  #layout:right  #audio:Class_Miko_4

Dad: Ok I’ll put that in. #speaker:Dad  #portrait:dad  #layout:right #audio:Class_Miko_4

-> END