using System.Threading.Tasks;

namespace IO.Contracts
{
    public interface IWriter
    {
       void Write(string message);

       void WriteLine(string message);

       Task WriteAsync(string message);

       Task WriteLineAsync(string message);
    }
}