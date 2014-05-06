using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Subject : MonoBehaviour {

	private static List< Observer > listOfObservers = new List< Observer >( );

	public static void AddObserver( Observer newObserver ) {
		GarbageCollectObservers( );
		if( !listOfObservers.Contains( newObserver ) ) {
		    listOfObservers.Add( newObserver );
		} else {
			Debug.LogWarning( "List already contains " 
			                  + newObserver.name );
		}
	}

	public static void RemoveObserver( Observer oldObserver ) {
		if( listOfObservers.Contains( oldObserver ) ) {
		    listOfObservers.Remove( oldObserver );
		} else {
			Debug.LogWarning( "No observer named " 
			                  + oldObserver.name );
		}
	}

	public static void Notify( object sender, string eventName ) {
		foreach( Observer eventObserver in listOfObservers ) {
			eventObserver.OnNotify( sender, new EventArguments( eventName ) );
		}
	}

	public static void NumberOfObserversAdded( ){
		Debug.Log( listOfObservers.Count );
	}

	static void GarbageCollectObservers( ){
		listOfObservers.RemoveAll( item => item == null );
	}
}
