using System;
using System.Linq;

namespace KrakenMPSPConsole.Helpers
{
    public static class ManagerObjectHelper
    {
        public static void CopyValues<T>(T target, object source)
        {
            Type t = typeof(T);

            var properties = t.GetProperties().Where(prop => prop.CanRead && prop.CanWrite);

            foreach (var prop in properties)
            {
                var targetType = prop.PropertyType.ToString();
                var sourceType = source.GetType().ToString();
                if (targetType == sourceType)
                {
                    prop.SetValue(target, source);
                }
            }
        }
    }
}
