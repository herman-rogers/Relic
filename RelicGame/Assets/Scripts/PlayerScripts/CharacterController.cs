﻿using UnityEngine;
using System.Collections;
using Spine;

public class CharacterController : MonoBehaviour {
	CharacterAnimations characterAnimations;
	GameObject monster;
	Vector3 monsterPosition;
	float travelDistance;
	float runSpeed;
	const float playerBodyWidth = 0.7f;
	const float stopWithinRange = 0.01f;
	const float monsterMoveSpeedMax = 1.0f;
	const float monsterMoveSpeedMin = 1.5f;

	void Awake( ) {
		monster = this.transform.parent.gameObject;
		characterAnimations = new CharacterAnimations( );
		characterAnimations.InitAnimations( this.gameObject );
	}

	public void MoveMonsterOnXAxis( float xPosition ){
		MoveMonsterOnXAxis( xPosition, CharacterAnimations.AnimationList.Walking , false );
	}

	public void MoveMonsterOnXAxis( float xPosition, CharacterAnimations.AnimationList animation ){
		MoveMonsterOnXAxis( xPosition, animation, false );
	}

	public void MoveMonsterOnXAxis( float touchPositionX, CharacterAnimations.AnimationList animation, bool isCameraPosition ){
		Vector3 newMonsterPositionX = new Vector3( 0,0,0 );
		if( isCameraPosition ){
			newMonsterPositionX = Camera.main.ScreenToWorldPoint(
				new Vector3( touchPositionX, 0.0f, 0.0f ) );
		} else {
			newMonsterPositionX = new Vector3( touchPositionX, 0.0f, 0.0f );
		}
		if( shouldChangeDirectionFacing( newMonsterPositionX.x ) ) {
			ChangeDirectionFacing( );
		}
		GetNewMonsterPosition( newMonsterPositionX );
		FindTravelDistance( );
		StartMovementAnimation( animation );
	}

	void GetNewMonsterPosition( Vector3 newDestination ){
		monsterPosition = new Vector3(  newDestination.x, monster.transform.position.y, monster.transform.position.z );
	}

	void FindTravelDistance( ){
		travelDistance = Vector3.Distance( monster.transform.position, monsterPosition );
	}

	bool shouldChangeDirectionFacing( float positionMovingTowards ) {
		return ( monster.transform.localScale.x > 0.0f &&
		    monster.transform.position.x < positionMovingTowards ) ||
			( monster.transform.localScale.x < 0.0f &&
			 monster.transform.position.x > positionMovingTowards );
	}

	void ChangeDirectionFacing( ){
//		int facingDirection = Mathf.FloorToInt( monster.transform.localRotation.y );
//		if ( monster.transform.position.x < monsterPosition.x && facingDirection == 0 ) {
//			monster.transform.rotation = new Quaternion( 0,180,0,0 );
//		}
//		else if ( monster.transform.position.x > monsterPosition.x && facingDirection == 1  ) {
//			monster.transform.rotation = new Quaternion( 0,0,0,0 );
//		}

		Vector3 changedDirection = monster.transform.localScale;
		changedDirection.x = -changedDirection.x;
		monster.transform.localScale = changedDirection;
	}

	void StartMovementAnimation( CharacterAnimations.AnimationList monsterAnimation ){
		if ( travelDistance > playerBodyWidth && monsterAnimation != characterAnimations.runningAnimation ){
			characterAnimations.PlayNewAnimation( monsterAnimation, true );
			SetMonsterMovementSpeed( monsterAnimation );
			StartCoroutine( "DisplayMonsterMovement" );
		}
	}

	void SetMonsterMovementSpeed( CharacterAnimations.AnimationList monsterAnimation ){
		if( monsterAnimation == CharacterAnimations.AnimationList.Walking ){
			runSpeed = monsterMoveSpeedMin;
		}
		else if( monsterAnimation == CharacterAnimations.AnimationList.Running ){
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

	IEnumerator BlendMonsterMovement( float xPosition, CharacterAnimations.AnimationList animation ){
		bool startAnimation = false;
		while( !startAnimation ){
			if( characterAnimations.runningAnimation != CharacterAnimations.AnimationList.Idle ){
				continue;
			}
			yield return new WaitForSeconds( 1.0f );
		    MoveMonsterOnXAxis( xPosition, animation );
			startAnimation = true;
		}
	}

	void StopMovement( ){
		characterAnimations.PlayNewAnimation( CharacterAnimations.AnimationList.Idle );
		StopCoroutine( "DisplayMonsterMovement" );
	}
}