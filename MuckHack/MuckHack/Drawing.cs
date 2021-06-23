using System;
using UnityEngine;

// Token: 0x02000165 RID: 357
public class Drawing
{
	// Token: 0x0600086B RID: 2155 RVA: 0x000076DB File Offset: 0x000058DB
	public static void DrawLine(Rect rect)
	{
		Drawing.DrawLine(rect, GUI.contentColor, 1f);
	}

	// Token: 0x0600086C RID: 2156 RVA: 0x000076ED File Offset: 0x000058ED
	public static void DrawLine(Rect rect, Color color)
	{
		Drawing.DrawLine(rect, color, 1f);
	}

	// Token: 0x0600086D RID: 2157 RVA: 0x000076FB File Offset: 0x000058FB
	public static void DrawLine(Rect rect, float width)
	{
		Drawing.DrawLine(rect, GUI.contentColor, width);
	}

	// Token: 0x0600086E RID: 2158 RVA: 0x00007709 File Offset: 0x00005909
	public static void DrawLine(Rect rect, Color color, float width)
	{
		Drawing.DrawLine(new Vector2(rect.x, rect.y), new Vector2(rect.x + rect.width, rect.y + rect.height), color, width);
	}

	// Token: 0x0600086F RID: 2159 RVA: 0x00007748 File Offset: 0x00005948
	public static void DrawLine(Vector2 pointA, Vector2 pointB)
	{
		Drawing.DrawLine(pointA, pointB, GUI.contentColor, 1f);
	}

	// Token: 0x06000870 RID: 2160 RVA: 0x0000775B File Offset: 0x0000595B
	public static void DrawLine(Vector2 pointA, Vector2 pointB, Color color)
	{
		Drawing.DrawLine(pointA, pointB, color, 1f);
	}

	// Token: 0x06000871 RID: 2161 RVA: 0x0000776A File Offset: 0x0000596A
	public static void DrawLine(Vector2 pointA, Vector2 pointB, float width)
	{
		Drawing.DrawLine(pointA, pointB, GUI.contentColor, width);
	}

	// Token: 0x06000872 RID: 2162 RVA: 0x0002A4E8 File Offset: 0x000286E8
	public static void DrawLine(Vector2 pointA, Vector2 pointB, Color color, float width)
	{
		Matrix4x4 matrix = GUI.matrix;
		if (!Drawing.lineTex)
		{
			Drawing.lineTex = new Texture2D(1, 1);
		}
		Color color2 = GUI.color;
		GUI.color = color;
		float num = Vector3.Angle(pointB - pointA, Vector2.right);
		if (pointA.y > pointB.y)
		{
			num = -num;
		}
		GUIUtility.ScaleAroundPivot(new Vector2((pointB - pointA).magnitude, width), new Vector2(pointA.x, pointA.y + 0.5f));
		GUIUtility.RotateAroundPivot(num, pointA);
		GUI.DrawTexture(new Rect(pointA.x, pointA.y, 1f, 1f), Drawing.lineTex);
		GUI.matrix = matrix;
		GUI.color = color2;
	}

	public static void DrawBox(float x, float y, float w, float h, Color color)
	{
		Drawing.DrawLine(new Vector2(x, y), new Vector2(x + w, y), color);
		Drawing.DrawLine(new Vector2(x, y), new Vector2(x, y + h), color);
		Drawing.DrawLine(new Vector2(x + w, y), new Vector2(x + w, y + h), color);
		Drawing.DrawLine(new Vector2(x, y + h), new Vector2(x + w, y + h), color);
	}

	// Token: 0x040008E7 RID: 2279
	public static Texture2D lineTex;
}

