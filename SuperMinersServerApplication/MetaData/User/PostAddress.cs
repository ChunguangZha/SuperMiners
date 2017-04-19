﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.User
{
    public class PostAddress
    {
        public string Province = "";

        public string City = "";

        public string County = "";

        public string DetailAddress = "";

        public string ReceiverName = "";

        public string PhoneNumber = "";

        /// <summary>
        /// 以;号分隔
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string text = Province + ";" + City + ";" + County + ";" + DetailAddress + ";" + ReceiverName + ";" + PhoneNumber;
            return text;
        }

        public static PostAddress FromString(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }

            string[] ps = text.Split(new char[] { ';' });
            if (ps == null || ps.Length != 6)
            {
                return null;
            }

            PostAddress address = new PostAddress()
            {
                Province = ps[0],
                City = ps[1],
                County = ps[2],
                DetailAddress = ps[3],
                ReceiverName = ps[4],
                PhoneNumber = ps[5]
            };

            return address;
        }
    }
}
