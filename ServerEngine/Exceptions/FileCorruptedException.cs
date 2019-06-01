using System;

namespace ServerEngine.Exceptions
{
    class FileCorruptedException : Exception
    {
        public string Filename;

        public FileCorruptedException(string Message, string Filename) : base(Message)
        {
            this.Filename = Filename;
        }
    }
}
