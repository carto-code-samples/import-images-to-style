# import-svgs-to-style
Imports a folder of image files into an ArcGIS Pro style. The new .stylx file is saved in the same folder as the .svg files. 

Supported image file extentions:
- .jpg
- .jpeg
- .png
- .gif
- .bmp
- .svg
- .emf

### Requirements
- ArcGIS Pro versions 3.3 or later (not tested on earlier versions)

### To use:
- After installing this add-in, open ArcGIS Pro and go to the Add-In tab. 
- Click the Import Images To Style button, navigate to and select a folder that contains image files, then click OK. 
- A new style file will be created in the same folder as the image files and automatially added to the current project. 

### To install:
Clone this repository and double-click the correct .esriAddinX file for your version. It will then automatically be copied into your ArcGIS Pro Addins folder and will  be accessible in the Add-Ins tab in ArcGIS Pro the next time you open it.
- For Pro 3.3 or later:
`..\import-images-to-style\v33\ImportImagesToStyle\bin\Debug\net8.0-windows\ImportImagesToStyle.esriAddinX`

To install without cloning this repository, download that individual file from this repo and run it.
- For Pro 3.3 or later:
https://github.com/carto-code-samples/import-images-to-style/blob/main/v33/ImportImagesToStyle/bin/Debug/net8.0-windows/ImportImagesToStyle.esriAddinX

### To modify:
Install Visual Studio 2022 and the ArcGIS Pro SDK for developers following the instructions in this repo:

https://github.com/Esri/arcgis-pro-sdk/wiki/ProGuide-Installation-and-Upgrade

Clone this repository and open the solution file inside the main folder named ImportImagesToStyle.sln
