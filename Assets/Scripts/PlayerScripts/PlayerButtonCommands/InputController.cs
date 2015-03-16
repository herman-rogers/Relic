using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour
{
    private Command TOUCH_INPUT;
    private Command MOUSE_INPUT;

    private void Awake( )
    {
        BindButtonsToCommand( );
    }

    private void BindButtonsToCommand( )
    {
        TOUCH_INPUT = new MovementCommand( );
        MOUSE_INPUT = new MovementCommand( );
    }

    private void FixedUpdate( )
    {
        UpdateControls( );
    }

    private void UpdateControls( )
    {
        if ( Input.touchCount == 1 )
        {
            TouchInput( );
        }
        if ( Input.GetMouseButton( 0 ) ) {
            DevTouchInput( );
        }
    }

    private void TouchInput( ) {
        TOUCH_INPUT.Execute( gameObject );
    }

    private void DevTouchInput( ) {
        Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
        MOUSE_INPUT.ExecuteWithCoordinates( this.gameObject, ray.direction );
    }
}