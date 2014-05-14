using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Subject : MonoBehaviour {
	private static List< Observer > listOfObservers = new List< Observer >( );
	private static GameObject[] arrayOfUnityObservers;
	
	public static void AddObserver( Observer newObserver ) {
		GarbageCollectObservers( );
		if( !listOfObservers.Contains( newObserver ) ) {
		    listOfObservers.Add( newObserver );
		} else {
			Debug.LogWarning( "List already contains " 
			                  + newObserver.ToString( ) );
		}
	}

	public static void AddUnityObservers( ) {
		arrayOfUnityObservers = new GameObject[]{ };
		arrayOfUnityObservers = GameObject.FindGameObjectsWithTag( "UnityObserver" );
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
		if( arrayOfUnityObservers != null ){
			for( int i = 0; i < arrayOfUnityObservers.Length; i++ ){
				arrayOfUnityObservers[i].GetComponent< UnityObserver >( ).OnNotify( sender, 
				                                                                    new EventArguments( eventName ) );
			}
		}
	}

	static void GarbageCollectObservers( ){
		listOfObservers.RemoveAll( item => item == null );
	}
}
