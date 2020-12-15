using System;
using DataProvider;
using Microsoft.MixedReality.Toolkit.Utilities;
using TMPro;
using UnityEngine;

public class PackageScanListControl : PackageListControlNew
{
    void OnEnable()
    {
        InitList();
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
            var reportButton = packageUiItem.transform.Find("ReportButton").gameObject;
            PackageListButton listButton = reportButton.GetComponent<PackageListButton>();
            listButton.setId(package.code);
            packageText.text = "Paket\n" + package.code;
            packageUiItem.transform.SetParent(packageCollection.transform,false);
        }

        // hide the package item template
        itemTemplate.SetActive(false);

        // update Layout 
        packageCollection.UpdateCollection();
        UpdateList();
    }
}