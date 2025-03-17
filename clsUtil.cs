using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockByte
{
    public class clsUtil
    {
        public readonly static string GlobalEncryptionExtension = ".lockbyte";

        public class OnEncryptionCompletedEventArgs : EventArgs
        {
            public string originalfilepath { get; }
            public string encryptedfilepath { get; }
            public string key { get; }

            public OnEncryptionCompletedEventArgs(string originalfilepath, string encryptedfilepath, string key)
            {
                this.originalfilepath = originalfilepath;
                this.encryptedfilepath = encryptedfilepath;
                this.key = key;
            }
        }
        public class OnDecryptionCompletedEventArgs : EventArgs
        {
            public string encryptedfilepath { get; }
            public string decryptedfilepath { get; }
            public string key { get; }

            public OnDecryptionCompletedEventArgs(string encryptedfilepath, string decryptedfilepath, string key)
            {
                this.encryptedfilepath = encryptedfilepath;
                this.decryptedfilepath = decryptedfilepath;
                this.key = key;
            }
        }

        public static bool IsValidExtension(string filepath)
        {
            return Path.GetExtension(filepath) == GlobalEncryptionExtension;
        }

    }
}
