using UnityEngine;
using System.Collections;

public class MovementCommand : Command {

    public override void ExecuteWithCoordinates( GameObject controllableObject, Vector3 coordinates ) {
        controllableObject.GetComponent<RelicPlayerController>( ).MovePlayer( coordinates );
    }
}