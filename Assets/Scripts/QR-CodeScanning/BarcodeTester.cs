using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.QrCode;

public class BarcodeTester : MonoBehaviour
{
    private WebCamTexture wCamTexture;
    private Rect screenRect;
    private IBarcodeReader barcodeReader;

    int delay = 0;
    Image scanArea;

    void Awake()
    {
        barcodeReader = new BarcodeReader();
        scanArea = transform.GetComponent<Image>();
        screenRect = new Rect(0, 0, Screen.width, Screen.height);
        wCamTexture = new WebCamTexture();
        wCamTexture.requestedHeight = Screen.height;
        wCamTexture.requestedWidth = Screen.width;
        if (wCamTexture != null)
        {
            wCamTexture.Play();
        }
        else
        {
            Debug.Log("Keine Webcam erkannt");
        }
    }

    void Update()
    {
        delay++;
        if (delay == 10)
        {
            try
            {
                var result = barcodeReader.Decode(wCamTexture.GetPixels32(),
                  wCamTexture.width, wCamTexture.height);
                if (result != null)
                {
                    Debug.Log("Qr-Code read: " + result);
                }
                else
                {
                    Debug.Log("No QR-Code Found");
                }
            }
            catch (Exception ex) { 
                Debug.LogWarning(ex.Message);
            }
            delay = 0;
        }
    }
}
