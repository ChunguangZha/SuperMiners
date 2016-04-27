using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaData
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class DatabaseFieldAttribute : Attribute
    {
        public bool NotNull { get; set; }

        public bool Unique { get; set; }

        public object DefaultValue { get; set; }
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class NonDatabaseFieldAttribute : Attribute
    {

    }
}
