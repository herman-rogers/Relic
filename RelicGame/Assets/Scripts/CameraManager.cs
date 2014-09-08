﻿using UnityEngine;

[ System.Serializable ]
public class CameraManager : MonoBehaviour {

	[ Range( 0.1f, 5.0f ) ]
	public float cameraMoveSpeedHorizontally = 0.5f;
	[ Range( 0.1f, 5.0f ) ]
	public float cameraMoveSpeedVertically = 0.5f;
	bool isMoving = false;
	public static Vector3 cameraPos;

	public Transform topLeftAnchor;
	public Transform botRightAnchor;

	bool shouldCheckForAnchors;

    private GameObject _player;
    GameObject Player{
        get{
            return _player ?? ( CharacterController.GetCharacterController( ) );
        }
    }

	public float CameraHalfWidth {
		get {
			return ( camera.orthographicSize * camera.aspect );
		}
	}

	public float CameraHalfHeight {
		get {
			return ( camera.orthographicSize );
		}
	}

	private void Awake( ){
		shouldCheckForAnchors = HasAnchors( );
	}

    bool HasAnchors( ) {
		return ( topLeftAnchor != null && botRightAnchor != null && 
		        ( topLeftAnchor != null && botRightAnchor != null ) );
	}

	public void Update( ) {
		if( !isMoving && ( this.camera.WorldToScreenPoint( this.Player.transform.position ) ).x < 250.0f || 
		   !isMoving && ( ( (float)Screen.width ) - this.camera.WorldToScreenPoint( this.Player.transform.position ).x ) < 250.0f ) {
			isMoving = true;
		}
		else if( isMoving ) {
			isMoving = !( ( this.camera.WorldToScreenPoint(this.Player.transform.position ) ).x > 300.0f && 
			             ( ( (float)Screen.width ) - this.camera.WorldToScreenPoint( this.Player.transform.position ).x ) > 300.0f );
		}
		LerpCamera( );
	}

	void LerpCamera( ) {
		Vector3 monsterPosition = this.Player.transform.position;
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
		return ( this.transform.position.x - this.CameraHalfWidth > topLeftAnchor.position.x );
	}
	
	bool CanMoveRight( ) {
		return ( this.transform.position.x + this.CameraHalfWidth < botRightAnchor.position.x );
	}

	bool CanMoveUp( ) {
		return ( this.transform.position.y + this.CameraHalfWidth < topLeftAnchor.position.y );
	}
	bool CanMoveDown( ) {
		return ( this.transform.position.y - this.CameraHalfWidth > botRightAnchor.position.y );
	}

}
