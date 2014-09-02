using UnityEngine;
using System.Collections;

public class Observer
{
    public Observer( )
	{
        Subject.AddObserver( this );
    }

    public virtual void OnNotify( UnityEngine.Object sender, EventArguments e ){ }

	public virtual void UnityUpdate( ){ }
}

public class EventArguments
{
    public string eventMessage{ get; set; }
    public string extendedMessage{ get; set; }
    public int extendedMessageNumber{ get; private set; }

    public EventArguments( string newEventMessage, string newExtendedMessage )
	{
        int newInteger;
        eventMessage = newEventMessage;
        extendedMessage = newExtendedMessage;
        if( int.TryParse( newExtendedMessage, out newInteger ) )
		{
            extendedMessageNumber = newInteger;
        }
    }
}