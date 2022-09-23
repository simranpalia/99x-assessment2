using System;
using Microsoft.Ajax.Utilities;

namespace _99xAssessment2.Utils
{
    public static class _99XExtensions
    {
        public static int ToInt(this string stringValue)
        {
            if (stringValue.IsNullOrWhiteSpace())
                return 0;

            return Convert.ToInt32(stringValue);
        }
    }
}