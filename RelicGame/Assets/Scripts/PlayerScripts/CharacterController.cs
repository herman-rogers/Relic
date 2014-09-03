using UnityEngine;
using System.Collections;
using Spine;
using TouchScript;
using TouchScript.Gestures;
using System;

public class CharacterController : PressGesture {

	public GameObject monster;
	public Transform rightExit;
	public Transform leftExit;
	public Transform upExit;
	public Transform downExit;

	CharacterAnimations characterAnimations;
	Vector3 monsterPosition;
	float travelDistance;
	float runSpeed;
    NavigationMesh2D navMesh;

	const float playerBodyWidth = 0.7f;
	const float stopWithinRange = 0.01f;
	const float monsterMoveSpeedMax = 1.0f;
	const float monsterMoveSpeedMin = 1.5f;
    const string NAVIGATION_MESH_TAG = "NavigationMesh";

    void Start( ) {
        navMesh = FindNavigationMesh( );
    }

    NavigationMesh2D FindNavigationMesh( ) {

        GameObject[ ] navMeshLookup = GameObject.FindGameObjectsWithTag( NAVIGATION_MESH_TAG );
        ArrayList foundNavMeshes = new ArrayList( );
        foreach( GameObject go in navMeshLookup ) {
            NavigationMesh2D navMesh = go.GetComponent<NavigationMesh2D>( );
            if( navMesh != null ) {
                foundNavMeshes.Add( navMesh );
            }
        }
        if( foundNavMeshes.Count > 1 ) {
            Debug.LogError( "Found multiple navigation meshes! \n" +
                            "There should only be one navigation mesh per scene." );
        } else if( foundNavMeshes.Count == 0 ) {
            Debug.LogWarning( "No navigation meshes found. Creating dummy navigation mesh. \n" + 
                "Check to see if the NavigationMesh is tagged as NavigationMesh." );
            foundNavMeshes.Add( CreateDummyNavigationMesh( ) );
        }
        return foundNavMeshes[ 0 ] as NavigationMesh2D;
    }

    NavigationMesh2D CreateDummyNavigationMesh( ) {
        GameObject dummyNavigation = new GameObject( );
        dummyNavigation.name = "DummyNavigationMesh";
        NavigationMesh2D navMesh = dummyNavigation.AddComponent<NavigationMesh2D>( );
        navMesh.tag = NAVIGATION_MESH_TAG;
        Polygon poly = navMesh.GetOrAddComponent<Polygon>( );
        poly.polygonCorners = new Vector2[ 4 ] {
                new Vector2( -1000.0f, 1000.0f ),
                new Vector2( 1000.0f, 1000.0f ),
                new Vector2( 1000.0f, -1000.0f ),
                new Vector2( -1000.0f, -1000.0f )
            };
        return navMesh;
    } 

	void OnLevelWasLoaded( int level ) {
		try {
			
			SceneLoaderOnTouch.SceneLoadInfomation.DoorInformation[ ] doorInRoom = SceneLoaderOnTouch.SceneLoadInfomation.GetSceneMapFor( 
                                     ( SceneLoaderOnTouch.SceneLoadInfomation )SceneLoaderOnTouch.sceneStack.Pop( ) );
			foreach( SceneLoaderOnTouch.SceneLoadInfomation.DoorInformation door in doorInRoom ) {
				if( door.destinationSceneName == Application.loadedLevelName ) {
					InitiateMonsterPosition( door.sideOfSceneToLoadOn );
				}
			}
		} catch( Exception ex ) {
			Debug.LogWarning( "The previous scene didn't load any information about where the player should start. \n" + ex.Message );
		}
	}

	void InitiateMonsterPosition( SceneLoaderOnTouch.SceneLoadInfomation.SceneDoor doorToMoveTo ) {
		switch( doorToMoveTo ) {
		case( SceneLoaderOnTouch.SceneLoadInfomation.SceneDoor.RIGHT ):
			if( rightExit != null ) {
				SetMonsterXAndYPosition( rightExit.position.x, rightExit.position.y );
			}
			break;
		case( SceneLoaderOnTouch.SceneLoadInfomation.SceneDoor.LEFT ):
			if( leftExit != null ) {
				SetMonsterXAndYPosition( leftExit.position.x, leftExit.position.y );
			}
			break;
		case( SceneLoaderOnTouch.SceneLoadInfomation.SceneDoor.UP ):
			if( upExit != null ) {
				SetMonsterXAndYPosition( upExit.position.x, upExit.position.y );
			}
			break;
		case( SceneLoaderOnTouch.SceneLoadInfomation.SceneDoor.DOWN ):
			if( downExit != null ) {
				SetMonsterXAndYPosition( downExit.position.x, downExit.position.y );
			}
			break;
		}
	}

	void SetMonsterXAndYPosition( float newXPosition, float newYPosition ) {
		this.monster.transform.position = 
			new Vector3( newXPosition, newYPosition, this.monster.transform.position.z );
	}

	void Awake( ) {
		base.Start( );
		characterAnimations = new CharacterAnimations( );
		characterAnimations.InitAnimations( this.gameObject );
		this.StateChanged += StateChangeHandler;
	}

	public void MoveMonster( Vector3 worldCoordinates, CharacterAnimations.AnimationList animation ){
		if( shouldChangeDirectionFacing( worldCoordinates.x ) ) {
			ChangeDirectionFacing( );
		}
		monsterPosition = NewMonsterPosition( worldCoordinates );
		travelDistance = FindDistanceToTravel( );
		StartMovementAnimation( animation );
	}

	Vector3 NewMonsterPosition( Vector3 newDestination ) {
		return new Vector3( newDestination.x, newDestination.y, monster.transform.position.z );
	}

	float FindDistanceToTravel( ) {
		return Vector3.Distance( monster.transform.position, monsterPosition );
	}

	bool shouldChangeDirectionFacing( float positionMovingTowards ) {
		return ( monster.transform.localScale.x > 0.0f &&
		    monster.transform.position.x < positionMovingTowards ) ||
			( monster.transform.localScale.x < 0.0f &&
			 monster.transform.position.x > positionMovingTowards );
	}

	void StateChangeHandler( object sender, TouchScript.Events.GestureStateChangeEventArgs e ) {
		switch( e.State ) {
		case Gesture.GestureState.Recognized:
                Vector3 worldCoords = ConvertScreenToWorldSpace( ScreenPositionAsVector3( ) );
                if( navMesh.CanMoveTo( worldCoords ) ) {
                    this.MoveMonster( worldCoords , CharacterAnimations.AnimationList.Walking );
                }
			break;
		}
	}

	void ChangeDirectionFacing( ) {
		Vector3 changedDirection = monster.transform.localScale;
		changedDirection.x = -changedDirection.x;
		monster.transform.localScale = changedDirection;
	}

	void StartMovementAnimation( CharacterAnimations.AnimationList monsterAnimation ) {
		if ( travelDistance > playerBodyWidth && monsterAnimation != characterAnimations.runningAnimation ) {
			characterAnimations.PlayNewAnimation( monsterAnimation, true );
			SetMonsterMovementSpeed( monsterAnimation );
			StartCoroutine( "DisplayMonsterMovement" );
		}
	}

	void SetMonsterMovementSpeed( CharacterAnimations.AnimationList monsterAnimation ) {
		if( monsterAnimation == CharacterAnimations.AnimationList.Walking ) {
			runSpeed = monsterMoveSpeedMin;
		}
		else if( monsterAnimation == CharacterAnimations.AnimationList.Running ) {
			runSpeed = monsterMoveSpeedMax;
		}
	}

	IEnumerator DisplayMonsterMovement( ){
		for( float i = travelDistance; i > stopWithinRange; ) {
			float speed = ( ( runSpeed * Time.deltaTime ) / travelDistance );
			travelDistance = Vector3.Distance( monster.transform.position, monsterPosition );
			if( travelDistance <= stopWithinRange ) {
			    StopMovement( );
			}
			i = travelDistance;
			monster.transform.position = Vector3.Lerp( monster.transform.position, monsterPosition, speed );
			yield return new WaitForSeconds( 0.01f );
		}
	}

	void StopMovement( ){
		characterAnimations.PlayNewAnimation( CharacterAnimations.AnimationList.Idle );
		StopCoroutine( "DisplayMonsterMovement" );
	}

	public static Vector3 ConvertScreenToWorldSpace( Vector3 screenSpace ) {
		return Camera.main.ScreenToWorldPoint( screenSpace );
	}
	
	public Vector3 ScreenPositionAsVector3( ) {
		return new Vector3( ScreenPosition.x, ScreenPosition.y, 0.0f );
    }
}