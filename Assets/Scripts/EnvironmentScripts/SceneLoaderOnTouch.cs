using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneLoaderOnTouch {

	public string sceneName;
	public SceneLoadInfomation.SceneDoor sceneExitPosition = SceneLoadInfomation.SceneDoor.UNDEFINED;
	public static Stack sceneStack;

	void Start( ) {
        //base.Start( );
        //StateChanged += shouldChangeScene;
	}

    //protected void shouldChangeScene(object sender, TouchScript.TouchEventArgs e) {
    //    switch( e.Touches.Contains ){
    //    case Gesture.GestureState.Recognized:
    //        LoadScene( sceneName );
    //        break;
    //    case Gesture.GestureState.Began:
    //        break;
    //    }
    //}

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
		//This is a complicated mess. You're not supposed to understand it as it is.
		static Dictionary< string, DoorInformation[ ] > sceneMap = new Dictionary< string, DoorInformation[ ] >( ) {
			{ "oldLab", new DoorInformation[ ] {
					new DoorInformation( "oldLabHallway", SceneDoor.RIGHT )
				}
			},
			{ "oldLabHallway", new DoorInformation[ ] {
					new DoorInformation( "oldLabHallway", SceneDoor.LEFT ),
					new DoorInformation( "colleagueRoom", SceneDoor.LEFT ),
					new DoorInformation( "oldLab4", SceneDoor.LEFT ),
					new DoorInformation( "stairway", SceneDoor.RIGHT )
				}
			},
			{ "oldLab4", new DoorInformation[ ] {
					new DoorInformation( "oldLabHallway", SceneDoor.DOWN )
				}
			},
			{ "colleagueRoom", new DoorInformation[ ] {
					new DoorInformation( "oldLabHallway", SceneDoor.UP )
				}
			},
			{ "stairway", new DoorInformation[ ] {
					new DoorInformation( "oldLabHallway", SceneDoor.LEFT ),
					new DoorInformation( "oldLab2", SceneDoor.RIGHT )
				}
			},
			{ "oldLab2", new DoorInformation[ ] { 
					new DoorInformation( "stairway", SceneDoor.UP ),
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
			},
            { "Fishing", new DoorInformation[ ] {
					new DoorInformation( "Cabin", SceneDoor.RIGHT )
				}
			},
            { "Cabin", new DoorInformation[ ] {
					new DoorInformation( "CampFire", SceneDoor.RIGHT ) 
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
			public SceneLoadInfomation.SceneDoor sideOfSceneToLoadOn;

			public DoorInformation( string sceneToLoad, SceneDoor sideToEnterFrom ) {
				this.destinationSceneName = sceneToLoad;
				this.sideOfSceneToLoadOn = sideToEnterFrom;
			}
		}
	}
}
