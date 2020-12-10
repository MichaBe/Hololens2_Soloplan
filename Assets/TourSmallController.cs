using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TourSmallController : MonoBehaviour
{
    [SerializeField] private GameObject currentPackageItem;
    [SerializeField] private GameObject withActivePackage;
    [SerializeField] private GameObject withoutActivePackage ;
    [SerializeField] private TextMeshPro titel;

    void OnEnable()
    {
        titel.text = "Tour: " + DataManager.Instance.currentTour.id;
    }

    
    void Update() 
    {
        // TODO: find a way to not do this in every frame
        if ((DataManager.Instance.currentPackage != null) && !string.IsNullOrEmpty(DataManager.Instance.currentPackage.code))
        {
            var textMesh = currentPackageItem.transform.Find("Title").gameObject;
            var packageText = textMesh.GetComponent<TextMeshPro>();
            packageText.text = "Paket\n" + DataManager.Instance.currentPackage.code;
            // set color to soloplan
            withActivePackage.SetActive(true);
            withoutActivePackage.SetActive(false);
        }
        else
        {
            withActivePackage.SetActive(false);
            withoutActivePackage.SetActive(true);   
        }
    }
}
