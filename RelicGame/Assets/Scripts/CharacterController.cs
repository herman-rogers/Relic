using UnityEngine;
using System.Collections;
using Spine;

[RequireComponent( typeof( dyCharacterPressGesture ) )]
public class dyCharacterController : MonoBehaviour {
	public AnimationList runningAnimation{ get; private set; }
	dyCharacterPressGesture pressGesture;
	SkeletonAnimation spineAnimation;
	GameObject monster;
	Vector3 monsterPosition;
	float travelDistance;
	float runSpeed;
	const float playerBodyWidth = 0.7f;
	const float stopWithinRange = 0.01f;
	const float monsterMoveSpeedMax = 3.0f;
	const float monsterMoveSpeedMin = 1.0f;

	public enum AnimationList {
		Walking,
		Idle,
		Running,
		Activate,
	}

	void Awake( ){
		monster = this.transform.parent.gameObject;
		spineAnimation = monster.GetComponent< SkeletonAnimation >( );
		pressGesture = this.GetComponent< dyCharacterPressGesture >( );
	}

	void Start( ){
		runningAnimation = AnimationList.Idle;
		spineAnimation.state.Complete += AnimationComplete;
	}

	public void MoveMonsterOnXAxis( float xPosition ){
//		DisableUserInput( );
		MoveMonsterOnXAxis( xPosition, AnimationList.Walking, false );
	}

	public void MoveMonsterOnXAxis( float xPosition, AnimationList animation ){
//		DisableUserInput( );
		MoveMonsterOnXAxis( xPosition, animation, false );
	}

	public void MoveMonsterOnXAxis( float xPosition, AnimationList animation, bool isCameraPosition ){
		GetNewMonsterPosition( xPosition, isCameraPosition );
		FindTravelDistance( );
		ChangeDirectionFacing( );
		StartMovementAnimation( animation );
	}

	public void BlendNewMovement( float xPosition, AnimationList animation ){
		StartCoroutine( BlendMonsterMovement( xPosition, animation ) );
	}

	public void PlayNewAnimation( AnimationList animation ){
		PlayNewAnimation( animation, false );
	}

	public void PlayNewAnimation( AnimationList animation, bool isLooped ){
		spineAnimation.state.AddAnimation( 1, animation.ToString( ), isLooped, 0.5f );
		runningAnimation = animation;
	}

	public void DisableUserInput( ){
		pressGesture.enabled = false;
	}
	
	public void EnableUserInput( ){
		pressGesture.enabled = true;
	}

	void AnimationComplete( Spine.AnimationState animationState, int trackIndex, int loopCount ){
		switch( animationState.ToString( ) ){
		case "Idle" :
			runningAnimation = AnimationList.Idle;
			break;
		case "Walking":
			runningAnimation = AnimationList.Idle;
			break;
		case "Running":
			runningAnimation = AnimationList.Running;
			break;
		case "Activte":
			runningAnimation = AnimationList.Activate;
			break;
		}
	}

	void GetNewMonsterPosition( float newDestination, bool isCameraPosition ){
		Vector3 newMonsterPosition = new Vector3( 0,0,0 );
		if( isCameraPosition ){
		    newMonsterPosition = Camera.main.ScreenToWorldPoint(
			new Vector3( newDestination, 0.0f, 0.0f ) );
		} else {
			newMonsterPosition = new Vector3( newDestination, 0.0f, 0.0f );
		}
		monsterPosition = new Vector3(  newMonsterPosition.x, monster.transform.position.y, monster.transform.position.z );
	}

	void FindTravelDistance( ){
		travelDistance = Vector3.Distance( monster.transform.position, monsterPosition );
	}

	void ChangeDirectionFacing( ){
		int facingDirection = Mathf.FloorToInt( monster.transform.localRotation.y );
		if ( monster.transform.position.x < monsterPosition.x && facingDirection == 0 ){
			monster.transform.rotation = new Quaternion( 0,180,0,0 );
		}
		else if ( monster.transform.position.x > monsterPosition.x && facingDirection == 1  ) {
			monster.transform.rotation = new Quaternion( 0,0,0,0 );
		}
	}

	void StartMovementAnimation( AnimationList monsterAnimation ){
		if ( travelDistance > playerBodyWidth && monsterAnimation != runningAnimation ){
			PlayNewAnimation( monsterAnimation, true );
			SetMonsterMovementSpeed( monsterAnimation );
			StartCoroutine( "DisplayMonsterMovement" );
		}
	}

	void SetMonsterMovementSpeed( AnimationList monsterAnimation ){
		if( monsterAnimation == AnimationList.Walking ){
			runSpeed = monsterMoveSpeedMin;
		}
		else if( monsterAnimation == AnimationList.Running ){
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

	IEnumerator BlendMonsterMovement( float xPosition, AnimationList animation ){
		bool startAnimation = false;
		while( !startAnimation ){
			if( runningAnimation != AnimationList.Idle ){
				continue;
			}
			yield return new WaitForSeconds( 1.0f );
		    MoveMonsterOnXAxis( xPosition, animation );
			startAnimation = true;
		}
	}

	void StopMovement( ){
		PlayNewAnimation( AnimationList.Idle );
		StopCoroutine( "DisplayMonsterMovement" );
	}
}