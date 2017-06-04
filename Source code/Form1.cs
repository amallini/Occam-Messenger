using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Windows.Forms.ComponentModel;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Security.Cryptography;
using System.Globalization;

// Voice
using NAudio.Wave;
using NAudio.FileFormats;
using NAudio.CoreAudioApi;
using NAudio;
using System.Media;
using System.Diagnostics;
using System.Collections.Specialized;



namespace OccamMsgr
{
    public partial class MainForm : Form
    {
        // Globals
        public static string CurrentDirectory = "";
        public static bool AudioDeviceExists = false;
        public static int PNumber = 0;                  // Number of message from Primary abonent
        public static int SNumber = 0;                  // Number of message from Secondary abonent

        // Forms
        static CreateChannel fCreateChannel = null;     
        static CFPath fCFPath = null; // CF = Common Folder

        // SQLite
        public static SQLiteConnection dbConnection;

        // Voice
        WaveIn waveIn;
        WaveFileWriter writer;
        string outputFilename = "";
        SoundPlayer sp;
        int VoiceNumber = 0;

        // Form
        public MainForm()
        {
            // Language
            if (!String.IsNullOrEmpty(Properties.Settings.Default.Language))
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo(Properties.Settings.Default.Language);
                System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.GetCultureInfo(Properties.Settings.Default.Language);
            }

            InitializeComponent();

            // Create forms
            fCreateChannel = new CreateChannel();
            fCFPath = new CFPath();

            // Create Sound Player
            sp = new SoundPlayer();

        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            LInfo1.Text = MyResource.Loading; //"Loading...";
            Application.DoEvents();

            // Get current language
            CBLanguage.DataSource = new System.Globalization.CultureInfo[]
            {
                System.Globalization.CultureInfo.GetCultureInfo("ru-RU"),
                System.Globalization.CultureInfo.GetCultureInfo("en-US")
            };
            CBLanguage.DisplayMember = "NativeName";
            CBLanguage.ValueMember = "Name";
            if (!String.IsNullOrEmpty(Properties.Settings.Default.Language))
            {
                CBLanguage.SelectedValue = Properties.Settings.Default.Language;
            }

            CurrentDirectory = Directory.GetCurrentDirectory();
            dbConnection = new SQLiteConnection("Data Source=db.db3;Version=3;");
            dbConnection.Open();
            // Voice devices
            if (WaveIn.DeviceCount > 0)
                AudioDeviceExists = true;
            else
                LVoice.Text = MyResource.NoAudioDevice;

            // Status reset
            string sql = "update ChannelTable set Status = '" + MyResource.Away + "'";
            ExecuteSQL(sql);

            // Show channels
            GetChannels();

            LInfo1.Text = "";
            Application.DoEvents();

            // Tips for buttons
            ToolTip toolTip = new ToolTip();
            toolTip.Active = true;
            toolTip.SetToolTip(BAttachFile, MyResource.AttachFile);
            toolTip.IsBalloon = true;

            ToolTip toolTip2 = new ToolTip();
            toolTip2.Active = true;
            toolTip2.SetToolTip(BAdd, MyResource.AddChannel);
            toolTip2.IsBalloon = true;

            ToolTip toolTip3 = new ToolTip();
            toolTip3.Active = true;
            toolTip3.SetToolTip(BDel, MyResource.DelChannel);
            toolTip3.IsBalloon = true;

            ToolTip toolTip4 = new ToolTip();
            toolTip4.Active = true;
            toolTip4.SetToolTip(BStartRecord, MyResource.StartRecord);
            toolTip4.IsBalloon = true;

            ToolTip toolTip5 = new ToolTip();
            toolTip5.Active = true;
            toolTip5.SetToolTip(BStopRecord, MyResource.StopRecord);
            toolTip5.IsBalloon = true;

            // Show Hello template for message
            TEdit.Text = MyResource.Hello; //"Hello!";
            TEdit.Focus();

            BAdd.Visible = true;

            // Send control message "I'm here" to each channel
            bool NeedGetChannels = false;
            foreach (DataGridViewRow row in DGAbonents.Rows)
            {
                string CFPath = row.Cells[1].Value.ToString();
                if (CFPath == "")
                {
                    // if channel has no Common Folder Path then show dialog
                    fCFPath.ShowDialog();
                    if (fCFPath.DialogResult == DialogResult.OK)
                    {
                        CFPath = fCFPath.TCFPath.Text;
                        string ChannelGUID = row.Cells[0].Value.ToString();
                        // store in DB
                        ExecuteSQL("update ChannelTable set CFPath = '" + CFPath + "' where ChannelGUID = '" + ChannelGUID + "'");
                        NeedGetChannels = true;
                    }
                }
                string PrimaryOrSecondary = row.Cells[4].Value.ToString();
                string Body = "Status:On";

                SendTXTToAbonent(Body, CFPath, PrimaryOrSecondary, "C");
            }
            if (NeedGetChannels)
                GetChannels();
            // turn on Timer for receiving messages
            timer1.Enabled = true;
        }
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // close DB connection
            dbConnection.Close();
        }

        // Event Handlers
        private void UpdateButtonState()
        {
            // if you want to send to abonents who away
            BSend.Enabled = true;
            BAttachFile.Enabled = true;
            if (AudioDeviceExists)
                BStartRecord.Enabled = true;
            BStopRecord.Enabled = false;

            // if you want to send only to abonents who here then uncomment code below
            /*
            // if there is at least one abonent with HERE status then enable buttons
            // если среди выбранных абонентов есть хотя бы один HERE то сделать кнопки доступными
            int Count = 0;
            string Status = "";
            foreach (DataGridViewRow row in DGAbonents.SelectedRows)
            {
                Status = row.Cells[3].Value.ToString();
                if (Status == MyResource.Here)//"Here")
                    Count++;
            }
            if (Count > 0)
            {
                BSend.Enabled = true;
                BAttachFile.Enabled = true;
                if (AudioDeviceExists)
                    BStartRecord.Enabled = true;
                BStopRecord.Enabled = false;
            }
            else
            {
                BSend.Enabled = false;
                BStartRecord.Enabled = false;
                BStopRecord.Enabled = false;
                BAttachFile.Enabled = false;
            }
            */
        }
        private void DGAbonents_SelectionChanged(object sender, EventArgs e)
        {
            LAbonentList.Text = "";
            string ChannelGUID = "";
            string NickName = "";
            bool FirstTime = true;
            try
            {
                // show the list of selected abonents in one string
                // в шапке сменить имя абонента (пройти по всем выбранным и их через запятую в шапку)
                foreach (DataGridViewRow row in DGAbonents.SelectedRows)
                {
                    NickName = row.Cells[2].Value.ToString();
                    ChannelGUID = row.Cells[0].Value.ToString();

                    if (FirstTime)
                    {
                        LAbonentList.Text = NickName;
                        FirstTime = false;
                    }
                    else
                    {
                        LAbonentList.Text = LAbonentList.Text + ", " + NickName;
                        // может не поместиться в поле - обрезать
                        if (LAbonentList.Text.Length > 70)
                            LAbonentList.Text = LAbonentList.Text.Substring(0, 70) + "...";
                    }
                }
                
            }
            catch (Exception Ex)
            {
                ShowError(Ex.Message);
            }
            UpdateButtonState();
            TEdit.Focus();
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) 
        {
            // послать служебное сообщение об изменении статуса
            foreach (DataGridViewRow row in DGAbonents.Rows)
            {
                string CFPath = row.Cells[1].Value.ToString();
                string PrimaryOrSecondary = row.Cells[4].Value.ToString();
                string Body = "Status:Off";

                SendTXTToAbonent(Body, CFPath, PrimaryOrSecondary, "C");
            }
            Properties.Settings.Default.Language = CBLanguage.SelectedValue.ToString();
            Properties.Settings.Default.Save();
        }
        private void TEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.Enter)) // if (e.KeyCode == Keys.Enter && e.Control)
            {
                e.SuppressKeyPress = true;
                SendButtonClicked();
            }
        }

        // Buttons
        private void BAdd_Click(object sender, EventArgs e)
        {
            fCreateChannel.TAbonent1NickName.Text = "";
            fCreateChannel.TAbonent2NickName.Text = "";
            fCreateChannel.BCreate.Enabled = true;
            fCreateChannel.BClose.Enabled = false;
            fCreateChannel.ShowDialog();
            if (fCreateChannel.DialogResult == DialogResult.OK)
            {
                GetChannels();
            }
        }
        private void SendButtonClicked()
        {
            LError.Text = "";
            if (TEdit.Text == "")
                return;
            BSend.Enabled = false;
            BSend.Text = MyResource.Sending; 
            Application.DoEvents();
            SendTXT("T", TEdit.Text, "Send", ""); // send to all selected abonents
            Application.DoEvents();
            AddToHistoryStr("Me", TEdit.Text);
            BSend.Text = MyResource.Send;
            BSend.Enabled = true;
        }
        private void BSend_Click(object sender, EventArgs e)
        {
            SendButtonClicked();
        }
        private void BAttachFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            string InFName = openFileDialog1.FileName;
            if (System.IO.File.Exists(InFName))
            {
                SendFile("F", InFName, "", "");  // F = File  V = Voice
                AddToHistoryStr("Me", InFName + " " + MyResource.FileSent);
            }
            else
                ShowError(MyResource.FileNotFound);
        }
        private void BStartRecord_Click(object sender, EventArgs e)
        {
            LRecording.Visible = true;
            BStartRecord.Enabled = false;
            BStopRecord.Enabled = true;
            Application.DoEvents();
            VoiceNumber++;
            outputFilename = "VoiceMessage_" + VoiceNumber.ToString() + ".wav";
            try
            {
                waveIn = new WaveIn();
                //Дефолтное устройство для записи (если оно имеется)
                waveIn.DeviceNumber = 0;
                //Прикрепляем к событию DataAvailable обработчик, возникающий при наличии записываемых данных
                waveIn.DataAvailable += waveIn_DataAvailable;
                //Прикрепляем обработчик завершения записи
                waveIn.RecordingStopped += new EventHandler<NAudio.Wave.StoppedEventArgs>(waveIn_RecordingStopped);
                //Формат wav-файла - принимает параметры - частоту дискретизации и количество каналов(здесь mono)
                waveIn.WaveFormat = new WaveFormat(8000, 1);
                //Инициализируем объект WaveFileWriter
                writer = new WaveFileWriter(outputFilename, waveIn.WaveFormat);
                //Начало записи
                waveIn.StartRecording();
            }
            catch (Exception ex)
            { ShowError(ex.Message); }

        }
        private void BStopRecord_Click(object sender, EventArgs e) 
        {
            LRecording.Visible = false;
            Application.DoEvents();
            if (waveIn != null)
            {
                waveIn.StopRecording();
                // имеем файл outputFilename
                // вставить в свой! RTF 
                // файл в outputFilename
                // отослать автоматом с пометкой V
                SendFile("V", outputFilename, "", ""); // Voice - чтобы на другом конце проиграть автоматом
                File.Delete(outputFilename);

                AddToHistoryStr("Me", outputFilename + " " + MyResource.AudioHasBeenSent);
            }
            BStartRecord.Enabled = true;
            BStopRecord.Enabled = false;
        }
        private void BDel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Delete channel", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string ChannelGUID = DGAbonents.SelectedRows[0].Cells[0].Value.ToString();
                ExecuteSQL("delete from ChannelTable where ChannelGUID = '" + ChannelGUID + "'");
                GetChannels();
            }
        }

        // Sending
        public void SendTXT(string What, string TextToSend, string Mode, string ExcludeGUID)
        {
            // What = T (Text) or C (Control)
            // Mode = Send or Resend
            string TextWithFrom = "";
            foreach (DataGridViewRow row in DGAbonents.SelectedRows)
            {
                string ChannelGUID = row.Cells[0].Value.ToString();
                if (ChannelGUID == ExcludeGUID)
                {
                    // do nothing
                }
                else
                {
                    string CFPath = row.Cells[1].Value.ToString();
                    string Abonent2NickName = row.Cells[2].Value.ToString(); // receiver всегда (в SQL уже подмена была)
                    string Status = row.Cells[3].Value.ToString();
                    string PrimaryOrSecondary = row.Cells[4].Value.ToString();
                    string Abonent1NickName = row.Cells[5].Value.ToString(); // sender всегда (в SQL уже подмена была)
                    // uncomment if you want to send message to only abonents who is Here
                    // if (Status == MyResource.Here)
                    {
                        if (Mode == "Send") // add From in the message
                            TextWithFrom = MyResource.From + Abonent1NickName + ":" + Environment.NewLine + TextToSend;
                        else
                            TextWithFrom = TextToSend; // From inside message
                        SendTXTToAbonent(TextWithFrom, CFPath, PrimaryOrSecondary, What);
                    }
                    //else
                    //    ShowError(MyResource.AbonentIsAway);
                }
            }
        } 
        public void SendTXTToAbonent(string TextToSend,
                                     string CFPath,
                                     string PrimaryOrSecondary,
                                     string What)
        {
            // form MetaMessage
            string MetaMessage = What + ";" + TextToSend;
            LInfo1.Text = MyResource.Transferring; 
            Application.DoEvents();
            SendToSharedFolder(MetaMessage, PrimaryOrSecondary, CFPath);
            LInfo1.Text = "";
            Application.DoEvents();
        }
        public void SendToSharedFolder(string MetaMessage, string PrimaryOrSecondary, string CFPath)
        {
            // Primary abonent sends files like P_<N>. Secondary abonent sends files like S_<N>
            // сначала определяем имя для файла - если первичный канал то P_<N> иначе S_<N>  N - global var
            string FName = "";
            string Template = "";
            if (PrimaryOrSecondary == "Primary")
            {
                Template = "P_*";
                FName = "P_" + PNumber.ToString();
                PNumber++;
            }
            else
            {
                Template = "S_*";
                FName = "S_" + SNumber.ToString();
                SNumber++;
            }

            // if you want to send messages only to abonents who Here then uncomment code below
            /*
            // delete all not received files
            // если передается Status:Off то удалить все предыдущие файлы по шаблону

            if (MetaMessage == "C;Status:Off")
            {
                //список файлов 
                string[] files = Directory.GetFiles(CFPath, Template);
                foreach (string f in files)
                {
                    File.Delete(f);
                }
            }
            */
            // create file in common folder
            // создаем файл в общей папке данного канала
            FName = CFPath + "\\" + FName;
            // Send
            // посылаем
            try
            {
                // just write MetaMessage in file
                // записать MetaMessage в файл и положить его в общую папку
                File.WriteAllText(FName, MetaMessage, Encoding.UTF8);
                // File will be syncronized by means of cloud service
            }
            catch (Exception Ex)
            {
                ShowError(Ex.Message);
            }
        }
        public void SendFile(string FileOrVoice, string FName, string ExcludeGUID, string SenderNickName) 
        {
            foreach (DataGridViewRow row in DGAbonents.SelectedRows)
            {
                string ChannelGUID = row.Cells[0].Value.ToString();
                if (ChannelGUID == ExcludeGUID)
                {
                    // do nothing
                }
                else
                {
                    string CFPath = row.Cells[1].Value.ToString();
                    string Abonent2NickName = row.Cells[2].Value.ToString(); // receiver всегда (в SQL уже подмена была)
                    string Status = row.Cells[3].Value.ToString();
                    string PrimaryOrSecondary = row.Cells[4].Value.ToString();
                    string Abonent1NickName = row.Cells[5].Value.ToString(); // sender всегда (в SQL уже подмена была)
                    // uncomment if you want to send files only to abonents who is Here
                    //if (Status == MyResource.Here)
                    {
                        if (SenderNickName != "")              // for resending to conference participants
                            Abonent1NickName = SenderNickName; // для пересылки файлов от участника конференции через босса
                        SendFileToAbonent(FName, CFPath, Abonent1NickName, PrimaryOrSecondary, FileOrVoice);
                    }
                    //else
                    //    ShowError(MyResource.AbonentIsAway);
                }
            }
        }
        public void SendFileToAbonent(string FName,
                                     string CFPath,
                                     string Abonent1NickName, 
                                     string PrimaryOrSecondary,
                                     string FileOrVoice) 
        {
            string SenderNickName = Abonent1NickName; // yes. Abonent1 is always a sender. всегда имя первого - это источник. в SQL подмена была уже
            LInfo1.Text = MyResource.Uploading;
            Application.DoEvents();
            SendFileViaSharedFolder(FName, CFPath, FileOrVoice, Abonent1NickName, PrimaryOrSecondary);
            LInfo1.Text = "";
            Application.DoEvents();
        }
        public void SendFileViaSharedFolder(string FName, string CFPath, string FileOrVoice, string SenderNickName, string PrimaryOrSecondary) //OK
        {
            // FName is a Path. We need in FileName only
            // FName - это полный путь. Передать в контрольном сообщении надо только имя файла
            string FNameOnly = System.IO.Path.GetFileName(FName);
            // копируем файл в общую папку канала
            // copy file to common folder
            string FNamePath = CFPath + "\\" + FNameOnly; 
            System.IO.File.Copy(FName, FNamePath); 
            // now file is syncronizing with abonent's folder by means of cloud service
            // and we send control message
            // теперь этот файл синхронизируется с папкой абонента
            // а мы тем временем отсылаем control message
            //GetFile:F:FName:SenderNickName     file
            //GetFile:V:FName:SenderNickName     voice
            // we send only FileName in control message
            string ControlMessage = "GetFile:" + FileOrVoice + ":" + FNameOnly + ":" + SenderNickName;// отсылаем абоненту только имя файла. Он будет в его папке дропбокса.
            string MetaMessage = "C;" + ControlMessage;
            SendToSharedFolder(MetaMessage, PrimaryOrSecondary, CFPath);
        }
    
        // Receiving
        public void ReadData(string MetaMessage, string ChannelGUID)
        {
            // plain text      MetaMessage = "T;" + Message
            // control message MetaMessage = "C;" + Message

            // T - Message -> Text
            // C - Message -> Command:Arguments
            // Command GetFile      - Arguments = F/V:FName
            //         GetFile:F:FName                  file
            //         GetFile:V:FName                  voice            
            // Command GenerateKeys - Arguments = Params
            string What;
            string Message;
            // парсим метасообщение
            Char delimiter = ';';
            String[] substrings = MetaMessage.Split(delimiter);
            What = substrings[0];
            Message = substrings[1];
            if (What == "T")  // T = Text
            {
                LInfo2.Text = MyResource.Receiving; 
                Application.DoEvents();
                // resend in all selected channels (conference)
                // ретранслировать в другие выбранные каналы (конференция)
                SendTXT("T", Message, "Resend", ChannelGUID); // EXCLUDE this channel
                // вставить в историю в любом случае
                AddToHistoryStr("Abonent", Message);
            }
            else // Control message getfile, GenerateKeys
            if (What == "C")  // C = ControlMessage
            {
                LInfo2.Text = MyResource.Downloading; 
                Application.DoEvents();
                string ControlMessage = Message;
                // Parse message
                // парсим сообщение и достаем Command, Params или FName
                Char delimiter2 = ':';
                String[] substrings2 = ControlMessage.Split(delimiter2);
                //GetFile:F:FName     file
                //GetFile:V:FName     voice            
                string Command = substrings2[0];
                if (Command == "Status")
                {
                    string OnOff = substrings2[1]; 
                    // change status
                    // меняем статус в базе и обновляем каналы
                    string MyStatus = "";
                    if (OnOff == "On")
                    {
                        MyStatus = MyResource.Here; 
                        // send confirmation
                        // и нужно послать подтверждение что статус получен и что я тоже онлайн
                        string CFPath = GetParameterFromChannelTable("CFPath", ChannelGUID);
                        string PrimaryOrSecondary = GetParameterFromChannelTable("PrimaryOrSecondary", ChannelGUID);
                        SendTXTToAbonent("ConfirmStatus", CFPath, PrimaryOrSecondary, "C");
                    }
                    else
                        MyStatus = MyResource.Away;
                    string sql = "update ChannelTable set Status = '" + MyStatus + "' where " +
                        "ChannelGUID = '" + ChannelGUID + "'";
                    ExecuteSQL(sql);
                    GetChannels();
                    Application.DoEvents();
                }
                if (Command == "ConfirmStatus")
                {
                    string MyStatus = MyResource.Here;
                    string sql = "update ChannelTable set Status = '" + MyStatus + "' where " +
                        "ChannelGUID = '" + ChannelGUID + "'";
                    ExecuteSQL(sql);
                    GetChannels();
                    Application.DoEvents();
                }
                if (Command == "GetFile")
                {
                    string FileVoice = substrings2[1];
                    string FName = substrings2[2];
                    string CFPath = GetParameterFromChannelTable("CFPath", ChannelGUID);
                    string PathToFile = CFPath + "\\" + FName; 
                    bool OK = false;
                    int k = 0;
                    while (!OK)
                    {
                        // check to new file in common folder
                        // проверяем наличие файла в папке - ожидаем синхронизации
                        OK = System.IO.File.Exists(PathToFile);
                        k++;
                        Thread.Sleep(10);
                        Application.DoEvents();
                    }
                    Thread.Sleep(100); // for security
                    // synchronozation is over
                    // синхронизировался.
                    // may be name collisions
                    // so add ChannelGUID
                    // При перемещении файла в общую папку могут возникнуть коллизии по именам.
                    // добавляем GUID канала к файлу
                    string FullFName = CurrentDirectory + "\\Files\\" + ChannelGUID + "_" + FName;
                    // Move file from common folder to Files folder
                    // теперь надо этот файл переместить в папку Files
                    System.IO.File.Move(PathToFile, FullFName);
                    // now retranslate to all selected channels (conference)
                    // это ретрансляция. нужно передать имя отправителя (это не я)
                    string AbonentNickName = GetAbonentNickName(ChannelGUID);
                    if (FileVoice == "F") // file
                    {
                        SendFile("F", FullFName, ChannelGUID, AbonentNickName); // ResendFile
                        // открыть папку
                        Process.Start(CurrentDirectory + "\\Files\\");
                    }
                    else                  // voice
                    {
                        LInfo2.Text = MyResource.VoiceProcessing;
                        Application.DoEvents();
                        // no retranslation but play sound
                        sp.SoundLocation = FullFName;
                        sp.Play();
                    }
                    AddToHistoryStr("Abonent", MyResource.From + " " + AbonentNickName + ":" + 
                                    Environment.NewLine + FName + " " + MyResource.FileReceived);
                }
            }
            LInfo2.Text = "";
            Application.DoEvents();
        }

        // Utils
        public void GetChannels()
        {
            string sql = "select ChannelGUID, CFPath, Abonent2NickName as Nick, Status, primaryorsecondary, Abonent1NickName " +
                         "from channeltable where primaryorsecondary = 'Primary' " +
                         "union " +
                         "select ChannelGUID, CFPath, Abonent1NickName as Nick, Status, primaryorsecondary, Abonent2NickName " +
                         "from channeltable where primaryorsecondary = 'Secondary' " +
                         "order by Nick";
            DataTable dt;
            SQLiteDataAdapter sqlda = new SQLiteDataAdapter(sql, dbConnection);
            using (dt = new DataTable())
            {
                try
                {
                    sqlda.Fill(dt);
                    DGAbonents.DataSource = dt;
                }
                catch
                {
                }
            }
            DGAbonents.Columns[0].Visible = false;
            DGAbonents.Columns[1].Visible = false;
            DGAbonents.Columns[2].HeaderText = MyResource.Abonent;
            DGAbonents.Columns[3].HeaderText = MyResource.Status;
            DGAbonents.Columns[4].Visible = false;
            DGAbonents.Columns[5].Visible = false;
            // Delete button
            if (DGAbonents.Rows.Count == 0)
            {
                BDel.Enabled = false;
                BSend.Enabled = false;
                BAttachFile.Enabled = false;
                BStartRecord.Enabled = false;
            }
            else
                BDel.Enabled = true;
        }
        public void AddToHistoryStr(string WhoItIs, string TXTStr)
        {
            if (WhoItIs == "Me")
            {
                TXTStr = MyResource.From + " " + MyResource.Me + ":" + Environment.NewLine + TXTStr;
            }
            THistory.AppendText(Environment.NewLine + Environment.NewLine + TXTStr);
            // clear editor field
            // чистим редактор если это мое сообщение
            if (WhoItIs == "Me")
            {
                TEdit.Text = "";
                TEdit.Focus();
            }
            Application.DoEvents();
        }
        public string GetParameterFromChannelTable(string Field, string ChannelGUID)
        {
            string sql = "select " + Field + " from ChannelTable where ChannelGUID = '" + ChannelGUID + "'";
            SQLiteCommand command = new SQLiteCommand(sql, MainForm.dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            string Result = "";
            while (reader.Read())
            {
                Result = reader[0].ToString();
            }
            return Result;
        }
        public static string ToDateSQLite(DateTime value)
        {
            string format_date = "yyyy-MM-dd HH:mm:ss.fff";
            return value.ToString(format_date);
        }
        public void ShowError(string ErrorMessage)
        {
            LError.Text = ErrorMessage;
            TEdit.Focus();
        }
        public void ExecuteSQL(string sql)
        {
            SQLiteCommand command = new SQLiteCommand(sql, MainForm.dbConnection);
            command.ExecuteNonQuery();
        }
        public string GetClientNickName(string ChannelGUID)
        {
            // depends on PrimaryOrSecondary
            string PrimaryOrSecondary = GetParameterFromChannelTable("PrimaryOrSecondary", ChannelGUID);
            string sql = "";
            if (PrimaryOrSecondary == "Primary")
                sql = "select Abonent1NickName from ChannelTable where ChannelGUID = '" + ChannelGUID + "'";
            else
                sql = "select Abonent2NickName from ChannelTable where ChannelGUID = '" + ChannelGUID + "'";
            SQLiteCommand command = new SQLiteCommand(sql, MainForm.dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            string NickName = "";
            while (reader.Read())
            {
                NickName = reader[0].ToString();
            }
            return NickName;
        }
        public string GetAbonentNickName(string ChannelGUID)
        {
            // depends on PrimaryOrSecondary
            string PrimaryOrSecondary = GetParameterFromChannelTable("PrimaryOrSecondary", ChannelGUID);
            string sql = "";
            if (PrimaryOrSecondary == "Primary")
                sql = "select Abonent2NickName from ChannelTable where ChannelGUID = '" + ChannelGUID + "'";
            else
                sql = "select Abonent1NickName from ChannelTable where ChannelGUID = '" + ChannelGUID + "'";
            SQLiteCommand command = new SQLiteCommand(sql, MainForm.dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            string NickName = "";
            while (reader.Read())
            {
                NickName = reader[0].ToString();
            }
            return NickName;
        }

        //NAudio --------------------------------------------------------------------------------------
        void waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (this.InvokeRequired)
                this.BeginInvoke(new EventHandler<WaveInEventArgs>(waveIn_DataAvailable), sender, e);
            else
                writer.WriteData(e.Buffer, 0, e.BytesRecorded);
        }
        private void waveIn_RecordingStopped(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new EventHandler(waveIn_RecordingStopped), sender, e);
            }
            else
            {
                waveIn.Dispose();
                waveIn = null;
                writer.Close();
                writer = null;
            }
        }

        // Timer
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            // check for new files in each channel
            // опросить все каналы
            foreach (DataGridViewRow row in DGAbonents.Rows)
            {
                string ChannelGUID        = row.Cells[0].Value.ToString();
                string CFPath             = row.Cells[1].Value.ToString();
                string PrimaryOrSecondary = row.Cells[4].Value.ToString();
                // get list of files in folder
                // получить список файлов в папке
                DirectoryInfo dir = new DirectoryInfo(CFPath);
                FileSystemInfo[] files = dir.GetFileSystemInfos();
                var orderedFiles = files.OrderBy(f => f.CreationTime);
                foreach (var item in orderedFiles)// dir.GetFiles())
                {
                    bool OK = false;
                    string FName = item.Name;
                    // read only P_* and S_* files
                    // читать только файлы P_ и S_    
                    if (PrimaryOrSecondary == "Primary")
                        if (FName.Contains("S_"))
                            OK = true;
                    if (PrimaryOrSecondary == "Secondary")
                        if (FName.Contains("P_"))
                            OK = true;
                    if (OK)
                    {
                        FName = CFPath + "\\" + FName;
                        // read MetaMessage from file and delete
                        // прочитать в строку а файл удалить
                        // файл может уже быть создан но недописан. А здесь мы его уже читаем.
                        // Если исключение - ничего не делать
                        try
                        {
                            string MetaMessage = File.ReadAllText(FName, Encoding.UTF8);
                            File.Delete(FName);
                            // Process MetaMessage
                            // обработать
                            ReadData(MetaMessage, ChannelGUID);
                        }
                        catch
                        { }
                    }
                }
            }
            timer1.Enabled = true;
        }

    }
}



