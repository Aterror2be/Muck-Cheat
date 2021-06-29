using System;
using UnityEngine;

namespace Loader
{
    class Load : MonoBehaviour
    {
        public static GameObject newGameObject;
        public static void CreateGameObject()
        {
            newGameObject = new GameObject();
            UnityEngine.Object.DontDestroyOnLoad(newGameObject);
            newGameObject.AddComponent<Main>();
            newGameObject.AddComponent<ItemGenerator>();
            newGameObject.AddComponent<Flyhack>();
            newGameObject.AddComponent<Aimbot>();
            newGameObject.AddComponent<Esp>();
            newGameObject.AddComponent<Drawing>();
        }
    }
}
//Injection Steps
//Injector https://www.unknowncheats.me/forum/downloads.php?do=file&id=31184
//Open Muck
//Click Refresh
//1.  NameSpace:     Loader
//2.  Class Name:    load
//3.  Method Name:   CreateGameObject
