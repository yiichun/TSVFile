using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Speech.Synthesis;

namespace TSVFile
{
    public partial class frmTSVFile : Form
    {
        /// <summary>
        /// 關於視窗
        /// </summary>
        frmAbout about = new frmAbout();

        /// <summary>
        /// 單字清單
        /// </summary>
        WordCollection _WordList = new WordCollection();

        public frmTSVFile()
        {
            InitializeComponent();
        }

        private void UpdateListView(string keyword = "")
        {
            lvwWord.BeginUpdate(); // 暫停重繪

            // 清除ListView的所有項目
            lvwWord.Items.Clear();

            // 將WordCollection物件中的資料篩選後載入到ListView中
            foreach (WordItem item in _WordList)
            {
                // 判斷：若 keyword 是空的，或者單字/中文解釋包含關鍵字 (ToLower 忽略大小寫)
                if (string.IsNullOrEmpty(keyword) ||
                    item.Word.ToLower().Contains(keyword.ToLower()) ||
                    item.Explain.ToLower().Contains(keyword.ToLower()))
                {
                    // 建立ListViewItem物件
                    ListViewItem lvi = new ListViewItem(item.Word);
                    lvi.SubItems.Add(item.Phonogram);
                    lvi.SubItems.Add(item.SoundPath);
                    lvi.SubItems.Add(item.Explain);
                    lvwWord.Items.Add(lvi);
                }
            }
            lvwWord.EndUpdate(); // 重繪
        }

        /// <summary>
        /// 當搜尋框文字改變時即時過濾
        /// </summary>
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            UpdateListView(txtSearch.Text);
        }

        private void lvwWord_ItemActivate(object sender, EventArgs e)
        {
            if (lvwWord.SelectedItems.Count > 0)
            {
                string selectedWord = lvwWord.SelectedItems[0].Text;

                try
                {
                    this.tsslMessage.Text = $"正在發音單字：{selectedWord}";

                    using (SpeechSynthesizer tts = new SpeechSynthesizer())
                    {
                        tts.Volume = 100; 
                        tts.Rate = 0;  

                        tts.SelectVoiceByHints(VoiceGender.Neutral, VoiceAge.Adult, 0, new System.Globalization.CultureInfo("en-US"));

                        tts.Speak(selectedWord);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("語音引擎啟動失敗：" + ex.Message, "提示");
                }
            }
        }

        private void frmTSVFile_Load(object sender, EventArgs e)
        {
            this.tsslMessage.Text = "請開啟TSV單字檔案...";
        }

        private void tsmiAbout_Click(object sender, EventArgs e)
        {
            about.ShowDialog();
        }

        private void tsmiOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "TSV files (*.tsv)|*.tsv|Textfiles (*.txt)|*.txt|Allfiles (*.*)|*.*";
            ofd.Title = "開啟檔案";
            // 設定初始目錄為程式所在目錄
            ofd.InitialDirectory = Application.StartupPath;
            DialogResult dr = ofd.ShowDialog(this);
            if (dr == DialogResult.OK)
            {
                // 讀取檔案並且將每一行的資料放入字串陣列
                string[] lines = File.ReadAllLines(ofd.FileName, Encoding.UTF8);
                // 將字串陣列的資料載入到WordCollection物件中
                _WordList.LoadFromStringArray(lines);

                // 開啟新檔案時，自動重設搜尋框文字
                txtSearch.Text = "";

                // 將WordCollection物件中的資料載入到ListView中
                UpdateListView();
                this.tsslMessage.Text = $"{_WordList.Count} 單字已成功載入: {ofd.FileName}";
            }
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTSVFile_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show("確定要離開嗎?", "離開", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}