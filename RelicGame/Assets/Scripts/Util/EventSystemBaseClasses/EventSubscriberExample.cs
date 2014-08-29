using UnityEngine;
using System.Collections;

public class EventSubscriberExample : MonoBehaviour {

	void Start ( ) {
		//Use this to notify the event system of a new message
		Subject.Notify( this, "TestEvent" );
	}
}
