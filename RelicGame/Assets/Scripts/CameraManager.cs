using UnityEngine;
using System.Collections;
using TouchScript;
using TouchScript.Events;

[ System.Serializable ]
public class CameraManager : MonoBehaviour {

	public GameObject monster;
	[ Range( 0.1f, 5.0f ) ]
	public float cameraMoveSpeedHorizontally = 0.5f;
	[ Range( 0.1f, 5.0f ) ]
	public float cameraMoveSpeedVertically = 0.5f;
	bool isMoving = false;
	public static Vector3 cameraPos;

	public Vector3 topLeftAnchor {
		get;
		set;
	}
	public Vector3 botRightAnchor {
		get;
		set;
	}

	bool shouldCheckForAnchors;
	public float cameraHalfWidth {
		get {
			return ( camera.orthographicSize * camera.aspect );
		}
	}

	public float cameraHalfHeight {
		get {
			return ( camera.orthographicSize );
		}
	}

	private void Awake( ) {
		shouldCheckForAnchors = HasAnchors( );
	}

	bool HasAnchors( ) {
		return ( topLeftAnchor != null && botRightAnchor != null && 
		        ( topLeftAnchor != Vector3.zero && botRightAnchor != Vector3.zero ) );
	}

	public void Update( ) {
		if( !isMoving && ( this.camera.WorldToScreenPoint( monster.transform.position ) ).x < 250.0f || 
		   !isMoving && ( ( (float)Screen.width ) - this.camera.WorldToScreenPoint( monster.transform.position ).x ) < 250.0f ) {
			isMoving = true;
		}
		else if( isMoving ) {
			isMoving = !( ( this.camera.WorldToScreenPoint(monster.transform.position ) ).x > 300.0f && 
			             ( ( (float)Screen.width ) - this.camera.WorldToScreenPoint( monster.transform.position ).x ) > 300.0f );
		}
		LerpCamera( );
	}

	void LerpCamera( ) {
		Vector3 monsterPosition = monster.transform.position;
		if( shouldCheckForAnchors ) {
			bool canMoveHorizontally = ( monsterPosition.x > this.transform.position.x ) ? CanMoveRight( ) : CanMoveLeft( );
			bool canMoveVertically = ( monsterPosition.y > this.transform.position.y ) ? CanMoveUp( ) : CanMoveDown( );
			monsterPosition.x = ( canMoveHorizontally ) ? monsterPosition.x : this.transform.position.x;
			monsterPosition.y = ( canMoveVertically ) ? monsterPosition.y : this.transform.position.y;
		}
		this.transform.position = new Vector3( 
		                                      Mathf.Lerp( this.transform.position.x, monsterPosition.x, cameraMoveSpeedHorizontally * Time.deltaTime ), 
		                                      Mathf.Lerp( this.transform.position.y, monsterPosition.y, cameraMoveSpeedVertically * Time.deltaTime ), 
		                                      this.transform.position.z );
		cameraPos = this.transform.position;
	}

	bool CanMoveLeft( ) {
		return ( this.transform.position.x - cameraHalfWidth > topLeftAnchor.x );
	}
	
	bool CanMoveRight( ) {
		return ( this.transform.position.x + cameraHalfWidth < botRightAnchor.x );
	}

	bool CanMoveUp( ) {
		return ( this.transform.position.y + cameraHalfWidth < topLeftAnchor.y );
	}
	bool CanMoveDown( ) {
		return ( this.transform.position.y - cameraHalfWidth > botRightAnchor.y );
	}

}
