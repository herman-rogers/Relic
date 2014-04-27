using UnityEngine;
using System.Collections;
using UnityEditor;

[ CustomEditor ( typeof ( CameraManager ) ) ]
public class CameraManagerEditor : Editor {

	CameraManager cameraManagerInstance;

	void OnEnable( ) {
		cameraManagerInstance = target as CameraManager;
	}

	void OnSceneGUI( ) {
		Debug.Log( "HELLO CAMERA MANAGER EDITOR!" );
		Event sceneEvent = Event.current;

		Vector2[ ] screenPoints = new Vector2[ 4 ];
		for( int i = 0; i < 4; i ++ ) {
			screenPoints[i] = cameraCorners( )[ i ];//HandleUtility.WorldToGUIPoint( cameraCorners( )[ i ] );
		}
//		Handles.DrawLine( new Vector3( 0.0f, 0.0f, 0.0f ), new Vector3( 100.0f, 100.0f, 100.0f ) );
		Handles.color = Color.blue;
		Handles.DrawLine( screenPoints[ 0 ], screenPoints[ 1 ] );
		Handles.DrawLine( screenPoints[ 1 ], screenPoints[ 3 ] );
		Handles.DrawLine( screenPoints[ 3 ], screenPoints[ 2 ] );
		Handles.DrawLine( screenPoints[ 2 ], screenPoints[ 0 ] );
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
