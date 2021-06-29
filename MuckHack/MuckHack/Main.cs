using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;


class Main : MonoBehaviour
{
	public static bool flyhack = false;             //Main
	String flyHack;
	bool breakeverything = false;
	String breakEverything;
	bool godmode = false;
	String godMode;
	bool attack = false;
	String Attack;
	bool pickupall = false;
	String pickupAll;
	bool pingspammer = false;
	String pingSpammer;
	bool jumphack = false;
	String jumpHack;
	bool speedhack = false;
	String speedHack;
	bool mobteleporthit = false;
	String mobteleportHit;
	bool followplayer = false;
	String followPlayer;
	bool spawnboss = false;
	String spawnBoss;

	public static bool drawfov;
	public String drawFov;
	public static float smoothingammount = 2;
	public static bool aimbot;
	String aimBot;
	public static float Radius = 100;

	public static int lineposition;         //ESP
	public static bool playeresp;
	public static bool playersnapline;
	public static bool mobesp;
	public static bool mobsnaplines;
	public static bool resourceesp;
	public static bool resourcesnaplines;
	public static bool treeesp;
	public static bool treesnaplines;
	public static bool respawnshrine;
	public static bool bossshrine;
	public static bool mobshrine;

	public static bool chams = true;
	public static bool chamsmob = false;
	public static bool chamsplayer = false;
	public static bool chamsdroped = false;

	public static int tabs = 1;       //MENU
	public static bool toggles = true;
	public static bool menu = false;

	//New and need to add to menu
	public bool timechanger;
	public String timeChanger;
	public float time;
	public bool nograss = true;
	public String noGrass;
	public bool instarevive;
	public String instaRevive;
	public bool raycastteleport;
	public String raycastTeleport;
	public bool freerespawn;
	public String freeRespawn;
	public bool fov;
	public String Fov;
	public float fovvalue = 90f;
	
	private Vector3 FindPingPos()
	{
		Transform playerCam = PlayerMovement.Instance.playerCam;
		RaycastHit raycastHit;
		if (Physics.Raycast(playerCam.position, playerCam.forward, out raycastHit, 1500f))
		{
			Vector3 b = Vector3.zero;
			if (raycastHit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
			{
				b = Vector3.one;
			}
			return raycastHit.point + b;
		}
		return Vector3.zero;
	}

	public void Update()
	{

		if (Input.GetKeyDown(KeyCode.Insert))
		{
			menu = !menu;
		}
		if (Input.GetKeyDown(KeyCode.Delete))
		{
			toggles = !toggles;
		}

		if (Input.GetKeyDown(KeyCode.F1))
		{
			Main.flyhack = !Main.flyhack;
		}

		if (Input.GetKeyDown(KeyCode.F2))
		{
            this.godmode = !this.godmode;
        }

        if (Input.GetKeyDown(KeyCode.F3))
		{
			this.attack = !this.attack;
		}

		if (Input.GetKeyDown(KeyCode.F4))
		{
			this.speedhack = !this.speedhack;
		}

		if (Input.GetKeyDown(KeyCode.F5))
		{
			this.jumphack = !this.jumphack;
		}

		if (Input.GetKeyDown(KeyCode.F6))
		{
			this.breakeverything = !this.breakeverything;
		}

		if (this.fov)
		{
			Camera.main.fieldOfView = fovvalue;
		}
        else
        {
			Camera.main.fieldOfView = 85f;
        }

		if (this.freerespawn)
		{
			RespawnTotemUI.Instance.basePrice = 0;
		}

		if (this.raycastteleport)
		{
			if (Input.GetKeyDown(KeyCode.F))
			{
				Transform playerCam = PlayerMovement.Instance.playerCam;
				RaycastHit raycastHit;
				Physics.Raycast(playerCam.position, playerCam.forward, out raycastHit, 1500f);
				Vector3 b = Vector3.zero;
				if (raycastHit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
				{
					b = Vector3.one;
				}
				PlayerMovement.Instance.transform.position = raycastHit.point + b;
			}
		}


		if (this.instarevive && PlayerStatus.Instance.IsPlayerDead())
		{
			ClientSend.RevivePlayer(LocalClient.instance.myId, -1, false);
		}

		if (GameManager.state == GameManager.GameState.Playing && this.nograss)
		{
			GameObject.FindObjectOfType<DrawGrass>().sizeWidth = 0;
			GameObject.FindObjectOfType<DrawGrass>().sizeLength = 0;
			this.nograss = false;
		}
		else
		{
			nograss = false;
		}

		if (this.jumphack)
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				Rigidbody rb = PlayerMovement.Instance.GetRb();
				rb.AddRelativeForce(Vector3.up * 15f, ForceMode.Impulse);
			}
		}

		if (mobteleporthit && Input.GetKey(KeyCode.Mouse0))
		{
			foreach (Mob mob1 in UnityEngine.Object.FindObjectsOfType(typeof(Mob)) as Mob[])
			{
				PlayerMovement.Instance.transform.position = mob1.transform.position + new Vector3(0.0f, 0.0f, 3.0f);
			}
		}

		if (this.followplayer)
		{
			foreach (OnlinePlayer player in UnityEngine.Object.FindObjectsOfType(typeof(OnlinePlayer)) as OnlinePlayer[])
			{
				PlayerMovement.Instance.transform.position = player.transform.position;
			}
		}

		if (this.pingspammer)
		{
			Vector3 vector = this.FindPingPos();
			if (vector == Vector3.zero)
			{
				return;
			}
			UnityEngine.Object.Instantiate<GameObject>(PingController.Instance.pingPrefab, vector, Quaternion.identity).GetComponent<PlayerPing>().SetPing(GameManager.players[LocalClient.instance.myId].username, "");
			ClientSend.PlayerPing(vector);
		}
		if (this.spawnboss)
		{
			MobType bossMob = GameLoop.Instance.bosses[0];
			GameLoop.Instance.StartBoss(bossMob);
			this.spawnboss = false;
		}

		if (this.speedhack)
		{
			PlayerStatus.Instance.currentSpeedArmorMultiplier = 50;
		}
		else
		{
			PlayerStatus.Instance.currentSpeedArmorMultiplier = 1;
		}

		if (this.godmode)
		{
			PlayerStatus.Instance.maxHp = 9999;
			PlayerStatus.Instance.hp = (float)PlayerStatus.Instance.maxHp;
			PlayerStatus.Instance.maxHunger = 9999;
			PlayerStatus.Instance.hunger = (float)PlayerStatus.Instance.maxHunger;
			PlayerStatus.Instance.maxStamina = 9999;
			PlayerStatus.Instance.stamina = (float)PlayerStatus.Instance.maxStamina;
			PlayerStatus.Instance.maxShield = 9999;
			PlayerStatus.Instance.shield = (float)PlayerStatus.Instance.maxShield;
		}

		if (this.breakeverything)
		{
			Hitable[] array = UnityEngine.Object.FindObjectsOfType<Hitable>();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Hit(int.MaxValue, float.MaxValue, 9999, new Vector3(0f, 0f, 0f));
			}
			breakeverything = false;
		}

		if (this.attack)
		{
			Hotbar.Instance.currentItem.attackDamage = 999;
			Hotbar.Instance.currentItem.resourceDamage = 999;
			Hotbar.Instance.currentItem.attackSpeed = 999;
		}
        else
        {
			Hotbar.Instance.currentItem.attackDamage = 1;
			Hotbar.Instance.currentItem.resourceDamage = 1;
			Hotbar.Instance.currentItem.attackSpeed = 1;
		}

		if (this.pickupall)
		{
			PickupInteract[] array2 = UnityEngine.Object.FindObjectsOfType<PickupInteract>();
			for (int k = 0; k < array2.Length; k++)
			{
				array2[k].Interact();
			}
		}

		if(this.timechanger)
        {
			DayCycle.time = time;
		}

	}

    public void OnGUI()
	{
		if (Main.flyhack)
		{
			this.flyHack = "Enabled";
		}
		if (!Main.flyhack)
		{
			this.flyHack = "Disabled";
		}

		if (this.pickupall)
		{
			this.pickupAll = "Enabled";
		}
		if (!this.pickupall)
		{
			this.pickupAll = "Disabled";
		}

		if (this.attack)
		{
			this.Attack = "Enabled";
		}
		if (!this.attack)
		{
			this.Attack = "Disabled";
		}

		if (this.godmode)
		{
			this.godMode = "Enabled";
		}
		if (!this.godmode)
		{
			this.godMode = "Disabled";
		}

		if (this.speedhack)
		{
			this.speedHack = "Enabled";
		}
		if (!this.speedhack)
		{
			this.speedHack = "Disabled";
		}

		if (this.jumphack)
		{
			this.jumpHack = "Enabled";
		}
		if (!this.jumphack)
		{
			this.jumpHack = "Disabled";
		}

		if (this.mobteleporthit)
		{
			this.mobteleportHit = "Enabled";
		}
		if (!this.mobteleporthit)
		{
			this.mobteleportHit = "Disabled";
		}

		if (this.breakeverything)
		{
			this.breakEverything = "Enabled";
		}
		if (!this.breakeverything)
		{
			this.breakEverything = "Disabled";
		}

		if (this.followplayer)
		{
			this.followPlayer = "Enabled";
		}
		if (!this.followplayer)
		{
			this.followPlayer = "Disabled";
		}

		if (this.spawnboss)
		{
			this.spawnBoss = "Enabled";
		}
		if (!this.spawnboss)
		{
			this.spawnBoss = "Disabled";
		}

		if (this.pingspammer)
		{
			this.pingSpammer = "Enabled";
		}
		if (!this.pingspammer)
		{
			this.pingSpammer = "Disabled";
		}

		if (Main.aimbot)
		{
			this.aimBot = "Enabled";
		}
		if (!Main.aimbot)
		{
			this.aimBot = "Disabled";
		}

		if (Main.drawfov)
		{
			this.drawFov = "Enabled";
		}
		if (!Main.drawfov)
		{
			this.drawFov = "Disabled";
		}

		if (this.timechanger)
		{
			this.timeChanger = "Enabled";
		}
		if (!this.timechanger)
		{
			this.timeChanger = "Disabled";
		}

		if (this.nograss)
		{
			this.noGrass = "Enabled";
		}
		if (!this.nograss)
		{
			this.noGrass = "Disabled";
		}

		if (this.instarevive)
		{
			this.instaRevive = "Enabled";
		}
		if (!this.instarevive)
		{
			this.instaRevive = "Disabled";
		}

		if (this.raycastteleport)
		{
			this.raycastTeleport = "Enabled";
		}
		if (!this.raycastteleport)
		{
			this.raycastTeleport = "Disabled";
		}

		if (this.freerespawn)
		{
			this.freeRespawn = "Enabled";
		}
		if (!this.freerespawn)
		{
			this.freeRespawn = "Disabled";
		}

		if (this.fov)
		{
			this.Fov = "Enabled";
		}
		if (!this.fov)
		{
			this.Fov = "Disabled";
		}

		//WaterMark
		GUI.Box(new Rect(0f, 0f, (float)Screen.width, 30f), "ShitWare.cc -Aterror2be | FlyHack = F1 | GodMode = F2 |  AttackStats = F3 | SpeedHack = F4 | JumpHack = F5 | BreakEverything = F6 |");

		if (Main.toggles)
		{
			GUI.Box(new Rect(0f, 50f, 200f, 415f), "Toggles [Delete]");

			GUI.Label(new Rect(10f, 80f, 100f, 20f), "Aimbot");
			GUI.Label(new Rect(140f, 80f, 100f, 20f), this.aimBot);
			GUI.Label(new Rect(10f, 100f, 100f, 20f), "DrawFov");
			GUI.Label(new Rect(140f, 100f, 100f, 20f), this.drawFov);
			GUI.Label(new Rect(10f, 120f, 100f, 20f), "AttackStats");
			GUI.Label(new Rect(140f, 120f, 100f, 20f), this.Attack);
			GUI.Label(new Rect(10f, 140f, 100f, 20f), "MobTeleportHit");
			GUI.Label(new Rect(140f, 140f, 100f, 20f), this.mobteleportHit);
			GUI.Label(new Rect(10f, 160f, 100f, 20f), "FlyHack");
			GUI.Label(new Rect(140f, 160f, 100f, 20f), this.flyHack);
			GUI.Label(new Rect(10f, 180f, 100f, 20f), "PickUpAll");
			GUI.Label(new Rect(140f, 180f, 100f, 20f), this.pickupAll);
			GUI.Label(new Rect(10f, 200f, 100f, 20f), "GodMode");
			GUI.Label(new Rect(140f, 200f, 100f, 20f), this.godMode);
			GUI.Label(new Rect(10f, 220f, 100f, 20f), "SpeedHack");
			GUI.Label(new Rect(140f, 220f, 100f, 20f), this.speedHack);
			GUI.Label(new Rect(10f, 240f, 100f, 20f), "JumpHack");
			GUI.Label(new Rect(140f, 240f, 100f, 20f), this.jumpHack);
			GUI.Label(new Rect(10f, 260f, 100f, 20f), "RaycastTeleport");
			GUI.Label(new Rect(140f, 260f, 100f, 20f), this.raycastTeleport);
			GUI.Label(new Rect(10f, 280f, 100f, 20f), "InstantRevive");
			GUI.Label(new Rect(140f, 280f, 100f, 20f), this.instaRevive);
			GUI.Label(new Rect(10f, 300f, 100f, 20f), "FovChanger");
			GUI.Label(new Rect(140f, 300f, 100f, 20f), this.Fov);
			GUI.Label(new Rect(10f, 320f, 100f, 20f), "BreakEverything");
			GUI.Label(new Rect(140f, 320f, 100f, 20f), this.breakEverything);
			GUI.Label(new Rect(10f, 340f, 100f, 20f), "SpawnBoss");
			GUI.Label(new Rect(140f, 340f, 100f, 20f), this.spawnBoss);
			GUI.Label(new Rect(10f, 360f, 100f, 20f), "FollowPlayer");
			GUI.Label(new Rect(140f, 360f, 100f, 20f), this.followPlayer);
			GUI.Label(new Rect(10f, 380f, 100f, 20f), "PingSpammer");
			GUI.Label(new Rect(140f, 380f, 100f, 20f), this.pingSpammer);
			GUI.Label(new Rect(10f, 400f, 100f, 20f), "NoGrass");
			GUI.Label(new Rect(140f, 400f, 100f, 20f), this.noGrass);
			GUI.Label(new Rect(10f, 420f, 100f, 20f), "FreeRespawn");
			GUI.Label(new Rect(140f, 420f, 100f, 20f), this.freeRespawn);
			GUI.Label(new Rect(10f, 440f, 100f, 20f), "TimeChanger");
			GUI.Label(new Rect(140f, 440f, 100f, 20f), this.timeChanger);
		}

		if (menu)
		{
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;

			//Tabs
			GUI.Box(new Rect(210f, 50f, 500f, 415f), "");
			if (GUI.Button(new Rect(210f, 55f, 100, 20f), "ESP"))
			{
				tabs = 1;
			}
			if (GUI.Button(new Rect(310f, 55f, 100f, 20f), "Weapon"))
			{
				tabs = 2;
			}
			if (GUI.Button(new Rect(410f, 55f, 100, 20f), "Player"))
			{
				tabs = 3;
			}
			if (GUI.Button(new Rect(510f, 55f, 100f, 20f), "World"))
			{
				tabs = 4;
			}
			if (GUI.Button(new Rect(610f, 55f, 100f, 20f), "ItemSpawner"))
            {
                tabs = 5;
            }
			Drawing.DrawLine(new Vector2(210, 77), new Vector2(710, 77), Color.black);
			//Tabs

			switch (tabs)
			{
				case 1:
					//ESP tab
					GUI.Label(new Rect(220f, 80f, 200f, 20f), "SnapLine Position");

					if (GUI.Button(new Rect(220, 105, 100, 20), "Bottom"))
					{
						lineposition = 1;
					}
					if (GUI.Button(new Rect(320, 105, 100, 20), "Center"))
					{
						lineposition = 2;
					}
					Drawing.DrawLine(new Vector2(210, 130), new Vector2(710, 130), Color.black);

					GUI.Label(new Rect(220f, 140f, 100f, 20f), "Player Esp");
                    playeresp = GUI.Toggle(new Rect(220, 160, 100, 15), playeresp, "Enabled");
					playersnapline = GUI.Toggle(new Rect(320, 160, 100, 15), playersnapline, "Snaplines");
					chamsplayer = GUI.Toggle(new Rect(420, 160, 100, 15), chamsplayer, "Player Chams");
					Drawing.DrawLine(new Vector2(210, 180), new Vector2(710, 180), Color.black);

					GUI.Label(new Rect(220f, 190f, 100f, 20f), "Mob Esp");
					mobesp = GUI.Toggle(new Rect(220, 210, 100, 15), mobesp, "Enabled");
					mobsnaplines = GUI.Toggle(new Rect(320, 210, 100, 15), mobsnaplines, "Snaplines");
					chamsmob = GUI.Toggle(new Rect(420, 210, 100, 15), chamsmob, "Mob Chams");
					Drawing.DrawLine(new Vector2(210, 230), new Vector2(710, 230), Color.black);

					GUI.Label(new Rect(220f, 240f, 100f, 20f), "Resource Esp");
					resourceesp = GUI.Toggle(new Rect(220, 260, 100, 15), resourceesp, "Enabled");
					resourcesnaplines = GUI.Toggle(new Rect(320, 260, 100, 15), resourcesnaplines, "Snaplines");
					chamsdroped = GUI.Toggle(new Rect(420, 260, 200, 15), chamsdroped, "Ground Chams");
					Drawing.DrawLine(new Vector2(210, 280), new Vector2(710, 280), Color.black);

					GUI.Label(new Rect(220f, 290f, 100f, 20f), "Tree Esp");
					treeesp = GUI.Toggle(new Rect(220, 310, 100, 15), treeesp, "Enabled");
					treesnaplines = GUI.Toggle(new Rect(320, 310, 100, 15), treesnaplines, "Snaplines");
					Drawing.DrawLine(new Vector2(210, 330), new Vector2(710, 330), Color.black);

					GUI.Label(new Rect(220f, 340f, 100f, 20f), "Shrine Esp");
					mobshrine = GUI.Toggle(new Rect(220, 360, 100, 15), mobshrine, "Mob Shrine");
					bossshrine = GUI.Toggle(new Rect(320, 360, 100, 15), bossshrine, "Boss Shrine");
					respawnshrine = GUI.Toggle(new Rect(420, 360, 200, 15), respawnshrine, "Respawn Shrine");
					Drawing.DrawLine(new Vector2(210, 380), new Vector2(710, 380), Color.black);
					break;
				case 2:
					//Weapon
					 GUI.Label(new Rect(220f, 80f, 200f, 20f), "Aimbot");
					 aimbot = GUI.Toggle(new Rect(220, 100, 100, 15), aimbot, "Enabled");
					 Drawing.DrawLine(new Vector2(210, 120), new Vector2(710, 120), Color.black);

					 drawfov = GUI.Toggle(new Rect(220, 130, 100, 15), drawfov, "Draw Fov");
					 Drawing.DrawLine(new Vector2(210, 150), new Vector2(710, 150), Color.black);

					 GUI.Label(new Rect(220f, 160f, 200f, 20f), "Aimbot Fov: " + Radius.ToString());
					 Main.Radius = GUI.HorizontalSlider(new Rect(220f, 180f, 200f, 20f), Main.Radius, 1f, 1200f);
					 Drawing.DrawLine(new Vector2(210, 200), new Vector2(710, 200), Color.black);

					GUI.Label(new Rect(220f, 210f, 200f, 20f), "Smoothing Ammount: " + smoothingammount.ToString());
					smoothingammount = GUI.HorizontalSlider(new Rect(220f, 230f, 200f, 20f), smoothingammount, 1f, 5f);
					Drawing.DrawLine(new Vector2(210, 250), new Vector2(710, 250), Color.black);

					attack = GUI.Toggle(new Rect(220, 260, 125, 15), attack, "AttackStats");
					Drawing.DrawLine(new Vector2(210, 280), new Vector2(710, 280), Color.black);

					mobteleporthit = GUI.Toggle(new Rect(220, 290, 200, 15), mobteleporthit, "MobTeleportHit");
					Drawing.DrawLine(new Vector2(210, 310), new Vector2(710, 310), Color.black);
					break;
				case 3:
					//Player tab
					GUI.Label(new Rect(220f, 80f, 200f, 20f), "Player");
					flyhack = GUI.Toggle(new Rect(220, 100, 100, 17), flyhack, "Flyhack");
					Drawing.DrawLine(new Vector2(210, 120), new Vector2(710, 120), Color.black);

					pickupall = GUI.Toggle(new Rect(220, 130, 100, 15), pickupall, "PickUpAll");
					Drawing.DrawLine(new Vector2(210, 150), new Vector2(710, 150), Color.black);

					godmode = GUI.Toggle(new Rect(220, 160, 100, 15), godmode, "GodMode");
					Drawing.DrawLine(new Vector2(210, 180), new Vector2(710, 180), Color.black);

					speedhack = GUI.Toggle(new Rect(220, 190, 100, 15), speedhack, "SpeedHack");
					Drawing.DrawLine(new Vector2(210, 210), new Vector2(710, 210), Color.black);

					jumphack = GUI.Toggle(new Rect(220, 220, 100, 15), jumphack, "JumpHack");
					Drawing.DrawLine(new Vector2(210, 240), new Vector2(710, 240), Color.black);

					raycastteleport = GUI.Toggle(new Rect(220, 250, 200, 15), raycastteleport, "RayCast Teleport");
					Drawing.DrawLine(new Vector2(210, 270), new Vector2(710, 270), Color.black);

					instarevive = GUI.Toggle(new Rect(220, 280, 200, 15), instarevive, "Instant Revive");
					Drawing.DrawLine(new Vector2(210, 300), new Vector2(710, 300), Color.black);

					fov = GUI.Toggle(new Rect(220, 310, 100, 15), fov, "Fov Changer");
					fovvalue = GUI.HorizontalSlider(new Rect(220f, 330f, 200f, 20f), fovvalue, 1f, 180f);
					Drawing.DrawLine(new Vector2(210, 350), new Vector2(710, 350), Color.black);

					break;
				case 4:
					//World tab
					GUI.Label(new Rect(220f, 80f, 200f, 20f), "World");

					if (GUI.Button(new Rect(220, 100, 150, 20), "BreakEverything"))
					{
						breakeverything = true;
					}
					Drawing.DrawLine(new Vector2(210, 125), new Vector2(710, 125), Color.black);
					if (GUI.Button(new Rect(220, 135, 150, 20), "SpawnBoss"))
					{
						spawnboss = true;
					}
					Drawing.DrawLine(new Vector2(210, 160), new Vector2(710, 160), Color.black);

					followplayer = GUI.Toggle(new Rect(220, 170, 100, 15), followplayer, "FollowPlayer");
					Drawing.DrawLine(new Vector2(210, 190), new Vector2(710, 190), Color.black);

					pingspammer = GUI.Toggle(new Rect(220, 200, 100, 15), pingspammer, "PingSpammer");
					Drawing.DrawLine(new Vector2(210, 220), new Vector2(710, 220), Color.black);

					nograss = GUI.Toggle(new Rect(220, 230, 100, 15), nograss, "No Grass");
					Drawing.DrawLine(new Vector2(210, 250), new Vector2(710, 250), Color.black);

					freerespawn = GUI.Toggle(new Rect(220, 260, 200, 15), freerespawn, "Free Respawn");
					Drawing.DrawLine(new Vector2(210, 280), new Vector2(710, 280), Color.black);

					timechanger = GUI.Toggle(new Rect(220, 290, 200, 15), timechanger, "Time Changer");
					time = GUI.HorizontalSlider(new Rect(220f, 310f, 200f, 20f), time, 0f, 1f);
					Drawing.DrawLine(new Vector2(210, 330), new Vector2(710, 330), Color.black);

					break;
				case 5:
					ItemGenerator.DrawMenu();
					break;
			}
		}
        else
        {
			//Cursor.visible = false;
			//Cursor.lockState = CursorLockMode.Locked;
		}
	}
}
