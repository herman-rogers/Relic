using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Subject : MonoBehaviour
{
    private static readonly List< Observer > listOfObservers = new List< Observer >( );
    private static List< GameObject > listOfUnityObservers = new List< GameObject >( );

    public static void AddObserver( Observer newObserver )
    {
        if( !listOfObservers.Contains( newObserver ) )
        {
            listOfObservers.Add( newObserver );
			return;
        } 
        Debug.LogError( "Cannot Add The Same Observer Twice" );
    }

    public static void AddUnityObservers( )
    {
        listOfUnityObservers = ( GameObject.FindGameObjectsWithTag( "UnityObserver" ) ).ToList( );
    }

    public static void RemoveAllObservers( )
    {
        listOfObservers.Clear( );
    }

    public static int ObserverCount( )
    {
        return listOfObservers.Count;
    }

    public static int UnityObserverCount( )
    {
        return listOfUnityObservers.Count( );
    }

    public static void Notify( string staticEventName )
    { 
        var stubbedObject = new Object( );
        SendToObservers( stubbedObject, staticEventName, "NoMessage" );
    }

    public static void NotifyExtendedMessage( string staticEventName, string extendedMessage )
    {
        var stubbedObject = new Object( );
        SendToObservers( stubbedObject, staticEventName, extendedMessage );
    }

    public static void NotifySendAll( Object sender, string staticEventName, string extendedMessage )
    {
        SendToObservers( sender, staticEventName, extendedMessage );
    }

    private static void SendToObservers( Object sender, string eventName, string extendedMessage )
    {
        GarbageCollectObservers( );
        foreach ( var observer in listOfObservers )
        {
            observer.OnNotify( sender, new EventArguments( eventName, extendedMessage ) );
        }
        NotifyUnityObservers( sender, eventName, extendedMessage );
    }

    private static void NotifyUnityObservers( Object sender, string unityEventName, string extendedMessage )
    {
        foreach ( var unityObserver in listOfUnityObservers )
        {
			List< UnityObserver > ObserverObjects = unityObserver.GetComponents< UnityObserver >( ).ToList( );
			ObserverObjects.ForEach( item => 
			                         item.OnNotify( sender, 
			                                        new EventArguments( unityEventName, extendedMessage ) ) );
        }
    }

    public static void GarbageCollectObservers( )
    {
        listOfObservers.RemoveAll( item => item == null );
        listOfUnityObservers.RemoveAll( item => item == null );
    }
}