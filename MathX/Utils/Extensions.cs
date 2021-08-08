using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MathX.Utils
{
    public static class Extensions
    {
        public static int GetActualPosition(this StreamReader streamReader)
        {
            int charpos = (int)streamReader.GetType().InvokeMember("_charPos",
            BindingFlags.DeclaredOnly |
            BindingFlags.Public | BindingFlags.NonPublic |
            BindingFlags.Instance | BindingFlags.GetField
             , null, streamReader, null);

            int charlen = (int)streamReader.GetType().InvokeMember("_charLen",
            BindingFlags.DeclaredOnly |
            BindingFlags.Public | BindingFlags.NonPublic |
            BindingFlags.Instance | BindingFlags.GetField
                , null, streamReader, null);

            return (int)streamReader.BaseStream.Position - charlen + charpos;
        }
    }
}
