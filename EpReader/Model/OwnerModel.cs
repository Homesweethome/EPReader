using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using PropertyChanged;

namespace EpReader.Model
{
    [AddINotifyPropertyChangedInterface]
    public class OwnerModel
    {
        public string OwnerFamily { get; set; }
        public string OwnerName { get; set; }
        public string OwnerSname { get; set; }        
        public string OwnerSex { get; set; }
        public string OwnerBdate { get; set; }
        public string OwnerBplace { get; set; }
        public string PoliceNumber { get; set; }
        public string PoliceDate { get; set; }
        public string PoliceLong { get; set; }
        public string PoliceSnils { get; set; }
        public string Gcode { get; set; }
        public string Gtext { get; set; }
    }
}
