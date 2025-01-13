INCLUDE globals.ink

-> home

=== home ===
Dad: Welcome home. How was school? #speaker:Dad  #portrait:dad  #layout:right #audio:Home_Dinner_1
  

Mimi: Good enough. How was work?  #speaker:Mimi    #portrait:mimi  #layout:right #audio:Home_Dinner_2

Dad: We just did safety and orientation stuff today. Tomorrow I’ll start working in the lab properly. #speaker:Dad  #portrait:dad  #layout:right #audio:Home_Dinner_3

#audio:Home_Dinner_4
Dad: It looks like I’ll have to be on the early shift for the next few months so you’ll have to take care of your own breakfast.
 #speaker:Dad  #portrait:dad  #layout:right 
 
Mimi: That’s fine, I’m a big girl. #speaker:Mimi    #portrait:mimi  #layout:right #audio:Home_Dinner_5

Dad: Do you want anything in particular? We have to put in our grocery order tonight. #speaker:Dad  #portrait:dad  #layout:right #audio:Home_Dinner_6

+ [Could you get some baking powder?]
    Mimi: Could you get some baking powder? #speaker:Mimi   #portrait:mimi  #layout:right #audio:Home_Dinner_7
    -> Dad_answer

+ [Could you get some baking soda?]
    Mimi: Could you get some baking soda? #speaker:Mimi  #portrait:mimi  #layout:right #audio:Home_Dinner_8
    -> Dad_answer

=== Dad_answer ===
Dad: Are you going to take up baking? #speaker:Dad  #portrait:dad  #layout:right #audio:Home_Dinner_9
Mimi: No, one of my classmates missed her order. She’s trying to make a birthday cake. #speaker:Mimi  #portrait:mimi  #layout:right #audio:Home_Dinner_10

Dad: Ok I’ll put that in. #speaker:Dad  #portrait:dad  #layout:right #audio:Home_Dinner_11

~goal = "tbc"

-> END