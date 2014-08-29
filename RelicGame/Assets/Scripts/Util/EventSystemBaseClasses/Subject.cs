using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Subject : MonoBehaviour {
	private static List< Observer > listOfObservers = new List< Observer >( );
	private static List< GameObject > listOfUnityObservers = new List< GameObject >( );
	
	public static void AddObserver( Observer newObserver ) {
		if( !listOfObservers.Contains( newObserver ) ) {
		    listOfObservers.Add( newObserver );
		} else {
			Debug.LogWarning( "List already contains " 
			                  + newObserver.ToString( ) );
		}
	}

	public static void AddUnityObservers( ) {
		listOfUnityObservers = ( GameObject.FindGameObjectsWithTag( "UnityObserver" ) ).ToList( );
	}

	public static void RemoveObserver( Observer oldObserver ) {
		if( listOfObservers.Contains( oldObserver ) ) {
		    listOfObservers.Remove( oldObserver );
		} else {
			Debug.LogWarning( "No observer named "
			                  + oldObserver.ToString( ) );
		}
	}

	public static void Notify( object sender, string eventName ) {
		GarbageCollectObservers( );
		foreach( Observer eventObserver in listOfObservers ) {
			eventObserver.OnNotify( sender, new EventArguments( eventName ) );
		}
		NotifyUnityObservers( sender, eventName );
	}

	public static int NumberOfObserversAdded( ){
		return listOfObservers.Count;
	}

	public static void ClearAllObservers( ){
		listOfObservers.Clear( );
	}

	static void NotifyUnityObservers( object sender, string eventName ) {
	    foreach( GameObject unityObserver in listOfUnityObservers ){
			unityObserver.GetComponent< UnityObserver >( ).OnNotify( sender, 
				                                                         new EventArguments( eventName ) );
		}
	}

	static void GarbageCollectObservers( ){
		listOfObservers.RemoveAll( item => item == null );
		listOfUnityObservers.RemoveAll( item => item == null );
	}
}
