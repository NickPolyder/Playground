using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using Newtonsoft.Json;
using Playground.NETCORE.Models;

namespace Playground.NETCORE.Tests.Json
{
    public class JsonTests : ITestCase
    {
        public bool Enabled { get; } = true;
        public string Name { get; } = "Json Test Case";
        public void Run()
        {
            Console.WriteLine();

            var jsonString = _createJsonString();
            Console.WriteLine($"Default Json String:\n {jsonString}\n\n");

            var bytesFromString = _createBytesFromString(jsonString);
            Console.WriteLine($"Bytes from string:\n {GetStringFromByte(bytesFromString)}\n\n");

            var base64Str = _CreateBase64FromBytes(bytesFromString);
            Console.WriteLine($"Base64 string:\n {base64Str}\n\n");

            var compressed = GetZippedStr(bytesFromString);
            Console.WriteLine($"Compressed string:\n {compressed}\n\n");

            var uncompressedJson = _createStringFromBytes(_CreateFromBase64(GetUnZippedStr(_CreateFromBase64(compressed))));
            Console.WriteLine($"Uncompressed Json string:\n {uncompressedJson}\n\n");

            Console.WriteLine($"Json                   String Length: {jsonString.Length}\n" +
                              $"Base64                 String Length: {base64Str.Length} \n" +
                              $"Compressed             String Length: {compressed?.Length} \n" +
                              $"Uncompressed Json      String Length: {uncompressedJson.Length}\n");

        }

        private string _createJsonString()
        {
            var list = new List<Contact>();
            for (var i = 0; i < 10; i++)
            {
                list.Add(new Contact
                {
                    FullName = $"Full Name {i}",
                    Phone = $"695{i}"
                });
            }
            return JsonConvert.SerializeObject(list);
        }

        private byte[] _createBytesFromString(string str)
        {
            return Encoding.Default.GetBytes(str);
        }

        private string _createStringFromBytes(byte[] bytes)
        {
            return Encoding.Default.GetString(bytes);
        }
        private string _CreateBase64FromBytes(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }


        private byte[] _CreateFromBase64(string str)
        {
            return Convert.FromBase64String(str);
        }

        private string GetStringFromByte(byte[] bytes)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var bt in bytes)
            {
                builder.Append($"{bt}, ");
            }
            return builder.ToString();
        }

        private string GetZippedStr(byte[] bytes)
        {
            using (var memory = new MemoryStream())
            {
                using (var gzip = new GZipStream(memory, CompressionMode.Compress))
                {
                    gzip.Write(bytes, 0, bytes.Length);
                }

                return _CreateBase64FromBytes(memory.ToArray());
            }
        }

        private string GetUnZippedStr(byte[] bytes)
        {
            using (var decompresionStream = new MemoryStream())
            {
                using (var memory = new MemoryStream(bytes))
                {
                    using (var gzip = new GZipStream(memory, CompressionMode.Decompress))
                    {
                        gzip.CopyTo(decompresionStream);
                        return _CreateBase64FromBytes(decompresionStream.ToArray());
                    }

                }
            }
        }
    }
}
