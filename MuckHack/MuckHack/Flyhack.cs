using System;
using UnityEngine;

class Flyhack : MonoBehaviour
{
	public void FixedUpdate()
	{
		if (Main.flyhack)
		{
			if (!Input.GetKey(InputManager.jump) || !Input.GetKey(InputManager.forward) || !Input.GetKey(InputManager.backwards) || !Input.GetKey(InputManager.left) || !Input.GetKey(InputManager.right))
			{
				Rigidbody rb1 = PlayerMovement.Instance.GetRb();
				rb1.AddForce(Vector3.up * 65);
			}
		}
	}
	public void Update()
    {
		if (Main.flyhack)
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
	}
}
