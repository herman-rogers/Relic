using UnityEngine;
using System.Collections;

//Base class for the Observer of the Event System
//You must inherit this class to listen to events
public class EventArguments {
	public EventArguments( string newEventMessage ) { EventMessage = newEventMessage; }
	public string EventMessage { get; private set; }
}

public class Observer : MonoBehaviour {
	void Awake( ) {
		Subject.addObserver( this );
	}

	public virtual void OnNotify( object sender, EventArguments e ) {
	    //Do special event things here
	}
}
