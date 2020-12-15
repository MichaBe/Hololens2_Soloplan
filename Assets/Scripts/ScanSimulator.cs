using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataProvider;
using TMPro;
using Microsoft.MixedReality.Toolkit.Utilities;

public class ScanSimulator : MonoBehaviour
{
    Image scanArea;

    Color white = new Color(255.0f, 255.0f, 255.0f);
    Color reading = new Color(255.0f, 165.0f, 0.0f);
    Color accepted = new Color(0.0f, 255.0f, 0.0f);
    Color rejected = new Color(255.0f, 0.0f, 0.0f);

    public CheckIdType sDatatype;

    public string id;
    public bool randomPackageIds;

    public GameObject currentUI;
    public GameObject nextUI;
    public GameObject ErrorUI;
    public TextMeshProUGUI employeeInformation;
    public PackageListControlNew tourlist;

    public GameObject objectToScan;
    public Transform scanPosition;
    public Transform startPosition;

    private string[] ids;
    private int currIDPos;

    private MeshRenderer scanObjectMeshRenderer;
    float fadeAmount;

    void OnEnable()
    {
        scanArea = transform.GetComponent<Image>();

        fadeAmount = 1 / (1.0f / 0.05f);
        
        currIDPos = 0;
        if (randomPackageIds)
        {
            string tempID;

            ids = new string[DataManager.Instance.currentTour.ssccs.Length];
            Debug.Log(ids.Length);
            for (int i = 0; i < DataManager.Instance.currentTour.ssccs.Length; i++)
            {
                ids[i] = DataManager.Instance.currentTour.ssccs[i].code.ToString();
            }
            for (int i = 0; i< DataManager.Instance.currentTour.ssccs.Length; i++)
            {
                int rnd = Random.Range(0, ids.Length);
                tempID = ids[rnd];
                ids[rnd] = ids[i];
                ids[i] = tempID;
            }

            Debug.Log(ids[0]);

            id = ids[currIDPos];
        }

        scanObjectMeshRenderer = objectToScan.GetComponent<MeshRenderer>();


        foreach (Material m in scanObjectMeshRenderer.materials)
        {
            Color c = m.color;
            c.a = 0.0f;
            m.color = c;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(ReadID(true));
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(ReadID(false));
        }
    }

    IEnumerator ReadID(bool correct)
    {
        float t = 0.0f;

        while (t < 1.0f)
        {
            t += Time.deltaTime / 1.0f;

            objectToScan.transform.position = Vector3.Lerp(startPosition.position, scanPosition.position, t);
            foreach (Material m in scanObjectMeshRenderer.materials)
            {
                Color c = m.color;
                c.a = t;
                m.color = c;
            }
            yield return new WaitForUpdate();
        }

        scanArea.color = reading;

        yield return new WaitForSeconds(2.0f);

        if (correct)
        {

            scanArea.color = accepted;
            if (nextUI != null)
            {
                yield return new WaitForSeconds(0.2f);
                scanArea.color = white;
                objectToScan.GetComponent<ScanObject>().startMove(scanPosition, startPosition, 1.0f);
                DataManager.Instance.CheckID(id, sDatatype, currentUI, nextUI, ErrorUI);
                {
                    if(randomPackageIds)
                    {
                        currIDPos++;
                        id = ids[currIDPos];
                    }
                    if (sDatatype == CheckIdType.login)
                    {
                        employeeInformation.text = DataManager.Instance.currentEmployee.name + "\n" + DataManager.Instance.currentEmployee.id;
                    }
                    else if ((sDatatype == CheckIdType.package) && tourlist.isActiveAndEnabled)
                    {
                        tourlist.UpdateList();
                    }
                }
            }
        }
        else
        {
            scanArea.color = rejected;
        }

        scanArea.color = white;

        t = 1.0f;

        while (t > 0.0f)
        {
            t += Time.deltaTime / 1.0f;

            objectToScan.transform.position = Vector3.Lerp(startPosition.position, scanPosition.position, t);
            foreach (Material m in scanObjectMeshRenderer.materials)
            {
                Color c = m.color;
                c.a = t;
                m.color = c;
            }
            yield return new WaitForUpdate();
        }

        foreach (Material m in scanObjectMeshRenderer.materials)
        {
            Color c = m.color;
            c.a = 0.0f;
            m.color = c;
        }

        yield return null;
    }
}
