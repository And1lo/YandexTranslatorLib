using System.Text.Json.Serialization;

namespace TranslateService.Models.YandexContracts;

public class YandexTranslateContract
{
    [JsonPropertyName("sourceLanguageCode")]
    public string SourceLanguageCode { get; set; } = Lang.ru.ToString();

    [JsonPropertyName("targetLanguageCode")]
    public string TargetLanguageCode { get; set; } = Lang.en.ToString();
    
    [JsonPropertyName("texts")]
    public string[] Texts { get; set; }
}