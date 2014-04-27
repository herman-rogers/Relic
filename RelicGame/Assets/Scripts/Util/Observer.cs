using UnityEngine;
using System.Collections;

public class EventArguments {
	public EventArguments( string newEvent ) { Text = newEvent; }
	public string Text { get; private set; }
}

public class Observer : MonoBehaviour {

	public delegate void EventHandler( object sender, EventArguments e );

	public event EventHandler eventHandler;

	protected virtual void OnNotify( ) {
		if (eventHandler != null) {
			eventHandler( this, new EventArguments( "Event Triggered" ) );
		}
	}
}
