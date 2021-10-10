using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IT.Services.Misc
{
    public static class Object
    {
        #region object Extensions

        public static int ToInt(this object obj)
        {
            return Convert.ToInt32(obj);
        }

        public static bool ToBoolean(this object obj)
        {
            return Convert.ToBoolean(obj);
        }

        public static DateTime ToDateTime(this object obj)
        {
            return Convert.ToDateTime(obj);
        }

        public static float ToFloat(this object obj)
        {
            return (float)Convert.ToDouble(obj);
        }

        public static double ToDouble(this object obj)
        {
            return Convert.ToDouble(obj);
        }

        public static decimal ToDecimal(this object obj)
        {
            return Convert.ToDecimal(obj);
        }

        #endregion object Extensions
    }
}
