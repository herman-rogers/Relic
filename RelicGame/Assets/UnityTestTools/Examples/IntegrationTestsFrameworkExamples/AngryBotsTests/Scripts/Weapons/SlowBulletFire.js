#pragma strict

var bulletPrefab : GameObject;
var frequency : float = 2;
var coneAngle : float = 1.5;
var fireSound : AudioClip;
var firing : boolean = false;

private var lastFireTime : float = -1;

function Update () {
	if (firing) {
		if (Time.time > lastFireTime + 1 / frequency) {
			Fire ();
		}
	}
}

function Fire () {
	// Spawn bullet
	var coneRandomRotation = Quaternion.Euler (Random.Range (-coneAngle, coneAngle), Random.Range (-coneAngle, coneAngle), 0);
	Spawner.Spawn (bulletPrefab, transform.position, transform.rotation * coneRandomRotation);
	
	if (GetComponent.<AudioSource>() && fireSound) {
		GetComponent.<AudioSource>().clip = fireSound;
		GetComponent.<AudioSource>().Play ();
	}
	
	lastFireTime = Time.time;
}

function OnStartFire () {
	firing = true;
}

function OnStopFire () {
	firing = false;
}