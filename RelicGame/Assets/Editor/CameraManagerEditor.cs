using UnityEngine;
using System.Collections;
using UnityEditor;


//This editor script is just for practise and will be thrown away as it doesn't add any real value.
[ System.Serializable ]
[ CustomEditor ( typeof ( CameraManager ) ) ]
public class CameraManagerEditor : Editor {

	[ SerializeField ]
	CameraManager cameraManagerInstance;

	void OnEnable( ) {
		cameraManagerInstance = target as CameraManager;
	}

	void OnSceneGUI( ) {
		Event sceneEvent = Event.current;
		DrawCameraBorders( );
		DrawCameraAnchors( cameraManagerInstance );
	}

	void DrawCameraBorders( ) {
		Vector2[ ] screenPoints = new Vector2[ 4 ];
		for( int i = 0; i < 4; i ++ ) {
			screenPoints[ i ] = cameraCorners( )[ i ];
		}
		Handles.color = Color.blue;
		Handles.DrawLine( screenPoints[ 0 ], screenPoints[ 1 ] );
		Handles.DrawLine( screenPoints[ 1 ], screenPoints[ 3 ] );
		Handles.DrawLine( screenPoints[ 3 ], screenPoints[ 2 ] );
		Handles.DrawLine( screenPoints[ 2 ], screenPoints[ 0 ] );
	}

	void DrawCameraAnchors( CameraManager cameraManager ) {
		cameraManager.topLeftAnchor = DrawAndUpdateAnchor( cameraManager.topLeftAnchor );
		cameraManager.botRightAnchor = DrawAndUpdateAnchor( cameraManager.botRightAnchor );
	}

	Vector3 DrawAndUpdateAnchor( Vector3 anchor ) {
		DrawAnchor( anchor );
		return Handles.PositionHandle( anchor, Quaternion.identity );
	}

	void DrawAnchor( Vector3 anchor ) {
		Handles.color = Color.green;
		Handles.DrawLine( anchor + new Vector3( 0.2f, 0.0f, 0.0f ), 
		                 anchor - new Vector3( 0.2f, 0.0f, 0.0f ));
		Handles.DrawLine( anchor + new Vector3( 0.0f, 0.2f, 0.0f ), 
		                 anchor - new Vector3( 0.0f, 0.2f, 0.0f ));
	}

	Vector3[ ] cameraCorners( ) {
		Vector3 cameraPosition = cameraManagerInstance.transform.position;
		Vector3[ ] cameraCorners = new Vector3[ 4 ];
		{
			Vector3 topLeft = cameraPosition;
			topLeft.x -= cameraManagerInstance.cameraHalfWidth;
			topLeft.y += cameraManagerInstance.cameraHalfHeight;
			cameraCorners[ 0 ] = topLeft;
		}
		{
			Vector3 topRight = cameraPosition;
			topRight.x += cameraManagerInstance.cameraHalfWidth;
			topRight.y += cameraManagerInstance.cameraHalfHeight;
			cameraCorners[ 1 ] = topRight;
		}
		{
			Vector3 botLeft = cameraPosition;
			botLeft.x -= cameraManagerInstance.cameraHalfWidth;
			botLeft.y -= cameraManagerInstance.cameraHalfHeight;
			cameraCorners[ 2 ] = botLeft;
		}
		{
			Vector3 botRight = cameraPosition;
			botRight.x += cameraManagerInstance.cameraHalfWidth;
			botRight.y -= cameraManagerInstance.cameraHalfHeight;
			cameraCorners[ 3 ] = botRight;
		}
		return cameraCorners;
	}
}
