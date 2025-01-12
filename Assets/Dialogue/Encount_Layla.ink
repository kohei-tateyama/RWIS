INCLUDE globals.ink
VAR social_meter = 0
VAR classmate_name = "Alex"
VAR classmate_name_capital = "ALEX"
VAR player_input = ""

-> home

=== home ===
Layla Oh hey Mimi. Did you get what I asked for?
 #speaker:Layla #portrait:layla  #layout:right #audio:Class_Miko_1

Mimi: Yes, here you go.  #speaker:Mimi    #portrait:mimi  #layout:right #audio:Class_Miko_2

Layla: Thank you! I’ll try to make you a cupcake if I have extra batter.
 #speaker:Dad  #portrait:dad  #layout:right #audio:Class_Miko_1
 ~ social_meter = social_meter + 1
Mimi: That’s fine, I’m a big girl.  #speaker:Mimi    #portrait:mimi  #layout:right #audio:Class_Miko_4

Layla: I asked for baking powder not baking soda. I guess it will work if I make a lemon cake. I think I still have lemon juice from last time. 
 ~ social_meter = social_meter - 1
 #speaker:Dad  #portrait:dad  #layout:right #audio:Class_Miko_4

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