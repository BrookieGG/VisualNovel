EXTERNAL place_characters(left_character_name, right_character_name)
EXTERNAL change_emotion(emotion, ID)
EXTERNAL remove_character(ID)

# sfx:chatting

The Dining Hall is full of very wealthy looking people that you don’t recognize in the slightest. They’re all loudly chatting and laughing with each other, as if they’re all old friends. 
You feel extremely out of place and wonder why you’re even here. Before you can finish contemplating whether or not you should just leave, a large, affluent man approaches you. 

{place_characters("", "George")}
{change_emotion("Neutral", 1)}

Well well, who do we have here? I don’t recognize you, and I recognize everyoneeee who attends Richards’ partiessss. There isn’t a person here whom I don’t have investments innnnn. #speaker:George

The way he drawls his voice on certain words fills you with a small amount of inexplicable rage.

Oh! Uh… I’m Riche’s childhood friend... and to be honest, I haven’t seen Richie in years. This is all a big shock to me. When I last saw him he was… certainly not wealthy. #speaker:You

{change_emotion("Angry", 1)}

Oh reallyyyyy? So you’ve known Richard for a whileeee, have you? I’ve known him for around two yearssss now. Oh, I’m George Banks by the wayyyy. #speaker:George

His voice is actually beginning to make you go insane.

Now I mustttt know. What do you dooo for work? As I’m sure you’ve picked up on, I’m an investorrrr. There isn’t one person at this party whose business I don’t have a share of. #speaker:George

(Remember, your choices matter!)

* [I’m a detective!] -> George_Detective
* [I’d rather not say.] -> George_Refuse

== George_Detective ==

{place_characters("", "George")}
{change_emotion("Neutral", 1)}

Oh… I seeee. That explains your lackkk of proper attire. I assumed you were likely one of Richard’s lesssss wealthy acquaintances based on your outfit but… well I’m sure there’s some reason you’re here aside from your status. I have no reason to continue speaking with you. Good day. #speaker:George

{remove_character(1)}

-> George_Refuse

== George_Refuse ==

{place_characters("", "George")}
{change_emotion("Neutral", 1)}


Oh reallyyyy? Well that’s quite rude of you. I told you my profession and you don’t have the courtesy to do the same? I’ll be going now. #speaker:George

{remove_character(1)}

After talking with the interesting character known as George Banks, you continue to make your way through the dining hall that happens to be three times larger than your own home.

You approach the massive table spanning the entire horizontal axis of the room. It is filled with a variety of fancy foods such as caviar, lobster, and even a gigantic turducken. As you stare at the food in complete disbelief, a thin tall woman approaches you.

{place_characters("", "Penelope")}
{change_emotion("Neutral", 1)}

Have an interest in the food? I had a feeling you’d be the type. Penelope Rothschild, food critic and influencer. #speaker:Penelope

She definitely got right to the point.

Uh… Hello! I’m Riche’s childhood friend. You uh… seem like you know how things work around here and I’m pretty new to this. How does… all this work? #speaker:You

{change_emotion("Happy", 1)}

Oh aren’t you a funny one. This is a dinner party, how else would this possibly work? You eat as little as possible and shame everyone else. #speaker:Penelope

Oh… OH! I uh, just meant I’m not really used to anything this… rich. Also, didn’t you say you were a food critic? What was with that… comment? #speaker:You

{change_emotion("Neutral", 1)}

You really think I eat the slop I critique? Oh my, you have so much to learn. No, my diet consists of three cups of Oolong tea, as well as three cups of Gyokuro Green Tea per day. Anything more and well… that would be ludicrous. #speaker:Penelope

These people are ludicrous.

Yeah… I guess that makes sense. #speaker:You

Well… enjoy the food. As I said, I had a feeling you’d be the type. #speaker:Penelope

{remove_character(1)}

Right on cue, two more people walk right up to you.

The first, a tall man in a suit with a very symmetrical face and perfect sleek black hair. The second has an eyepatch and… a skinned tiger on his back.


-> END

