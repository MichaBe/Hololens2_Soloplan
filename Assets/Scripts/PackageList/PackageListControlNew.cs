using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataProvider;
using Microsoft.MixedReality.Toolkit.Utilities;
using TMPro;

public class PackageListControlNew : MonoBehaviour
{
    [SerializeField]
    private GameObject itemTemplate;

    [SerializeField]
    private TextMeshPro titel;

    [SerializeField]
    private GridObjectCollection packageCollection;

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
            GameObject packageItem = Instantiate(itemTemplate, packageCollection.transform);
            var textMesh = packageItem.transform.Find("Title").gameObject;
            var packageText = textMesh.GetComponent<TextMeshPro>();
            packageText.text = "Paket\n" + package.code;
        }
        // hide the package item template
        Destroy(itemTemplate);
        
        // update Layout 
        packageCollection.UpdateCollection();
    }

    void RemoveList()
    {
       
    }
}
