using UnityEngine;
using System.Collections;

public class buildingCoordDB : MonoBehaviour {

	public Vector3 getInsideCoords(string building)
	{
		switch (building)
		{
			case "testHouseInterior": return new Vector3(0,-3,0);
			case "pokemonCenterInterior": return new Vector3(-1, -6, 0);
			default: return new Vector3(0,0,0);
		}
	}

	public Vector3 getOutsideCoords(string building)
	{
		switch (building)
		{
		case "testHouseInterior": return new Vector3(-6,-1,0);
		case "pokemonCenterInterior": return new Vector3(5, -1, 0);
		default: return new Vector3(0,0,0);
		}
	}
}
