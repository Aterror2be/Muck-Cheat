using System;
using UnityEngine;

public class Drawing : MonoBehaviour
{
	public static void DrawLine(Rect rect)
	{
		Drawing.DrawLine(rect, GUI.contentColor, 1f);
	}

	public static void DrawLine(Rect rect, Color color)
	{
		Drawing.DrawLine(rect, color, 1f);
	}

	public static void DrawLine(Rect rect, float width)
	{
		Drawing.DrawLine(rect, GUI.contentColor, width);
	}

	public static void DrawLine(Rect rect, Color color, float width)
	{
		Drawing.DrawLine(new Vector2(rect.x, rect.y), new Vector2(rect.x + rect.width, rect.y + rect.height), color, width);
	}

	public static void DrawLine(Vector2 pointA, Vector2 pointB)
	{
		Drawing.DrawLine(pointA, pointB, GUI.contentColor, 1f);
	}

	public static void DrawLine(Vector2 pointA, Vector2 pointB, Color color)
	{
		Drawing.DrawLine(pointA, pointB, color, 1f);
	}

	public static void DrawLine(Vector2 pointA, Vector2 pointB, float width)
	{
		Drawing.DrawLine(pointA, pointB, GUI.contentColor, width);
	}

	public static Texture2D lineTex;
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

	public static Material mat;
	public static void DrawCircle(Color col, Vector2 Center, float Radius)
	{
		mat = new Material(Shader.Find("Hidden/Internal-Colored"))
		{
			hideFlags = HideFlags.DontSaveInEditor | HideFlags.HideInHierarchy
		};

		mat.SetInt("_SrcBlend", 5);
		mat.SetInt("_DstBlend", 10);
		mat.SetInt("_Cull", 0);
		mat.SetInt("_ZTest", 8);
		mat.SetInt("_ZWrite", 0);
		mat.SetColor("_Color", col);

		GL.PushMatrix();
		mat.SetPass(0);
		GL.LoadPixelMatrix();
		GL.Color(col);
		GL.Begin(GL.LINES);
		for (float num = 0f; num < 6.28318548f; num += 0.05f)
		{
			GL.Vertex(new Vector3(Mathf.Cos(num) * Radius + Center.x, Mathf.Sin(num) * Radius + Center.y));
			GL.Vertex(new Vector3(Mathf.Cos(num + 0.05f) * Radius + Center.x, Mathf.Sin(num + 0.05f) * Radius + Center.y));
		}
		GL.End();
		GL.PopMatrix();
	}
}

