using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPICopyGroup
{
    [TransactionAttribute(TransactionMode.Manual)]
    public class CopyGroup : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiApp = commandData.Application;
            UIDocument uiDoc = uiApp.ActiveUIDocument;
            Document doc = uiDoc.Document;

            Reference reference = uiDoc.Selection.PickObject(ObjectType.Element, "Выберите элемент");
            Element element = doc.GetElement(reference);
            Group group = element as Group;

            XYZ point = uiDoc.Selection.PickPoint("Выберите точку");

            using (var ts = new Transaction(doc, "Копирование группы объектов"))
            {
                ts.Start();
                doc.Create.PlaceGroup(point, group.GroupType);
                ts.Commit();
            }
            return Result.Succeeded;
        }
    }
}
