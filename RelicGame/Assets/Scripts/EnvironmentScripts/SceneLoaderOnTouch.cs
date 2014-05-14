﻿using UnityEngine;
using System.Collections;
using TouchScript.Gestures;
using System.Collections.Generic;

public class SceneLoaderOnTouch : PressGesture {

	public string sceneName;
	public SceneLoadInfomation.SceneDoor sceneExitPosition = SceneLoadInfomation.SceneDoor.UNDEFINED;
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
		public SceneDoor sceneLoadedFrom = SceneDoor.UNDEFINED;
		static Dictionary< string, DoorInformation[ ] > sceneMap = new Dictionary< string, DoorInformation[ ] >( ) {
			{ "oldLab", new DoorInformation[ ] {
					new DoorInformation( "oldLab2", SceneDoor.RIGHT )
				}
			},
			{ "oldLab2", new DoorInformation[ ] { 
					new DoorInformation( "oldLab", SceneDoor.LEFT ),
					new DoorInformation( "elevatorShaft(Top)", SceneDoor.RIGHT ),
					new DoorInformation( "oldLab3", SceneDoor.LEFT )
				}
			},
			{ "oldLab3",new DoorInformation[ ] {
					new DoorInformation( "oldLab2", SceneDoor.LEFT ) 
				}
			},
			{ "elevatorShaft(Underground)", new DoorInformation[ ] { 
					new DoorInformation( "elevatorShaft(Top)", SceneDoor.UP ),
					new DoorInformation( "oldLab2", SceneDoor.UP ) 
				}
			},
			{ "elevatorShaft(Top)", new DoorInformation[ ] {
					new DoorInformation( "elevatorShaft(Underground)", SceneDoor.DOWN ),
					new DoorInformation( "oldLabExit", SceneDoor.RIGHT ) 
				}
			}
		};

		public enum SceneDoor {
			LEFT,
			RIGHT,
			DOWN,
			UP,
			UNDEFINED
		}

		public SceneLoadInfomation( string previousSceneName, SceneDoor leaveSceneFrom ) {
				this.previousSceneName = previousSceneName;
				this.sceneLoadedFrom = leaveSceneFrom;
		}

		public static DoorInformation[ ] GetSceneMapFor( SceneLoadInfomation sceneName ) {
			try {
				return sceneMap[ sceneName.previousSceneName ];
			} catch( System.Exception ex ) {
				Debug.LogError( "Something happened \n" + ex.Message );
				return sceneMap[ "ERROR!" ];
			}
		}

		public class DoorInformation {
			public string destinationSceneName;
			public SceneDoor sideOfSceneToLoadOn;

			public DoorInformation( string sceneToLoad, SceneDoor sideToEnterFrom ) {
				this.destinationSceneName = sceneToLoad;
				this.sideOfSceneToLoadOn = sideToEnterFrom;
			}
		}
	}
}
