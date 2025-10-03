# alphaNumberTextbox.js
※本文件內容由AI生成

一個輕量級的 JavaScript 函式庫，用於對輸入欄位套用字元類型和長度限制。


## 功能特色

- ✅ 支援多種輸入模式：純數字、純英文字母、英數混合
- ✅ 可自訂最大字元長度限制
- ✅ 完整支援中文輸入法（IME）組字過程
- ✅ 智慧處理貼上事件，自動過濾不符合規則的字元
- ✅ 無相依套件，純原生 JavaScript 實作
- ✅ 輕量化，壓縮後檔案極小

## 安裝方式

直接引入 JavaScript 檔案到您的 HTML 中：

```html
<!-- 開發版本 -->
<script src="alphaNumberTextbox.js"></script>

<!-- 或使用壓縮版本 -->
<script src="alphaNumberTextbox.min.js"></script>
```

## 使用方法

### 基本用法

```javascript
// 取得輸入欄位元素
const inputElement = document.getElementById('myInput');

// 套用限制（使用預設設定）
applyInputRestriction(inputElement, {});
```

### 進階設定

```javascript
// 僅允許數字，最多 10 個字元
applyInputRestriction(inputElement, {
    pattern: 'number',
    maxLength: 10
});

// 僅允許英文字母，最多 15 個字元
applyInputRestriction(inputElement, {
    pattern: 'english',
    maxLength: 15
});

// 允許英數混合，最多 20 個字元（預設）
applyInputRestriction(inputElement, {
    pattern: 'alphanumeric',
    maxLength: 20
});
```

## API 說明

### applyInputRestriction(inputElement, config)

對指定的輸入欄位套用輸入限制。

#### 參數

- **inputElement** (`HTMLElement`, 必填)
  - 目標輸入欄位元素

- **config** (`Object`, 必填)
  - **maxLength** (`Number`, 選填, 預設: `20`)
    - 允許輸入的最大字元數
  
  - **pattern** (`String`, 選填, 預設: `'alphanumeric'`)
    - 允許的字元類型，可選值：
      - `'number'` - 僅允許數字 (0-9)
      - `'english'` - 僅允許英文字母 (a-z, A-Z)
      - `'alphanumeric'` - 允許英數混合 (a-z, A-Z, 0-9)

#### 範例

```html
<!DOCTYPE html>
<html>
<head>
    <title>輸入限制範例</title>
    <script src="alphaNumberTextbox.js"></script>
</head>
<body>
    <input type="text" id="numberOnly" placeholder="僅允許數字">
    <input type="text" id="englishOnly" placeholder="僅允許英文">
    <input type="text" id="alphanumeric" placeholder="允許英數混合">

    <script>
        // 數字輸入欄位
        applyInputRestriction(
            document.getElementById('numberOnly'),
            { pattern: 'number', maxLength: 10 }
        );

        // 英文輸入欄位
        applyInputRestriction(
            document.getElementById('englishOnly'),
            { pattern: 'english', maxLength: 20 }
        );

        // 英數混合輸入欄位
        applyInputRestriction(
            document.getElementById('alphanumeric'),
            { pattern: 'alphanumeric', maxLength: 30 }
        );
    </script>
</body>
</html>
```

## 功能說明

### 中文輸入法支援

此函式庫完整支援中文輸入法（IME）的組字過程。在使用者進行中文輸入時：
- 組字過程中不會觸發字元過濾
- 完成組字後才會執行字元類型檢查
- 確保中文輸入體驗流暢自然

### 貼上行為

當使用者貼上文字時：
- 自動過濾不符合設定模式的字元
- 若貼上內容超過長度限制，自動截斷至允許的最大長度
- 保持游標位置在貼上內容之後

### 長度限制

- 即時檢查輸入長度
- 超過限制時自動截斷
- 同時設定 HTML `maxlength` 屬性作為備用防護

## 瀏覽器相容性

支援所有現代瀏覽器：
- Chrome / Edge (最新版本)
- Firefox (最新版本)
- Safari (最新版本)
- Opera (最新版本)

需要支援以下 JavaScript 功能：
- ES6 解構賦值
- 展開運算子（Spread operator）
- Arrow functions
- `compositionstart` / `compositionend` 事件

## 授權

本專案採用 MIT 授權條款。

## 注意事項

- 此函式庫僅處理客戶端驗證，伺服器端仍需進行二次驗證
- 建議在表單提交前再次檢查輸入值的有效性
- 對於特殊字元需求，可自行修改正則表達式規則

## 更新日誌

### v1.0.0
- 初始版本發布
- 支援數字、英文、英數混合三種模式
- 完整的 IME 輸入法支援
- 智慧貼上處理功能
