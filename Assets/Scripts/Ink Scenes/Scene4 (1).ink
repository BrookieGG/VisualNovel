EXTERNAL place_characters(left_character_name, right_character_name)
EXTERNAL change_emotion(emotion, ID)
EXTERNAL remove_character(ID)

== Doorbell ==

#sound:doorbell
#transition:fade_out
#transition:fade_in_entryway

{place_characters("Butler", "YOU")}
{change_emotion("Neutral", 0)}
{change_emotion("Neutral", 1)}

Good evening. Welcome to Master Richard’s Reunion Gala. Might I suggest making your
way to the Dining Hall? The others have already begun to socialize there. #speaker:BUTLER

Uh… yeah! Okay, thank you very much. (Riche has a butler!? Jeez, and look at the size of 
this place. What the hell have you been up to, Riche?) #speaker:YOU

{remove_character(0)}
{remove_character(1)}

* [Make your way to the Dining Hall] -> DiningHall_Intro

== DiningHall_Intro ==
#sound:footsteps
#transition:fade_out
#transition:fade_in_dining_hall

{place_characters("YOU", "")}
{change_emotion("Uncomfortable", 0)}

The Dining Hall is full of very wealthy looking people that you don’t recognize in the slightest. They’re all loudly chatting and laughing with each other, as if they’re all old friends. You feel extremely out of place and wonder why you’re even here. Before you can finish contemplating whether or not you should just leave, a large, affluent man approaches you. #speaker:NARRATOR

{remove_character(0)}

-> END
