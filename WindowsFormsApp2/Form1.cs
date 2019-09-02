using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
      
        
            private int setstart;
            private double minlen;
            private double[] dist;
            private int start;
            private int goal;
            private int[] hash;
            private int[] road;
            private int[] permlist;
            private int[] inwhatline;
            private string resual;
            private int N = 149;
            private string[] name;
            private double[,] mmap;
            private double[,] mmin;
            private int[,] pre;
            private double inf = 1e100;
            private double eps = 1e-9;
            private int counter = 0;
            private CheckBox[] ckbox = new CheckBox[200];
        
        private void read_location(Graphics grp)
        {
            string strpath;
            strpath = Application.StartupPath;
            FileStream fs = new FileStream(strpath + "\\location.txt", FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            string temp;
            int[,] p = new int[200, 2];
            int i = 0;
            while ((temp = sr.ReadLine()) != null)
            {
                string[] names = temp.Split(' ');
                p[i, 0] = int.Parse(names[0]);
                p[i, 1] = int.Parse(names[1]);
                i++;
            }
            fs.Close();

            fs = new FileStream(strpath + "\\name.txt", FileMode.Open);
            sr = new StreamReader(fs);
            string name_temp;
            string []name_arr_temp = new string[N + 1];
            while ((name_temp = sr.ReadLine()) != null)
            {
                string[] names = name_temp.Split(' ');
                int a = int.Parse(names[0]);
                string b = names[1];
                name_arr_temp[a] = b;
            }
            fs.Close();

            fs = new FileStream(strpath + "\\roads.txt", FileMode.Open);
            sr = new StreamReader(fs);
            string roads_temp;
            int[,] roads_arr = new int[200, 2];
            int road_i = 0;
            while ((roads_temp = sr.ReadLine()) != null)
            {
                string[] names = roads_temp.Split(' ');
                roads_arr[road_i, 0] = int.Parse(names[0]);
                roads_arr[road_i, 1] = int.Parse(names[1]);
                road_i++;
            }
            fs.Close();


            //string name = "CheckBox";
            N = i;
            for (int j = 0; j < N; j++)
            {
                DateShare.location[j, 1] = p[j, 1];
                DateShare.location[j, 0] = p[j, 0];
            }
            DateShare.number = N;
            for (int j = 0; j < i; j++)
            {
                if (j < 149)
                {
                    ckbox[j] = new CheckBox();
                    ckbox[j].Name = "checkBox" + i.ToString();
                    ckbox[j].Text = "";
                    ckbox[j].Location = new Point(p[j, 0], p[j, 1]);
                    ckbox[j].Size = new System.Drawing.Size(15, 14);
                    ckbox[j].TabIndex = 140;
                    ckbox[j].UseVisualStyleBackColor = true;
                    ckbox[j].Checked = false;
                    this.Controls.Add(ckbox[j]);

                }
                else{
                    ckbox[j] = new CheckBox();
                    ckbox[j].Name = "checkBox" + i.ToString();
                    ckbox[j].Text = "";
                    ckbox[j].Location = new Point(p[j, 0], p[j, 1]);
                    ckbox[j].Size = new System.Drawing.Size(15, 14);
                    ckbox[j].TabIndex = 140;
                    ckbox[j].UseVisualStyleBackColor = true;
                    ckbox[j].Checked = false;
                    
                    this.Controls.Add(ckbox[j]);
                    
                   
                    Font drawFont = new Font("宋体", 8);
                    //Graphics grp = CreateGraphics();
                    //绘制线条 起始窗口坐标10,50 终止坐标20,350 红色，2px宽
                    grp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    for(int i_line = 0; i_line < 200; i_line++) {
                        if (roads_arr[i_line, 0] == j+1){
                            grp.DrawLine(new Pen(Color.Red, 2), new Point(p[roads_arr[i_line, 1]-1, 0], p[roads_arr[i_line, 1]-1, 1]), new Point(p[roads_arr[i_line, 0]-1, 0], p[roads_arr[i_line, 0]-1, 1]));
                            
                        }
                    }
                   
                    grp.DrawString(name_arr_temp[j+1], drawFont, Brushes.Blue, p[j, 0]+20, p[j, 1]);
                }
                //ckbox.Click += new EventHandler(CheckBoxClick);
                //ckbox = (CheckBox)this.Controls["CheckBox" + j.ToString()];
                //ckbox.CheckedChanged += click;
                //调用添加文本控件的方法
                //AddTxt(ckbox);*/
            }
        }

        #region 全排列
        private void dummy(int[] a, int n)
        {
            double[] tempdist = new double[15];
            double sum = mmin[road[0], road[a[0]]];
            tempdist[0] = sum;
            for (int i = 0; i < n - 1; i++)
            {
                tempdist[i + 1] = mmin[road[a[i]], road[a[i + 1]]];
                sum += mmin[road[a[i]], road[a[i + 1]]];
            }
            sum += mmin[road[a[n - 1]], road[0]];
            if (minlen - sum > eps)
            {
                minlen = sum;
                for (int i = 0; i < n; i++)
                {
                    dist[i] = tempdist[i];
                    permlist[i] = a[i];
                }
            }
            else if (Math.Abs(minlen - sum) < eps)
            {
                for (int i = 0; i < n; i++)
                {
                    if (Math.Abs(dist[i] - tempdist[i]) < eps)
                        continue;
                    if (dist[i] - tempdist[i] < -eps)
                        break;
                    else
                    {
                        for (int j = 0; j < n; j++)
                        {
                            dist[j] = tempdist[j];
                            permlist[j] = a[j];
                        }
                        break;
                    }
                }
            }
        }
        private void _gen_perm_swap(int[] a, int n, int l, int[] pos, int[] dir)
        {
            int i, p1, p2, t;
            if (l == n)
                dummy(a, n);
            else
            {
                _gen_perm_swap(a, n, l + 1, pos, dir);
                for (i = 0; i < l; i++)
                {
                    p2 = (p1 = pos[l]) + dir[l];
                    t = a[p1];
                    a[p1] = a[p2];
                    a[p2] = t;
                    pos[a[p1] - 1] = p1;
                    pos[a[p2] - 1] = p2;
                    _gen_perm_swap(a, n, l + 1, pos, dir);
                }
                dir[l] = -dir[l];
            }
        }
        private void gen_perm_swap(int n)
        {
            int[] a = new int[12];
            int[] pos = new int[12];
            int[] dir = new int[12];
            int i;
            for (i = 0; i < n; i++)
            {
                a[i] = i + 1;
                pos[i] = i;
                dir[i] = -1;
            }
            _gen_perm_swap(a, n, 0, pos, dir);
        }
        #endregion
        private void click(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            if (cb.Checked)
            {
                hash[int.Parse(cb.Name.Substring(8))] = 1;
            }
            else
                hash[int.Parse(cb.Name.Substring(8))] = 0;

            /*for(int i = 0; i < N; i++)
         {
             if(ckbox[i].Checked==true)
                 hash[int.Parse(ckbox[i].Name.Substring(8))] = 1;
             else
                 hash[int.Parse(ckbox[i].Name.Substring(8))] = 0;
         }*/

        }
        private void button1_Click(object sender, EventArgs e)//查询
        {
            if (setstart == 0)
            {
                DialogResult dr = MessageBox.Show("请先选择起点站!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            dist = new double[15];
            permlist = new int[15];
            int k = 1;
            if (k == 1)
            {
                for (int i = 0; i < N; i++)
                {
                    if (ckbox[i].Checked && i != start)
                    {
                        road[k++] = i + 1;
                        goal = i;
                        break;
                    }
                }
            }
            if (k == 1)
            {
                DialogResult dr = MessageBox.Show("请选择1个到达站!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            minlen = inf;
            gen_perm_swap(k - 1);
            resual = "起点站: " + name[road[0]] + "\r\n\r\n";
           
            display(road[0], road[permlist[0]]);

            minlen = minlen / 2;
            resual += "总路程: " + minlen.ToString() + "km";
            Form2 frm = new Form2();
            frm.textBox1.Text = resual;
            frm.ShowDialog();
            for (int i = 0; i < N; i++)
            {
                ckbox[i].Checked = false;
            }

        }
        private void floyd()
        {
            mmin = new double[N + 1, N + 1];
            pre = new int[N + 1, N + 1];
            int i, j, k;
            for (i = 1; i <= N; i++)
            {
                for (j = 1; j <= N; j++)
                {
                    mmin[i, j] = mmap[i, j];
                    pre[i, j] = i;
                    if (i == j) pre[i, j] = -1;
                }
            }
            for (k = 1; k <= N; k++)
                for (i = 1; i <= N; i++)
                    for (j = 1; j <= N; j++)
                        if (mmin[i, k] + mmin[k, j] < mmin[i, j])
                        {
                            mmin[i, j] = mmin[i, k] + mmin[k, j];
                            pre[i, j] = pre[k, j];
                        }
        }
        private void readinfo(){
            string strpath;
            strpath = Application.StartupPath;
            this.BackgroundImage = Image.FromFile(strpath + "\\back.jpg");
           
            FileStream fs = new FileStream(strpath + "\\name.txt", FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            


            string temp1;
            int[,] p = new int[200, 2];
            int n_counter = 0;
            while ((temp1 = sr.ReadLine()) != null)
            {
                string[] names = temp1.Split(' ');
                n_counter++;
            }
            fs.Close();
            N = n_counter;

            fs = new FileStream(strpath + "\\name.txt", FileMode.Open);
            sr = new StreamReader(fs);
            string temp;
            name = new string[N + 1];
            while ((temp = sr.ReadLine()) != null)
            {
                string[] names = temp.Split(' ');
                int a = int.Parse(names[0]);
                string b = names[1];
                name[a] = b;
            }
            fs.Close();

            fs = new FileStream(strpath + "\\roads.txt", FileMode.Open);
            sr = new StreamReader(fs);
            mmap = new double[N + 1, N + 1];
            hash = new int[N + 1];
            setstart = 0;
            for (int i = 1; i <= N; i++)
                for (int j = 1; j <= N; j++)
                {
                    mmap[i, j] = inf;
                    if (i == j) mmap[i, j] = 0;
                }
            while ((temp = sr.ReadLine()) != null)
            {
                string[] names = temp.Split(' ');
                int a = int.Parse(names[0]);
                int b = int.Parse(names[1]);
                double c = double.Parse(names[2]);
                mmap[a, b] = c;
                mmap[b, a] = c;
            }
            fs.Close();
            inwhatline = new int[N + 1];
            fs = new FileStream(strpath + "\\inwhatline.txt", FileMode.Open);
            sr = new StreamReader(fs);
            for (int i = 1; i <= N; i++)
                inwhatline[i] = int.Parse(sr.ReadLine());
            fs.Close();
        }
        private void getline(int x)
        {
            int pow = 1;
            int flag = 0;
            while (x != 0)
            {
                if ((x & 1) != 0)
                {
                    if (flag == 1) resual += ",";
                    else flag = 1;
                    resual += (pow * (x & 1)).ToString();
                }
                x >>= 1;
                pow++;
            }
            resual += "号线: ";
        }
        private void display(int start, int end){
            resual += name[start] + " --------> " + name[end] + "\r\n";
            int k = 1;
            int[] listtemp = new int[N + 1];
            int[] list = new int[N + 1];
            listtemp[0] = end;
            while (pre[start, end] != -1)
            {
                listtemp[k] = pre[start, end];
                end = listtemp[k];
                k++;
            }
            for (int j = 1; j <= N; j++)
                DateShare.Name[j] = name[j];
            int len = k;
            k--;
            counter = len;
            for (int i = 0; k >= 0; i++, k--)
                list[i] = listtemp[k];
            for (int j = 0; j < len; j++)
                DateShare.List[j] = list[j];
            DateShare.Couter = len;
            int now = list[0];
            int nowline = inwhatline[now] & inwhatline[list[1]];
            for (int i = 2; i < len; i++)
            {
                int comline1 = inwhatline[list[i]] & inwhatline[list[i - 1]];
                int comline = comline1 & nowline;
                //resual += name[list[i]];
                if (comline == 0)
                {
                    getline(nowline);
                    //resual += name[j]; //+ "----" + name[list[i - 1]] DateShare.List[i-1]
                    resual +=name[now] + "----" + name[DateShare.List[i - 1]];
                    resual += " " + mmin[now, DateShare.List[i - 1]].ToString() + "km";
                    now = DateShare.List[i - 1];
                    nowline = comline1;
                    resual += "\r\n";
                }
                else
                    nowline = comline;
            }
            getline(nowline & inwhatline[DateShare.List[len - 1]]);
            resual += name[now] + "----" + name[DateShare.List[len - 1]];
            resual += " " + mmin[now, DateShare.List[len - 1]].ToString() + "km";
            resual += "\r\n\r\n";
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeComponent();
            //Graphics grp = CreateGraphics();
            //read_location(grp);
            
            readinfo();
            floyd();
        }
        private void button3_Click(object sender, EventArgs e)//清除
        {
            hash = new int[N + 1];
            setstart = 0;
            for (int i = 0; i < N; i++)
            {
                ckbox[i].Checked = false;
            }
        }
        private void button2_Click(object sender, EventArgs e)//设为起点站
        {
            road = new int[15];
            int k = 1;
            for (int i = 0; i < N; i++)
            {
                if (ckbox[i].Checked == true)
                {
                    hash[int.Parse(ckbox[i].Name.Substring(8))] = 1;
                    k++;
                    start = i;
                    road[0] = i + 1;
                    break;
                }
                else
                {
                    hash[int.Parse(ckbox[i].Name.Substring(8))] = 0;
                }
            }
            if (k != 2)
            {
                MessageBox.Show("请选择1个起点站!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            MessageBox.Show("已将 \"" + name[road[0]] + "\" 设为起点站!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            hash[road[0]] = 0;
            setstart = 1;
            
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Form3 frm = new Form3();
            for (int j = 1; j <= N; j++)
                DateShare.Name[j] = name[j];
            frm.ShowDialog();
        }

        private void Button5_Click(object sender, EventArgs e) {
            Graphics grp = CreateGraphics();
            read_location(grp);
            for (int i = 0; i < N; i++)
            {
                ckbox[i].CheckedChanged += click;
            }
        }
    }
}
