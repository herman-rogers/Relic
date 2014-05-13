using UnityEngine;
using System.Collections;

public class DoorLockElevator : MonoBehaviour {

	public static bool isDoorOpen = false;
	public SceneLoaderOnTouch doorScript;

	void Update( ) {
		doorScript.enabled = isDoorOpen;
	}

}
