using System;
using System.Collections.Generic;
using System.Text;

namespace FSELink.Utilities
{
    public class CommonHelper
    {

        /// <summary>
        /// 将字符转成ASCII码
        /// </summary>
        /// <param name="_char">需要转换的字符</param>
        /// <returns></returns>
        public static int CharToASCII(string _char)
        {
            byte[] array = System.Text.Encoding.ASCII.GetBytes(_char);
            int asciicode = (int)(array[0]);
            string ASCIIstr1 = Convert.ToString(asciicode);
            return Convert.ToInt32(ASCIIstr1);
        }

        /// <summary>
        /// 将ASCII码转成对应的字符
        /// </summary>
        /// <param name="number">ASCII码</param>
        /// <returns></returns>
        public static string ASCIIToChar(int number)
        {
            byte[] array = new byte[1];
            array[0] = (byte)(Convert.ToInt32(number)); //ASCII码强制转换二进制
            return Convert.ToString(System.Text.Encoding.ASCII.GetString(array));
        }

        /// <summary>
        /// Determine of specified type is nullable
        /// </summary>
        public static bool IsNullable(Type t)
        {
            return !t.IsValueType || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        /// <summary>
        /// Return underlying type if type is Nullable otherwise return the type
        /// </summary>
        /// 
        public static Type GetCoreType(Type t)
        {
            if (t != null && IsNullable(t))
            {
                if (!t.IsValueType)
                {
                    return t;
                }
                else
                {
                    return Nullable.GetUnderlyingType(t);
                }
            }
            else
            {
                return t;
            }
        }

    }
}
