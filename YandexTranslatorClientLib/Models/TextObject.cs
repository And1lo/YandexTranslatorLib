namespace YandexTranslatorClientLib.Models;

public class TextObject
{
    public string Text { get; set; }
    public Lang Language { get; set; }

    public override string ToString() => Text;
    public bool Equals(string comparisonText, StringComparison stringComparison = StringComparison.CurrentCulture)
    => Text.Equals(comparisonText, stringComparison);
    
}