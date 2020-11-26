using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectManager : MonoBehaviour
{
    private string[] mainScreenNames = new string[] { "LoginSelectionMenu", "LoginVoice", "LoginAusweis", "Einstempeln", "Ausstempeln", "HOME MENU", "Inbound", "Outbound", "Benachrichtigungen", "DailyTasksMenu"};
    private GameObject[] mainScreenObjects;

    // Start is called before the first frame update
    void Start()
    {
        mainScreenObjects = new GameObject[mainScreenNames.Length];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setActiveGameObjectsInactive()
    {
        mainScreenObjects = findActiveGameObjectsByName();
        foreach (GameObject go in mainScreenObjects)
        {
            if (go != null)
            {
                go.SetActive(false);
            }
        }
    }

    private GameObject[] findActiveGameObjectsByName()
    { 
        GameObject[] gameObjects = new GameObject[mainScreenNames.Length];
        for (int i = 0; i < mainScreenNames.Length; i++)
        {
            gameObjects[i] = GameObject.Find(mainScreenNames[i]);
        }
        return gameObjects;
    }
}
