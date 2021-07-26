using Scalable_Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Scalable_Web.Helpers
{
    public static class ExtensionMethods
    {

        public static string EncodeBase64(this string value)
        {
            var valueBytes = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(valueBytes);
        }

        public static string DecodeBase64(byte[] value)
        {
            var valueBytes = System.Convert.ToBase64String(value);
            return valueBytes;
        }

        public static int GetOriginalLengthInBytes(string base64string)
        {
            if (string.IsNullOrEmpty(base64string)) { return 0; }

            var characterCount = base64string.Length;
            var paddingCount = base64string.Substring(characterCount - 2, 2)
                                           .Count(c => c == '=');

            
            var data1 = base64string.Substring(characterCount - 2, 1);
            var data2 = base64string.Substring(characterCount - 2, 2);

            return (3 * (characterCount / 4)) - paddingCount;
        }

        public static bool AreEqual(Difference difference)
        {
            if (GetOriginalLengthInBytes(DecodeBase64(difference.Left))== GetOriginalLengthInBytes(DecodeBase64(difference.Right)))
            {
                return true;
            }
            return false;
        }

        public static byte[] Base64BitsDifferent(byte[] left, byte[] right)
        {
            string resultL = Encoding.UTF8.GetString(left).TrimEnd('\0');

            string resultR = Encoding.UTF8.GetString(right).TrimEnd('\0');

            string diff = ReturnDifferences(resultL, resultR);

            string base64ToReturn = EncodeBase64(diff);
            
            byte[] base64ToReturn2 = System.Convert.FromBase64String(base64ToReturn);
            return base64ToReturn2;

        }



        public static string ReturnDifferences(string resultLeft, string resultRight)
        {
            string outcome = "";
            //var File1 = new List<string>(resultLeft.Split(' '));
            //var File2 = new List<string>(resultRight.Split(' '));

            int j = 0;
            for (int i = 0; i < resultLeft.Length && j < resultRight.Length; i++, j++)
            {
                if(resultLeft[i].ToString()==" " && resultRight[j].ToString() == " ")
                {
                    outcome += "*";
                }
                else if(resultLeft[i].ToString() == resultRight[j].ToString())
                {
                    outcome += resultLeft[i];
                }
                else
                {
                    outcome += "<i>" + resultRight[j] + "</i>" + resultLeft[i].ToString();
                    i--;
                }
            }

            return outcome;
        }
    }
}
