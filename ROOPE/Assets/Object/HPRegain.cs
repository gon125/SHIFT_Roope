﻿using UnityEngine;
using System.Collections;

public class HPRegain : Item
{
	void OnTriggerEnter2D (Collider2D other)
	{
	}

	// HP +1
	public override void collideWithCharacter(Player player)
    {
		FindObjectOfType<GameManager> ().addHP (1);
    }

	public override RopeCollisionType collideWithRopeHead (Rope rope) {
		return RopeCollisionType.CAN_NOT_ATTACH_AND_THROUGH;
	}

	public override RopeCollisionType collideWithRopeLine (RopeLine line) {
		return RopeCollisionType.CAN_NOT_ATTACH_AND_THROUGH;
	}
}