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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        private String message="";
        private bool addline(String text)
        {
            bool flag = false;
            int N = DateShare.number;
            String[] name = new String[N + 1];
            for(int i = 1; i <= N; i++)
                name[i] = DateShare.Name[i];
            for(int j = 1; j <= N; j++)
            {
                if(text==name[j])
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }
        private bool insectline(String text1, String text2)
        {
            bool flag = false;
            int N = DateShare.number;
            String[] name = new String[N + 1];
            int k = 0;
            for (int i = 1; i <= N; i++)
                name[i] = DateShare.Name[i];
            for (int j = 1; j <= N; j++)
            {
                if (text1 == name[j] || text2 == name[j])
                    k++;
                if (k == 2)
                {
                    flag = true;
                    break;
                }
            }
            if (flag)
            {
                //inwhatline

               


            }
            return flag;
        }
        private void writer(String text, String str)
        {
            string strpath;
            strpath = Application.StartupPath;
            string path = strpath + "\\inwhatline.txt";
            //FileStream fs = new FileStream(strpath + "\\inwhatline.txt", FileMode.Open);
            StreamWriter wr = new StreamWriter(path, true);
            wr.Write(""+ "\r\n");
            wr.Close();
        }
        public bool IsNumber(string str)
        {
            bool yes = true;
            char ch;
            int t = 0, len;
            len = str.Length;
            if (String.IsNullOrEmpty(str))
                yes = false;
            else
            {
                for (int i = 0; i < len; i++)
                {
                    ch = Char.Parse(str.Substring(i, 1));
                    if (ch == '.')
                    {
                        t++;
                        t++;
                    }
                    if (!Char.IsDigit(ch) && ch != '.')
                    {
                        yes = false;
                        break;
                    }
                }
            }
            if (t > 1)
                yes = false;
            return yes;

        }
        private void Button1_Click(object sender, EventArgs e)//添加站点
        {
            bool flag_stop = false;
            bool flag = false;
            int k = 0;
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("横坐标不能为空");
                return;
            }//横坐标
            else if (textBox2.Text.Trim() == "")
            {
                MessageBox.Show("纵坐标不能为空");
                return;
            }//纵坐标
            else if (textBox7.Text.Trim() == "")
            {
                MessageBox.Show("请填写间隔距离");
                return;
            }//站间距离
            else if (textBox5.Text.Trim() == "")
            {
                MessageBox.Show("必须填写地铁站名");
                return;
            }//地铁站名
            else if (textBox10.Text.Trim() == "")
            {
                MessageBox.Show("必须填写新地铁站名");
                return;
            }//地铁新站
            else {
                String []message_text=new String[4];
                if (IsNumber(textBox1.Text.Trim())&&textBox1.Text.Trim().Length<4)
                    k++;
                else
                {
                    message += "横坐标 ";
                }
                if (IsNumber(textBox2.Text.Trim())&& textBox2.Text.Trim().Length < 4)
                    k++;
                else
                {
                    message += "纵坐标 ";
                }
                if (IsNumber(textBox7.Text.Trim()))
                    k++;
                else
                {
                    message += "距离 ";
                }
                if (k == 3)
                {
                    string strpath;
                    strpath = Application.StartupPath;
                    this.BackgroundImage = Image.FromFile(strpath + "\\back.jpg");
                    FileStream fs = new FileStream(strpath + "\\name.txt", FileMode.Open);
                    StreamReader sr = new StreamReader(fs);
                    string temp;
                    String[] name = new string[DateShare.number + 1];
                    while ((temp = sr.ReadLine()) != null)
                    {
                        string[] names = temp.Split(' ');
                        int a = int.Parse(names[0]);
                        string b = names[1];
                        name[a] = b;
                    }
                    fs.Close();
                    int counter = 0;
                    for (int i = 1; i <= DateShare.number; i++)
                    {
                        if (textBox5.Text.Trim() == name[i])
                            counter++;
                    }
                    if (counter == 1)
                        k++;
                    else
                    {
                        k = 3;
                        message += " 重复";
                    }
                }
                if (k == 4)
                    flag = true;
                float n = 0;
                flag_stop = addline(textBox5.Text.Trim());
                if (flag&& flag_stop) {//写入信息
                    int j = 1;
                    for (j = 1; j <=DateShare.number; j++)
                        if (textBox5.Text.Trim()== DateShare.Name[j])
                            break;
                    string strpath;
                    strpath = Application.StartupPath;
                    FileStream fs = new FileStream(strpath + "\\inwhatline.txt", FileMode.Open);
                    StreamReader sr = new StreamReader(fs);
                    String []inwhatline = new String[DateShare.number + 1];
                    for (int i = 1; i <= DateShare.number; i++)
                        inwhatline[i] = sr.ReadLine();
                    fs.Close();
                    String str_inwhatline = inwhatline[j];

                    String message_name = "\r\n"+(DateShare.number + 1).ToString() + " " + textBox10.Text.Trim();
                    String message_roads = "\r\n"+(DateShare.number + 1).ToString() + " "+j.ToString()+" "+textBox7.Text.Trim();
                    String message_inwhatline = "\r\n"+ str_inwhatline;
                    String message_location = "\r\n" + textBox1.Text.Trim() + " " + textBox2.Text.Trim();
                    String message_temp= "\r\n"+(DateShare.number + 1).ToString() + " "+ textBox10.Text.Trim()+" "+str_inwhatline;
                    strpath = Application.StartupPath;
                    string path = strpath + "\\name.txt";
                    StreamWriter wr = new StreamWriter(path, true);
                    wr.Write(message_name);
                    wr.Close();
                    strpath = Application.StartupPath;
                    path = strpath + "\\inwhatline.txt";
                    wr = new StreamWriter(path, true);
                    wr.Write(message_inwhatline);
                    wr.Close();
                    strpath = Application.StartupPath;
                    path = strpath + "\\location.txt";
                    wr = new StreamWriter(path, true);
                    wr.Write(message_location);
                    wr.Close();
                    strpath = Application.StartupPath;
                    path = strpath + "\\roads.txt";
                    wr = new StreamWriter(path, true);
                    wr.Write(message_roads);
                    wr.Close();
                    strpath = Application.StartupPath;
                    path = strpath + "\\temp.txt";
                    wr = new StreamWriter(path, true);
                    wr.Write(message_temp);
                    wr.Close();
                    MessageBox.Show("添加成功!");
                }
                else
                    MessageBox.Show("添加失败!,"+message+"出错");
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)//创建链接
        {
            bool flag = false;
            bool flag_stop_1_and_2 = false;
            int k = 0;
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("横坐标不能为空");
                return;
            }//横坐标
            else if (textBox2.Text.Trim() == "")
            {
                MessageBox.Show("纵坐标不能为空");
                return;
            }//纵坐标
            else if (textBox4.Text.Trim() == "")
            {
                MessageBox.Show("必须填写站点一名称");
                return;
            }//地铁站名
            else if (textBox6.Text.Trim() == "")
            {
                MessageBox.Show("必须填写站点二名称");
                return;
            }//地铁站名
            else if (textBox8.Text.Trim() == "")
            {
                MessageBox.Show("请填写间隔距离1");
                return;
            }//站间距离
            else if (textBox9.Text.Trim() == "")
            {
                MessageBox.Show("请填写间隔距离2");
                return;
            }//站间距离
            else if (textBox10.Text.Trim() == "")
            {
                MessageBox.Show("必须填写新地铁站名");
                return;
            }//地铁站名
            else
            {
                String[] message_text = new String[4];
                if (IsNumber(textBox1.Text.Trim()) && textBox1.Text.Trim().Length < 4)
                    k++;
                else
                    message += "横坐标 ";

                if (IsNumber(textBox2.Text.Trim()) && textBox2.Text.Trim().Length < 4)
                    k++;
                else
                    message += "纵坐标 ";

                if (IsNumber(textBox8.Text.Trim()))
                    k++;
                else
                    message += "距离1 ";

                if (IsNumber(textBox9.Text.Trim()))
                    k++;
                else
                    message += "距离2 ";
                if (k == 4)
                {
                    string strpath;
                    strpath = Application.StartupPath;
                    this.BackgroundImage = Image.FromFile(strpath + "\\back.jpg");
                    FileStream fs = new FileStream(strpath + "\\name.txt", FileMode.Open);
                    StreamReader sr = new StreamReader(fs);
                    string temp;
                    String[] name = new string[DateShare.number + 1];
                    while ((temp = sr.ReadLine()) != null)
                    {
                        string[] names = temp.Split(' ');
                        int a = int.Parse(names[0]);
                        string b = names[1];
                        name[a] = b;
                    }
                    fs.Close();
                    int counter = 0;
                    for (int i = 1; i <= DateShare.number; i++)
                    {
                        if (textBox10.Text.Trim() == name[i])
                            counter++;
                    }
                    if (counter == 1)
                        k++;
                    else
                    {
                        k = 4;
                        message += " 重复";
                    }
                }
                flag_stop_1_and_2 = insectline(textBox4.Text.Trim(),textBox6.Text.Trim());
                if (k == 5)
                    flag = true;
                if (flag&& flag_stop_1_and_2){
                    int j1 = 1;
                    for (j1 = 1; j1 <= DateShare.number; j1++)
                        if (textBox4.Text.Trim() == DateShare.Name[j1])
                            break;
                    int j2 = 1;
                    for (j2 = 1; j2<= DateShare.number; j2++)
                        if (textBox6.Text.Trim() == DateShare.Name[j2])
                            break;
                    string strpath;
                    strpath = Application.StartupPath;
                    FileStream fs = new FileStream(strpath + "\\inwhatline.txt", FileMode.Open);
                    StreamReader sr = new StreamReader(fs);
                    String[] inwhatline = new String[DateShare.number + 1];
                    for (int i = 1; i <= DateShare.number; i++)
                        inwhatline[i] = sr.ReadLine();
                    fs.Close();
                    String str_inwhatline = inwhatline[j1];
                    
                    String message_name = "\r\n" + (DateShare.number + 1).ToString() + " " + textBox10.Text.Trim();
                    String message_roads_1 = "\r\n" + (DateShare.number + 1).ToString() + " " + j1.ToString() + " " + textBox8.Text.Trim();
                    String message_roads_2 = "\r\n" + (DateShare.number + 1).ToString() + " " + j2.ToString() + " " + textBox9.Text.Trim();
                    String message_inwhatline = "\r\n" + str_inwhatline;
                    String message_location = "\r\n" + textBox1.Text.Trim() + " " + textBox2.Text.Trim();
                    String message_temp = "\r\n" + (DateShare.number + 1).ToString() + " " + textBox10.Text.Trim() + " " + str_inwhatline;

                    strpath = Application.StartupPath;
                    string path = strpath + "\\name.txt";
                    StreamWriter wr = new StreamWriter(path, true);
                    wr.Write(message_name);
                    wr.Close();
                    strpath = Application.StartupPath;
                    path = strpath + "\\inwhatline.txt";
                    wr = new StreamWriter(path, true);
                    wr.Write(message_inwhatline);
                    wr.Close();
                    strpath = Application.StartupPath;
                    path = strpath + "\\location.txt";
                    wr = new StreamWriter(path, true);
                    wr.Write(message_location);
                    wr.Close();
                    strpath = Application.StartupPath;
                    path = strpath + "\\roads.txt";
                    wr = new StreamWriter(path, true);
                    wr.Write(message_roads_1);
                    wr.Write(message_roads_2);
                    wr.Close();
                    strpath = Application.StartupPath;
                    path = strpath + "\\temp.txt";
                    wr = new StreamWriter(path, true);
                    wr.Write(message_temp);
                    wr.Close();

                    MessageBox.Show("添加成功!");
                }
                else
                    MessageBox.Show("添加失败!," + message + "出错");
            }

        }

       
    }
}
