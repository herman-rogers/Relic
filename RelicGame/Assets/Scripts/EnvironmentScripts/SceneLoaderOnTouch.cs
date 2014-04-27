using UnityEngine;
using System.Collections;
using TouchScript.Gestures;

public class SceneLoaderOnTouch : PressGesture {

	public string sceneName;
	public SceneLoadInfomation.SceneExitUsed sceneExitPosition = SceneLoadInfomation.SceneExitUsed.UNDEFINED;
	public static Stack sceneStack;

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
		if( sceneStack == null ) { 
			sceneStack = new Stack( );
		}
		sceneStack.Push( new SceneLoadInfomation( Application.loadedLevelName, sceneExitPosition ) );
		Application.LoadLevel( sceneToLoad );
	}

	public class SceneLoadInfomation {

		public string previousSceneName = "";
		public SceneExitUsed sceneLoadedFrom = SceneExitUsed.UNDEFINED;

		public enum SceneExitUsed {
			LEFT,
			RIGHT,
			DOWN,
			UP,
			UNDEFINED
		}

		public SceneLoadInfomation( string previousSceneName, SceneExitUsed leaveSceneFrom ) {
			this.previousSceneName = previousSceneName;
			this.sceneLoadedFrom = leaveSceneFrom;
		}
	}
}
