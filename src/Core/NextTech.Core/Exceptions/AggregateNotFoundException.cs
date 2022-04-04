using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextTech.Core.Exceptions
{
    public class AggregateNotFoundException : Exception
    {
        public AggregateNotFoundException(string typeName, int id) : base($"{typeName} with id '{id}' was not found")
        {

        }

        public static AggregateNotFoundException For<T>(int id)
        {
            return new AggregateNotFoundException(typeof(T).Name, id);
        }
    }    
}
