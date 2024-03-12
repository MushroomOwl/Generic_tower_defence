using System.Collections.Generic;

namespace TD
{
    public interface IFileManager
    {
        void WriteFile(in string data, string filename);
        void ReadFile(out string data, string filename);
        List<string> ListFiles();
    }
}
