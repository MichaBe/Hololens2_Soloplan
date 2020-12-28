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
    private GameObject inboundHorizontalPath1;

    [SerializeField]
    private GameObject inbound2;
    [SerializeField]
    private GameObject inboundHorizontalPath2;

    [SerializeField]
    private GameObject outbound10;
    [SerializeField]
    private GameObject outbound10Packages;
    [SerializeField]
    private GameObject inboundHorizontalPath10;
    
    [SerializeField]
    private GameObject outbound11;
    [SerializeField]
    private GameObject outbound11Packages;
    [SerializeField]
    private GameObject inboundHorizontalPath11;
    
    [SerializeField]
    private GameObject interimStorage;
    [SerializeField]
    private GameObject interimStoragePackages;
    
    [SerializeField]
    private GameObject hardcodedPathToZl;
    [SerializeField]
    private GameObject hardcodedInterimStoragePackages;
    

    private static List<GameObject> activeGroundNavigationItems = new List<GameObject>();
    private static GameObject currentActivePackagePlaceholder;
    
    public void updateGroundNavigation()
    {
        if (isCurrentTourUnload())
            // show ground navigation for an unloading tour 
        {
            
            disableAllActiveElementsInGroundNavigation();
            activateInbound();
            
            // set the right outbound lane active and the according package placeholder 
            // TODO: Decide whether to use strings or ints for lane numbers/ids 
            var outboundlane =  DataManager.Instance.currentPackage.destinationLane.id;
            switch (outboundlane)
            {
                case 10: 
                    outbound10.SetActive(true);
                    inboundHorizontalPath10.SetActive(true);
                    activeGroundNavigationItems.Add(outbound10);
                    activeGroundNavigationItems.Add(inboundHorizontalPath10);
                    setPackagePlaceholderActive(outbound10Packages, true);
                    break;
                case 11:
                    outbound11.SetActive(true);
                    inboundHorizontalPath11.SetActive(true);
                    activeGroundNavigationItems.Add(outbound11);
                    activeGroundNavigationItems.Add(inboundHorizontalPath11);
                    setPackagePlaceholderActive(outbound11Packages, true);
                    break;
                case 99: // Zwischenlager
                    interimStorage.SetActive(true);
                    activeGroundNavigationItems.Add(interimStorage);
                    setPackagePlaceholderActive(interimStoragePackages, true);
                    break;
            }
        }
        else // show ground navigation for a loading tour 
        {
            if (DataManager.Instance.currentTour.areaType == 2 &&
                DataManager.Instance.currentPackage.destinationLane.laneType == "zl"
            ) // Falls es sich um eine beladen Tour handelt und das Paket im Zwischenlager liegt
            {
                hardcodedPathToZl.SetActive(true);
                activeGroundNavigationItems.Add(hardcodedPathToZl);
                setPackagePlaceholderActive(hardcodedInterimStoragePackages, true);
            }
        }
    }

    private void activateInbound()
    {
        // set the right inbound lane active 
        var inboundLane =  DataManager.Instance.currentTour.startLane;
        switch (inboundLane)
        {
            case "1": 
                inbound1.SetActive(true);
                inboundHorizontalPath1.SetActive(true);
                activeGroundNavigationItems.Add(inbound1);
                activeGroundNavigationItems.Add(inboundHorizontalPath1);
                break;
            case "2":
                inbound2.SetActive(true);
                inboundHorizontalPath2.SetActive(true);
                activeGroundNavigationItems.Add(inbound2);
                activeGroundNavigationItems.Add(inboundHorizontalPath2);
                break;
        }
    }

    private void setPackagePlaceholderActive(GameObject lanePackages, bool shouldActivate)
    {
        int lanePosition = DataManager.Instance.currentPackage.destinationLane.posInLane;
        var lanePackagesList = new GameObject[lanePackages.transform.childCount];
        for (int i = 0; i < lanePackages.transform.childCount; i++)
        {
            lanePackagesList[i] = lanePackages.transform.GetChild(i).gameObject;
        }
        Array.Reverse(lanePackagesList);
        lanePackagesList[lanePosition].SetActive(shouldActivate);
        currentActivePackagePlaceholder = lanePackagesList[lanePosition];
        activeGroundNavigationItems.Add(lanePackagesList[lanePosition]);
    }

    public void deactivateCurrentPackagePlaceholder()
    {
        currentActivePackagePlaceholder.SetActive(false);
        if (!isCurrentTourUnload())
        {
            hardcodedPathToZl.SetActive(false);
        }
    }

    public void enableInboundWithoutHorizontalPath()
    {
        var inboundLane =  DataManager.Instance.currentTour.startLane;
        switch (inboundLane)
        {
            case "1": 
                inbound1.SetActive(true);
                activeGroundNavigationItems.Add(inbound1);
                inboundHorizontalPath1.SetActive(false);
                break;
            case "2":
                inbound2.SetActive(true);
                activeGroundNavigationItems.Add(inbound2);
                inboundHorizontalPath2.SetActive(false);
                break;
            case "10": 
                outbound10.SetActive(true);
                activeGroundNavigationItems.Add(outbound10);
                inboundHorizontalPath10.SetActive(false);
                break;
            case "11":
                outbound11.SetActive(true);
                activeGroundNavigationItems.Add(outbound11);
                inboundHorizontalPath11.SetActive(false);
                break;
        }
    }

    public void disableAllActiveElementsInGroundNavigation()
    {
        foreach (var element in activeGroundNavigationItems)
        {
            element.SetActive(false);
        }
        activeGroundNavigationItems.Clear();
    }

    private bool isCurrentTourUnload()
    {
        return DataManager.Instance.currentTour.areaType == 1; // (areaType == 1  --->   unload) 
    }
}
