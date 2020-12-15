using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataProvider;
using TMPro;

public class PackageListControl : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonTemplate;

    [SerializeField]
    private TextMeshProUGUI titel;

    List<PackageData> tourPackages;

    public GameObject currentUI;
    public GameObject reportUI;

    private DataManager manager;

    private List<GameObject> packageButtons;

    Color[] buttonColors =
    {
        new Color(1.0f, 1.0f, 1.0f),
        new Color(0.0f, 1.0f, 0.0f),
        new Color(1.0f, 0.0f, 0.0f)
    };

    private void Start()
    {
        manager = DataManager.Instance;
        packageButtons = new List<GameObject>();

        //manager.setCurrentTour(139149);
        //manager.SetCurrentPackage("400000000000400004");

        UpdateList();
    }

    public void UpdateList()
    {
        RemoveList();

        titel.text = "Tour: " + manager.currentTour.id;
        foreach (var t in manager.currentTour.ssccs)
        {
            GameObject button = Instantiate(buttonTemplate) as GameObject;
            button.SetActive(true);

            PackageListButton packageListButton = button.GetComponent<PackageListButton>();
            

            packageListButton.currentUI = currentUI;
            packageListButton.nextUI = reportUI;

            button.transform.SetParent(buttonTemplate.transform.parent, false);

            if (t.SSCCStatus == 1)
            {
                button.GetComponent<Image>().color = buttonColors[1];
            }
            else if (t.SSCCStatus >= 2)
            {
                button.GetComponent<Image>().color = buttonColors[2];
            }

            packageButtons.Add(button);
        }
    }

    void RemoveList()
    {
        foreach (GameObject go in packageButtons)
        {
            Destroy(go);
        }

        packageButtons.Clear();
    }
}
