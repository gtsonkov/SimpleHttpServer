using IO.Contracts;
using System;
using System.Threading.Tasks;

namespace IO
{
    /// <summary>
    /// Using to make logging from server process, in this case write it on the console.
    /// </summary>
    public class Logger : IWriter
    {
        public void Write(string message)
        {
            Console.Write(message);
        }

        public Task WriteAsync(string message)
        {
            Console.Write(message);
            return Task.CompletedTask;
        }

        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        public Task WriteLineAsync(string message)
        {
            Console.WriteLine(message);
            return Task.CompletedTask;
        }
    }
}