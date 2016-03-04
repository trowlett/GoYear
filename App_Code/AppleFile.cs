using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ReadFile
{
    class AppleFile
    {
        public static int Rows { get; set; }
        public static int Cols { get; set; }
        private const int maxRows = 200;
        private const int maxCols = 25;
        private static string[,] data = new string[maxRows, maxCols];

        public static string[,] GetAppleCSVFile(string FileName)
        {

            int i;
            int vRow;
            int vCol;
            int mCol;
            char[] cs = null;


            try
            {

                using (StreamReader sr = new StreamReader(FileName))
                {
                    //This is an arbitrary size for this example. 
                    cs = new char[sr.BaseStream.Length];
                    sr.Read(cs, 0, cs.Length);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
                string derp = string.Format("DERP:  The process failed:  {0}", e.ToString());
                throw new InvalidOperationException(derp);
                //                throw;
            }

            vRow = 0;
            mCol = 0;
            vCol = 0;
            for (i = 0; i < cs.Length; i++)
            {
                if (cs[i] == '\n')          // is the char a new line
                {
                    vRow++;
                    if (vCol > mCol) mCol = vCol;
                    vCol = 0;
                    continue;
                }
                string field = GetField(cs, ref i);
                data[vRow, vCol] = field;
                vCol++;
            }
            Rows = vRow;
            Cols = mCol;
            return data;
        }
        protected static string GetField(char[] c, ref int n)
        {
            string r = "";
            if (c[n] == '"')
            {
                r = GetQuotedField(c, ref n);
                return r;
            }
            while ((n < c.Length) && (c[n] != ','))
            {
                if (c[n] == '\r')
                {
                    if (c[n + 1] == '\r') n++;
                    return r;
                }
                r += c[n];
                n++;
            }
            return r;
        }

        protected static string GetQuotedField(char[] c, ref int n)
        {
            n++;
            string rslt = "";
            while (c[n] != '"')
            {
                rslt += c[n];
                n++;
            }
            n++;
            return rslt;
        }


    }
}
