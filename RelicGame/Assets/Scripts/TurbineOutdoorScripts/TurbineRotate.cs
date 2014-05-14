using UnityEngine;
using System.Collections;

public class TurbineRotate : MonoBehaviour {

	public GameObject windTurbineOne;
	public GameObject windTurbineTwo;
	public static bool activateFirstWindmill = false;
	public static bool activateSecondWindmill = true;
	public static bool activateBothWindmills = false;

	//TODO: Possible make this some sort of state rather than bools
	// STubbed for now
	void Start( ) {
		if( activateFirstWindmill || activateSecondWindmill && !activateBothWindmills ) {
		    StartCoroutine( RotateSprite( ) );
		} 
		if ( activateBothWindmills ){
			StartCoroutine( RotateSprite( ) );
		}
	}

	IEnumerator RotateSprite( ) {
		while( true ){
			if( activateFirstWindmill ) {
			    windTurbineOne.transform.Rotate( Vector3.forward );
			}
			if( !activateSecondWindmill ){
			    windTurbineTwo.transform.Rotate( Vector3.forward );
			}
			yield return new WaitForSeconds( 0.009f );
		}
	}

	IEnumerator RotateBothSprites( ){
		while( true ){
			windTurbineOne.transform.Rotate( Vector3.forward );
			windTurbineTwo.transform.Rotate( Vector3.forward );
		}
	}
}
