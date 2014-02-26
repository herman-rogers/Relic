using UnityEngine;
using System.Collections;
using TouchScript;
using TouchScript.Events;

public class CameraManager : MonoBehaviour {
	public GameObject monster;
	public Vector2    firstTouchPosition;
	public Vector2    secondTouchPosition;
	public Vector2    currentTouchMovement;
	public bool       hasTouchEnded{ get; private set; }
	bool              isMoving = false;
	public static Vector3 cameraPos;

	void Start( ){
//		TouchManager.Instance.TouchesBegan += OnTouchBegin;
//		TouchManager.Instance.TouchesMoved += OnTouchContinue;
//		TouchManager.Instance.TouchesEnded += OnTouchEnd;
	}

	public void OnTouchBegin( object sender, TouchEventArgs e ){
		foreach( TouchPoint point in e.TouchPoints ){
			firstTouchPosition = new Vector2( point.Position.x, point.Position.y );
		}
	}

	public void OnTouchContinue( object sender, TouchEventArgs e ){
		foreach( var point in e.TouchPoints ){
			secondTouchPosition  = new Vector2( point.Position.x, point.Position.y );
			currentTouchMovement = new Vector2( ( secondTouchPosition.x - firstTouchPosition.x ),
			                                    ( secondTouchPosition.y - firstTouchPosition.y ));
			currentTouchMovement.Normalize( );
		}
    }

    public void OnTouchEnd( object sender, TouchEventArgs e ){
		hasTouchEnded = true;
    }

    public void Update()
	{
		if(!isMoving && (this.camera.WorldToScreenPoint(monster.transform.position)).x < 250.0f || 
		   !isMoving && (((float)Screen.width) - this.camera.WorldToScreenPoint(monster.transform.position).x) < 250.0f)
		{
			isMoving = true;
			LerpCamera();
		}
		else if(isMoving)
		{
			LerpCamera();
			if((this.camera.WorldToScreenPoint(monster.transform.position)).x > 300.0f && 
			   (((float)Screen.width) - this.camera.WorldToScreenPoint(monster.transform.position).x) > 300.0f)
			{
				isMoving = false;
			}
		}
	}

	void LerpCamera()
	{
		Vector3 monsterX = monster.transform.position;
		monsterX.y = this.transform.position.y;
		monsterX.z = this.transform.position.z;
		this.transform.position = Vector3.Lerp(this.transform.position, monsterX, 0.5f * Time.deltaTime);
		cameraPos = this.transform.position;
	}
}
