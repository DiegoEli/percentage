# percentage

![](https://raw.githubusercontent.com/kas/percentage/master/percentage.png)

See your battery percentage in the Windows 10 system tray

## Installing

1. [Download the latest release](https://github.com/kas/percentage/releases)
1. Put percentage.exe in your startup folder
   1. To get to your startup folder, press Windows+R, type "shell:startup", then press enter

## Compiling

This project was compiled with Visual Studio Community 2019.

Select ".NET desktop development" when setting up Visual Studio.

To build the project
1. Open the percentage/percentage.sln file with Visual Studio
1. Click "Build > Build Solution"
1. percentage.exe can be found at percentage\percentage\percentage\bin\Debug\percentage.exe

## Contributions

My goal for this project is to keep it as simple as possible. I welcome suggestions, but for complicated features I'd recommend forking the project.



******************************
## PAGE-REPOSITORY_ADD_PERSONAL
   REFERENCE_ICON
![](https://i.imgur.com/d7jCVHF.png)

FIRST CODE
* pageSite:   https://github.com/kas/percentage
* Font:       Segoe UI     - font defaultWindows
* sizeFont:   18   (19-ideal)    - scale 100 %

SECOND CODE
* pageSite:   Microsoft-Store Battery_Percentage_Icon
* Font:       Segoe UI
* sizeFont:   09 point

## FAILS-CURRENT_ACTUAL
* mostar fondo transparetente
* mostrar texto claro con modo oscuro
* mostrar texto oscuro con modo claro
* Font size becomes small (when charging or plugged in) with a "*" when it should remain the same size.
* The interface does not adapt (or is not visible) to the loading text [100] in LIGHT mode
* The percentage sign(%) is not shown or does not appear, it should be shown like this [100%] or this [100 %]

## CODE-GITHHUB
ruta: https://github.com/kas/percentage/blob/master/percentage/percentage/TrayIcon.cs

## LINE-CODE-CHANGE_MODIFY
* CORRECTION #1

* CORRECTION #2
https://github.com/OrdosX/percentage

* CORRECTION #3

   **LINE 76-original**: String bitmapText = isCharging ? percentage + "*" : percentage;

   **LINE 76-change**: String bitmapText = isCharging ? percentage + "%" : percentage + "%";

## ADD:
   los dos se rompen en modo claro :v
   
   https://github.com/kas/percentage/blob/master/percentage/percentage/TrayIcon.cs
   
   https://github.com/thumperward/percentage/releases/tag/1.2.0

**WARNING** uso maximo de recursos  de 6.0 MB en memoria ram
