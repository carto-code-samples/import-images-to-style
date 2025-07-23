using ArcGIS.Core.CIM;
using ArcGIS.Core.Data;
using ArcGIS.Core.Geometry;
using ArcGIS.Desktop.Catalog;
using ArcGIS.Desktop.Core;
using ArcGIS.Desktop.Editing;
using ArcGIS.Desktop.Extensions;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using ArcGIS.Desktop.Framework.Dialogs;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.KnowledgeGraph;
using ArcGIS.Desktop.Layouts;
using ArcGIS.Desktop.Mapping;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportImagesToStyle
{
  internal class ImportImagesToStyle : Button
  {
    protected override void OnClick()
    {
      string selectedFolder = SelectFolder();
      ImportImages(selectedFolder);
    }

    private async void ImportImages(string selectedFolder)
    {
      await QueuedTask.Run(async () =>
      {
        try
        {
          var imageExtensions = new List<string> { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".svg", ".emf" };
          IEnumerable<String> imagePaths = Directory.EnumerateFiles(selectedFolder, "*.*", SearchOption.TopDirectoryOnly)
                                                   .Where(s => imageExtensions.Any(ext => s.EndsWith(ext, StringComparison.OrdinalIgnoreCase)));
          if (imagePaths.Count() > 0)
          {
            // create a style in selectedFolder
            string stylePath = selectedFolder + "\\" + "ImportedImages.stylx";
            StyleHelper.CreateStyle(Project.Current, stylePath);
            StyleProjectItem style = Project.Current.GetItems<StyleProjectItem>().First(x => x.Path == stylePath);
            SymbolStyleItem styleItem = null;
            CIMSymbol symbol = null;
            CIMMarker marker = null;


            // loop through the images
            foreach (string image in imagePaths)
            {
              styleItem = new SymbolStyleItem();
              marker = SymbolFactory.Instance.ConstructMarkerFromFile(image);
              symbol = SymbolFactory.Instance.ConstructPointSymbol(marker);
              styleItem.Symbol = symbol;
              styleItem.Name = Path.GetFileNameWithoutExtension(image);
              style.AddItem(styleItem);
            }

          }
          ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show("Done importing images. A new style has been added to this project.");
        }
        catch (Exception ex)
        {
          System.Diagnostics.Trace.WriteLine(ex.Message);
        }
      });
    }

    public static string SelectFolder()
    {
      var folderFilter = BrowseProjectFilter.GetFilter("esri_browseDialogFilters_folders");
      var dlg = new OpenItemDialog()
      {
        BrowseFilter = folderFilter,
        Title = "Select a folder"
      };
      if (!dlg.ShowDialog().Value)
        return "";
      var item = dlg.Items.First();
      return item.Path;
    }
  }
}
