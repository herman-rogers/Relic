using UnityEngine;
using System.Collections;
using TouchScript.Gestures;
using TouchScript;
using System.Collections.Generic;

public class DoorTween : PressGesture {

	public SpriteRenderer renderer;
	public FlickerTween computer;//TODO: make this non coupled with FlickerTween.
	public Transform player;//TODO: also decouple the player from the door.
	CharacterController controller;

	void Start( ) {
		base.Start( );
		controller = player.GetComponentInChildren< CharacterController >( );
		StateChanged += StateChangeHandler;
	}

	void StateChangeHandler( object sender, TouchScript.Events.GestureStateChangeEventArgs e ) {
		switch( e.State ) {
		case Gesture.GestureState.Recognized:
			DoorTrigger( );
			break;
		}
	}

	void DoorTrigger( ) {
		if( DoorCanOpen( ) ) {
			if( !IsInvoking( "WaitForAnimation" ) ) {
				StartCoroutine( "WaitForAnimation" );
			}
		}
	}

	bool DoorCanOpen( ) {
		return (
			computer.frequency.Evaluate( Time.timeSinceLevelLoad / computer.rateOfFlicker ) > 0.9f
			&& Mathf.Abs( this.transform.position.x - player.position.x ) < 2.2f );
	}

	IEnumerator WaitForAnimation( ) {
		yield return new WaitForSeconds( 0.7f );
		Color newColor = ( this.renderer.color.a <= 0.0f ) ? new Color( 1.0f, 1.0f, 1.0f, 1.0f ) : new Color( 1.0f, 1.0f, 1.0f, 0.0f );
		this.renderer.color = newColor;//Turns on and off the spiret via the alpha channel.
		Application.LoadLevel( "oldLabHallway" );
	}
}
