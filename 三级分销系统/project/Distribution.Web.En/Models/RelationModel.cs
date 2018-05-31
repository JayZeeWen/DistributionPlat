using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Distribution.Web.En.Models
{
    [Serializable]
    public class RelationModel
    {
        public string name { get; set; }

        public string number { get; set; }

        public string level { get; set; }

        public List<RelationModel> children { get; set; }
    }
}