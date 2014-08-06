using UnityEngine;
using System.Collections;

public class characterLines : MonoBehaviour
{

	public string getLines (string actor, int index)
	{
		if (actor == "testHouseBook") 
		{
			switch (index)
			{
				case 0:
					return "Book: This is your first time reading this book, you illiterate bastard.";
				case 1:
					return "Book: You've already read this book dummy, but since your arms are too short to turn the pages you're fucked.";
				default:
					return "Ran out of lines";
			}
		} 
		else if (actor == "misty") 
		{
			switch(index)
			{
				case 0:
					return "Misty: Oh snap what's good homie? I see you done entered the world of the uDoGz game! It's not even close to being done, but right now you can walk around (without walking through things) and go inside this house! You can even read the book in there! try it out";
				default:
					return "Ran out of lines";
			}
		} 
		else if (actor == "brock") 
		{
			switch(index)
			{
			case 0:
				return "Brock: Yo what's good? I'm Brock.";
			default:
				return "Ran out of lines";
			}
		} 
		else if (actor == "john") 
		{
			switch(index)
			{
			case 0:
				return "John: I'm just here for the beer and the bitches.";
			case 1:
				return "John: Get out of my face you ugly bastard. You don't even have any pokemon. Jackass.";
			case 2:
				return "John: Get the hell out of here!! I'm procrasturbating!";
			case 3:
				return "John: Oh Hyloyn <3 <3 <3";
			default:
				return "Ran out of lines";
			}
		} 
		else if (actor == "xavier") 
		{
			switch(index)
			{
			case 0:
				return "Xavier: Hold up man, *whispers* I'm finna get it in with this hot white nurse.";
			case 1:
				return "Xavier: Hold. The fuck up. I will punch you in the face with a brick.";
			default:
				return "Ran out of lines";
			}
		} 
		else if (actor == "shawn") 
		{
			switch(index)
			{
			case 0:
				return "Shawn: Hello there young boy! Fancy a blow on my whistle?";
			case 1:
				return "Shawn: Bes' get on before ur mum comes along.";
			default:
				return "Ran out of lines";
			}
		} 
		else if (actor == "jeremiah") 
		{
			switch(index)
			{
			case 0:
				return "Jeremiah: Get the hell out of here. You Suck. Noob.";
			case 1:
				return "Jeremiah: Let's play GTA! Come oooooon! Pleeeeeease?!";
			default:
				return "Ran out of lines";
			}
		}
		else if (actor == "jesus") 
		{
			switch(index)
			{
			case 0:
				return "Jesus: I have to start coming here more often.";
			case 1:
				return "Jesus: #TurnUp!!";
			default:
				return "Ran out of lines";
			}
		}
		else if (actor == "adrian") 
		{
			switch(index)
			{
			case 0:
				return "Adrian: I shall enforce the creed of the uDoGz.";
			case 1:
				return "Adrian: We are an unstoppable force for good.";
			case 2:
				return "Adrian: I bet I could kick that guys ass in Halo.";
			default:
				return "Ran out of lines";
			}
		}
		else if (actor == "arnold") 
		{
			switch(index)
			{
			case 0:
				return "Arnold: Why the fuck don't you have a bathroom in this house??";
			case 1:
				return "Arnold: Seriously, I have to piss and you don't have a bathroom. The fuck is this?";
			case 2:
				return "Arnold: I'm gonna piss in your fucking plant when you're not looking.";
			case 3:
				return "Arnold: Now that I think about it, you don't have a bed, or a fridge, you don't even have a fucking kitchen counter. You have music playing but no speakers. Why the fuck do you have a TV that's not on even though you have company, but you don't have a fucking toilet?";
			default:
				return "Ran out of lines";
			}
		}
		else if (actor == "aaron") 
		{
			switch(index)
			{
			case 0:
				return "Aaron: I just wanna see how long this nigga is gonna walk around in circles.";
			default:
				return "Ran out of lines";
			}
		}
		else
			return "";
	}
}
