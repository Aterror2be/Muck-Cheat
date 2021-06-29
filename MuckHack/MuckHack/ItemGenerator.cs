using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    public static List<string> ItemNames = new List<string>();
	private static int ItemIndex;
	private static string SearchItem = "";
	public static List<Sprite> ItemSprites = new List<Sprite>();
	private static int itemAmmount = 9999;

	static ItemGenerator()
	{
        foreach (InventoryItem inventoryItem in ItemManager.Instance.allItems.Values)
		{
			ItemGenerator.ItemNames.Add(inventoryItem.name);
			ItemGenerator.ItemSprites.Add(inventoryItem.sprite);
		}
	}

    static void SpawnItem(string itemName)
	{
		if (!InventoryUI.Instance.IsInventoryFull())
		{
			foreach (InventoryItem inventoryItem in ItemManager.Instance.allItems.Values)
			{
				if (inventoryItem.name.ToLower() == itemName.ToLower())
				{
					InventoryItem inventoryItem2 = inventoryItem;
					inventoryItem2.amount = ItemGenerator.itemAmmount;
					InventoryUI.Instance.AddItemToInventory(inventoryItem2);
					break;
				}
			}
		}
	}

    public static void DrawMenu()
    {
        ItemGenerator.ItemIndex = (int)GUI.VerticalScrollbar(new Rect(695f, 80f, 10f, 370f), (float)ItemGenerator.ItemIndex, 10f,0f, (float)(ItemGenerator.ItemNames.Count - 40));
        ItemGenerator.itemAmmount = (int)GUI.HorizontalSlider(new Rect(215f, 135f, 200f, 20f), (float)ItemGenerator.itemAmmount, 1f, 9999f);
        GUI.Label(new Rect(215f, 115f, 200f, 20f), "Item Ammount: " + ItemGenerator.itemAmmount);
        ItemGenerator.SearchItem = GUI.TextField(new Rect(490f, 115f, 200f, 30f), ItemGenerator.SearchItem);

        if (GUI.Button(new Rect(430f, 115f, 60f, 30f), "Search"))
        {
            int i = 0;
            while (i < ItemGenerator.ItemNames.Count)
            {
                string text = ItemGenerator.ItemNames[i].ToLower();
                string value = ItemGenerator.SearchItem.ToLower();
                if (text.Contains(value))
                {
                    if (i + 40 > ItemGenerator.ItemNames.Count - 1)
                    {
                        ItemGenerator.ItemIndex = ItemGenerator.ItemNames.Count - 40;
                        break;
                    }
                    ItemGenerator.ItemIndex = i;
                    break;
                }
                else
                {
                    i++;
                }
            }
        }

        if (Main.tabs == 5)
        {
            if (GUI.Button(new Rect(210f, 150f, 60f, 60f), new GUIContent(ItemGenerator.ItemSprites[ItemGenerator.ItemIndex].texture, ItemGenerator.ItemNames[ItemGenerator.ItemIndex])))
            {
                ItemGenerator.SpawnItem(ItemGenerator.ItemNames[ItemGenerator.ItemIndex]);
            }
            if (GUI.Button(new Rect(270f, 150f, 60f, 60f), new GUIContent(ItemGenerator.ItemSprites[ItemGenerator.ItemIndex + 1].texture)))
            {
                ItemGenerator.SpawnItem(ItemGenerator.ItemNames[ItemGenerator.ItemIndex + 1]);
            }
            if (GUI.Button(new Rect(330f, 150f, 60f, 60f), new GUIContent(ItemGenerator.ItemSprites[ItemGenerator.ItemIndex + 2].texture)))
            {
                ItemGenerator.SpawnItem(ItemGenerator.ItemNames[ItemGenerator.ItemIndex + 2]);
            }
            if (GUI.Button(new Rect(390f, 150f, 60f, 60f), new GUIContent(ItemGenerator.ItemSprites[ItemGenerator.ItemIndex + 3].texture)))
            {
                ItemGenerator.SpawnItem(ItemGenerator.ItemNames[ItemGenerator.ItemIndex + 3]);
            }
            if (GUI.Button(new Rect(450f, 150f, 60f, 60f), new GUIContent(ItemGenerator.ItemSprites[ItemGenerator.ItemIndex + 4].texture)))
            {
                ItemGenerator.SpawnItem(ItemGenerator.ItemNames[ItemGenerator.ItemIndex + 4]);
            }
            if (GUI.Button(new Rect(510f, 150f, 60f, 60f), new GUIContent(ItemGenerator.ItemSprites[ItemGenerator.ItemIndex + 5].texture)))
            {
                ItemGenerator.SpawnItem(ItemGenerator.ItemNames[ItemGenerator.ItemIndex + 5]);
            }
            if (GUI.Button(new Rect(570f, 150f, 60f, 60f), new GUIContent(ItemGenerator.ItemSprites[ItemGenerator.ItemIndex + 6].texture)))
            {
                ItemGenerator.SpawnItem(ItemGenerator.ItemNames[ItemGenerator.ItemIndex + 6]);
            }
            if (GUI.Button(new Rect(630f, 150f, 60f, 60f), new GUIContent(ItemGenerator.ItemSprites[ItemGenerator.ItemIndex + 7].texture)))
            {
                ItemGenerator.SpawnItem(ItemGenerator.ItemNames[ItemGenerator.ItemIndex + 7]);
            }
            if (GUI.Button(new Rect(210f, 210f, 60f, 60f), new GUIContent(ItemGenerator.ItemSprites[ItemGenerator.ItemIndex + 8].texture)))
            {
                ItemGenerator.SpawnItem(ItemGenerator.ItemNames[ItemGenerator.ItemIndex + 8]);
            }
            if (GUI.Button(new Rect(270f, 210f, 60f, 60f), new GUIContent(ItemGenerator.ItemSprites[ItemGenerator.ItemIndex + 9].texture)))
            {
                ItemGenerator.SpawnItem(ItemGenerator.ItemNames[ItemGenerator.ItemIndex + 9]);
            }
            if (GUI.Button(new Rect(330f, 210f, 60f, 60f), new GUIContent(ItemGenerator.ItemSprites[ItemGenerator.ItemIndex + 10].texture)))
            {
                ItemGenerator.SpawnItem(ItemGenerator.ItemNames[ItemGenerator.ItemIndex + 10]);
            }
            if (GUI.Button(new Rect(390f, 210f, 60f, 60f), new GUIContent(ItemGenerator.ItemSprites[ItemGenerator.ItemIndex + 11].texture)))
            {
                ItemGenerator.SpawnItem(ItemGenerator.ItemNames[ItemGenerator.ItemIndex + 11]);
            }
            if (GUI.Button(new Rect(450f, 210f, 60f, 60f), new GUIContent(ItemGenerator.ItemSprites[ItemGenerator.ItemIndex + 12].texture)))
            {
                ItemGenerator.SpawnItem(ItemGenerator.ItemNames[ItemGenerator.ItemIndex + 12]);
            }
            if (GUI.Button(new Rect(510f, 210f, 60f, 60f), new GUIContent(ItemGenerator.ItemSprites[ItemGenerator.ItemIndex + 13].texture)))
            {
                ItemGenerator.SpawnItem(ItemGenerator.ItemNames[ItemGenerator.ItemIndex + 13]);
            }
            if (GUI.Button(new Rect(570f, 210f, 60f, 60f), new GUIContent(ItemGenerator.ItemSprites[ItemGenerator.ItemIndex + 14].texture)))
            {
                ItemGenerator.SpawnItem(ItemGenerator.ItemNames[ItemGenerator.ItemIndex + 14]);
            }
            if (GUI.Button(new Rect(630f, 210f, 60f, 60f), new GUIContent(ItemGenerator.ItemSprites[ItemGenerator.ItemIndex + 15].texture)))
            {
                ItemGenerator.SpawnItem(ItemGenerator.ItemNames[ItemGenerator.ItemIndex + 15]);
            }
            if (GUI.Button(new Rect(210f, 270f, 60f, 60f), new GUIContent(ItemGenerator.ItemSprites[ItemGenerator.ItemIndex + 16].texture)))
            {
                ItemGenerator.SpawnItem(ItemGenerator.ItemNames[ItemGenerator.ItemIndex + 16]);
            }
            if (GUI.Button(new Rect(270f, 270f, 60f, 60f), new GUIContent(ItemGenerator.ItemSprites[ItemGenerator.ItemIndex + 17].texture)))
            {
                ItemGenerator.SpawnItem(ItemGenerator.ItemNames[ItemGenerator.ItemIndex + 17]);
            }
            if (GUI.Button(new Rect(330f, 270f, 60f, 60f), new GUIContent(ItemGenerator.ItemSprites[ItemGenerator.ItemIndex + 18].texture)))
            {
                ItemGenerator.SpawnItem(ItemGenerator.ItemNames[ItemGenerator.ItemIndex + 18]);
            }
            if (GUI.Button(new Rect(390f, 270f, 60f, 60f), new GUIContent(ItemGenerator.ItemSprites[ItemGenerator.ItemIndex + 19].texture)))
            {
                ItemGenerator.SpawnItem(ItemGenerator.ItemNames[ItemGenerator.ItemIndex + 19]);
            }
            if (GUI.Button(new Rect(450f, 270f, 60f, 60f), new GUIContent(ItemGenerator.ItemSprites[ItemGenerator.ItemIndex + 20].texture)))
            {
                ItemGenerator.SpawnItem(ItemGenerator.ItemNames[ItemGenerator.ItemIndex + 20]);
            }
            if (GUI.Button(new Rect(510f, 270f, 60f, 60f), new GUIContent(ItemGenerator.ItemSprites[ItemGenerator.ItemIndex + 21].texture)))
            {
                ItemGenerator.SpawnItem(ItemGenerator.ItemNames[ItemGenerator.ItemIndex + 21]);
            }
            if (GUI.Button(new Rect(570f, 270f, 60f, 60f), new GUIContent(ItemGenerator.ItemSprites[ItemGenerator.ItemIndex + 22].texture)))
            {
                ItemGenerator.SpawnItem(ItemGenerator.ItemNames[ItemGenerator.ItemIndex + 22]);
            }
            if (GUI.Button(new Rect(630f, 270f, 60f, 60f), new GUIContent(ItemGenerator.ItemSprites[ItemGenerator.ItemIndex + 23].texture)))
            {
                ItemGenerator.SpawnItem(ItemGenerator.ItemNames[ItemGenerator.ItemIndex + 23]);
            }
            if (GUI.Button(new Rect(210f, 330f, 60f, 60f), new GUIContent(ItemGenerator.ItemSprites[ItemGenerator.ItemIndex + 24].texture)))
            {
                ItemGenerator.SpawnItem(ItemGenerator.ItemNames[ItemGenerator.ItemIndex + 24]);
            }
            if (GUI.Button(new Rect(270f, 330f, 60f, 60f), new GUIContent(ItemGenerator.ItemSprites[ItemGenerator.ItemIndex + 25].texture)))
            {
                ItemGenerator.SpawnItem(ItemGenerator.ItemNames[ItemGenerator.ItemIndex + 25]);
            }
            if (GUI.Button(new Rect(330f, 330f, 60f, 60f), new GUIContent(ItemGenerator.ItemSprites[ItemGenerator.ItemIndex + 26].texture)))
            {
                ItemGenerator.SpawnItem(ItemGenerator.ItemNames[ItemGenerator.ItemIndex + 26]);
            }
            if (GUI.Button(new Rect(390f, 330f, 60f, 60f), new GUIContent(ItemGenerator.ItemSprites[ItemGenerator.ItemIndex + 27].texture)))
            {
                ItemGenerator.SpawnItem(ItemGenerator.ItemNames[ItemGenerator.ItemIndex + 27]);
            }
            if (GUI.Button(new Rect(450f, 330f, 60f, 60f), new GUIContent(ItemGenerator.ItemSprites[ItemGenerator.ItemIndex + 28].texture)))
            {
                ItemGenerator.SpawnItem(ItemGenerator.ItemNames[ItemGenerator.ItemIndex + 28]);
            }
            if (GUI.Button(new Rect(510f, 330f, 60f, 60f), new GUIContent(ItemGenerator.ItemSprites[ItemGenerator.ItemIndex + 29].texture)))
            {
                ItemGenerator.SpawnItem(ItemGenerator.ItemNames[ItemGenerator.ItemIndex + 29]);
            }
            if (GUI.Button(new Rect(570f, 330f, 60f, 60f), new GUIContent(ItemGenerator.ItemSprites[ItemGenerator.ItemIndex + 30].texture)))
            {
                ItemGenerator.SpawnItem(ItemGenerator.ItemNames[ItemGenerator.ItemIndex + 30]);
            }
            if (GUI.Button(new Rect(630f, 330f, 60f, 60f), new GUIContent(ItemGenerator.ItemSprites[ItemGenerator.ItemIndex + 31].texture)))
            {
                ItemGenerator.SpawnItem(ItemGenerator.ItemNames[ItemGenerator.ItemIndex + 31]);
            }
            if (GUI.Button(new Rect(210f, 390f, 60f, 60f), new GUIContent(ItemGenerator.ItemSprites[ItemGenerator.ItemIndex + 32].texture)))
            {
                ItemGenerator.SpawnItem(ItemGenerator.ItemNames[ItemGenerator.ItemIndex + 32]);
            }
            if (GUI.Button(new Rect(270f, 390f, 60f, 60f), new GUIContent(ItemGenerator.ItemSprites[ItemGenerator.ItemIndex + 33].texture)))
            {
                ItemGenerator.SpawnItem(ItemGenerator.ItemNames[ItemGenerator.ItemIndex + 33]);
            }
            if (GUI.Button(new Rect(330f, 390f, 60f, 60f), new GUIContent(ItemGenerator.ItemSprites[ItemGenerator.ItemIndex + 34].texture)))
            {
                ItemGenerator.SpawnItem(ItemGenerator.ItemNames[ItemGenerator.ItemIndex + 34]);
            }
            if (GUI.Button(new Rect(390f, 390f, 60f, 60f), new GUIContent(ItemGenerator.ItemSprites[ItemGenerator.ItemIndex + 35].texture)))
            {
                ItemGenerator.SpawnItem(ItemGenerator.ItemNames[ItemGenerator.ItemIndex + 35]);
            }
            if (GUI.Button(new Rect(450f, 390f, 60f, 60f), new GUIContent(ItemGenerator.ItemSprites[ItemGenerator.ItemIndex + 36].texture)))
            {
                ItemGenerator.SpawnItem(ItemGenerator.ItemNames[ItemGenerator.ItemIndex + 36]);
            }
            if (GUI.Button(new Rect(510f, 390f, 60f, 60f), new GUIContent(ItemGenerator.ItemSprites[ItemGenerator.ItemIndex + 37].texture)))
            {
                ItemGenerator.SpawnItem(ItemGenerator.ItemNames[ItemGenerator.ItemIndex + 37]);
            }
            if (GUI.Button(new Rect(570f, 390f, 60f, 60f), new GUIContent(ItemGenerator.ItemSprites[ItemGenerator.ItemIndex + 38].texture)))
            {
                ItemGenerator.SpawnItem(ItemGenerator.ItemNames[ItemGenerator.ItemIndex + 38]);
            }
            if (GUI.Button(new Rect(630f, 390f, 60f, 60f), new GUIContent(ItemGenerator.ItemSprites[ItemGenerator.ItemIndex + 39].texture)))
            {
                ItemGenerator.SpawnItem(ItemGenerator.ItemNames[ItemGenerator.ItemIndex + 39]);
            }
        }
    }
}
