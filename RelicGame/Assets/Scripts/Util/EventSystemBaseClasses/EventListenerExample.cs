using UnityEngine;
using System.Collections;

//You must inherit from the observer class to listen to events
public class EventListenerExample : Observer {
	//Override the OnNotify in the observer class to
	//handle incoming notifications in your preferred way
	public override void OnNotify ( object sender, EventArguments e ) {
		switch( e.EventMessage ) {
		    case "TestEvent":
			    Debug.Log( "New Event Was Received" );
			    break;
		}
	}
}