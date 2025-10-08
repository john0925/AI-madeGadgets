※本文件內容由AI生成

## 注意事項

- 若驗證類型或正則規則不正確，會拋出 `ArgumentException` 並提供多語系錯誤訊息。
- 支援 .NET 8 與 C# 12.0。

## 檔案結構

- `ValidatorFactory.cs`：主要驗證邏輯與規則管理。
- `ValidatorPatterns.cs`：各驗證類型的正則表達式定義。
- `validateType.cs`：驗證類型列舉。
- `ValidatorResources.resx`：多語系資源檔。

---

如需擴充驗證規則，請使用 `AddPattern` 方法動態新增，或修改 `ValidatorPatterns.cs` 內容。