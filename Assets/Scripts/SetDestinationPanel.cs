using DataProvider;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetDestinationPanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI destination;

    private void OnEnable()
    {
        if (DataManager.Instance.currentTour.areaType == 2) // Falls Einladen
        {
            destination.text= "In den LKW";
        }
        else
        {
            destination.text = DataManager.Instance.currentPackage.destinationLane.designation;
        }
    }
}
