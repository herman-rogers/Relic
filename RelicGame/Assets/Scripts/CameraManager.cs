using UnityEngine;
using System.Collections;
using TouchScript;
using TouchScript.Events;

public class CameraManager : MonoBehaviour {

	public GameObject monster;
	[ Range( 0.1f, 5.0f ) ]
	public float cameraMoveSpeed = 0.5f;
	bool isMoving = false;
	public static Vector3 cameraPos;

	public Transform topLeftAnchor;
	public Transform botRightAnchor;
	bool shouldCheck;
	float cameraHalfWidth;

	private void Awake( ) {
		shouldCheck = (topLeftAnchor != null && botRightAnchor != null);
		cameraHalfWidth = ( camera.orthographicSize * camera.aspect );
	}

	public void Update( ) {
		if( !isMoving && ( this.camera.WorldToScreenPoint( monster.transform.position ) ).x < 250.0f || 
		   !isMoving && ( ( (float)Screen.width ) - this.camera.WorldToScreenPoint( monster.transform.position ).x ) < 250.0f ) {
			isMoving = true;
			LerpCamera( );
		}
		else if( isMoving ) {
			LerpCamera( );
			isMoving = !( ( this.camera.WorldToScreenPoint(monster.transform.position ) ).x > 300.0f && 
			             ( ( (float)Screen.width ) - this.camera.WorldToScreenPoint( monster.transform.position ).x ) > 300.0f );
		}
	}

	void LerpCamera( ) {
		Vector3 monsterX = monster.transform.position;
		if( shouldCheck ) {
			bool canMove = ( monsterX.x > this.transform.position.x ) ? CanMoveRight( ) : CanMoveLeft( );
			if( !canMove ) {
				return;
			}
		}
		monsterX.y = this.transform.position.y;
		monsterX.z = this.transform.position.z;
		this.transform.position = Vector3.Lerp( this.transform.position, monsterX, cameraMoveSpeed * Time.deltaTime );
		cameraPos = this.transform.position;
	}

	bool CanMoveLeft( ) {
		return ( this.transform.position.x - cameraHalfWidth > topLeftAnchor.position.x );
	}
	
	bool CanMoveRight( ) {
		return ( this.transform.position.x + cameraHalfWidth < botRightAnchor.position.x );
	}
}
