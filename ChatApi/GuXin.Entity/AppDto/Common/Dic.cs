using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuXin.Entity.AppDto.Common
{
    public class Dic<T>
    {
        public string key { get; set; }
        public T value { get; set; }

        public Dic(string k, T v)
        {
            key = k;
            value = v;
        }
    }
    public class DicGuid<T>
    {
        public Guid key { get; set; }
        public T value { get; set; }

        public DicGuid(Guid k, T v)
        {
            key = k;
            value = v;
        }
    }
    public class DicInt<T>
    {
        public int key { get; set; }
        public T value { get; set; }

        public DicInt(int k, T v)
        {
            key = k;
            value = v;
        }
    }
}
