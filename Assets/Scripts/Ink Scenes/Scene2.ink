EXTERNAL place_characters(left_character_name, right_character_name)
EXTERNAL change_emotion(emotion, ID)
EXTERNAL remove_character(ID)
EXTERNAL place_center_object(name)
EXTERNAL wait(seconds)
EXTERNAL remove_center_object()


    You grab your phone off your nightstand.
    ~ place_center_object("phone")
    ~ wait(3.0)
    
    A Reminder pops up on your phone.
    
    ~ remove_center_object()
    ~ wait(2.0)
    
    I guess I better start getting ready.

    -> END