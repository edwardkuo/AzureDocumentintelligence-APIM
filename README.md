# AzureDocumentintelligence-APIM
透過APIM呼叫Document intelligence，讓檔案上傳並辨識其內容
# Document Intelligence 應用程式使用說明文件

## 📋 系統概述

此應用程式是一個基於 Windows Forms 的文件智能分析工具，使用 Azure Document Intelligence 服務來處理 PDF 文件。

## 🔑 系統配置

### 認證資訊
```csharp
private const string EndpointUrl = "";
private const string ApiKey = "";
```

## 🏗️ 系統架構

### 主要元件

#### 1. DocumentAnalysisClient
- 負責與 Azure Document Intelligence 服務通訊
- 使用自訂的 HTTP 標頭策略進行認證
- 初始化時設置端點 URL 和認證資訊

#### 2. AddHeadersPolicy 類別
- 繼承自 `HttpPipelineSynchronousPolicy`
- 處理 HTTP 請求標頭的添加
- 支援多重標頭配置

## 🛠️ 核心功能

### 1. 文件上傳
- 支援 PDF 檔案選擇
- 使用 OpenFileDialog 實現
- 包含檔案類型過濾（僅 PDF）

### 2. 文件分析功能
透過 `ProcessDocument` 方法實現以下功能：
- 文件頁面分析
- 文字內容提取
- 關鍵值對識別
- 表格資料解析

## 📊 分析結果呈現

### 1. 基本資訊
- 顯示文件總頁數
- 依頁面順序呈現內容

### 2. 文字內容
- 按頁面順序展示
- 保留原始文件的行結構

### 3. 關鍵值對
- 以鍵值對形式展示
- 格式：`鍵: 值`

### 4. 表格資料
- 顯示表格維度（列數 x 行數）
- 以矩陣形式展示表格內容
- 使用方括號區隔單元格內容

## 🔄 錯誤處理機制

### 1. 檔案處理
- 使用 using 語句確保資源正確釋放
- 包含完整的例外處理

### 2. 異常處理
- 捕獲並顯示詳細錯誤訊息
- 使用者友善的錯誤提示
- 保持介面回應性

## 👨‍💻 使用方式

1. 啟動應用程式
2. 點選上傳按鈕
3. 在檔案選擇器中選擇 PDF 檔案
4. 等待處理完成
5. 查看分析結果：
   - 文件頁數
   - 文字內容
   - 關鍵值對（如有）
   - 表格資料（如有）

## ⚠️ APIM配置
```json
{
    "openapi": "3.0.1",
    "info": {
        "title": "doc",
        "description": "",
        "version": "1.0"
    },
    "servers": [{
        "url": ""
    }],
    "paths": {
        "/*": {
            "post": {
                "summary": "doc-Instance",
                "operationId": "doc-instance",
                "responses": {
                    "200": {
                        "description": null
                    }
                }
            },
            "get": {
                "summary": "analyzeResults",
                "operationId": "analyzeresults",
                "responses": {
                    "200": {
                        "description": null
                    }
                }
            }
        }
    },
    "components": {
        "securitySchemes": {
            "apiKeyHeader": {
                "type": "apiKey",
                "name": "api-key",
                "in": "header"
            },
            "apiKeyQuery": {
                "type": "apiKey",
                "name": "subscription-key",
                "in": "query"
            }
        }
    },
    "security": [{
        "apiKeyHeader": []
    }, {
        "apiKeyQuery": []
    }]
}


