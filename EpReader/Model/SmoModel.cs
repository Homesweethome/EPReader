using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PropertyChanged;

namespace EpReader.Model
{
    [AddINotifyPropertyChangedInterface]
    public class SmoModel
    {
        public string SmoOgrn { get; set; }
        public string SmoOkato { get; set; }
        public string SmoBegin { get; set; }
        public string SmoEnd { get; set; }
        public string SmoRegion { get; set; }
        public string SmoName { get; set; }
    }
}
