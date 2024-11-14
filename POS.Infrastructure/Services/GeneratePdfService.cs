using DinkToPdf;
using DinkToPdf.Contracts;
using POS.Application.Interfaces.Services;
using System;
using System.Threading.Tasks;

namespace POS.Infrastructure.Services
{
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
            // Obtener la plantilla HTML desde la base de datos usando el UnitOfWork
            var templateContent = await GetTemplateContentAsync(templateId);
            if (string.IsNullOrEmpty(templateContent))
                throw new Exception("No se pudo cargar la plantilla.");

            // Rellenar los datos en la plantilla HTML
            var populatedHtml = PopulateTemplate(templateContent, data);

            // Generar el PDF a partir del HTML
            return GeneratePdfFromHtml(populatedHtml);
        }

        private async Task<string> GetTemplateContentAsync(int templateId)
        {
            // Obtener la plantilla desde la base de datos usando el UnitOfWork
            var template = await _unitOfWork.DocumentTemplate.GetByIdAsync(templateId);
            if (template == null || string.IsNullOrEmpty(template.Content))
                throw new Exception("No se encontró la plantilla o está vacía.");

            return template.Content;
        }

        private string PopulateTemplate<T>(string templateContent, T data)
        {
            // Reemplazar las variables dentro del HTML con los valores de 'data'
            string populatedHtml = templateContent;

            foreach (var property in typeof(T).GetProperties())
            {
                var key = "{{" + property.Name + "}}";
                var value = property.GetValue(data)?.ToString();
                if (value != null)
                {
                    populatedHtml = populatedHtml.Replace(key, value);
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

            // Crear un objeto de configuración para el contenido HTML
            var objectSettings = new ObjectSettings
            {
                HtmlContent = htmlContent,
                WebSettings = new WebSettings
                {
                    DefaultEncoding = "utf-8",
                    LoadImages = true
                }
            };

            // Añadir el objeto a la configuración del documento
            doc.Objects.Add(objectSettings);

            // Generar el PDF y devolver los bytes
            return _pdfConverter.Convert(doc);
        }
    }
}
