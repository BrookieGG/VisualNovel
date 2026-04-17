EXTERNAL place_characters(left_character_name, right_character_name)
EXTERNAL change_emotion(emotion, ID)
EXTERNAL remove_character(ID)

# sfx:chatting

{place_characters("Brad", "Viktor")}
{change_emotion("Neutral", 0)} {change_emotion("Neutral", 1)}

Quick! Help us settle a debate. What’s the bigger flex, doing your own stunts in over a dozen high octane world famous action movies or killing some animal that can barely survive on its own? #speaker:Brad

How could you possibly think a white rhino can barely survive on its own? They are vicious beasts that would tear you limb from limb. #speaker:Viktor


{change_emotion("Angry", 0)}
Oh come onnnnn… Viktor, they’re endangered for a reason man! That's like… nature's way of telling you it’s easy as hell to kill them. At least hunt something that doesn’t just die on its own. It’s kind of embarrassing. Unlike being an action movie superstar like me… Brad Chiles. #speaker:Brad

You are a moron, and a fool. There is nothing more thrilling than hunting down a wild beast. You may do all these stunts, nothing more than playing pretend really, whereas I do these things for real. I have killed more tigers and elephants than the days you’ve been alive. #speaker:Viktor

{change_emotion("Neutral", 0)}

Oh man… you really have no idea how hard it is to be me. It’s like… crazy to me that you really think that. I could shoot a stupid big cat blindfolded. #speaker:Brad

They continue to argue while pushing right past you. They didn’t even acknowledge your existence.

{remove_character(0)}
{remove_character(1)}


Oh my god… I need to get out of here. This is crazy. #speaker:You

-> END