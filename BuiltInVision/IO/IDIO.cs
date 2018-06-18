using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalvoScanner.IO
{
    public interface IDIO
    {
        void InitDIO();
        int GetInputCount();                        // Input 개수
        int GetOutputCount();                       // Output 개수
        bool WriteOutBit(int index, uint value);    // Input 개수
        bool ReadOutBit(int index, ref uint value); // Output bit 값 확인
        bool ReadInBit(int index, ref uint value);  // Input bit 값 확인
        void SaveIOMap(string path);
        void LoadIOMap(string path);
    }
}
