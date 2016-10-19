using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersWPF.Utility
{
    public class RegistryOper
    {
        private static string RegistryPath = "XunLineKuangChang";

        public static void SaveUserNamePassword(string userName, string password, bool savePassword)
        {
            RegistryKey rootKey = null;
            RegistryKey subKey = null;

            try
            {
                rootKey = Registry.CurrentUser.OpenSubKey("SOFTWARE", true);

                if (!rootKey.GetSubKeyNames().Contains(RegistryPath))
                {
                    if (!savePassword)
                    {
                        return;
                    }
                    subKey = rootKey.CreateSubKey(RegistryPath);
                }
                else
                {
                    subKey = rootKey.OpenSubKey(RegistryPath);
                }

                if (savePassword)
                {
                    subKey.SetValue("username", DESEncrypt_Client.EncryptDES(userName));
                    subKey.SetValue("password", DESEncrypt_Client.EncryptDES(password));
                }
                else
                {
                    subKey.DeleteValue("username");
                    subKey.DeleteValue("password");
                }

            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            finally
            {
                if (subKey != null)
                {
                    subKey.Close();
                }
                if (rootKey != null)
                {
                    rootKey.Close();
                }
            }
        }

        public static string[] ReadUserNamePassword()
        {
            RegistryKey rootKey = null;
            RegistryKey subKey = null;

            try
            {
                string username;
                string password;
                rootKey = Registry.CurrentUser.OpenSubKey("SOFTWARE", true);
                if (!rootKey.GetSubKeyNames().Contains(RegistryPath))
                {
                    return null;
                }

                subKey = rootKey.OpenSubKey(RegistryPath);

                object objUserName = subKey.GetValue("username");
                object objPassword = subKey.GetValue("password");

                if (objUserName == null || objPassword == null)
                {
                    return null;
                }

                username = DESEncrypt_Client.DecryptDES(objUserName.ToString());
                password = DESEncrypt_Client.DecryptDES(objPassword.ToString());

                string[] values = new string[]{
                    username,
                    password
                };

                return values;
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
                return null;
            }
            finally
            {
                if (subKey != null)
                {
                    subKey.Close();
                }
                if (rootKey != null)
                {
                    rootKey.Close();
                }
            }
        }
        
    }
}
