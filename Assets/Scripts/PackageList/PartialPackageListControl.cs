using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataProvider;
using TMPro;

public class PartialPackageListControl : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonTemplate;

    [SerializeField]
    private TextMeshProUGUI titel;

    [SerializeField]
    private int packageMode;

    List<PackageData> tourPackages;

    private List<GameObject> packageButtons;

    public GameObject currentUI;
    public GameObject reportUI;

    private void Start()
    {
        //packageButtons = new List<GameObject>();
    }

    void OnDisable()
    {
        RemoveList();
    }

    void RemoveList()
    {
        foreach (GameObject go in packageButtons)
        {
            Debug.Log("Removelist");
            Destroy(go);
        }

        packageButtons.Clear();
    }

    public void SetPackageMode(int mode)
    {
        packageMode = mode;

        CreateList();
    }

    public void CreateList()
    {
        if (packageButtons == null)
        {
            packageButtons = new List<GameObject>();
        }

        RemoveList();

        switch (packageMode)
        {
            case 0:
                titel.text = "Missing of Tour: " + DataManager.Instance.currentTour.id;
                break;
            case 1:
                titel.text = "Correct of Tour: " + DataManager.Instance.currentTour.id;
                break;
            case 2:
                titel.text = "Errors of Tour: " + DataManager.Instance.currentTour.id;
                break;
            case 3:
                titel.text = "Added of Tour: " + DataManager.Instance.currentTour.id;
                break;
        }
        foreach (var t in DataManager.Instance.currentTour.ssccs)
        {
            if (t.SSCCStatus == packageMode)
            {
                GameObject button = Instantiate(buttonTemplate) as GameObject;
                button.SetActive(true);

                PackageListButton packageListButton = button.GetComponent<PackageListButton>();

                packageListButton.currentUI = currentUI;
                packageListButton.nextUI = reportUI;

                button.transform.SetParent(buttonTemplate.transform.parent, false);

                packageButtons.Add(button);
            }
        }

        Debug.Log(packageMode);
    }
}
