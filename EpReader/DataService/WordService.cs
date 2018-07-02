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
                document.InsertParagraph(info.OwnerSex);
                document.InsertParagraph(info.OwnerBdate);
                document.InsertParagraph(info.OwnerBplace).SpacingAfter(5d);

                document.InsertParagraph(info.PoliceNumber);
                document.InsertParagraph(info.PoliceDate);
                document.InsertParagraph(info.PoliceLong);
                document.InsertParagraph(info.PoliceSnils);

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
    }
}
