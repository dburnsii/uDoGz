using UnityEngine;
using System.Collections;

public class characterLines : MonoBehaviour {

	public string getLines(string actor, int index)
	{
		if (actor == "testHouseBook") {
						switch (index) {
						case 0:
								return "This is your first time reading this book, you illiterate bastard.";
						case 1:
								return "You've already read this book dummy, but since your arms are too short to turn the pages you're fucked.";
						default:
								return "Ran out of lines";
						}
				} else
						return "";
	}
}
