using UnityEngine;
using System.Collections;
using Spine;
using TouchScript;
using TouchScript.Gestures;
using System;

public enum PlayerInput
{
	STOP_PLAYER_INPUT,
	START_PLAYER_INPUT
}

public class CharacterController : PressGesture {
	public GameObject player;
	public static PlayerInput togglePlayerInput = PlayerInput.START_PLAYER_INPUT;
	private CharacterAnimations characterAnimations;
	private Vector3 monsterPosition;
	private float travelDistance;
	private float runSpeed;
	private const float playerBodyWidth = 0.7f;
	private const float stopWithinRange = 0.01f;
	private const float monsterMoveSpeedMax = 1.0f;
	private const float monsterMoveSpeedMin = 1.5f;

	void Awake( ) {
		base.Start( );
		characterAnimations = new CharacterAnimations( );
		characterAnimations.InitAnimations( gameObject );
		StateChanged += StateChangeHandler;
	}

	private void SetPlayerPosition( float newXPosition, float newYPosition ) {
		player.transform.position = new Vector3( newXPosition, 
		                                         newYPosition, 
		                                         player.transform.position.z );
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
		return new Vector3( newDestination.x, player.transform.position.y, player.transform.position.z );
	}

	float FindDistanceToTravel( ) {
		return Vector3.Distance( player.transform.position, monsterPosition );
	}

	bool shouldChangeDirectionFacing( float positionMovingTowards ) {
		return ( player.transform.localScale.x > 0.0f &&
		         player.transform.position.x < positionMovingTowards ) ||
			   ( player.transform.localScale.x < 0.0f &&
			     player.transform.position.x > positionMovingTowards );
	}

	void StateChangeHandler( object sender, TouchScript.Events.GestureStateChangeEventArgs e ) {
		if( togglePlayerInput == PlayerInput.STOP_PLAYER_INPUT )
		{
			return;
		}
		switch( e.State ) {
		case Gesture.GestureState.Recognized:
			MoveMonster( ConvertScreenToWorldSpace( ScreenPositionAsVector3( ) ) ,
			             CharacterAnimations.AnimationList.Walking );
			break;
		}
	}

	void ChangeDirectionFacing( ) {
		Vector3 changedDirection = player.transform.localScale;
		changedDirection.x = -changedDirection.x;
		player.transform.localScale = changedDirection;
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
			travelDistance = Vector3.Distance( player.transform.position, monsterPosition );
			if ( travelDistance <= stopWithinRange ){
			    StopMovement( );
			}
			i = travelDistance;
			player.transform.position = Vector3.Lerp( player.transform.position, monsterPosition, speed );
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