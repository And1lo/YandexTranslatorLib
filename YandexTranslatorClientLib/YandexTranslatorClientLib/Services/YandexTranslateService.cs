using Newtonsoft.Json;
using RestSharp;
using TranslateService.Models;
using TranslateService.Models.YandexContracts;

namespace YandexTranslatorClientLib.Services;

public class YandexTranslateService : ITranslateService
{
    private readonly List<Lang> AllLangs = new List<Lang>()
    {
        Lang.en,//english
        Lang.es,//spanish
        Lang.ar,//arabic
        Lang.fr,//french
        Lang.it,//italian
        Lang.id,//indonesian
        Lang.sv,//swedish
        Lang.pt,//portugese
        Lang.tr,//turkish
        Lang.ru,//russian
    };
    public async Task<TextObject> TranslateAsync(string text, Lang targetLang)
    {
        var client = new RestClient(Consts.ApiEndpoint);
        var request = new RestRequest("translate", Method.Post);
        request.AddHeader("Authorization", Consts.ApiKey);

        YandexTranslateContract contract = new YandexTranslateContract
        {
            SourceLanguageCode = "ru",
            TargetLanguageCode = targetLang.ToString(),
            Texts =
            [
                text
            ]
        };
        request.AddBody(contract);

        var response = await client.PostAsync(request);

        if (response.StatusCode != System.Net.HttpStatusCode.OK)
            throw new Exception(response.StatusCode + response.ErrorMessage);
        
        var jsonResponse = response.Content;
        var responseModel = JsonConvert.DeserializeObject<YandexTranslateResponse>(jsonResponse);
            
        if (responseModel == null) throw new Exception("Yandex response error");
            
        var translationText = responseModel.Translations.FirstOrDefault()?.Text;
        if (string.IsNullOrEmpty(translationText)) throw new Exception("Translated text not found");
        responseModel.Translations[0].Text = translationText;
            
        return new TextObject
        {
            Text =  translationText,
            Language = targetLang
        };
    }
    
    public async Task<List<TextObject>> TranslateAsync(string text, List<Lang>? targetLangs = null)
    {
        if (targetLangs == null || targetLangs.Count == 0)
        {
            targetLangs = AllLangs;
        }
        
        var translations = new List<TextObject>();
        foreach (var lang in targetLangs)
        {
            var translation = await TranslateAsync(text, lang);
            translations.Add(translation);
        }
        return translations;
    }
}