using System;
using DataProvider;
using Microsoft.MixedReality.Toolkit.Utilities;
using TMPro;
using UnityEngine;

public class PackageListControlNew : MonoBehaviour
{
    [SerializeField] protected GameObject itemTemplate;

    [SerializeField] protected TextMeshPro titel;

    [SerializeField] protected GridObjectCollection packageCollection;
    
    private readonly Color32 failureColor = new Color32(247, 71, 71, 1);
    private readonly Color32 soloplanColor = new Color32(173, 20, 93, 1);
    private readonly Color32 successColor = new Color32(21, 175, 70, 1);
    private readonly Color32 warningColor = new Color32(246, 231, 133, 1);
    
    void OnEnable()
    {
        InitList();
    }

    void OnDisable()
    {
        RemoveList();
    }

    private void InitList()
    {
        titel.text = "Tour: " + DataManager.Instance.currentTour.id;
        foreach (var package in DataManager.Instance.currentTour.ssccs)
        {
            var packageUiItem = Instantiate(itemTemplate);
            packageUiItem.SetActive(true);
            var textMesh = packageUiItem.transform.Find("Title").gameObject;
            var packageText = textMesh.GetComponent<TextMeshPro>();
            packageText.text = "Paket\n" + package.code;
            packageUiItem.transform.SetParent(packageCollection.transform,false);
        }

        // hide the package item template
        itemTemplate.SetActive(false);

        // update Layout 
        packageCollection.UpdateCollection();
        UpdateList();
    }

    public void UpdateList()
    {
        var index = 0;
        foreach (var package in DataManager.Instance.currentTour.ssccs)
        {
            SetBackgroundColorAccordingToPackageStatus(package, index);
            index++;
        }
    }

    private void SetBackgroundColorAccordingToPackageStatus(PackageData package, int index)
    {
        var packageUiItem = packageCollection.transform.GetChild(index).gameObject;
        var backPlate = packageUiItem.transform.Find("BackPlate").gameObject;
        var textMesh = packageUiItem.transform.Find("Title").gameObject;
        var packageText = textMesh.GetComponent<TextMeshPro>();

        // set color for currently scanned package
        if (DataManager.Instance.currentPackage != null && DataManager.Instance.currentPackage.code == package.code)
            backPlate.GetComponent<Renderer>().material.color = soloplanColor;

        var packageStatus = package.SSCCStatus;
        switch (packageStatus)
        {
            case 1: // SSCCStatus = 1  means package is processed normally 
                backPlate.GetComponent<Renderer>().material.color = successColor;
                break;
            case 2: // SSCCStatus = 2  means package is missing 
                backPlate.GetComponent<Renderer>().material.color = failureColor;
                break;
            case 3: // SSCCStatus = 3  means package is damaged 
                backPlate.GetComponent<Renderer>().material.color = warningColor;
                packageText.color = Color.black;
                break;
            
        }
    }

    public void RemoveList()
    {
        foreach (Transform child in packageCollection.transform) {
            Destroy(child.gameObject);
        }
    }
}