using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Distribution.Logic
{
    public static class CommLogic
    {
        public static void DeepClone<T>(T OriEntity, T UpdateEntity)
        {
            PropertyInfo[] props = null;
            Type type = typeof(T);
            props = type.GetProperties();
            foreach (var p in props)
            {
                var value = p.GetValue(UpdateEntity);
                p.SetValue(OriEntity, value);
            }
        }
    }
}
