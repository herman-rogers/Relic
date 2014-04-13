using UnityEngine;
using System.Collections;

public class CharacterControllerTest : MonoBehaviour {
	public CharacterController characterController;
	
	void Start( ){
	characterController.MoveMonsterOnXAxis( 10.0f );
//	characterController.BlendNewMovement( -10.0f, CharacterController.AnimationList.Running );
	//characterController.BlendNewMovement( 10.0f, dyCharacterController.AnimationList.Running );
	//	characterController.BlendNewAnimation( dyCharacterController.AnimationList.Running );
	}
}
