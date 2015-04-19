﻿using System.IO;
using System.Text;

namespace Sloccer.Core
{
    public class FileStringRetriver : IStringRetriever
    {
        private FileInfo _file;

        public FileStringRetriver(FileInfo file)
        {
            _file = file;
        }

        public string GetString()
        {
            var fileSt = new StringBuilder();
            using (var rdr = new StreamReader(_file.FullName))
            {
                while (rdr.Peek() > -1)
                {
                    fileSt.AppendLine(rdr.ReadLine());
                }
            }

            return fileSt.ToString();
        }
    }
}
