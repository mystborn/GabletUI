using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GabletUI.Utils
{
    public static class StringUtils
    {
        public static string AddCaller(
            this string value, 
            [CallerFilePath] string file = null,
            [CallerLineNumber] int number = 0,
            [CallerMemberName] string member = null)
        {
            if (value is null)
                value = string.Empty;

            file = file.Remove(0, file.LastIndexOf("GabletUI"));
            file = file.Remove(file.LastIndexOf('.'));

            return $"[{file}.{member}:{number}] {value}";
        }
    }
}
