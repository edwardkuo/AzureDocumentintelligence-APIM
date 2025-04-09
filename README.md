# AzureDocumentintelligence-APIM
透過APIM呼叫Document intelligence，讓檔案上傳並辨識其內容


概述
這是一個使用 Azure Document Intelligence 服務的 Windows Forms 應用程式，主要用於分析 PDF 文件，能夠提取文字內容、關鍵值對（key-value pairs）以及表格資料。

主要組件
設定資訊
應用程式需要三個驗證參數：

Azure 服務端點 URL
API 金鑰
JWT 認證令牌
核心類別
Form1
主要表單類別，負責：

Document Intelligence 客戶端初始化
使用者介面互動
文件處理流程
AddHeadersPolicy
自訂 HTTP 管道政策類別，用於添加必要的認證標頭。

主要功能
1. 文件上傳
通過 OpenFileDialog 實現檔案選擇
僅允許選擇 PDF 檔案
包含錯誤處理和使用者介面回饋
2. 文件分析
ProcessDocument 方法執行以下分析任務：

頁面內容提取
關鍵值對識別
表格偵測和解析
輸出格式
分析結果顯示以下內容：

總頁數
文件內容（文字）
關鍵值對（如果存在）
表格資料（如果存在），包含行列資訊
使用流程
啟動應用程式
點選上傳按鈕
選擇 PDF 文件
等待處理完成
查看分析結果
錯誤處理
完整的 try-catch 錯誤處理機制
使用者友善的錯誤訊息
使用 using 語句確保資源正確釋放
處理過程中的介面狀態管理
安全考量
敏感憑證目前以常數儲存（建議移至安全配置）
JWT 令牌和 API 金鑰在生產環境中應妥善保護
檔案處理包含適當的資源釋放機制
