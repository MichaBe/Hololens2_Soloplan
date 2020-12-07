using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTour : MonoBehaviour
{
    public void SetCurrentPackageToNull()
    {
        DataManager.Instance.currentPackage = null;
    }
}
