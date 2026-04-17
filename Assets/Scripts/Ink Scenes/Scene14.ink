EXTERNAL place_characters(left_character_name, right_character_name)
EXTERNAL change_emotion(emotion, ID)
EXTERNAL remove_character(ID)

You push past Brad, who seems to be the one that screamed and look in horror 
ahead. Richard is on the table, knife stabbed into his head. He is dead. 

Suddenly all of the guests come running in one by one, shocked at what they see.

{place_characters("Brad", "")}
{change_emotion("Angry", 0)}
What… what the hell! #speaker:Brad


{place_characters("", "George")}
{change_emotion("Sad", 1)}
Oh… oh myyyy. #speaker:George
{remove_character(0)}
{remove_character(1)}

{place_characters("Penelope", "")}
{change_emotion("Sad", 0)}
I think I’m going to be sick… #speaker:Penelope
{remove_character(0)}


-> END