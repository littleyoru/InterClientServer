using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace InterClientServer
{
    internal class EncryptionV
    {
        string key1 = "firefly";
        //string key2 = "";

        // hash key
        RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();


        protected internal byte[] EncryptV(byte[] text)
        {
            var encryptedMessage = new byte[text.Length];
            var key1Repeated = key1;

            // repeat key if text is longer
            int j = 0;
            while (text.Length > key1Repeated.Length)
            {
                if (j >= key1.Length) j = 0;
                key1Repeated += key1[j];
                j++;
            }
            Console.WriteLine("test key repeated: " + key1Repeated);
            var byteKey = Encoding.UTF8.GetBytes(key1Repeated);

            for (int i = 0; i < text.Length; i++)
            {
                //Console.WriteLine("text[i], byteKey[i]: " + text[i] + "," + byteKey[i]);
                var calc = (text[i] + byteKey[i]) % 256;
                //Console.WriteLine("calc " + (text[i] + byteKey[i]) + "," + calc);
                encryptedMessage[i] = (byte)calc;
            }

            return encryptedMessage;
        }

        protected internal byte[] DecryptV(byte[] encryptedText)
        {
            var message = new byte[encryptedText.Length];
            var key1Repeated = key1;

            // repeat key if text is longer
            int j = 0;
            while (encryptedText.Length > key1Repeated.Length)
            {
                if (j >= key1.Length) j = 0;
                key1Repeated += key1[j];
                j++;
            }
            Console.WriteLine("test key repeated: " + key1Repeated);
            var byteKey = Encoding.UTF8.GetBytes(key1Repeated);

            for (int i = 0; i < encryptedText.Length; i++)
            {
                var calc = (encryptedText[i] - key1Repeated[i]) % 256;
                message[i] = (byte)calc;
            }

            return message;
        }
    }
}
