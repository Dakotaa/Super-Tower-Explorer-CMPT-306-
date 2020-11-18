﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunTower : Tower {
	// change in values for each subsequent level
	public float levelup_searchInterval = -0.1f;
	public float levelup_searchRange = 1.0f;
	public float levelup_cooldown = -0.1f;
	public float levelup_damage = 0.1f;
	public float levelup_bulletVelocity = 1.0f;
	public List<Sprite> bodies = new List<Sprite>();
	public int numShots = 6;
	public float spread = 10.0f;
	private SpriteRenderer body;

    public override void Start() {
		base.Start();
		body = transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>();
		body.sprite = bodies[0];
		this.level = 1;
		this.maxLevel = 5;
		this.upgradable = true;
	}

	public override string GetInfo() {
		string info =	"Level " + this.level + " Shotgun Tower\n" +
						"Targeting Speed: " + this.searchInterval.ToString("n2") + "s\n" +
						"Targeting Range: " + this.searchRange.ToString("n2") + "\n" +
						"Cooldown: " + this.cooldown.ToString("n2") + "s\n" +
						"Damage: " + this.damage.ToString("n2") + "\n" +
						"Bullet Velocity: " + this.bulletVelocity.ToString("n2");

		if (this.level < this.maxLevel) {
			info += "\n" +
					"Click to upgrade (1 metal)";
		}

		return info;
	}

	// shoot at an enemy
	public override bool ShootAt(Vector3 targetPos) {
		Vector3 bulletPos = new Vector3(transform.position.x + 0.5f, transform.position.y + 0.5f, 0.0f);
		for (int i = 0; i < numShots; i++) {
			Bullet shotBullet = Instantiate(bullet, bulletPos, Quaternion.identity);
			Vector3 direction = (targetPos - transform.position).normalized;
			direction.x += (Random.Range(-(spread / 2), (spread / 2)))/100;
			direction.y += (Random.Range(-(spread / 2), (spread / 2)))/100;
			shotBullet.Setup(direction, bulletVelocity);
		}
		return true;
	}

	public override void IncreaseLevel() {
		if (this.IsMaxLevel()) return;
		this.level++;
		this.searchInterval += this.levelup_searchInterval;
		this.searchRange += this.levelup_searchRange;
		this.cooldown += this.levelup_cooldown;
		this.damage += this.levelup_damage;
		this.bulletVelocity += this.levelup_bulletVelocity;
		body.sprite = bodies[this.level - 1];
		tooltip.Set(this.GetInfo());
	}


}
