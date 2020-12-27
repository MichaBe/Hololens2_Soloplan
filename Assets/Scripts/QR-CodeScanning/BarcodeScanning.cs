﻿using System;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.QrCode;

public class BarcodeScanning : MonoBehaviour
{
    [SerializeField]
    bool addingNew = false;
    private Webcam webcam;
    public CheckIdType sDatatype;

    private WebCamTexture wCamTexture;

    private DataManager manager;

    float delay = 0;

    Image scanArea;

    Color white = new Color(255.0f, 255.0f, 255.0f);
    Color reading = new Color(255.0f, 165.0f, 0.0f);
    Color accepted = new Color(0.0f, 255.0f, 0.0f);
    Color rejected = new Color(255.0f, 0.0f, 0.0f);

    public GameObject nextUI;
    public GameObject ErrorUI;
    public GroundNavigationController groundNavigation;

    private GameObject gameManager;
    
    public PackageListControlNew tourlist;
    private IBarcodeReader barcodeReader;

    void Awake()
    {
        if (gameObject.GetComponent<BarcodeScanning>().enabled)
        {
            barcodeReader = new BarcodeReader();
            barcodeReader.Options.PossibleFormats = new System.Collections.Generic.List<BarcodeFormat>();
            barcodeReader.Options.PossibleFormats.Add(BarcodeFormat.QR_CODE);
            barcodeReader.Options.TryHarder = false;
            scanArea = transform.GetComponent<Image>();
            gameManager = GameObject.FindGameObjectWithTag("Manager");
            manager = DataManager.Instance;
            wCamTexture = gameManager.GetComponent<Webcam>().GetWebCamTexture();
            if (wCamTexture != null)
            {
                wCamTexture.Play();
            }
        }
    }

    private static Color32[] Encode(string textForEncoding,
    int width, int height)
    {
        var bWriter = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Height = height,
                Width = width
            }
        };
        return bWriter.Write(textForEncoding);
    }

    public Texture2D generateQR(string text)
    {
        var encoded = new Texture2D(256, 256);
        var color32 = Encode(text, encoded.width, encoded.height);
        encoded.SetPixels32(color32);
        encoded.Apply();
        return encoded;
    }

    private int frames = 0;
    private void Update()
    {
        frames++;
        if (frames % 40 == 0) {
            try
            {
                if (!wCamTexture.isPlaying)
                {
                    wCamTexture.Play();
                }

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
            delay = 0.0f;
        }
    }

    public void SwitchUI()
    {
        scanArea.color = white;
        nextUI.SetActive(true);
        transform.parent.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        wCamTexture.Stop();
    }
}