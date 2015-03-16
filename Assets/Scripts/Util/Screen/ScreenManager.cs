using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ScreenManager : MonoBehaviour {

	[ Range ( 0.1f, 2.0f ) ]
	public float cameraZoomAmount = 1.0f;

	private const float UNITS_PER_PIXEL = 1.0f / 100.0f;
	
	public void Update( ) {
		this.GetComponent<Camera>().orthographicSize = ( Screen.height / 2.0f ) * UNITS_PER_PIXEL * cameraZoomAmount;//Makes the camera pixel perfect for 2D games.
	}

}
