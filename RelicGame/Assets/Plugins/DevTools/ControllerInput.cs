using UnityEngine;
using System.Collections;

public class ControllerInput : MonoBehaviour
{
	void Update( )
	{
		if (Input.GetKeyDown( KeyCode.Space ) || Input.GetKeyDown( "joystick button 2" ) )
		{
			Subject.Notify( SceneStepper.PAUSE_GAME );
		}
		if( Input.GetKey( KeyCode.M ) || Input.GetKey( "joystick button 1" ) )
		{
			Subject.Notify( SceneStepper.STEP_THROUGH );
		}
		if( Input.GetKeyDown( KeyCode.H ) || Input.GetKeyDown( "joystick button 0" ) )
		{
			Subject.Notify( SceneDebugger.SHOW_GUI );
		}
		if( Input.GetMouseButtonDown( 0 ) )
		{
			Subject.Notify( SceneTransformTracker.RIGHT_MOUSE_BUTTON );
		}
		if( Input.GetMouseButtonDown( 1 ) )
		{
			Subject.Notify( SceneTransformTracker.LEFT_MOUSE_BUTTON );
		}
		if( Input.GetKey( KeyCode.B ) )
		{
			Subject.Notify( SceneReverser.REVERSER_STEP_FORWARD );
		}
		if( Input.GetKey( KeyCode.N ) )
		{
			Subject.Notify( SceneReverser.REVERSER_STEP_BACK );
		}
	}
}