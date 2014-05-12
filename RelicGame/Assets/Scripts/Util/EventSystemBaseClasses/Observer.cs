using UnityEngine;
using System.Collections;

//Base class for the Observer of the Event System
//You must inherit this class to listen to events
//Name the object you place your script on "EventObject"
public class Observer {
	protected GameObject eventListener;

	public Observer( ) {
		Subject.AddObserver( this );
		eventListener = UnityEngine.GameObject.Find( "EventObject" );
		if( eventListener == null ){ EventListenerIsNull( ); }
		GetSceneEvents( );
	}

	protected virtual void GetSceneEvents( ) { }

	protected virtual void EventListenerIsNull( ) {
		Debug.Log( "Warning: You need to create a EventObject in the current scene" );
	}

	public virtual void OnNotify( object sender, EventArguments e ) {
	    //Do special event things here
	}
}

public class EventArguments {
	public EventArguments( string newEventMessage ) { EventMessage = newEventMessage; }
	public string EventMessage { get; private set; }
}
