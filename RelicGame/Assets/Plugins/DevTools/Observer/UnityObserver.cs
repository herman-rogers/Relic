using UnityEngine;
using System.Collections;

public class UnityObserver : MonoBehaviour
{
    public void Awake( )
	{
        this.gameObject.tag = "UnityObserver";
        Subject.AddUnityObservers( );
    }

    public virtual void OnNotify( Object sender, EventArguments e )
	{
	}

	private void OnDestroy( )
	{
		Subject.GarbageCollectObservers( );
	}
}