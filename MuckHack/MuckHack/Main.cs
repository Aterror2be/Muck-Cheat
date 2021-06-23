using System;
using UnityEngine;

namespace MuckHack
{

	class Main : MonoBehaviour
    {
		bool flyhack = false;             //Main
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
		

		int lineposition;         //ESP
		bool playeresp = false;
		bool playersnapline = false;
		bool mobesp = false;
		bool mobsnaplines = false;
		bool resourceesp = false;
		bool resourcesnaplines = false;
		bool treeesp = false;
		bool treesnaplines = false;

		int tabs = 1;       //MENU
		bool toggles = true;
		bool menu = false;

		public void MakePing(Vector3 pos, string name, string pingedName)
		{
			UnityEngine.Object.Instantiate<GameObject>(PingController.Instance.pingPrefab, pos, Quaternion.identity).GetComponent<PlayerPing>().SetPing(name, pingedName);
		}
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
		public void FixedUpdate()
        {
			if(this.flyhack)
            {
				if (!Input.GetKey(InputManager.jump) || !Input.GetKey(InputManager.forward) || !Input.GetKey(InputManager.backwards) || !Input.GetKey(InputManager.left) || !Input.GetKey(InputManager.right))//Hover Code
				{
					Rigidbody rb1 = PlayerMovement.Instance.GetRb();
					rb1.AddForce(Vector3.up * 65);
				}
			}
		}
		public void Update()
        {
			if(Input.GetKeyDown(KeyCode.Insert))
            {
				menu = !menu;
            }
			if (Input.GetKeyDown(KeyCode.Delete))
			{
				toggles = !toggles;
			}

			if (Input.GetKeyDown(KeyCode.F1))
			{
				this.flyhack = !this.flyhack;
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

			if (this.flyhack)
			{
				PlayerMovement.Instance.GetRb().velocity = new Vector3(0f, 0f, 0f);
				float speed = Input.GetKey(KeyCode.LeftControl) ? 0.5f : (Input.GetKey(InputManager.sprint) ? 1f : 0.5f);
				if (Input.GetKey(InputManager.jump))
				{
					PlayerStatus.Instance.transform.position = new Vector3(PlayerStatus.Instance.transform.position.x, PlayerStatus.Instance.transform.position.y + speed, PlayerStatus.Instance.transform.position.z);
				}
				Vector3 playerTransformPosVec = PlayerStatus.Instance.transform.position;
				if (Input.GetKey(InputManager.forward))
				{
					PlayerStatus.Instance.transform.position = new Vector3(playerTransformPosVec.x + Camera.main.transform.forward.x * Camera.main.transform.up.y * speed, playerTransformPosVec.y + Camera.main.transform.forward.y * speed, playerTransformPosVec.z + Camera.main.transform.forward.z * Camera.main.transform.up.y * speed);
				}
				if (Input.GetKey(InputManager.backwards))
				{
					PlayerStatus.Instance.transform.position = new Vector3(playerTransformPosVec.x - Camera.main.transform.forward.x * Camera.main.transform.up.y * speed, playerTransformPosVec.y - Camera.main.transform.forward.y * speed, playerTransformPosVec.z - Camera.main.transform.forward.z * Camera.main.transform.up.y * speed);
				}
				if (Input.GetKey(InputManager.right))
				{
					PlayerStatus.Instance.transform.position = new Vector3(playerTransformPosVec.x + Camera.main.transform.right.x * speed, playerTransformPosVec.y, playerTransformPosVec.z + Camera.main.transform.right.z * speed);
				}
				if (Input.GetKey(InputManager.left))
				{
					PlayerStatus.Instance.transform.position = new Vector3(playerTransformPosVec.x - Camera.main.transform.right.x * speed, playerTransformPosVec.y, playerTransformPosVec.z - Camera.main.transform.right.z * speed);
				}
			}

			if(this.jumphack)
            {
				if (Input.GetKeyDown(KeyCode.Space))
				{
					Rigidbody rb = PlayerMovement.Instance.GetRb();
					rb.AddRelativeForce(Vector3.up * 15f, ForceMode.Impulse);
				}
			}

			if(mobteleporthit && Input.GetKey(KeyCode.Mouse2))
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
				this.MakePing(vector, GameManager.players[LocalClient.instance.myId].username, "");
				ClientSend.PlayerPing(vector);
			}
			if(this.spawnboss)
            {
				MobType bossMob = GameLoop.Instance.bosses[0];
				GameLoop.Instance.StartBoss(bossMob);
				this.spawnboss = false;
			}

			if(this.speedhack)
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

			if(this.breakeverything)
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
				Hotbar.Instance.currentItem.attackDamage = int.MaxValue;
				Hotbar.Instance.currentItem.resourceDamage = int.MaxValue;
				Hotbar.Instance.currentItem.attackSpeed = float.MaxValue;
			}
			if(this.pickupall)
            {
				PickupInteract[] array2 = UnityEngine.Object.FindObjectsOfType<PickupInteract>();
				for (int k = 0; k < array2.Length; k++)
				{
					array2[k].Interact();
				}
			}

		}
		public void OnGUI()
        {

			//Player Esp
			if (this.playeresp)
			{
				foreach (OnlinePlayer player in UnityEngine.Object.FindObjectsOfType(typeof(OnlinePlayer)) as OnlinePlayer[])
				{
					float distance = Vector3.Distance(PlayerStatus.Instance.transform.position, player.transform.position);
					int distanceToint = (int)distance;
					GUIStyle style = new GUIStyle
					{
						alignment = TextAnchor.MiddleCenter
					};
					style.normal.textColor = Color.white;
					Vector3 w2s = Camera.main.WorldToScreenPoint(player.transform.position);
					if (w2s.z > 0f)
					{
						GUI.Label(new Rect(w2s.x, (float)Screen.height - w2s.y, 0f, 0f), player.name.Replace("(Clone)", "") + " [" + distanceToint + "m]", style);//Name Esp
						if(playersnapline)
                        {
							if (lineposition == 1)
							{
								Drawing.DrawLine(new Vector2((float)(Screen.width / 2), (float)(Screen.height - 1)), new Vector2(w2s.x, (float)Screen.height - w2s.y), Color.red, 2f);//SnapLine
							}
							if (lineposition == 2)
							{
								Drawing.DrawLine(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), new Vector2(w2s.x, (float)Screen.height - w2s.y), Color.red, 2f);//SnapLine
							}
						}
					}
				}
			}
			if(this.mobesp)
            {
				//Mob Esp
				foreach (Mob enemy in UnityEngine.Object.FindObjectsOfType(typeof(Mob)) as Mob[])
				{
					float distance = Vector3.Distance(PlayerStatus.Instance.transform.position, enemy.transform.position);
					int distanceToint = (int)distance;
					GUIStyle style = new GUIStyle
					{
						alignment = TextAnchor.MiddleCenter
					};
					style.normal.textColor = Color.white;
					Vector3 w2s = Camera.main.WorldToScreenPoint(enemy.transform.position);
					if (w2s.z > 0f)
					{
						w2s.z = w2s.y + Screen.height;
						GUI.Label(new Rect(w2s.x, (float)Screen.height - w2s.y, 0f, 0f), enemy.name.Replace("(Clone)", "") + " [" + distanceToint + "m]", style);
						if (this.mobsnaplines)
						{
							if (lineposition == 1)
							{
								Drawing.DrawLine(new Vector2((float)(Screen.width / 2), (float)(Screen.height - 1)), new Vector2(w2s.x, (float)Screen.height - w2s.y), Color.red, 2f);//SnapLine
							}
							if (lineposition == 2)
							{
								Drawing.DrawLine(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), new Vector2(w2s.x, (float)Screen.height - w2s.y), Color.red, 2f);//SnapLine
							}
						}
					}
				}
			}
            if (this.treeesp)
            {
                //Tree Esp
                foreach (HitableTree tree in UnityEngine.Object.FindObjectsOfType(typeof(HitableTree)) as HitableTree[])
                {
                    GUIStyle style = new GUIStyle
                    {
                        alignment = TextAnchor.MiddleCenter
                    };
                    style.normal.textColor = Color.white;
                    Vector3 w2s = Camera.main.WorldToScreenPoint(tree.transform.position);
                    if (w2s.z > 0f)
                    {
                        w2s.z = w2s.y + Screen.height;
                        GUI.Label(new Rect(w2s.x, (float)Screen.height - w2s.y, 0f, 0f), tree.name.Replace("(Clone)", "") + " [" + tree.hp + " HP]", style);
                        if (this.treesnaplines)
                        {
                            if (lineposition == 1)
                            {
                                Drawing.DrawLine(new Vector2((float)(Screen.width / 2), (float)(Screen.height - 1)), new Vector2(w2s.x, (float)Screen.height - w2s.y), Color.green, 2f);//SnapLine
                            }
                            if (lineposition == 2)
                            {
                                Drawing.DrawLine(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), new Vector2(w2s.x, (float)Screen.height - w2s.y), Color.green, 2f);//SnapLine
                            }
                        }
                    }
                }
            }
			if(this.resourceesp)
            {
				foreach (HitableRock resource in UnityEngine.Object.FindObjectsOfType(typeof(HitableRock)) as HitableRock[])
				{
					GUIStyle style = new GUIStyle
					{
						alignment = TextAnchor.MiddleCenter
					};
					style.normal.textColor = Color.white;
					Vector3 w2s = Camera.main.WorldToScreenPoint(resource.transform.position);
					if (w2s.z > 0f)
					{
						w2s.z = w2s.y + Screen.height;
						GUI.Label(new Rect(w2s.x, (float)Screen.height - w2s.y, 0f, 0f), resource.name.Replace("(Clone)", "") + " [" + resource.hp + " HP]", style);
						if (this.resourcesnaplines)
						{
							if (lineposition == 1)
							{
								Drawing.DrawLine(new Vector2((float)(Screen.width / 2), (float)(Screen.height - 1)), new Vector2(w2s.x, (float)Screen.height - w2s.y), Color.gray, 2f);//SnapLine
							}
							if (lineposition == 2)
							{
								Drawing.DrawLine(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), new Vector2(w2s.x, (float)Screen.height - w2s.y), Color.gray, 2f);//SnapLine
							}
						}
					}
				}
			}

            if (this.flyhack)
			{
				this.flyHack = "Enabled";
			}
			if (!this.flyhack)
			{
				this.flyHack = "Disabled";
			}

			if(this.pickupall)
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


			//WaterMark
			GUI.Box(new Rect(0f, 0f, (float)Screen.width, 30f), "ShitWare.cc -Aterror2be#3778 | FlyHack = F1 | GodMode = F2 |  AttackStats = F3 | SpeedHack = F4 | JumpHack = F5 | BreakEverything = F6 |");

			if(this.toggles)
            {
				GUI.Box(new Rect(0f, 50f, 200f, 400f),"Toggles [Delete]");
				GUI.Label(new Rect(10f, 80f, 100f, 20f), "FlyHack");
				GUI.Label(new Rect(140f, 80f, 100f, 20f), this.flyHack);
				GUI.Label(new Rect(10f, 100f, 100f, 20f), "PickUpAll");
				GUI.Label(new Rect(140f, 100f, 100f, 20f), this.pickupAll);
				GUI.Label(new Rect(10f, 120f, 100f, 20f), "AttackStats");
				GUI.Label(new Rect(140f, 120f, 100f, 20f), this.Attack);
				GUI.Label(new Rect(10f, 140f, 100f, 20f), "GodMode");
				GUI.Label(new Rect(140f, 140f, 100f, 20f), this.godMode);
				GUI.Label(new Rect(10f, 160f, 100f, 20f), "SpeedHack");
				GUI.Label(new Rect(140f, 160f, 100f, 20f), this.speedHack);
				GUI.Label(new Rect(10f, 180f, 100f, 20f), "JumpHack");
				GUI.Label(new Rect(140f, 180f, 100f, 20f), this.jumpHack);
				GUI.Label(new Rect(10f, 200f, 100f, 20f), "MobTeleportHit");
				GUI.Label(new Rect(140f, 200f, 100f, 20f), this.mobteleportHit);
				GUI.Label(new Rect(10f, 220f, 100f, 20f), "BreakEverything");
				GUI.Label(new Rect(140f, 220f, 100f, 20f), this.breakEverything);
				GUI.Label(new Rect(10f, 240f, 100f, 20f), "FollowPlayer");
				GUI.Label(new Rect(140f, 240f, 100f, 20f), this.followPlayer);
				GUI.Label(new Rect(10f, 260f, 100f, 20f), "SpawnBoss");
				GUI.Label(new Rect(140f, 260f, 100f, 20f), this.spawnBoss);
				GUI.Label(new Rect(10f, 280f, 100f, 20f), "PingSpammer");
				GUI.Label(new Rect(140f, 280f, 100f, 20f), this.pingSpammer);
			}

			if (menu)
            {
			//Tabs
                GUI.Box(new Rect(210f, 50f, 500f, 400f),"");
				if (GUI.Button(new Rect(210f, 55f, 166.6f, 20f), "ESP"))
				{
					tabs = 1;
				}
				if (GUI.Button(new Rect(376.6f, 55f, 166.6f, 20f), "Player"))
				{
					tabs = 2;
				}
/*				if (GUI.Button(new Rect(460f, 55f, 125f, 20f), "ItemSpawner"))
				{
					tabs = 3;
				}*/
				if (GUI.Button(new Rect(543.2f, 55f, 166.6f, 20f), "World"))
				{
					tabs = 4;
				}
				Drawing.DrawLine(new Vector2(210, 77), new Vector2(710, 77), Color.black);
			//Tabs

				switch (tabs)
				{
					case 1:
						//ESP tab
						GUI.Label(new Rect(220f, 80f, 200f, 20f), "SnapLine Position");

						if (GUI.Button(new Rect(220, 110, 100, 20), "Bottom"))
						{
							lineposition = 1;
						}
						if (GUI.Button(new Rect(220, 135, 100, 20), "Center"))
						{
							lineposition = 2;
						}
						Drawing.DrawLine(new Vector2(210, 160), new Vector2(710, 160), Color.black);

						GUI.Label(new Rect(220f, 170f, 100f, 20f), "Player Esp");
						playeresp = GUI.Toggle(new Rect(220, 190, 100, 15), playeresp, "Enabled");
						playersnapline = GUI.Toggle(new Rect(220, 210, 100, 15), playersnapline, "Snaplines");
						Drawing.DrawLine(new Vector2(210, 230), new Vector2(710, 230), Color.black);

						GUI.Label(new Rect(220f, 240f, 100f, 20f), "Mob Esp");
						mobesp = GUI.Toggle(new Rect(220, 260, 100, 15), mobesp, "Enabled");
						mobsnaplines = GUI.Toggle(new Rect(220, 280, 100, 15), mobsnaplines, "Snaplines");
						Drawing.DrawLine(new Vector2(210, 300), new Vector2(710, 300), Color.black);

                        GUI.Label(new Rect(220f, 310f, 100f, 20f), "Resource Esp");
						resourceesp = GUI.Toggle(new Rect(220, 330, 100, 15), resourceesp, "Enabled");
						resourcesnaplines = GUI.Toggle(new Rect(220, 350, 100, 15), resourcesnaplines, "Snaplines");
						Drawing.DrawLine(new Vector2(210, 370), new Vector2(710, 370), Color.black);

						GUI.Label(new Rect(220f, 380f, 100f, 20f), "Tree Esp");
						treeesp = GUI.Toggle(new Rect(220, 400, 100, 15), treeesp, "Enabled");
						treesnaplines = GUI.Toggle(new Rect(220, 420, 100, 15), treesnaplines, "Snaplines");
						break;
					case 2:
						//Player tab
						GUI.Label(new Rect(220f, 80f, 200f, 20f), "Player");

						flyhack = GUI.Toggle(new Rect(220, 100, 100, 17), flyhack, "Flyhack");
						Drawing.DrawLine(new Vector2(210, 120), new Vector2(710, 120), Color.black);

						pickupall = GUI.Toggle(new Rect(220, 130, 100, 15), pickupall, "PickUpAll");
						Drawing.DrawLine(new Vector2(210, 150), new Vector2(710, 150), Color.black);

						attack = GUI.Toggle(new Rect(220, 160, 100, 15), attack, "AttackStats");
						Drawing.DrawLine(new Vector2(210, 180), new Vector2(710, 180), Color.black);

						godmode = GUI.Toggle(new Rect(220, 190, 100, 15), godmode, "GodMode");
						Drawing.DrawLine(new Vector2(210, 210), new Vector2(710, 210), Color.black);

						speedhack = GUI.Toggle(new Rect(220, 220, 100, 15), speedhack, "SpeedHack");
						Drawing.DrawLine(new Vector2(210, 240), new Vector2(710, 240), Color.black);

						jumphack = GUI.Toggle(new Rect(220, 250, 100, 15), jumphack, "JumpHack");
						Drawing.DrawLine(new Vector2(210, 270), new Vector2(710, 270), Color.black);

						mobteleporthit = GUI.Toggle(new Rect(220, 280, 125, 15), mobteleporthit, "MobTeleportHit");
						Drawing.DrawLine(new Vector2(210, 300), new Vector2(710, 300), Color.black);

						break;
					case 3:
						//ItemSpawner tab
						GUI.Label(new Rect(220f, 80f, 200f, 20f), "Items");

						if (GUI.Button(new Rect(220, 100, 150, 20), "BreakEverything"))
						{
							breakeverything = true;
						}
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
						break;
				}
			}
		}
    }
}
