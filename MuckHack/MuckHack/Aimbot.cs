using System.Runtime.InteropServices;
using UnityEngine;

class Aimbot : MonoBehaviour
{
    [DllImport("user32.dll")]
    static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

    public void Update()
    {
        if(Main.aimbot)
        {
            AimbotUpdate();
        }
    }

    public void OnGUI()
    {
        if(Main.drawfov)
        {
            Drawing.DrawCircle(Color.white, new Vector2(Screen.width / 2, Screen.height / 2), Main.Radius);
        }
    }

    private void AimbotUpdate()
    {
        float minDist = 99999;
        Vector2 AimTarget = Vector2.zero;

        foreach (Mob mob in UnityEngine.Object.FindObjectsOfType(typeof(Mob)) as Mob[])
        {
            Vector3 Enemy = mob.transform.position;
            var w2s = Camera.main.WorldToScreenPoint(Enemy);

            if (w2s.z > 0f)
            {
                float dist = System.Math.Abs(Vector2.Distance(new Vector2(w2s.x, Screen.height - w2s.y), new Vector2((Screen.width / 2), (Screen.height / 2))));
                if (dist < Main.Radius)
                {
                    if (dist < minDist)
                    {
                        minDist = dist;
                        AimTarget = new Vector2(w2s.x, Screen.height - w2s.y - 5);
                    }
                }
            }
        }
        foreach (OnlinePlayer mob in UnityEngine.Object.FindObjectsOfType(typeof(OnlinePlayer)) as OnlinePlayer[])
        {
            Vector3 Enemy = mob.transform.position;
            var w2s = Camera.main.WorldToScreenPoint(Enemy);

            if (w2s.z > 0f)
            {
                float dist = System.Math.Abs(Vector2.Distance(new Vector2(w2s.x, Screen.height - w2s.y), new Vector2((Screen.width / 2), (Screen.height / 2))));
                if (dist < Main.Radius)
                {
                    if (dist < minDist)
                    {
                        minDist = dist;
                        AimTarget = new Vector2(w2s.x, Screen.height - w2s.y - 5);
                    }
                }
            }
        }

        if (AimTarget != Vector2.zero)
        {
            double DistX = AimTarget.x - Screen.width / 2.0f;
            double DistY = AimTarget.y - Screen.height / 2.0f;

            DistX /= Main.smoothingammount;
            DistY /= Main.smoothingammount;

            if (Input.GetKey(KeyCode.Mouse1))
            {
                mouse_event(0x0001, (int)DistX, (int)DistY, 0, 0);
            }
        }
    }
}

