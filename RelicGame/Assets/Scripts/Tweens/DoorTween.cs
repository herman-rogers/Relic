using UnityEngine;
using System.Collections;
using TouchScript.Gestures;
using TouchScript;
using System.Collections.Generic;

[RequireComponent( typeof( PressGesture ) )]
public class DoorTween : SpriteTween {

	public PressGesture pressGesture;
	public SpriteRenderer renderer;
	public FlickerTween computer;//TODO: make this non coupled with FlickerTween.
	public Transform player;//TODO: also decouple the player from the door.
	CharacterController controller;

	void Start( ) {
		if( this.pressGesture == null ) {
			this.pressGesture = this.GetComponent<PressGesture>( );
		}
		controller = player.GetComponentInChildren< CharacterController >( );
		pressGesture.StateChanged += StateChangeHandler;
	}

	void StateChangeHandler( object sender, TouchScript.Events.GestureStateChangeEventArgs e ){
		switch( e.State ){
		case Gesture.GestureState.Recognized:
			if( player.transform.position.x > ( this.transform.position.x + 1.5 ) ){
			    controller.MoveMonsterOnXAxis( ( pressGesture.ScreenPosition.x + this.transform.position.x ), 
			    CharacterController.AnimationList.Walking, true );
			}
			DoorTrigger( );
			break;
		case Gesture.GestureState.Began:
			break;
		}
	}

	void DoorTrigger( ) {
		if( DoorCanOpen( ) ) {
			StartCoroutine( "WaitForAnimation" );
		}
	}

	bool DoorCanOpen( ) {
		return (
			computer.frequency.Evaluate( Time.timeSinceLevelLoad ) > 0.9f
			&& Mathf.Abs( this.transform.position.x - player.position.x ) < 2.2f//TODO: Build this as a visual component in the editor.
			);
	}

	IEnumerator WaitForAnimation( ){
		controller.PlayNewAnimation( CharacterController.AnimationList.Activate );
		yield return new WaitForSeconds( 0.7f );
		Color newColor = ( this.renderer.color.a <= 0.0f ) ? new Color( 1.0f, 1.0f, 1.0f, 1.0f ) : new Color( 1.0f, 1.0f, 1.0f, 0.0f );
		this.renderer.color = newColor;//Turns on and off the spiret via the alpha channel.
	}
}
