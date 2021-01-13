using System;
using System.Security.Cryptography;
using System.Text;
public class Encrypter
{

    public static string EncryptData(string data)
    {

        SHA256 hash = SHA256.Create();

        byte[] bytes = hash.ComputeHash(Encoding.UTF8.GetBytes(data));

        StringBuilder encryptedData = new StringBuilder();

        foreach (byte b in bytes)
        {

            encryptedData.Append(b.ToString("x2"));

        }

        return encryptedData.ToString();

    }
    
}