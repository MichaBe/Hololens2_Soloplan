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
        Debug.Log("setActiveGameObjectsInactive is called");
        findGameObjectsByName();
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject go in allObjects)
            if (System.Array.IndexOf(mainScreenObjects, go) != -1)
            {
                go.SetActive(false);
            }
    }

    private void findGameObjectsByName()
    {
        for (int i = 0; i < mainScreenNames.Length; i++)
        {
            mainScreenObjects[i] = GameObject.Find(mainScreenNames[i]);
            Debug.Log(mainScreenObjects[i]);
        }
    }
}
