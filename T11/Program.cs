namespace T11;

using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;

class Program
{

    static byte[] EncryptRSA(string text, RSAParameters key)
    {
        RSA rsa = RSA.Create();
        rsa.ImportParameters(key);
        byte[] bytes = rsa.Encrypt(Encoding.UTF8.GetBytes(text), RSAEncryptionPadding.OaepSHA512);
        return bytes;
    }

    static string DecryptRSA(byte[] enc, RSAParameters key)
    {
        RSA rsa = RSA.Create();
        rsa.ImportParameters(key);
        byte[] bytes = rsa.Decrypt(enc, RSAEncryptionPadding.OaepSHA512);
        return Encoding.UTF8.GetString(bytes);
    }
    static void Main(string[] args)
    {
        try
        {
            UserManager um = new UserManager();
            um.Register("longnn", "123");
            //um.Update();
            //User user = um.Login("hoangnm", "123");
            //if (user != null)
            //{
            //    Console.WriteLine("Login success");
            //    Console.WriteLine(user.Id);
            //}
            //else
            //{
            //    Console.WriteLine("Login fail");
            //}


        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        











        //string plainText = "Hello World!";
        //RSA rsa = RSA.Create();
        //RSAParameters rsaKeys = rsa.ExportParameters(true);

        //byte[] bytes = EncryptRSA(plainText, rsaKeys);
        //Console.WriteLine(Convert.ToHexString(bytes));

        //string text = DecryptRSA(bytes, rsaKeys);
        //Console.WriteLine(text);




        //// 1. Tao Secret Key - IV
        //DES des = DES.Create();
        //byte[] key = des.Key;
        //byte[] iv = des.IV;

        //// 2 . Encrytp
        //byte[] encBytes = Encrypt(plainText, key, iv);

        //// 3. Decrypt
        //string decText = Decrypt(encBytes, key, iv);

        //Console.WriteLine(decText);
        //if (decText == plainText)
        //{
        //    Console.WriteLine("OK");
        //}

    }

    private static string Decrypt(byte[] encBytes, byte[] key, byte[] iv)
    {
        try
        {
            byte[] decrypted = new byte[encBytes.Length];
            int offset = 0;

            DES des = DES.Create();
            ICryptoTransform cryptoTransform = des.CreateEncryptor(key, iv);

            MemoryStream memory = new MemoryStream();
            
            using (var cStream = new CryptoStream(memory, cryptoTransform, CryptoStreamMode.Read))
            {
                // Keep reading from the CryptoStream until it finishes (returns 0). 
                int read = 1;
                while (read > 0)
                {
                    read = cStream.Read(decrypted, offset, decrypted.Length - offset);
                    offset += read;
                }
            }
            return Encoding.UTF8.GetString(decrypted, 0, offset);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return "";
    }

    static byte[] Encrypt(string text, byte[] key, byte[] iv)
    {
        try
        {
            DES des = DES.Create();
            ICryptoTransform cryptoTransform = des.CreateEncryptor(key, iv);

            MemoryStream memory = new MemoryStream();
            CryptoStream stream = new CryptoStream(memory, cryptoTransform, CryptoStreamMode.Write);


            byte[] plainData = Encoding.UTF8.GetBytes(text);
            stream.Write(plainData, 0, plainData.Length);
            byte[] bytes = memory.ToArray();
            return bytes;

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return new byte[0];
    }
}

