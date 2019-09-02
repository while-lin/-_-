using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    class DateShare
    {
        static private int[] list = new int[50];
        static private int n;
        static private String[] name = new String[200];
        static private int[,] p = new int[200, 2];
        static private int N;
        public static int[] List
        {
            set { list = value; }
            get { return list; }
        }
        public static int Couter
        {
            set { n = value; }
            get { return n; }
        }
        public static int[,] location
        {
            set { p = value; }
            get { return p ; }
        }
        public static int number
        {
            set { N = value; }
            get { return N; }
        }
        public static String[] Name
        {
            set { name = value; }
            get { return name; }
        }
    }
}