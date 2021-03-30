using System;
using System.IO;

namespace EngineLib.Core
{
    public class Screenshot
    {
        private readonly string base64String = string.Empty;

        private readonly byte[] byteArray;


        internal Screenshot(byte[] array)
        {
            if (array == null || array.Length == 0)
            {
                throw new ArgumentException("Cannot be null or empty", "array");
            }

            this.byteArray = array;
            this.base64String = Convert.ToBase64String(this.byteArray);
        }


        public string AsBase64String()
        {
            return this.base64String;
        }


        public byte[] AsByteArray()
        {
            return this.byteArray;
        }


        public void SaveAsFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("Cannot be null or empty", "filePath");
            }

            var dir = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(dir))
            {
                Directory.CreateDirectory(dir);
            }

            using (var stream = File.OpenWrite(filePath))
            {
                stream.Write(this.byteArray, 0, this.byteArray.Length);
            }
        }

        public override string ToString()
        {
            return this.base64String;
        }
    }
}