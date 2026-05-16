# 📚 TSV 單字檔案讀取與智慧發音查詢系統 

這是一個使用 C# WinForms 開發的單字管理工具。主要利用 ListView 控制項來讀取與展示大批量的 TSV 單字資料，並在此基礎上擴充了「即時輸入過濾」與「雙擊呼叫 Windows TTS 語音發音」兩個功能。

🛠 主要功能

1. TSV 檔案讀取與欄位解析：

- 支援讀取以 Tab 分隔的 *.tsv 或 *.txt 檔案。
- 資料欄位完整對應：單字 (Word)、音標 (Phonogram)、音檔路徑 (SoundPath) 與 中文解釋 (Explain)。

2. 即時雙向搜尋過濾：

畫面加入查詢輸入框，不需按任何按鈕，打字時 ListView 就會動態重新整理。搜尋不區分英文字母大小寫。

3. 雙擊項目英文發音

4. 狀態列提示與安全關閉機制

執行畫面截圖：

1. 主介面

<img width="1221" height="838" alt="image" src="https://github.com/user-attachments/assets/1668ac11-d380-4b5c-9c25-1194221ade31" />

2. 關鍵字搜尋過濾

<img width="1226" height="831" alt="image" src="https://github.com/user-attachments/assets/b76aabf9-03fe-43a9-bbe8-ba2289d9922e" />

3. 雙擊單字播放發音

<img width="1221" height="837" alt="image" src="https://github.com/user-attachments/assets/62c0507a-c241-4a92-a4d3-1615e07a43c9" />

4. 程式關閉確認視窗

<img width="1229" height="835" alt="image" src="https://github.com/user-attachments/assets/1441003c-a354-4224-b376-b01ade4a64ba" />
