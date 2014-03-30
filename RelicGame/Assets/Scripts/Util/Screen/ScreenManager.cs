using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ScreenManager : MonoBehaviour {

	private const float UNITS_PER_PIXEL = 1.0f / 100.0f;

	public void Update( ) {
		this.camera.orthographicSize = ( Screen.height / 2.0f ) * UNITS_PER_PIXEL;//Makes the camera pixel perfect for 2D games.
	}

}
