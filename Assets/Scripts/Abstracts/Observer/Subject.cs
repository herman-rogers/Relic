using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Subject : MonoBehaviour {
    private static readonly List<Observer> listOfObservers = new List<Observer>( );
    private static List<GameObject> listOfUnityObservers = new List<GameObject>( );

    public static void AddObserver( Observer newObserver ) {
        if ( !listOfObservers.Contains( newObserver ) ) {
            listOfObservers.Add( newObserver );
            return;
        }
        Debug.LogWarning( "List Already Contains Observer" );
    }

    public static void AddUnityObservers( ) {
        listOfUnityObservers = ( GameObject.FindGameObjectsWithTag( "UnityObserver" ) ).ToList( );
    }

    public static void RemoveObserver( Observer oldObserver ) {
        if ( listOfObservers.Contains( oldObserver ) ) {
            listOfObservers.Remove( oldObserver );
        } else {
            Debug.LogWarning( "List Doesn't Contain Observer" );
        }
    }

    public static void RemoveAllObservers( ) {
        listOfObservers.Clear( );
    }

    public static string NumberOfObservers( ) {
        return "UnityObs: " + listOfObservers.Count;
    }

    public static string NumberOfUnityObservers( ) {
        return "Obs: " + listOfUnityObservers.Count( );
    }

    public static void GarbageCollectObservers( ) {
        listOfObservers.RemoveAll( item => item == null );
        listOfUnityObservers.RemoveAll( item => item == null );
    }

    public static void Notify( string staticEventName ) {
        NotifySendAll( null, staticEventName, null );
    }

    public static void NotifyExtendedMessage( string staticEventName, List<string> extendedMessage ) {
        NotifySendAll( null, staticEventName, extendedMessage );
    }

    public static void NotifySendObject( Object sender, string staticEventName ) {
        NotifySendAll( sender, staticEventName, null );
    }

    public static void NotifySendAll( Object sender, string eventName, List<string> extendedMessage ) {
        GarbageCollectObservers( );
        foreach ( var observer in listOfObservers ) {
            observer.OnNotify( sender, new EventArguments( eventName, extendedMessage ) );
        }
        NotifyUnityObservers( sender, eventName, extendedMessage );
    }

    private static void NotifyUnityObservers( Object sender, string unityEventName, List<string> extendedMessage ) {
        foreach ( var unityObserver in listOfUnityObservers ) {
            unityObserver.GetComponent<UnityObserver>( ).OnNotify( sender,
            new EventArguments( unityEventName, extendedMessage ) );
        }
    }
}