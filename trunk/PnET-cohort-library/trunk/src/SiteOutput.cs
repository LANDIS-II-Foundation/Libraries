﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Landis.Library.BiomassCohortsPnET
{
    public class SiteOutput
    {
        public const string PNEToutputsites = "PNEToutputsites";

        private List<string> FileContent;
        public string FileName { get; private set; }
        public string SiteName { get; private set; }
        public string Path { get; private set; }
        public SiteOutput(string SiteName, string FileName, string Header)
        {
            this.SiteName = SiteName;
            this.Path = "Output/" + PNEToutputsites + "/" + SiteName + "/";
            this.FileName = FileName;

            if (System.IO.File.Exists(Path + FileName))
            {
                System.IO.File.Delete(Path + FileName);
            }

            if (System.IO.Directory.Exists(Path) == false)
            {
                System.IO.Directory.CreateDirectory(Path);
            }
            FileContent = new List<string>(new string[] { Header });
            Write();
        }
        public void Add(string s)
        {
            FileContent.Add(s);
        }
        public void Write()
        {
            StreamWriter sw = new StreamWriter(Path+FileName, true);

            foreach (string line in FileContent)
            {
                sw.WriteLine(line);
            }
            sw.Close();
            FileContent.Clear();
        }
    }
}
