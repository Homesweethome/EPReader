using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EpReader.Model;
using Xceed.Words.NET;

namespace EpReader.DataService
{
    public class WordService : IWordService
    {
        private static readonly string DefaultTempPath = Path.GetTempPath();
        private string _fileName = "EpInfoTemp{0}.docx";

        private readonly List<string> _temporaryDocuments = new List<string>();

        public void Generate(InfoModel info)
        {
            string fileName = string.Format(_fileName, Guid.NewGuid().ToString());
            fileName = Path.Combine(DefaultTempPath, fileName);

            using (var document = DocX.Create(fileName))
            {
                document.InsertParagraph(info.OwnerFamily + " " + info.OwnerName + " " + info.OwnerSname).FontSize(15d).SpacingAfter(20d).Alignment = Alignment.center;
                document.InsertParagraph(GetAttribute(nameof(info.OwnerSex)) + ": " + info.OwnerSex);
                document.InsertParagraph(GetAttribute(nameof(info.OwnerBdate)) + ": " + info.OwnerBdate);
                document.InsertParagraph(GetAttribute(nameof(info.OwnerBplace)) + ": " + info.OwnerBplace).SpacingAfter(5d);

                document.InsertParagraph(GetAttribute(nameof(info.PoliceNumber)) + ": " + info.PoliceNumber);
                document.InsertParagraph(GetAttribute(nameof(info.PoliceDate)) + ": " + info.PoliceDate);
                document.InsertParagraph(GetAttribute(nameof(info.PoliceLong)) + ": " + info.PoliceLong);
                document.InsertParagraph(GetAttribute(nameof(info.PoliceSnils)) + ": " + info.PoliceSnils).SpacingAfter(5d);

                document.InsertParagraph(GetAttribute(nameof(info.Gcode)) + ": " + info.Gcode);
                document.InsertParagraph(GetAttribute(nameof(info.Gtext)) + ": " + info.Gtext).SpacingAfter(5d);

                document.InsertParagraph(GetAttribute(nameof(info.SmoRegion)) + ": " + info.SmoRegion);
                document.InsertParagraph(GetAttribute(nameof(info.SmoName)) + ": " + info.SmoName);
                document.InsertParagraph(GetAttribute(nameof(info.SmoOgrn)) + ": " + info.SmoOgrn);
                document.InsertParagraph(GetAttribute(nameof(info.SmoOkato)) + ": " + info.SmoOkato);
                document.InsertParagraph(GetAttribute(nameof(info.SmoBegin)) + ": " + info.SmoBegin);
                document.InsertParagraph(GetAttribute(nameof(info.SmoEnd)) + ": " + info.SmoEnd);

                document.Save();
            }
            _temporaryDocuments.Add(fileName);
            Process.Start(fileName);
        }

        ~WordService()
        {
            foreach (var document in _temporaryDocuments)
            {
                try
                {
                    File.Delete(document);
                }
                catch { }
            }
        }

        private string GetAttribute(string propertyName)
        {
            return typeof(InfoModel).GetProperty(propertyName).GetCustomAttributesData().First() .ConstructorArguments.First().Value.ToString();
        }
    }
}
