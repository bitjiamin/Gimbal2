using System;
using System.Text;
using System.IO;

namespace Gimbal
{
    public class TxtHelper
    {
        public void WriteText(string Path, string Data)
        {
            string[] pathArr = Path.Split(new char[] { '\\' });
            if (pathArr.Length > 1)
            {
                string strPath = pathArr[0];
                for (int i = 1; i < pathArr.Length - 1; i++)
                {
                    strPath = strPath + "\\" + pathArr[i];
                    if (!Directory.Exists(strPath))
                    {
                        Directory.CreateDirectory(strPath);
                    }
                }
                if (!File.Exists(Path))
                {
                    FileStream fs = new FileStream(Path, FileMode.Create, FileAccess.Write);
                    fs.Close();
                }
            }
            StreamWriter sw = new StreamWriter(Path, true, System.Text.Encoding.ASCII);
            sw.WriteLine(Data);
            sw.Close();
        }
    }
}
