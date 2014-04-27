using UnityEngine;
using System.Collections;

public class OutdoorsOneTextEvents : Observer {

	protected override void OnNotify( ) {

	}

	private void OutdoorsTextEvents( ) {
		Debug.Log("Meow");
	}
}
