using UnityEngine;
using System.Collections;
using TouchScript;
using TouchScript.Gestures;

public class MonsterAnimations : MonoBehaviour {
	private GameObject monster;
	private Vector3 monsterPosition;
	private bool toggleAnimation;
	private float distanceFromNewInput;

	void Start( ){
		monster = this.transform.parent.gameObject;
		toggleAnimation = true;
	}

	void UpdateMonsterCoordinates( ){
		monsterPosition = GetNewMonsterPosition( );
		distanceFromNewInput = Vector3.Distance( monster.transform.position, monsterPosition );
	}

	Vector3 GetNewMonsterPosition( ){
		PressGesture getPressedPosition = this.GetComponent< PressGesture >( );
		Vector3 newMonsterPosition = Camera.main.ScreenToWorldPoint(
			new Vector3( getPressedPosition.ScreenPosition.x, 0.0f, 0.0f ) );
		newMonsterPosition.z = monster.transform.position.z;
		newMonsterPosition.y = monster.transform.position.y;
		return newMonsterPosition;
	}

	public void StartWalkingAnimation( ){
		UpdateMonsterCoordinates( );
		if ( distanceFromNewInput > 0.7f ){
			ChangeMonsterLookDirection( );
		    if( toggleAnimation ){
			    toggleAnimation = false;
		        monster.GetComponent< SkeletonAnimation >( ).animationName = "Walking";
		        StartCoroutine( MoveMonster( ) );
			}
		}
	}

	void ChangeMonsterLookDirection( ){
		int currentLookDirection = Mathf.FloorToInt( monster.transform.localRotation.y );
		if ( monster.transform.position.x < monsterPosition.x && currentLookDirection == 0 ){
			monster.transform.rotation = new Quaternion( 0,180,0,0 );
		}
		else if ( monster.transform.position.x > monsterPosition.x && currentLookDirection == 1  ) {
			monster.transform.rotation = new Quaternion( 0,0,0,0 );
		}
	}

	IEnumerator MoveMonster( ){
		for( float i = distanceFromNewInput; i > 0.01f; ){
			float speed = ( ( 3.0f * Time.deltaTime ) / distanceFromNewInput );
			distanceFromNewInput = Vector3.Distance( monster.transform.position, monsterPosition );
			if ( distanceFromNewInput < 0.01f ){
			    StopMovement( );
			}
			i = distanceFromNewInput;
			monster.transform.position = Vector3.Lerp( monster.transform.position, monsterPosition, speed );
			yield return new WaitForSeconds( 0.01f );
		}
	}

	void StopMovement( ){
		monster.GetComponent< SkeletonAnimation >( ).animationName = "Idle";
		toggleAnimation = true;
		StopAllCoroutines( );
	}
}
