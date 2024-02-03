using TranslateService.Models;

namespace YandexTranslatorClientLib.Services;

public interface ITranslateService
{
    public Task<TextObject> TranslateAsync(string text, Lang targetLang);
    public Task<List<TextObject>> TranslateAsync(string text, List<Lang>? targetLangs = null);
}