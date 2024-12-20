using HospitalApi.Domain.Entities;
using HospitalApi.Domain.Enums;
using HospitalApi.Service.Exceptions;
using HospitalApi.WebApi.Configurations;
using iText.IO.Font;
using iText.IO.Image;
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
    private string _fontPath = "c:/windows/fonts/arial.ttf";

    #region
    private void CreateHeader(Document document)
    {
        var logoPath = Path.Combine(EnvironmentHelper.WebRootPath, "Clinic\\turonlogo.png");
        PdfFont font = PdfFontFactory.CreateFont(_fontPath, PdfEncodings.IDENTITY_H);

        // Create a table with two columns for horizontal layout
        Table headerTable = new Table(UnitValue.CreatePercentArray(new float[] { 1, 1, 1 }))
            .UseAllAvailableWidth();

        // First column: Uzbek text
        Cell leftCell = new Cell()
            .Add(new Paragraph("Ўзбекистон Республикаси")
                .SetFont(font)
                .SetFontSize(12)
                .SetTextAlignment(TextAlignment.CENTER))
            .Add(new Paragraph("Наманган вилояти"))
                .SetFont(font)
                .SetFontSize(12)
                .SetTextAlignment(TextAlignment.CENTER)
            .Add(new Paragraph("Косонсой тумани")
                .SetFont(font)
                .SetFontSize(12)
                .SetTextAlignment(TextAlignment.CENTER))
            .Add(new Paragraph("ТУРОН ТИББИЕТ МЧЖ")
                .SetFont(font)
                .SetFontSize(14)
                .SetTextAlignment(TextAlignment.CENTER))
            .SetBorder(Border.NO_BORDER)
            .SetFontColor(ColorConstants.BLUE);

        // Second column: Logo image
        Image logo = new Image(ImageDataFactory.Create(logoPath))
            .SetHorizontalAlignment(HorizontalAlignment.CENTER)
            .SetWidth(100); // Adjust size as needed

        Cell middleCell = new Cell()
            .Add(logo)
            .SetTextAlignment(TextAlignment.CENTER)
            .SetVerticalAlignment(VerticalAlignment.MIDDLE)
            .SetBorder(Border.NO_BORDER);

        Cell rightCell = new Cell()
            .Add(new Paragraph("O’zbekiston Respublikasi")
                .SetFont(font)
                .SetFontSize(12)
                .SetTextAlignment(TextAlignment.CENTER))
            .Add(new Paragraph("Namangan viloyati")
                .SetFont(font)
                .SetFontSize(12)
                .SetTextAlignment(TextAlignment.CENTER))
            .Add(new Paragraph("Kosonsoy tumani")
                .SetFont(font)
                .SetFontSize(12)
                .SetTextAlignment(TextAlignment.CENTER))
            .Add(new Paragraph("TURON TIBBIYOT MCHJ")
                .SetFont(font)
                .SetFontSize(14)
                .SetTextAlignment(TextAlignment.CENTER))
            .SetBorder(Border.NO_BORDER)
            .SetFontColor(ColorConstants.BLUE);

        // Add cells to the table
        headerTable.AddCell(leftCell);
        headerTable.AddCell(middleCell);
        headerTable.AddCell(rightCell);
        // Add table to document
        document.Add(headerTable);
    }
    #endregion

    #region
    private void CreateLaboratoryServiceDetails(Document document, LaboratoryTableType tableType)
    {
        PdfFont font = PdfFontFactory.CreateFont(_fontPath, PdfEncodings.IDENTITY_H);
        string tableName = string.Empty;

        if (tableType == LaboratoryTableType.AnalysisOfFeces)
            tableName = "Копрограмма";
        if (tableType == LaboratoryTableType.BiochemicalAnalysisOfBlood)
            tableName = "Қоннинг биокимёвий таҳлили";
        else if (tableType == LaboratoryTableType.CommonAnalysisOfBlood)
            tableName = "Қоннинг умумий таҳлили";
        else if (tableType == LaboratoryTableType.CommonAnalysisOfUrine)
            tableName = "Сийдик умумий тахлили";
        else if (tableType == LaboratoryTableType.Torch)
            tableName = "Экспресс усулда TORCH IgG/IgM аниклаш";

        Paragraph paragraph = new Paragraph($"{tableName}")
            .SetFont(font)
            .SetFontSize(16) // Font size
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFontColor(ColorConstants.RED);

        document.Add(paragraph);
    }
    #endregion

    #region
    private void CreateUserDetails(Document document, User client)
    {
        PdfFont font = PdfFontFactory.CreateFont(_fontPath, PdfEncodings.IDENTITY_H);

        document.Add(new Paragraph($"{DateOnly.FromDateTime(DateTime.UtcNow.AddHours(5))} й.    Соат {TimeOnly.FromDateTime(DateTime.UtcNow.AddHours(5)).ToString("HH:mm")}").SetFont(font).SetFontSize(15).SetMarginBottom(10));

        document.Add(new Paragraph($"Фамилия {client.LastName}, исми {client.FirstName} ёши {DateOnly.FromDateTime(DateTime.UtcNow.AddHours(5)).Year - client.Birth.Year}").SetFont(font).SetFontSize(15).SetMarginBottom(10));

        document.Add(new Paragraph($"Манзили {client.Address}").SetFont(font).SetFontSize(15).SetMarginBottom(10));
    }
    #endregion

    #region
    private void CreateFooter(PdfDocument pdf, Document document, User staff)
    {
        PdfFont font = PdfFontFactory.CreateFont(_fontPath, PdfEncodings.IDENTITY_H);
        float marginBottom = 40;

        Table table = new Table(UnitValue.CreatePercentArray(new float[] { 1, 3 }))
            .UseAllAvailableWidth();

        Cell firstCell = new Cell()
            .Add(new Paragraph("Лаборант:")
                .SetFont(font))
                .SetFontSize(16)
            .SetBorder(Border.NO_BORDER)
            .SetTextAlignment(TextAlignment.RIGHT);

        Cell secondCell = new Cell()
            .Add(new Paragraph($"{staff.LastName} {staff.FirstName}")
                .SetFont(font))
                .SetFontSize(16)
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

    private void CreateTable(Document document, Laboratory laboratory)
    {
        if (laboratory.LaboratoryTableType == LaboratoryTableType.AnalysisOfFeces)
            CreateAnalysisOfFecesTable(document, laboratory.TableId);
        else if (laboratory.LaboratoryTableType == LaboratoryTableType.BiochemicalAnalysisOfBlood)
            CreateBiochemicalAnalysisOfBloodTable(document, laboratory.TableId);
        else if (laboratory.LaboratoryTableType == LaboratoryTableType.CommonAnalysisOfBlood)
            CreateCommonAnalysisOfBloodTable(document, laboratory.TableId);
        else if (laboratory.LaboratoryTableType == LaboratoryTableType.CommonAnalysisOfUrine)
            CreateCommonAnalysisOfUrineTable(document, laboratory.TableId);
        else if (laboratory.LaboratoryTableType == LaboratoryTableType.Torch)
            CreateTorchTable(document, laboratory.TableId);
        else
            throw new ArgumentIsNotValidException($"{nameof(laboratory.LaboratoryTableType)} table is not exists with id = {laboratory.TableId}");
    }
}