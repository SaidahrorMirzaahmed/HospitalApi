using HospitalApi.Domain.Entities;
using HospitalApi.Domain.Enums;
using HospitalApi.Service.Exceptions;
using iText.IO.Font;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace HospitalApi.Service.Services.PdfGeneratorServices;

public partial class PdfGeneratorService
{
    #region
    private void CreateDetailsForRecipe(Document document)
    {
        PdfFont font = PdfFontFactory.CreateFont(_fontPath, PdfEncodings.IDENTITY_H);
        string tableName = string.Empty;

        Paragraph paragraph = new Paragraph($"АМБУЛАТОР КАРТА")
            .SetFont(font)
            .SetFontSize(16)
            .SetMarginTop(20)
            .SetMarginBottom(5)
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFontColor(ColorConstants.RED);

        document.Add(paragraph);
    }
    #endregion

    #region
    private void CreateUserDetailsForRecipe(Document document, User client)
    {
        PdfFont font = PdfFontFactory.CreateFont(_fontPath, PdfEncodings.IDENTITY_H);

        document.Add(new Paragraph($"Мурожаат санаси {DateOnly.FromDateTime(DateTime.Now)} й." +
            "                " +
            $"Соат {TimeOnly.FromDateTime(DateTime.Now)}").SetFont(font).SetFontSize(15).SetMarginBottom(10));

        document.Add(new Paragraph($"Бемор\nФИШ: {client.LastName}, {client.FirstName}").SetFont(font).SetFontSize(15).SetMarginBottom(10));

        document.Add(new Paragraph($"Туғилган йили: {DateOnly.FromDateTime(DateTime.Now).Year}").SetFont(font).SetFontSize(15).SetMarginBottom(10));

        document.Add(new Paragraph($"Яшаш манзили: {client.Address}").SetFont(font).SetFontSize(15).SetMarginBottom(10));
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

    #region
    private void CreateTableForRecipe(Document document, Recipe recipe)
    {
        PdfFont font = PdfFontFactory.CreateFont(_fontPath, PdfEncodings.IDENTITY_H);

        Paragraph complains = new Paragraph($"Шикоятлари:\n{recipe.Complaints}")
            .SetFont(font)
            .SetFontSize(15)
            .SetMarginBottom(10);

        Paragraph diagnosis = new Paragraph($"Ташхиси:\n{recipe.Diagnosis}")
            .SetFont(font)
            .SetFontSize(15)
            .SetMarginBottom(10);

        var checkUpsItems = new List<string>();
        foreach (var lab in recipe.CheckUps)
        {
            checkUpsItems.Add(GetLabTypes(lab));
        }
        Paragraph checkUps = new Paragraph($"Текширувлар:\n{string.Join(", ", checkUpsItems)}")
            .SetFont(font)
            .SetFontSize(15)
            .SetMarginBottom(10);

        Paragraph recommendations = new Paragraph($"Тавсиялар:\n{recipe.Recommendations}")
            .SetFont(font)
            .SetFontSize(15)
            .SetMarginBottom(10);

        document
            .Add(complains)
            .Add(diagnosis)
            .Add(checkUps)
            .Add(recommendations);
    }
    #endregion

    private string GetLabTypes(Laboratory lab)
    {
        var item = lab.LaboratoryTableType switch
        {
            LaboratoryTableType.AnalysisOfFeces => "Сийдик умумий тахлили",
            LaboratoryTableType.BiochemicalAnalysisOfBlood => "Қоннинг биокимёвий таҳлили",
            LaboratoryTableType.CommonAnalysisOfBlood => "Қоннинг умумий таҳлили",
            LaboratoryTableType.CommonAnalysisOfUrine => "Копрограмма",
            LaboratoryTableType.Torch => "Экспресс усулда TORCH IgG/IgM аниклаш",
            _ => throw new NotFoundException()
        };

        return item;
    }
}