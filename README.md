# Augemented Reality in der Logisitk
<img src="Documentation/img/Logo_Soloplan.png" alt="Soloplan Logo" width="250" height="75" style="margin-right: 1em">
<img src="Documentation/img/Logo_Hochschule_Kempten.png" alt="Hochschule Kempten Logo" width="200" height="75">


<small>Ein Projekt der Hochschule Kempten in Kooperation mit der Firma [__Soloplan__](https://www.soloplan.de/)</small>. 

Mit der AR Anwendung __Holo4Logistics__ für die Microsoft Hololens 2 sollen Mitarbeiter in einem Umschlagslager (Cross Dock) entlastet werden.  
Prozesse, wie das Ausladen eines LKWs und das Umfrachten der Waren, werden mithilfe von Augmented Reality abgebildet.

[![Video Holo4Logistics](https://img.youtube.com/vi/dy5mLPUU0eo/0.jpg)](https://www.youtube.com/watch?v=dy5mLPUU0eo)

Die initiale Anwendung wurde im Sommersemester 2020 im Rahmen des Bachelorprojekts für die Hololens 1 entwickelt.

Im Rahmen des Masterprojekts im Wintersemeter 2020/21 wurde die Anwendung für die bisher aktuelle Hololens 2 weiterentwickelt.


## Projekt Links
[Trello Board](https://trello.com/invite/b/fACSp7XL/202fc598d709dcb55ec93bb1a5463c5a/projekt-soloplan) | [Adobe XD Prototype](https://xd.adobe.com/view/440cbee8-2d6b-4293-86d9-ca2caed53f23-115f/) | [Google Drive](https://drive.google.com/drive/folders/1NpMZMq9XZaZmxf8EYzrtRIjRswPNyWxd)

## Development Setup
Tools die installiert werden müssen: 
1. Unity (bisher wurde 19.4.12 verwendet, neuere Versionen müssten auch funktionieren)
2. Windows 10 SDK (10.0.18362.0)
3. (Optional) HoloLens 2 Emulator

:warning: Das Mixed Reality Toolkit (MRTK) muss nicht intsalliert werden. Dieses ist bereits in der Version 2.3.0 eingebunden und wird nach dem clonen des Repos zur verfügung stehen. (Neuere Versionen sind vorhanden, es ist abzuwägen ob ein Upgrade lohnend und möglich ist)

__Detailiertere Anweisungen gibt es unter [Microsoft Mixed Reality Entwicklung - Installieren der Tools](https://docs.microsoft.com/de-de/windows/mixed-reality/develop/install-the-tools?tabs=unity)__

Nach der Installation der Tools, das Repo in ein beliebiges Verzeichnis clonen und das Projekt mit Unity aufmachen.

## Deployment auf Hololens 2 
Nachdem die oben genannten Tools installiert sind, das Repo gecloned wurde und das Projekt erfolgreich in Unity gestertet wurde, kann dieser [Guide](https://docs.microsoft.com/de-de/windows/mixed-reality/develop/unity/tutorials/mr-learning-base-02#building-your-application-to-your-hololens-2) befolgt werden, um die Anwendung auf die Hololens 2 zu deployen.

## Dokumentation der wichtigsten Codekomponenten

### Data Management 
Die Hauptkomponente der Anwendung ist [DataManager.cs](Assets/Scripts/DataManager.cs) welche an das Unity Objekt Manager angehängt ist.

Dieses Singleton liest die JSON Datei [ScanningData.json](Assets/Resources/ScanningData.json) die sich unter [Assets/Ressources/](Assets/Resources/) befindet, und kümmert sich für die hauptlogiken wie get/set der aktuellen Tour, get/set des aktuellen Benutzers, get/set des gescannten Pakets, get der Touren, get der Pakete einer Tour, etc. Die Methoden sind einfach zu verstehen und bedürfen keiner weiteren erklärung. 

Momentan wird [ScanningData.json](Assets/Resources/ScanningData.json) nur gelesen und Änderungen während der Verwendung der AR App werden nicht persistiert. Bei restart der App wird der Zustand also zurückgestzt wie er in der JSON definiert ist.

Dieses Singleton kann in anderen Scripten referenziert werden um beispielsweise eine Liste aller Touren zu erstellen:



### Format der JSON Daten Datei
Die [ScanningData.json](Assets/Resources/ScanningData.json) dient momentan als Datenqulle. In der sind alle Benutzer, Touren und Pakete der Touren gespeichert sowie die dazugehörigen IDs die für die QR Codes verwendet werden. 

__Benutzer__
```js
{
	"loginData": [
		{
			"id": "123456",      // <--- Wird in den QR-Code eingebettet (Mitarbeiterausweis)
			"name": "Max Mustermann"
		},
    ...
}

```
__Eine Tour mit Paketen__
```js
...
"tourData": [
		{
			"id": 648925,     // <--- Wird in den QR-Code eingebettet (Mitarbeiterausweis)
			"areaType": 1,    // <--- Art der Tour. 1 = Ausladen, 2 = Einladen
			"startLane": "1", // <--- Linie die bearbeitet werden soll
			"startTime": "15:00",
			"ssccs": [       // <--- Liste der Pakete
				{
					"id": 945,
					"code": "945",  // <--- Muss bei der Generierung des QR-Codes Mitgegeben werden
					"comment": null,
					"SSCCStatus": 0,   // <--- Status des Pakets. 0 = Paket noch nicht bearbeitet, 1 =Paket noraml bearbeitet, 2 = Paket  fehlt, 3 = Paket beschädigt
					"destinationLane": {
						"id": 10,   // <-- id oder nummer der Linie
						"designation": "Linie 10",
						"laneType": "normal",   // normal oder zl= zwischenlager
						"posInLane": 0     // <--- Position innerhalb der line
					},

					"weight": 12.3

				},
				{
					"id": 358,      // <--- ein weiteres Paket
					"code": "358",
					"comment": null,
					"SSCCStatus": 0,
					"destinationLane": {
						"id": 99,
						"designation": "Zwischenlager",
						"laneType": "zl",
						"posInLane": 0
					},

					"weight": 2.5
				},
                ...
            ]
        }, 
        ...
    }
 ]
```


### Scan der QR-Codes

### Handmenü

### Navigation auf den Boden

### Momentane Limitationen und Herausforderungen 
Persisiteren der Daten notwnendig 
Am ende einer Tour Zusammenfassung anzeigen 
Navigation nur Prototypisch und harcoded 
Performance des Sannings sehr schlecht 
Spracheingabe der Login oder paketcodes

### Wall of Fame (or Shame)

Bachelorprojekt 

Masterprojekt 

Happy Coding :hearts:


