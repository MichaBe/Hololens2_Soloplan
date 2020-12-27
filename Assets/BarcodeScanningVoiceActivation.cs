using System;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.QrCode;

public class BarcodeScanningVoiceActivation : MonoBehaviour
{
    [SerializeField]
    bool addingNew = false;
    private Webcam webcam;
    public CheckIdType sDatatype;

    private WebCamTexture wCamTexture;

    private DataManager manager;

    float delay = 0;

    public GameObject nextUI;
    public GameObject ErrorUI;
    public GroundNavigationController groundNavigation;

    private GameObject gameManager;
    
    public PackageListControlNew tourlist;
    private IBarcodeReader barcodeReader;

    private bool isScanningOn;

    void OnEnable()
    {
        barcodeReader = new BarcodeReader();
            gameManager = GameObject.FindGameObjectWithTag("Manager");
            manager = DataManager.Instance;
            wCamTexture = gameManager.GetComponent<Webcam>().GetWebCamTexture();
    }

    private int frames;
    private void Update()
    {
        if (isScanningOn)
        {
            frames++;
            if (frames % 40 == 0)
            {
                try
                {
                    if (!wCamTexture.isPlaying)
                    {
                        wCamTexture.Play();
                    }

               
                    barcodeReader.Options.PossibleFormats = new System.Collections.Generic.List<BarcodeFormat>();
                    barcodeReader.Options.PossibleFormats.Add(BarcodeFormat.QR_CODE);
                    barcodeReader.Options.TryHarder = false;

                    var result = barcodeReader.Decode(wCamTexture.GetPixels32(),
                        wCamTexture.width, wCamTexture.height);
                    if (result != null)
                    {
                        Debug.Log("DECODED TEXT FROM QR: " + result.Text);
                        if (addingNew)
                        {

                        }
                        else if (manager.CheckID(result.Text, sDatatype, transform.parent.gameObject, nextUI, ErrorUI))
                        {
                            wCamTexture.Stop();
                            if (sDatatype == CheckIdType.package)
                            {
                                tourlist.UpdateList();
                                groundNavigation.updateGroundNavigation();
                            }
                        }
                    }
                }
                catch (Exception ex) { Debug.LogWarning(ex.Message); }
            }
            
        }
    }

    public void StartScanning()
    {
        this.isScanningOn = true;

    }

    public void StopScanning()
    {
        this.isScanningOn = false;
        wCamTexture.Stop();
    }

    private void OnDisable()
    {
        this.isScanningOn = false;
        wCamTexture.Stop();
    }
}