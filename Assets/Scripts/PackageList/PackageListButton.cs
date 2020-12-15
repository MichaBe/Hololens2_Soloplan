using UnityEngine;

public class PackageListButton : MonoBehaviour
{
    public GameObject currentUI;
    public GameObject nextUI;
    private  string id;

    public void setId(string id)
    {
        this.id = id;
    }

    public void OnClick()
    {
        DataManager.Instance.SetCurrentPackage(id);
        SwitchUI();
    }

    public void SwitchUI()
    {
        nextUI.SetActive(true);
        currentUI.SetActive(false);
    }
}