using DataProvider;
using Microsoft.MixedReality.Toolkit.Utilities;
using TMPro;
using UnityEngine;

public class PackageListControlNew : MonoBehaviour
{
    [SerializeField] private GameObject itemTemplate;

    [SerializeField] private TextMeshPro titel;

    [SerializeField] private GridObjectCollection packageCollection;

    private readonly Color32 failureColor = new Color32(247, 71, 71, 1);
    private readonly Color32 soloplanColor = new Color32(173, 20, 93, 1);
    private readonly Color32 successColor = new Color32(21, 175, 70, 1);
    private readonly Color32 warningColor = new Color32(246, 231, 133, 1);

    private DataManager manager;


    private void Start()
    {
        manager = DataManager.Instance;
        UpdateList();
    }

    public void UpdateList()
    {
        RemoveList();

        titel.text = "Tour: " + manager.currentTour.id;
        foreach (var package in manager.currentTour.ssccs)
        {
            var packageUiItem = Instantiate(itemTemplate, packageCollection.transform);
            var textMesh = packageUiItem.transform.Find("Title").gameObject;
            var packageText = textMesh.GetComponent<TextMeshPro>();
            packageText.text = "Paket\n" + package.code;
            setBackgroundColorAccordingToPackageStatus(packageUiItem, packageText, package);
        }

        // hide the package item template
        Destroy(itemTemplate);

        // update Layout 
        packageCollection.UpdateCollection();
    }

    private void setBackgroundColorAccordingToPackageStatus(GameObject packageUiItem, TextMeshPro packageText,
        PackageData package)
    {
        var backPlate = packageUiItem.transform.Find("BackPlate").gameObject;

        // set color for currently scanned package
        if (manager.currentPackage.code == package.code)
            backPlate.GetComponent<Renderer>().material.color = soloplanColor;

        var packageStatus = package.SSCCStatus;
        switch (packageStatus)
        {
            case 1: // SSCCStatus = 1  means package is processed normally 
                backPlate.GetComponent<Renderer>().material.color = successColor;
                packageText.color = Color.black;
                break;
            case 2: // SSCCStatus = 2  means package is damaged 
                backPlate.GetComponent<Renderer>().material.color = warningColor;
                packageText.color = Color.black;
                break;
            case 3: // SSCCStatus = 3  means package is missing 
                backPlate.GetComponent<Renderer>().material.color = failureColor;
                // packageText.color = Color.black;
                break;
        }
    }

    private void RemoveList()
    {
    }
}