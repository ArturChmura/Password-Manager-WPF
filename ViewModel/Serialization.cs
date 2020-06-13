using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_Project;

namespace pwśg_wpf_lab2
{
    static class Serialization
    {
        public static byte[] ObjectToByteArray(object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public static object ByteArrayToObject(byte[] arrBytes)
        {
            using (var memStream = new MemoryStream())
            {
                var binForm = new BinaryFormatter();
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                return binForm.Deserialize(memStream); 
            }
        }

        public static bool Serialize(object obj,string password)
        {
         
            byte[] encryptedByteData = DataEncryption.Encrypt(password, ObjectToByteArray(obj));

            FileStream fs = new FileStream("Passwords.bin", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, encryptedByteData);
            }
            catch (SerializationException e)
            {
                MessageBox.Show("Failed to serialize. Reason: " + e.Message);
                return false;
            }
            finally
            {
                fs.Close();
            }
            return true;
        }
        public static bool Deserialize(string password,out object obj)
        {

            obj = null;

            FileStream fs = new FileStream("Passwords.bin", FileMode.Open);
            byte[] encryptedData;
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                encryptedData = (byte[])formatter.Deserialize(fs);
            }
            catch (SerializationException e)
            {
                MessageBox.Show("Failed to deserialize. Reason: " + e.Message);
                return false;
            }
            finally 
            { 
                fs.Close();
            }
            byte[] decryptedData  = DataEncryption.Decrypt(password, encryptedData);
            if(decryptedData == null)
            {
                return false;
            }
            obj  = ByteArrayToObject(decryptedData);
            return true;
        }
    }
}
