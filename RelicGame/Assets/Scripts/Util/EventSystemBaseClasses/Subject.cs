using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Subject : MonoBehaviour {

	private static List< Observer > listOfObservers = new List< Observer >( );

	public static void addObserver( Observer newObserver ) {
		if( !listOfObservers.Contains( newObserver ) ) {
		    listOfObservers.Add( newObserver );
		} else {
			Debug.LogWarning( "Observer " + newObserver.name 
			                  + " already in list" );
		}
	}

	public static void removeObserver( Observer oldObserver ) {
		if( listOfObservers.Contains( oldObserver ) ) {
		    listOfObservers.Remove( oldObserver );
		} else {
			Debug.LogWarning( "No observer named " 
			                  + oldObserver.name + " found" );
		}
	}

	public static void Notify( object sender, string eventName ) {
		foreach( Observer eventObserver in listOfObservers ) {
			eventObserver.OnNotify( sender, new EventArguments( eventName ) );
		}
	}
}
