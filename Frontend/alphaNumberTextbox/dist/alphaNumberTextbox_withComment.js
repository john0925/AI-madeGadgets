/**
 * 應用輸入限制到指定的 input 元素上。
 * * @param {HTMLElement} inputElement - 目標 input 元素。
 * @param {object} config - 配置物件。
 * @param {number} config.maxLength - 限制的最大字元數。
 * @param {('number'|'english'|'alphanumeric')} config.pattern - 允許的字元類型。
 */
function applyInputRestriction(inputElement, config) {
    // 預設配置
    const defaultConfig = {
        maxLength: 20,
        pattern: 'alphanumeric' // 預設為英文和數字
    };

    // 合併用戶配置
    const { maxLength, pattern } = { ...defaultConfig, ...config };

    // 依據配置決定要過濾掉哪些字元
    let allowedPatternRegex;
    switch (pattern) {
        case 'number':
            // 匹配所有非數字的字元
            allowedPatternRegex = /[^0-9]/g; 
            break;
        case 'english':
            // 匹配所有非英文字母的字元
            allowedPatternRegex = /[^a-zA-Z]/g; 
            break;
        case 'alphanumeric':
        default:
            // 匹配所有非英文和數字的字元 (預設)
            allowedPatternRegex = /[^a-zA-Z0-9]/g; 
            break;
    }

    // 狀態變數：追蹤輸入法是否正在組字 (為了解決中文輸入法問題)
    let isComposing = false;

    // 核心處理邏輯：過濾不允許的字元並限制長度
    const handleFilterAndLength = (inputEl) => {
        let value = inputEl.value;

        // 1. 過濾不允許的字元
        value = value.replace(allowedPatternRegex, '');

        // 2. 限制最大長度
        if (value.length > maxLength) {
            value = value.substring(0, maxLength);
        }

        // 更新輸入框的值
        inputEl.value = value;
    };


    // ----------------------
    // 1. 輸入法事件處理
    // ----------------------
    inputElement.addEventListener('compositionstart', () => {
        isComposing = true;
    });

    inputElement.addEventListener('compositionend', function() {
        isComposing = false;
        // 使用 setTimeout 確保在輸入法確認字元後立即執行過濾
        setTimeout(() => {
            handleFilterAndLength(this);
        }, 0); 
    });


    // ----------------------
    // 2. 鍵盤輸入事件處理 (Input Event)
    // ----------------------
    inputElement.addEventListener('input', function() {
        // 組字中則跳過過濾
        if (isComposing) {
            return;
        }
        handleFilterAndLength(this);
    });

    // ----------------------
    // 3. 貼上操作處理 (Paste Event)
    // ----------------------
    inputElement.addEventListener('paste', function(event) {
        event.preventDefault();

        const pasteData = event.clipboardData.getData('text');
        const currentValue = this.value;
        const selectionStart = this.selectionStart;
        const selectionEnd = this.selectionEnd;

        // 1. 過濾貼上內容
        let filteredData = pasteData.replace(allowedPatternRegex, '');

        // 2. 限制長度：計算還可以插入多少字元
        const maxAppendLength = maxLength - currentValue.length + (selectionEnd - selectionStart);

        if (filteredData.length > maxAppendLength) {
            filteredData = filteredData.substring(0, maxAppendLength);
        }

        // 插入處理後的內容
        this.value = currentValue.substring(0, selectionStart) +
                     filteredData +
                     currentValue.substring(selectionEnd);
        
        // 移動游標
        this.selectionStart = this.selectionEnd = selectionStart + filteredData.length;
    });
    
    // 額外：為 input 元素設置 HTML 的 maxlength 屬性以增強使用者體驗
    inputElement.setAttribute('maxlength', maxLength);
}