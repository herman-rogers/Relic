using UnityEngine;
using System.Collections;

public class TurbineSceneEventListener : Observer {

	public override void OnNotify (object sender, EventArguments e) {
		switch( e.EventMessage ){
		    case "TestEvent":
			    OutdoorsTextEvents( );
			    break;
		}
	}

	void OutdoorsTextEvents( ) {
		Debug.Log("Meow");
	}
}