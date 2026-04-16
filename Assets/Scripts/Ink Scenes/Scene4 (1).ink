EXTERNAL place_characters(left_character_name, right_character_name)
EXTERNAL change_emotion(emotion, ID)
EXTERNAL remove_character(ID)

{place_characters("","Butler")}
{change_emotion("Neutral", 0)}


Good evening. Welcome to Master Richard’s Reunion Gala. Might I suggest making your way to the Dining Hall? The others have already begun to socialize there. #speaker:Butler

Uh… yeah! Okay, thank you very much. #speaker:YOU

Riche has a butler!? Jeez, and look at the size of this place. What the hell have you been up to, Riche?

{remove_character(0)}


* [Go to the Dining Hall] -> END
