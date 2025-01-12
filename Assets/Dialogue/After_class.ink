INCLUDE globals.ink
VAR social_meter = 0
VAR classmate_name = "Alex"
VAR classmate_name_capital = "ALEX"
VAR player_input = ""

-> classroom

=== classroom ===
Layla: Hey, Mimi right? I’m Layla.  #speaker:Teacher #portrait:teacher #layout:left #audio:Class_After_1
Layla: Has your family put in your food order yet? #audio:Class_After_2

+ [What’s a food order]
    Mimi: What's a food order? #audio:Class_After_3
    Layla: Every week we put in a food order from the central grocery. Usually we order on Friday but since you’re new I think you can put one in now. #audio:Class_After_4
    Mimi: I don’t think we’ve ordered yet. #audio:Class_After_5

    -> Layla_explanation

+ [No]
    -> Layla_explanation

=== Layla_explanation ===
Layla: I’m trying to make a cake for Sasha’s birthday and I forgot I ran out of baking powder. Could you order it for me? Please please please. #speaker:Teacher #portrait:teacher #layout:left #audio:Class_After_6

+ [OK]

    -> END

+ [What do I need to order?]
~ social_meter = social_meter - 1
    Layla: Baking powder #audio:Class_After_7
    Mimi: OK #audio:Class_After_8
    Layla: Awesome you're the best. #audio:Class_After_9

    -> END