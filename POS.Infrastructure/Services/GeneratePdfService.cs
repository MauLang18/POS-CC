using DinkToPdf;
using DinkToPdf.Contracts;
using POS.Application.Interfaces.Services;
using System.Text;

namespace POS.Infrastructure.Services;

public class GeneratePdfService : IGeneratePdfService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConverter _pdfConverter;

    public GeneratePdfService(IUnitOfWork unitOfWork, IConverter pdfConverter)
    {
        _unitOfWork = unitOfWork;
        _pdfConverter = pdfConverter;
    }

    public async Task<byte[]> GeneratePdf<T>(T data, int templateId)
    {
        var templateContent = await GetTemplateContentAsync(templateId);
        if (string.IsNullOrEmpty(templateContent))
            throw new Exception("No se pudo cargar la plantilla.");

        var populatedHtml = PopulateTemplate(templateContent, data);

        return GeneratePdfFromHtml(populatedHtml);
    }

    private async Task<string> GetTemplateContentAsync(int templateId)
    {
        var template = await _unitOfWork.DocumentTemplate.GetByIdAsync(templateId);
        if (template == null || string.IsNullOrEmpty(template.Content))
            throw new Exception("No se encontró la plantilla o está vacía.");

        return template.Content;
    }

    private string PopulateTemplate<T>(string templateContent, T data)
    {
        string populatedHtml = templateContent;

        // Primero, reemplazar propiedades simples
        foreach (var property in typeof(T).GetProperties())
        {
            var key = "{{" + property.Name + "}}";
            var value = property.GetValue(data)?.ToString();
            if (value != null)
            {
                populatedHtml = populatedHtml.Replace(key, value);
            }
        }

        var matches = System.Text.RegularExpressions.Regex.Matches(populatedHtml, @"{{#each\s+(?<CollectionName>\w+)}}(?<Content>.*?){{/each}}", System.Text.RegularExpressions.RegexOptions.Singleline);

        foreach (System.Text.RegularExpressions.Match match in matches)
        {
            var collectionName = match.Groups["CollectionName"].Value;
            var contentTemplate = match.Groups["Content"].Value;

            var collectionProperty = typeof(T).GetProperty(collectionName);
            var collectionValue = collectionProperty?.GetValue(data) as IEnumerable<object>;

            if (collectionValue != null)
            {
                var collectionHtml = new StringBuilder();
                foreach (var item in collectionValue)
                {
                    string itemContent = contentTemplate;
                    foreach (var itemProperty in item.GetType().GetProperties())
                    {
                        var itemKey = "{{this." + itemProperty.Name + "}}";
                        var itemValue = itemProperty.GetValue(item)?.ToString();
                        itemContent = itemContent.Replace(itemKey, itemValue);
                    }
                    collectionHtml.Append(itemContent);
                }

                populatedHtml = populatedHtml.Replace(match.Value, collectionHtml.ToString());
            }
        }

        return populatedHtml;
    }

    private byte[] GeneratePdfFromHtml(string htmlContent)
    {
        var doc = new HtmlToPdfDocument
        {
            GlobalSettings = new GlobalSettings
            {
                PaperSize = PaperKind.A4,
                Orientation = Orientation.Portrait
            }
        };

        var objectSettings = new ObjectSettings
        {
            HtmlContent = htmlContent,
            WebSettings = new WebSettings
            {
                DefaultEncoding = "utf-8",
                LoadImages = true
            }
        };

        doc.Objects.Add(objectSettings);

        return _pdfConverter.Convert(doc);
    }
}