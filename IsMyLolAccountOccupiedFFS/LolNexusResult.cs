using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsMyLolAccountOccupiedFFS
{

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class LolNexusResult
    {

        private bool successfulField;

        /// <remarks/>
        public bool successful
        {
            get
            {
                return this.successfulField;
            }
            set
            {
                this.successfulField = value;
            }
        }
    }


}
