using UnityEngine;
using System.Collections;
using TouchScript.Gestures;

public class SceneLoaderOnTouch : PressGesture {

	public string sceneName;

	void Start( ) {
		base.Start( );
		StateChanged += shouldChangeScene;
	}

	protected void shouldChangeScene(object sender, TouchScript.Events.GestureStateChangeEventArgs e) {
		switch( e.State ){
		case Gesture.GestureState.Recognized:
			LoadScene( sceneName );
			break;
		case Gesture.GestureState.Began:
			break;
		}
	}

	private void LoadScene( string sceneToLoad ) {
		Application.LoadLevel( sceneToLoad );
	}
}
