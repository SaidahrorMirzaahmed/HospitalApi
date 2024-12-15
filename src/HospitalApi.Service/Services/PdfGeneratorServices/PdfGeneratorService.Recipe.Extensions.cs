using HospitalApi.Domain.Entities;
using iText.IO.Font;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace HospitalApi.Service.Services.PdfGeneratorServices;

public partial class PdfGeneratorService : IPdfGeneratorService
{
    #region
    private void CreateRecipeDetails(Document document)
    {
        PdfFont font = PdfFontFactory.CreateFont(_fontPath, PdfEncodings.IDENTITY_H);
        string tableName = string.Empty;

        Paragraph paragraph = new Paragraph($"АМБУЛАТОР КАРТА")
            .SetFont(font)
            .SetFontSize(14) // Font size
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFontColor(ColorConstants.RED);

        document.Add(paragraph);
    }
    #endregion

    #region
    private void CreateRecipeFooter(PdfDocument pdf, Document document, User staff)
    {
        PdfFont font = PdfFontFactory.CreateFont(_fontPath, PdfEncodings.IDENTITY_H);
        float marginBottom = 40;

        Table table = new Table(UnitValue.CreatePercentArray(new float[] { 1, 3 }))
            .UseAllAvailableWidth();

        Cell firstCell = new Cell()
            .Add(new Paragraph("Врач:")
                .SetFont(font)
                .SetFontSize(16))
            .SetBorder(Border.NO_BORDER)
            .SetTextAlignment(TextAlignment.RIGHT);

        Cell secondCell = new Cell()
            .Add(new Paragraph($"{staff.LastName} {staff.FirstName}")
                .SetFont(font)
                .SetFontSize(16))
            .SetBorder(Border.NO_BORDER)
            .SetTextAlignment(TextAlignment.CENTER);

        table.AddCell(firstCell);
        table.AddCell(secondCell);

        table.SetFixedPosition(
            pdf.GetNumberOfPages(),
            document.GetLeftMargin(),
            marginBottom,
            pdf.GetDefaultPageSize().GetWidth() - document.GetLeftMargin() - document.GetRightMargin()
        );

        document.Add(table);
    }
    #endregion
}