using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
           
        }
        String[] name = new String[200];
        int[] list = new int[50];
        int[,] p = new int[200, 2];
        private void Display()
        {

        }
        private void Form4_Load(object sender, EventArgs e)
        {
            for (int i = 1; i <= DateShare.number; i++)
                name[i] = DateShare.Name[i];
            for (int i = 0; i < DateShare.Couter; i++)
                list[i] = DateShare.List[i];
            for (int i = 0; i <= DateShare.number; i++)
            {
                p[i, 0] = DateShare.location[i, 0];
                p[i, 1] = DateShare.location[i, 1];
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Font drawFont = new Font("宋体", 8);
            Graphics grp = CreateGraphics();
            //绘制线条 起始窗口坐标10,50 终止坐标20,350 红色，2px宽
            grp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            for (int i = 0; i < DateShare.Couter - 1; i++)
            {
                if (i % 3 == 0)
                {
                    grp.DrawLine(new Pen(Color.Red, 2), new Point(p[DateShare.List[i] - 1, 0], p[DateShare.List[i] - 1, 1]), new Point(p[DateShare.List[i + 1] - 1, 0], p[DateShare.List[i + 1] - 1, 1]));
                    grp.DrawString(DateShare.Name[DateShare.List[i]], drawFont, Brushes.Blue, p[DateShare.List[i] - 1, 0], p[DateShare.List[i] - 1, 1]);
                }
                else if (i % 3 == 1)
                {
                    grp.DrawLine(new Pen(Color.Green, 2), new Point(p[DateShare.List[i] - 1, 0], p[DateShare.List[i] - 1, 1]), new Point(p[DateShare.List[i + 1] - 1, 0], p[DateShare.List[i + 1] - 1, 1]));
                    grp.DrawString(DateShare.Name[DateShare.List[i]], drawFont, Brushes.Red, p[DateShare.List[i] - 1, 0], p[DateShare.List[i] - 1, 1]);
                }
                else if (i % 3 == 2)
                {
                    grp.DrawLine(new Pen(Color.Blue, 2), new Point(p[DateShare.List[i] - 1, 0], p[DateShare.List[i] - 1, 1]), new Point(p[DateShare.List[i + 1] - 1, 0], p[DateShare.List[i + 1] - 1, 1]));
                    grp.DrawString(DateShare.Name[DateShare.List[i]], drawFont, Brushes.Green, p[DateShare.List[i] - 1, 0], p[DateShare.List[i] - 1, 1]);
                }
            }
            grp.DrawString(DateShare.Name[DateShare.List[DateShare.Couter - 1]], drawFont, Brushes.Blue, p[DateShare.List[DateShare.Couter - 1] - 1, 0], p[DateShare.List[DateShare.Couter - 1] - 1, 1]);
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
