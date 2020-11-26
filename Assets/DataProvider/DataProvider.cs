using System;
using System.IO;
using System.Linq;
using UnityEngine;

namespace DataProvider
{
    [Serializable]
    public class UserData
    {
        public string name;
        public string id;
    }

    [Serializable]
    public class Destination
    {
        public int id;
        public string designation;
        public int laneType;
    }

    [Serializable]
    public class PackageData
    {
        public int id;
        public string code; //NVE-Number, Barcode/QR-Code
        public int posId;   //internal reference
        public string comment;
        public string creator;
        public int SSCCStatus;  //0=not set, 1=successfully scanned
        public Destination destinationLane;

        public double weight;
    }

    [Serializable]
    public class TourData
    {
        public int id;
        public int tourNumber;  //short number/name
        public int areaType;    //1=inbound , 2=outbound
        public String startTime;
        public String startLane;
        public PackageData[] ssccs;

    }

    [Serializable]
    public class Data
    {
        public UserData[] loginData;
        public TourData[] tourData;
    }

    [CreateAssetMenu(fileName = "Data", menuName = "Scripts/DataProvider", order = 1)]
    public class DataProvider : ScriptableObject
    {
        public TextAsset tInputFile;

        public bool bSaveOnDisk = false;

        private Data dJSONData;

        private void saveOnDisk()
        {
            Debug.Log("save on disk: " + bSaveOnDisk.ToString());
            if (!bSaveOnDisk)
            {
                return;
            }
            string path = Path.Combine(Application.dataPath, "..", "tourOuput.json");
            File.WriteAllText(path, JsonUtility.ToJson(dJSONData));
            Debug.Log("Saved tours to " + path);
        }

        public void OnEnable()
        {
            dJSONData = JsonUtility.FromJson<Data>(tInputFile.ToString());
            Debug.Log(JsonUtility.ToJson(dJSONData));
        }

        public UserData FindUserById(string sUserId)
        {
            Debug.Log("Find User by UserId: " + sUserId);
            return dJSONData.loginData.FirstOrDefault(t => t.id.Equals(sUserId));
        }

        public PackageData FindPackageByCode(string sPackageCode)
        {
            Debug.Log("Find Package by PackageId: " + sPackageCode);
            return dJSONData.tourData.FirstOrDefault(t => t.ssccs.Any(p => p.code.Equals(sPackageCode))).ssccs.FirstOrDefault(p => p.code.Equals(sPackageCode));
        }

        /*public TourData FindTourByPackageCode(string sPackageCode)
        {
            Debug.Log("Find Tour by PackageId: " + sPackageCode);
            return dJSONData.tourData.FirstOrDefault(t => t.ssccs.Any(p => p.code.Equals(sPackageCode)));
        }*/

        public TourData FindTourById(int sTourId)
        {
            Debug.Log("Find Tour by TourId: " + sTourId.ToString());
            return dJSONData.tourData.FirstOrDefault(t => t.id.Equals(sTourId));
        }

        public void UpdateTour(TourData tour)
        {
            Debug.Log("Update tour " + tour.id.ToString());
            int index = Array.FindIndex(dJSONData.tourData, t => t.id == tour.id);
            dJSONData.tourData.SetValue(tour, index);

            saveOnDisk();
        }

        public TourData [] GetAllTours()
        {
            return dJSONData.tourData;
        }
    }
}
