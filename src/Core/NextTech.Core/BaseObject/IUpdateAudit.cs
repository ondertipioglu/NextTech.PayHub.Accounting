using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextTech.Core.BaseObject
{
    public interface IUpdateAudit
    {
        DateTime? UpdatedOn { get; }
    }
}
