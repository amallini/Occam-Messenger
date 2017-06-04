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

using System.Data.SqlClient;
using System.Net;
using System.Security.Cryptography;
using System.Windows.Forms.ComponentModel;



using System.Threading;
using System.Data.SQLite;
using System.Diagnostics;

namespace OccamMsgr
{
    public partial class CreateChannel : Form
    {
        static SQLiteConnection dbCon;

        public CreateChannel()
        {
            // управление языком интерфейса
            if (!String.IsNullOrEmpty(Properties.Settings.Default.Language))
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo(Properties.Settings.Default.Language);
                System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.GetCultureInfo(Properties.Settings.Default.Language);
            }
            InitializeComponent();
        }

        public void ExecuteSQL(string sql)
        {
            SQLiteCommand command = new SQLiteCommand(sql, MainForm.dbConnection);
            command.ExecuteNonQuery();
        }



        private void BCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void BClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
        public void ExecuteSQLinAbonentDB(string sql)
        {
            SQLiteCommand command = new SQLiteCommand(sql, dbCon);
            command.ExecuteNonQuery();
        }

        public string GetParameterFromAbonentDBChannelTable(string Field, string ChannelGUID)
        {
            string sql = "select " + Field + " from ChannelTable where ChannelGUID = '" + ChannelGUID + "'";
            SQLiteCommand command = new SQLiteCommand(sql, dbCon);
            SQLiteDataReader reader = command.ExecuteReader();
            string Result = "";
            while (reader.Read())
            {
                Result = reader[0].ToString();
            }
            return Result;
        }



        private void BCreate_Click(object sender, EventArgs e)
        {
            // CFPath must be in any case
            if (TCFPath.Text == "")
            {
                LError.Text = MyResource.EnterCFPath; 
                return;
            }

            BCreate.Enabled = false;
            try
            {
                // определяем какой канал Primary or secondary
                string ActualChannelGUID = ""; // может быть тот который сгенерировали (первичный) или тот, который в прислали - его GUID - это имя общей папки
                string PrimarySecondary = "";
                if (RBInitiator.Checked)
                {
                    PrimarySecondary = "Primary";
                    ActualChannelGUID = LChannelGUID.Text;
                    if (TAbonent1NickName.Text == "")
                    {
                        LError.Text = MyResource.EnterYourNickName; //"Enter your nickname.";
                        return;
                    }
                    if (TAbonent2NickName.Text == "")
                    {
                        LError.Text = MyResource.EnterYourAbonentNickName; //"Enter your abonent's nickname.";
                        return;
                    }
                    string sql = "insert into ChannelTable (ChannelGUID, Abonent1NickName, Abonent2NickName, CFPath, PrimaryOrSecondary, Status) values ('" +
                      ActualChannelGUID + "','" +
                      TAbonent1NickName.Text + "','" + TAbonent2NickName.Text + "', '" + TCFPath.Text + "','" + PrimarySecondary + "','" + MyResource.Away + "')";
                    ExecuteSQL(sql);

                    // теперь нужно скопировать в общую папку комплект файлов для абонента и инвертированную БД
                    CreateDistributive(ActualChannelGUID);
                }
                else
                {
                    PrimarySecondary = "Secondary";
                    ActualChannelGUID = Path.GetFileName(TCFPath.Text);
                    if (TDB3Path.Text == "")
                    {
                        LError.Text = MyResource.EnterDB3Path; 
                        return;
                    }
                    // прочитать из присланного файла db.db3 данные о канале (он там один) и создать новый канал (с новым путем к папке) в основной базе
                    dbCon = new SQLiteConnection("Data Source=" + TDB3Path.Text + ";Version=3;");
                    dbCon.Open();
                    string Abonent1NickName = GetParameterFromAbonentDBChannelTable("Abonent1NickName", ActualChannelGUID);
                    string Abonent2NickName = GetParameterFromAbonentDBChannelTable("Abonent2NickName", ActualChannelGUID);
                    string sql = "insert into ChannelTable (ChannelGUID, CFPath, PrimaryOrSecondary, Abonent1NickName, Abonent2NickName) " +
                                 "values ('" + ActualChannelGUID + "','" + TCFPath.Text + "','Secondary','" + Abonent1NickName + "','" + Abonent2NickName + "')";
                    ExecuteSQL(sql);
                    dbCon.Close();
                }

                LError.Text = MyResource.ChannelCreated; //"Channel created. Press Close button for keys generation.";
                BClose.Enabled = true;
            }
            catch (Exception ex)
            {
                LError.Text = ex.Message;
            }




        }

        void CopyDir(string FromDir, string ToDir)
        {
            Directory.CreateDirectory(ToDir);
            foreach (string s1 in Directory.GetFiles(FromDir))
            {
                string s2 = ToDir + "\\" + Path.GetFileName(s1);
                File.Copy(s1, s2);
            }
            foreach (string s in Directory.GetDirectories(FromDir))
            {
                CopyDir(s, ToDir + "\\" + Path.GetFileName(s));
            }
        }

        public void CreateDistributive(string ChannelGUID)
        {
            string Abonent2Root = TCFPath.Text + "\\OccamMessenger";
            // создать корневую папку 
            Directory.CreateDirectory(Abonent2Root);
            // скопировать файл программы!
            File.Copy("Occam.exe", Abonent2Root + "\\Occam.exe");
            // скопировать БД
            File.Copy("db.db3", Abonent2Root + "\\db.db3");
            // файлы для аудио рядом с программой - в корень канала
            File.Copy("NAudio.xml", Abonent2Root + "\\NAudio.xml");
            File.Copy("NAudio.dll", Abonent2Root + "\\NAudio.dll");
            // кучу служебных файлов и папок нужно скопировать
            CopyDir("Files", Abonent2Root + "\\Files");
            CopyDir("x64", Abonent2Root + "\\x64");
            CopyDir("x86", Abonent2Root + "\\x86");
            CopyDir("ru", Abonent2Root + "\\ru");
            CopyDir("ru-RU", Abonent2Root + "\\ru-RU");

           
            File.Copy("Occam.exe.config", Abonent2Root + "\\Occam.exe.config");
            File.Copy("Occam.pdb", Abonent2Root + "\\Occam.pdb");
            File.Copy("System.Data.SQLite.dll", Abonent2Root + "\\System.Data.SQLite.dll");
            File.Copy("System.Data.SQLite.xml", Abonent2Root + "\\System.Data.SQLite.xml");
            // документация
            File.Copy("Инструкция по работе с мессенджером.docx", Abonent2Root + "\\Инструкция по работе с мессенджером.docx");
        
            // подключиться к базе абонента и сделать там изменения
            // заменить ему Primary на Secondary
            // Subsrcibed = N
            dbCon = new SQLiteConnection("Data Source=" + Abonent2Root + "\\db.db3" + ";Version=3;");
            dbCon.Open();
            
            ExecuteSQLinAbonentDB("delete from ChannelTable where ChannelGUID <> '" + ChannelGUID + "'");
            // инвертировать канал и очистить поле CFPath так как путь будет другим
            ExecuteSQLinAbonentDB("update ChannelTable set PrimaryOrSecondary = 'Secondary', CFPath = '' where ChannelGUID = '" + ChannelGUID + "'");
            dbCon.Close();
        }


        private void CreateChannel_Load(object sender, EventArgs e)
        {
            Guid G = Guid.NewGuid();
            LChannelGUID.Text = G.ToString();
            LError.Text = "";
            Application.DoEvents();
        }

        

        private void BSelectFile2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            if (FBD.ShowDialog() == DialogResult.OK)
            {
                TCFPath.Text = FBD.SelectedPath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog FBD = new OpenFileDialog();
            if (FBD.ShowDialog() == DialogResult.OK)
            {
                TDB3Path.Text = FBD.FileName;
            }
        }

        private void RBInitiator_CheckedChanged(object sender, EventArgs e)
        {
            if (RBInitiator.Checked)
            {
                PInitiator.Visible = true;
                PAbonent.Visible = false;
            }
            else
            {
                PInitiator.Visible = false;
                PAbonent.Visible = true;
            }

        }
    }
}
