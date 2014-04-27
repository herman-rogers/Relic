using UnityEngine;
using System.Collections;
using TouchScript.Gestures;

public class SceneLoaderOnTouch : ObjectTouchInteractions {

	public string sceneName;

	protected override void ActivateNewEvent( ) {
		Application.LoadLevel( sceneName );
	}
}
