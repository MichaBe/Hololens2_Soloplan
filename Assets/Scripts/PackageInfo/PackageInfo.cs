using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DataProvider;


public class PackageInfo : MonoBehaviour
{
    public TextMeshProUGUI packageTitle;
    public TextMeshProUGUI packageInfo;

    private string[] PackageStatusName = new string[] { "No status", "Scanned successfully", "Package is missing", "Package is damaged", "Barcode is missing" };

    private void OnEnable()
    {
        ShowPackageInfo();
    }

    public void ShowPackageInfo()
    {
        ////////////// for testing //////////////
        //DataManager.Instance.setCurrentTour(139149);
        //PackageData package = DataManager.Instance.getPackage("400000000000400004");
        /////////////////////////////////////////

        PackageData package = DataManager.Instance.currentPackage;

        string infoString = "";

        if (package != null)
        {

            if (DataManager.Instance.currentTour.areaType == 2) // Falls Einladen
            {
                packageTitle.text = "[ In den LKW ]";
            }
            else // sonst ist es ausladen
            {
                string lane = package.destinationLane.designation;
                packageTitle.text = "[ " + lane + " ]";
            }
         
            int packageIndex = Array.FindIndex(DataManager.Instance.currentTour.ssccs, it => it.code == DataManager.Instance.currentPackage.code) + 1;
            int tourPackagesCount = DataManager.Instance.currentTour.ssccs.Length;
            infoString += "Package: " + packageIndex.ToString() + "/" + tourPackagesCount.ToString() + "\n";

            infoString += "Item: " + DataManager.Instance.currentPackage.code + "\n";

            infoString += "Weight: " + package.weight.ToString() + " KG\n";

            infoString += "Status: " + PackageStatusName[package.SSCCStatus] + "\n";

            if (!String.IsNullOrEmpty(package.comment))
            {
                infoString += "Comment: " + package.comment + "\n";
            }
        }
        else
        {
            infoString = "Package Not Found";
        }

        packageInfo.text = infoString;
    }
}
