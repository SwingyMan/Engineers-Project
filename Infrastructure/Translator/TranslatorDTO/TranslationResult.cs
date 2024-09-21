namespace Infrastructure.Translator.TranslatorDTO;

public class TranslationResult
{
    public DetectedLanguage detectedLanguage { get; set; }
    public List<Translation> translations { get; set; }
}