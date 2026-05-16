using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSVFile
{
    internal class WordItem
    {
        public string Word { get; set; }
        public string Phonogram { get; set; }
        public string SoundPath { get; set; }
        public string Explain { get; set; }

        ///<summary>
        ///建構子 從TAV字串建立WordItem物件
        ///</summary>
        ///<paramname=“str”>單行的單字資料</param>
        public WordItem(string str)
        { // 用Tab分隔字串
            string[] strLists = str.Split('\t');
            if (strLists.Length >= 3)
            {
                Word = strLists[0];
                Phonogram = strLists[1];
                SoundPath = strLists[2];
                Explain = string.Join(Environment.NewLine, strLists.Skip(3));
            }
        }
        ///<summary>
        /// 覆寫ToString() 可將物件自動轉換為字串
        ///</summary>
        ///<returns></returns>
        public override string ToString()
        {
            return Word;
        }
    }
}
