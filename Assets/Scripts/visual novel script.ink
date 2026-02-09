 //in Ink do
 //EXTERNAL place_characters(left_character_name, right_character_name)
 //EXTERNAL change_emotion(emotion, ID)
 //{place_characters("Charcter", "Character 1")}
 //{change_emotion("Angry", 0)} changes the character on the left to be angry
 
 //left = 0, right = 1

EXTERNAL place_characters(left_character_name, right_character_name)
EXTERNAL change_emotion(emotion, ID)
EXTERNAL remove_character(ID)


After a night shift at the office…
Eleven at night to seven in the morning.
Same Room. Same Desk. Same Chair.
You tell yourself you’re used to it by now.
The late hours. The exhaustion.
The cases that never quite leave your head.

 * [Get Out of Bed.] -> Bed

==Bed==
    {place_characters("Character", "Character 1")}
    {change_emotion("Normal",0)}
    I guess I better start getting ready. #speaker:YOU
    -> OutsideMansion1

==OutsideMansion1==
    {remove_character(0)} #speaker:
    It’s been years since you’ve been here. #speaker:
    The house seems larger than you last recall. #speaker:
    You walk up to the door. #speaker:

    -> END
