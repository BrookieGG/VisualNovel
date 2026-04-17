EXTERNAL place_characters(left_character_name, right_character_name)
EXTERNAL change_emotion(emotion, ID)
EXTERNAL remove_character(ID)

{place_characters("", "Butler")}
{change_emotion("Neutral", 1)}

Before you can move, the butler appears before everyone holding a large microphone. 

Good evening everyone! Welcome to Master Richard’s Reunion Gala! I am pleased to announce that Master Richard has arrived! #speaker:Butler

{remove_character(1)}


-> END