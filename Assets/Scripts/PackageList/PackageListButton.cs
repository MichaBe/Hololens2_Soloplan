using UnityEngine;

public class PackageListButton : MonoBehaviour
{
    public GameObject currentUI;
    public GameObject nextUI;
    public GameObject showInInterimStorageMenu;
    private  string id;

    public void setId(string id)
    {
        this.id = id;
    }

    public void OnClick()
    {
        DataManager.Instance.SetCurrentPackage(id);
        if (DataManager.Instance.currentTour.areaType == 2 &&
            DataManager.Instance.currentPackage.destinationLane.laneType == "zl") // Falls es sich um eine beladen Tour handelt und das Paket im Zwischenlager liegt
        {
            showInInterimStorageMenu.SetActive(true);
            currentUI.SetActive(false);
        }
        else
        {
            SwitchUI();
        }
    }

    public void SwitchUI()
    {
        nextUI.SetActive(true);
        currentUI.SetActive(false);
    }
}