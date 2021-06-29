using System;
using UnityEngine;
using System.Collections;

class Esp : MonoBehaviour
{
	public static Material chamsMob;
	public static Material chamsPlayer;
	public static Material chamsDroped;
	public static Material chamsDefault;
	public void Start()
	{
		chamsDefault = new Material(Shader.Find("Standard"))
		{
			hideFlags = HideFlags.DontSaveInEditor | HideFlags.HideInHierarchy
		};

		chamsMob = new Material(Shader.Find("Hidden/Internal-Colored"))
		{
			hideFlags = HideFlags.DontSaveInEditor | HideFlags.HideInHierarchy
		};

		chamsMob.SetInt("_SrcBlend", 5);
		chamsMob.SetInt("_DstBlend", 10);
		chamsMob.SetInt("_Cull", 0);
		chamsMob.SetInt("_ZTest", 8);
		chamsMob.SetInt("_ZWrite", 0);
		chamsMob.SetColor("_Color", Color.magenta);

		chamsPlayer = new Material(Shader.Find("Hidden/Internal-Colored"))
		{
			hideFlags = HideFlags.DontSaveInEditor | HideFlags.HideInHierarchy
		};

		chamsPlayer.SetInt("_SrcBlend", 5);
		chamsPlayer.SetInt("_DstBlend", 10);
		chamsPlayer.SetInt("_Cull", 0);
		chamsPlayer.SetInt("_ZTest", 8);
		chamsPlayer.SetInt("_ZWrite", 0);
		chamsPlayer.SetColor("_Color", Color.magenta);

		chamsDroped = new Material(Shader.Find("Hidden/Internal-Colored"))
		{
			hideFlags = HideFlags.DontSaveInEditor | HideFlags.HideInHierarchy
		};

		chamsDroped.SetInt("_SrcBlend", 5);
		chamsDroped.SetInt("_DstBlend", 10);
		chamsDroped.SetInt("_Cull", 0);
		chamsDroped.SetInt("_ZTest", 8);
		chamsDroped.SetInt("_ZWrite", 0);
		chamsDroped.SetColor("_Color", Color.gray);

	}

	IEnumerator ApplyChams()
	{
		yield return new WaitForSeconds(2);

		foreach (Mob mob in UnityEngine.Object.FindObjectsOfType(typeof(Mob)) as Mob[])
		{
			if (Main.chamsmob)
			{
				foreach (Renderer renderer in mob?.gameObject?.GetComponentsInChildren<Renderer>())
				{
					renderer.material = chamsMob;
				}
			}
			else
			{
				foreach (Renderer renderer in mob?.gameObject?.GetComponentsInChildren<Renderer>())
				{
					renderer.material = chamsDefault;
				}
			}
		}


		foreach (OnlinePlayer player in UnityEngine.Object.FindObjectsOfType(typeof(OnlinePlayer)) as OnlinePlayer[])
		{
			if (Main.chamsplayer)
			{
				foreach (Renderer renderer in player?.gameObject?.GetComponentsInChildren<Renderer>())
				{
					renderer.material = chamsPlayer;
				}
			}
            else
            {
				foreach (Renderer renderer in player?.gameObject?.GetComponentsInChildren<Renderer>())
				{
					renderer.material = chamsDefault;
				}
			}
		}

		foreach (PickupInteract droped in UnityEngine.Object.FindObjectsOfType(typeof(PickupInteract)) as PickupInteract[])
		{
			if (Main.chamsdroped)
			{
				foreach (Renderer renderer in droped?.gameObject?.GetComponentsInChildren<Renderer>())
				{
					renderer.material = chamsDroped;
				}
			}
			else
			{
				foreach (Renderer renderer in droped?.gameObject?.GetComponentsInChildren<Renderer>())
				{
					renderer.material = chamsDefault;
				}
			}
		}

		Main.chams = true;
	}

	public void Update()
    {
		if (Main.chams)
		{
			StartCoroutine(ApplyChams());
			Main.chams = false;
		}
	}

    public void OnGUI()
    {

		//Player Esp
		if (Main.playeresp)
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
					if (Main.playersnapline)
					{
						if (Main.lineposition == 1)
						{
							Drawing.DrawLine(new Vector2((float)(Screen.width / 2), (float)(Screen.height - 1)), new Vector2(w2s.x, (float)Screen.height - w2s.y), Color.red, 2f);//SnapLine
						}
						if (Main.lineposition == 2)
						{
							Drawing.DrawLine(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), new Vector2(w2s.x, (float)Screen.height - w2s.y), Color.red, 2f);//SnapLine
						}
					}
				}
			}
		}
		if (Main.mobesp)
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
					if (Main.mobsnaplines)
					{
						if (Main.lineposition == 1)
						{
							Drawing.DrawLine(new Vector2((float)(Screen.width / 2), (float)(Screen.height - 1)), new Vector2(w2s.x, (float)Screen.height - w2s.y), Color.red, 2f);//SnapLine
						}
						if (Main.lineposition == 2)
						{
							Drawing.DrawLine(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), new Vector2(w2s.x, (float)Screen.height - w2s.y), Color.red, 2f);//SnapLine
						}
					}
				}
			}
		}
		if (Main.treeesp)
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
					if (Main.treesnaplines)
					{
						if (Main.lineposition == 1)
						{
							Drawing.DrawLine(new Vector2((float)(Screen.width / 2), (float)(Screen.height - 1)), new Vector2(w2s.x, (float)Screen.height - w2s.y), Color.green, 2f);//SnapLine
						}
						if (Main.lineposition == 2)
						{
							Drawing.DrawLine(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), new Vector2(w2s.x, (float)Screen.height - w2s.y), Color.green, 2f);//SnapLine
						}
					}
				}
			}
		}
		if (Main.resourceesp)
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
					if (Main.resourcesnaplines)
					{
						if (Main.lineposition == 1)
						{
							Drawing.DrawLine(new Vector2((float)(Screen.width / 2), (float)(Screen.height - 1)), new Vector2(w2s.x, (float)Screen.height - w2s.y), Color.gray, 2f);//SnapLine
						}
						if (Main.lineposition == 2)
						{
							Drawing.DrawLine(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), new Vector2(w2s.x, (float)Screen.height - w2s.y), Color.gray, 2f);//SnapLine
						}
					}
				}
			}
		}

		if (Main.respawnshrine)
		{
			foreach (ShrineRespawn enemy in UnityEngine.Object.FindObjectsOfType(typeof(ShrineRespawn)) as ShrineRespawn[])
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
					GUI.Label(new Rect(w2s.x, (float)Screen.height - w2s.y, 0f, 0f), enemy.name.Replace("Interact", "Respawn Shrine") + " [" + distanceToint + "m]", style);
				}

			}
		}

		if (Main.bossshrine)
		{
			foreach (ShrineBoss enemy in UnityEngine.Object.FindObjectsOfType(typeof(ShrineBoss)) as ShrineBoss[])
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
					if (enemy.name == "Interact")
					{
						GUI.Label(new Rect(w2s.x, (float)Screen.height - w2s.y, 0f, 0f), enemy.name.Replace("Interact", "Chunk Shrine") + " [" + distanceToint + "m]", style);
					}
					else
					{
						GUI.Label(new Rect(w2s.x, (float)Screen.height - w2s.y, 0f, 0f), enemy.name.Replace("Interact (1)", "Gronk Shrine") + " [" + distanceToint + "m]", style);
					}
				}

			}
		}
		if (Main.mobshrine)

			foreach (ShrineInteractable enemy in UnityEngine.Object.FindObjectsOfType(typeof(ShrineInteractable)) as ShrineInteractable[])
			{

				GUIStyle style = new GUIStyle
				{
					alignment = TextAnchor.MiddleCenter
				};
				style.normal.textColor = Color.white;
				Vector3 w2s = Camera.main.WorldToScreenPoint(enemy.transform.position);
				if (w2s.z > 0f)
				{
					w2s.z = w2s.y + Screen.height;
					GUI.Label(new Rect(w2s.x, (float)Screen.height - w2s.y, 0f, 0f), enemy.name.Replace("Interact", "Mob Shrine"), style);
				}

			}
	}
}
