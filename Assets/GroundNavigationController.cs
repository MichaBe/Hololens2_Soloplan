using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GroundNavigationController : MonoBehaviour
{
    [SerializeField]
    private GameObject inbound1;

    [SerializeField]
    private GameObject inbound2;

    [SerializeField]
    private GameObject outbound10;
    [SerializeField]
    private GameObject outbound10Packages;
    
    [SerializeField]
    private GameObject outbound11;
    [SerializeField]
    private GameObject outbound11Packages;
    
    [SerializeField]
    private GameObject interimStorage;
    [SerializeField]
    private GameObject interimStoragePackages;

    private List<GameObject> activeGroundNavigationItems; 

    private void OnEnable()
    {
        activeGroundNavigationItems = new List<GameObject>(); 
        
        // show ground navigation only for an unloading tour (areaType == 1    --->   unload) 
        if (DataManager.Instance.currentTour.areaType == 1)
        {
            // set the right inbound lane active 
            var inboundLane =  DataManager.Instance.currentTour.startLane;
            switch (inboundLane)
            {
                case "1": 
                    inbound1.SetActive(true);
                    activeGroundNavigationItems.Add(inbound1);
                    break;
                case "2":
                    inbound2.SetActive(true);
                    activeGroundNavigationItems.Add(inbound2);
                    break;
            }
            
            // set the right outbound lane active and the according package placeholder 
            // TODO: Decide whether to use strings or ints for lane numbers/ids 
            var outboundlane =  DataManager.Instance.currentPackage.destinationLane.id;
            switch (outboundlane)
            {
                case 10: 
                    outbound10.SetActive(true);
                    activeGroundNavigationItems.Add(outbound10);
                    setPackagePlaceholderActive(outbound10Packages);
                    break;
                case 11:
                    outbound11.SetActive(true);
                    activeGroundNavigationItems.Add(outbound11);
                    setPackagePlaceholderActive(outbound11Packages);
                    break;
                case 99: // Zwischenlager
                    interimStorage.SetActive(true);
                    activeGroundNavigationItems.Add(interimStorage);
                    setPackagePlaceholderActive(interimStoragePackages);
                    break;
            }
        }
    }

    private void OnDisable()
    {
        foreach (var element in activeGroundNavigationItems)
        {
            element.SetActive(false);
        }
        
    }

    private void setPackagePlaceholderActive(GameObject lanePackages)
    {
        int lanePosition = DataManager.Instance.currentPackage.destinationLane.posInLane;
        var lanePackagesList = new GameObject[lanePackages.transform.childCount];
        for (int i = 0; i < lanePackages.transform.childCount; i++)
        {
            lanePackagesList[i] = lanePackages.transform.GetChild(i).gameObject;
        }
        Array.Reverse(lanePackagesList);
        lanePackagesList[lanePosition].SetActive(true);
        activeGroundNavigationItems.Add(lanePackagesList[lanePosition]);
    }
}
