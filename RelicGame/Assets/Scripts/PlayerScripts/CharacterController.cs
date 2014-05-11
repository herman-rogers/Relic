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

	CharacterAnimations characterAnimations;
	Vector3 monsterPosition;
	float travelDistance;
	float runSpeed;

	const float playerBodyWidth = 0.7f;
	const float stopWithinRange = 0.01f;
	const float monsterMoveSpeedMax = 1.0f;
	const float monsterMoveSpeedMin = 1.5f;

	void OnLevelWasLoaded( int level ) {
		try {
			SceneLoaderOnTouch.SceneLoadInfomation previousScene = SceneLoaderOnTouch.sceneStack.Pop( ) as SceneLoaderOnTouch.SceneLoadInfomation;
			InitiateMonsterPosition( previousScene );
		} catch( Exception ex ) {
			Debug.LogWarning( "The previous scene didn't load any information about where the player should start. \n" + ex.Message );
		}
	}

	void InitiateMonsterPosition( SceneLoaderOnTouch.SceneLoadInfomation sceneInfo ) {
		switch( sceneInfo.sceneLoadedFrom ) {
		case( SceneLoaderOnTouch.SceneLoadInfomation.SceneExitUsed.LEFT ):
			if( rightExit != null ) {
				SetMonsterXPosition( rightExit.position.x );
			}
			break;
		case( SceneLoaderOnTouch.SceneLoadInfomation.SceneExitUsed.RIGHT ):
			if( leftExit != null ) {
				SetMonsterXPosition( leftExit.position.x );
			}
			break;
		case( SceneLoaderOnTouch.SceneLoadInfomation.SceneExitUsed.UP ):
			if( upExit != null ) {
				SetMonsterXPosition( upExit.position.x );
			}
			break;
		case( SceneLoaderOnTouch.SceneLoadInfomation.SceneExitUsed.DOWN ):
			break;
		}
	}

	void SetMonsterXPosition( float newXPosition ) {
		this.monster.transform.position = 
			new Vector3( newXPosition, this.monster.transform.position.y, this.monster.transform.position.z );
	}

	void Awake( ) {
		base.Start( );
		characterAnimations = new CharacterAnimations( );
		characterAnimations.InitAnimations( this.gameObject );
		this.StateChanged += StateChangeHandler;
	}

	public void MoveMonster( Vector3 screenCoordinate, CharacterAnimations.AnimationList animation ){
		if( shouldChangeDirectionFacing( screenCoordinate.x ) ) {
			ChangeDirectionFacing( );
		}
		monsterPosition = NewMonsterPosition( screenCoordinate );
		travelDistance = FindDistanceToTravel( );
		StartMovementAnimation( animation );
	}

	Vector3 NewMonsterPosition( Vector3 newDestination ) {
		return new Vector3( newDestination.x, newDestination.y, monster.transform.position.z );//TODO: Add a raycast system to be able to 'block' sections of a level that cannot be moved to.
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
			this.MoveMonster( ConvertScreenToWorldSpace( ScreenPositionAsVector3( ) ) ,
			                        CharacterAnimations.AnimationList.Walking );
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
		for( float i = travelDistance; i > stopWithinRange; ){
			float speed = ( ( runSpeed * Time.deltaTime ) / travelDistance );
			travelDistance = Vector3.Distance( monster.transform.position, monsterPosition );
			if ( travelDistance <= stopWithinRange ){
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