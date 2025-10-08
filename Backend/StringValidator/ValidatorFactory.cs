using System.Collections.Concurrent;
using System.Globalization;
using System.Resources;

namespace john.aiGadgets.StringValidator
{
    public static class ValidatorFactory
    {
        private static readonly Dictionary<validateType, string> _patterns = new Dictionary<validateType, string>
        {
            { validateType.None, string.Empty },
            { validateType.Alpha, ValidatorPatterns.Alpha },
            { validateType.Numeric, ValidatorPatterns.Numeric },
            { validateType.AlphaNumeric, ValidatorPatterns.AlphaNumeric },
            { validateType.Chinese, ValidatorPatterns.Chinese },
            { validateType.Email, ValidatorPatterns.Email },
            { validateType.Url, ValidatorPatterns.Url },
            { validateType.IP, ValidatorPatterns.IP },
            { validateType.Currency, ValidatorPatterns.Currency },
            { validateType.Date, ValidatorPatterns.Date },
            { validateType.Time, ValidatorPatterns.Time },
            { validateType.DateTime, ValidatorPatterns.DateTime },
            { validateType.ZipCode, ValidatorPatterns.ZipCode }
        };

        // Regex 實例快取
        private static readonly ConcurrentDictionary<string, System.Text.RegularExpressions.Regex> _regexCache = new();

        // 多語系資源
        private static readonly ResourceManager _resourceManager = new ResourceManager("john.aiGadgets.StringValidator.ValidatorResources", typeof(ValidatorFactory).Assembly);

        /// <summary>
        /// 動態新增或覆蓋驗證規則
        /// </summary>
        /// <param name="type">驗證類型</param>
        /// <param name="pattern">Regular Expression 規則</param>
        public static void AddPattern(validateType type, string pattern)
        {
            _patterns[type] = pattern;
            _regexCache.TryRemove(pattern, out _); // 移除舊快取
        }

        /// <summary>
        /// 靜態驗證方法
        /// </summary>
        /// <param name="sourceStr">被驗證字串</param>
        /// <param name="type">驗證類型</param>
        /// <returns></returns>
        /// <example>
        /// ValidatorFactory.Validate("abc123", validateType.AlphaNumeric);
        /// </example>
        public static bool Validate(string sourceStr, validateType type)
        {
            if (string.IsNullOrEmpty(sourceStr))
                return false;

            if (type == validateType.None)
                return true;

            if (_patterns.TryGetValue(type, out string pattern))
            {
                try
                {
                    var regex = _regexCache.GetOrAdd(pattern, p => new System.Text.RegularExpressions.Regex(p, System.Text.RegularExpressions.RegexOptions.Compiled));
                    return regex.IsMatch(sourceStr);
                }
                catch (ArgumentException)
                {
                    throw new ArgumentException(_resourceManager.GetString("InvalidPattern", CultureInfo.CurrentUICulture) ?? $"不支援的驗證類型: {type}");
                }
            }

            throw new ArgumentException(_resourceManager.GetString("UnsupportedType", CultureInfo.CurrentUICulture) ?? $"不支援的驗證類型: {type}");
        }

        /// <summary>
        /// 使用自訂Regular Expression驗證字串
        /// </summary>
        /// <param name="sourceStr">被驗證字串</param>
        /// <param name="customPattern">自訂Regular Expression驗證規則</param>
        /// <returns></returns>
        /// <example>
        /// ValidatorFactory.Validate("abc", "^[a-z]+$");
        /// </example>
        public static bool Validate(string sourceStr, string customPattern)
        {
            if (string.IsNullOrEmpty(sourceStr) || string.IsNullOrEmpty(customPattern))
                return false;
            try
            {
                var regex = _regexCache.GetOrAdd(customPattern, p => new System.Text.RegularExpressions.Regex(p, System.Text.RegularExpressions.RegexOptions.Compiled));
                return regex.IsMatch(sourceStr);
            }
            catch (ArgumentException)
            {
                throw new ArgumentException(_resourceManager.GetString("InvalidPattern", CultureInfo.CurrentUICulture) ?? "自訂驗證規則格式錯誤");
            }
        }

        /// <summary>
        /// 取得指定驗證類型的Pattern
        /// </summary>
        /// <param name="type">驗證類型</param>
        /// <returns></returns>
        public static string GetPattern(validateType type)
        {
            return _patterns.TryGetValue(type, out string pattern) ? pattern : string.Empty;
        }
    }
}
