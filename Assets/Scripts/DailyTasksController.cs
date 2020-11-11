using Microsoft.MixedReality.Toolkit.Utilities;
using TMPro;
using UnityEngine;

public class DailyTasksController : MonoBehaviour
{
    private DataManager manager;

    // Start is called before the first frame update
    private void Start()
    {
        var taskCollection = GameObject.Find("TaskCollection");
        var taskButtonTemplate = GameObject.Find("TaskButtonTemplate");
        manager = DataManager.Instance;
        var tourList = manager.data.GetAllTours();
        foreach (var tour in tourList)
        {
            Debug.Log(JsonUtility.ToJson(tour.areaType));
            var task = Instantiate(taskButtonTemplate, taskCollection.transform);
            var taskContent = task.transform.Find("ButtonContent").gameObject;
            var textMesh = taskContent.transform.Find("IconAndText/TextMeshPro").gameObject;
            var mText = textMesh.GetComponent<TextMeshPro>();

            // set the right text and icon depending on Tour type
            // ("Ausladen": tour.areaType == 1, "Einladen": tour.areaType)
            if (tour.areaType == 1)
            {
                mText.text = "Ausladen";
                taskContent.transform.Find("IconAndText/UnloadIcon").gameObject.SetActive(true);
            }
            else if (tour.areaType == 2)
            {
                mText.text = "Einladen";
                taskContent.transform.Find("IconAndText/LoadIcon").gameObject.SetActive(true);
            }

            // set time
            var timeTMP = taskContent.transform.Find("Time/TextMeshPro").gameObject;
            timeTMP.GetComponent<TextMeshPro>().text = tour.startTime;

            //set Line
            var lineTMP = taskContent.transform.Find("Line/TextMeshPro").gameObject;
            lineTMP.GetComponent<TextMeshPro>().text = tour.startLane;
        }

        // hide the task template
        Destroy(taskButtonTemplate);

        // update Layout 
        taskCollection.GetComponent<GridObjectCollection>().UpdateCollection();
    }
}