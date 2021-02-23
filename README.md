# Augemented Reality in der Logisitk
<img src="Documentation/img/Logo_Soloplan.png" alt="Soloplan Logo" width="300" height="100" style="margin-right: 10em">
<img src="Documentation/img/Logo_Hochschule_Kempten.png" alt="Hochschule Kempten Logo" width="250" height="100">


Ein Projekt der Hochschule Kempten in Kooperation mit der Firma Soloplan. 

Mit der AR Anwendung __Holo4Logistics__ für die Microsoft Hololens 2 sollen Mitarbeiter in einem Umschlagslager (Cross Dock) entlastet werden.  
Prozesse, wie das Ausladen eines LKWs und das Umfrachten der Waren, werden mithilfe von Augmented Reality abgebildet.

[![Video Holo4Logistics](https://img.youtube.com/vi/dy5mLPUU0eo/0.jpg)](https://www.youtube.com/watch?v=dy5mLPUU0eo)

Die initiale Anwendung wurde im Sommersemester 2020 im Rahmen des Bachelorprojekts für die Hololens 1 entwickelt.

Im Rahmen des Masterprojekts im Wintersemeter 2020/21 wurde die Anwendung für die bisher aktuelle Hololens 2 weiterentwickelt.


## Projekt Links
[Trello Board](https://trello.com/invite/b/fACSp7XL/202fc598d709dcb55ec93bb1a5463c5a/projekt-soloplan) | [Adobe XD Prototype](https://xd.adobe.com/view/440cbee8-2d6b-4293-86d9-ca2caed53f23-115f/) | [Google Drive](https://drive.google.com/drive/folders/1NpMZMq9XZaZmxf8EYzrtRIjRswPNyWxd)

## Development Setup
Tools die intalliert werden müssen: 
1. Unity (bisher wurde 19.4.12 verwendet, neuere Versionen müssten auch funktionieren)
2. Windows 10 SDK (10.0.18362.0)
3. (Optional) HoloLens 2 Emulator

:grey_exclamation: Das Mixed Reality Toolkit (MRTK) muss nicht intsalliert werden. Dieses ist bereits in der Version 2.3.0 eingebunden und wird nach dem clonen des Repos zur verfügung stehen. (Neuere Versionen sind vorhanden, es ist abzuwägen ob ein Upgrade lohnend und möglich ist)

__Detailiertere Anweisungen gibt es unter [Microsoft Mixed Reality Entwicklung - Installieren der Tools](https://docs.microsoft.com/de-de/windows/mixed-reality/develop/install-the-tools?tabs=unity)__

Nach der Installation der Tools, das Repo in ein beliebiges Verzeichnis clonen und das Projekt mit Unity aufmachen.

## Deployment auf Hololens 2 
Nachdem die oben genannten Tools installiert sind, das Repo gecloned wurde und das Projekt erfolgreich in Unity gestertet wurde, kann dieser [Guide](https://docs.microsoft.com/de-de/windows/mixed-reality/develop/unity/tutorials/mr-learning-base-02#building-your-application-to-your-hololens-2) befolgt werden, um die Anwendung auf die Hololens 2 zu deployen.