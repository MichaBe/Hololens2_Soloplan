using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LogOutController : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro title;
    [SerializeField]
    private TextMeshPro persNrTextfield;

    private void OnEnable()
    {
        var currentEmploye = DataManager.Instance.currentEmployee;
        title.SetText(currentEmploye.name);
        persNrTextfield.SetText("Personalnummer: " + currentEmploye.id);
    }
}
