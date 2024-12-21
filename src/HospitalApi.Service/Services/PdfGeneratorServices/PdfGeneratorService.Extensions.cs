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
    private void CreateHeader(Document document, int headerFontSize, int logoWidth = 85)
    {
        var logoPath = Path.Combine(EnvironmentHelper.WebRootPath, "Clinic\\turonlogo.png");
        PdfFont font = PdfFontFactory.CreateFont(_fontPath, PdfEncodings.IDENTITY_H);

        // Create a table with two columns for horizontal layout
        Table headerTable = new Table(UnitValue.CreatePercentArray(new float[] { 1, 1, 1 }))
            .UseAllAvailableWidth();

        // First column: Uzbek text
        Cell leftCell = CreateCell(font, headerFontSize, TextAlignment.CENTER, "Ўзбекистон Республикаси", "Наманган вилояти", "Косонсой тумани", "ТУРОН ТИББИЕТ МЧЖ")
            .SetFontColor(ColorConstants.BLUE);

        // Second column: Logo image
        Image logo = new Image(ImageDataFactory.Create(logoPath))
            .SetHorizontalAlignment(HorizontalAlignment.CENTER)
            .SetWidth(85);

        Cell middleCell = new Cell()
            .Add(logo)
            .SetBorder(Border.NO_BORDER)
            .SetTextAlignment(TextAlignment.CENTER)
            .SetVerticalAlignment(VerticalAlignment.MIDDLE);

        Cell rightCell = CreateCell(font, headerFontSize, TextAlignment.CENTER, "O’zbekiston Respublikasi", "Namangan viloyati", "Kosonsoy tumani", "TURON TIBBIYOT MCHJ")
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
    private void CreateLaboratoryServiceDetails(Document document, LaboratoryTableType tableType, int laboratoryDetailsFontSize)
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

        document.Add(CreateParagraph(font, laboratoryDetailsFontSize, TextAlignment.CENTER, tableName).SetFontColor(ColorConstants.RED));
    }
    #endregion

    #region
    private void CreateUserInfo(Document document, User client, int userInfoFontSize)
    {
        PdfFont font = PdfFontFactory.CreateFont(_fontPath, PdfEncodings.IDENTITY_H);
        var time = $"{DateOnly.FromDateTime(DateTime.UtcNow.AddHours(5))} й.";
        var name = $"Фамилия, исми   {client.LastName} {client.FirstName}, ёши   {DateOnly.FromDateTime(DateTime.UtcNow.AddHours(5)).Year - client.Birth.Year}";
        var address = $"Манзили   {client.Address}";

        document.Add(CreateParagraph(font, userInfoFontSize, TextAlignment.LEFT, time));
        document.Add(CreateParagraph(font, userInfoFontSize, TextAlignment.LEFT, name));
        document.Add(CreateParagraph(font, userInfoFontSize, TextAlignment.LEFT, address));
    }
    #endregion

    #region
    private void CreateFooter(PdfDocument pdf, Document document, User staff, int footerFontSize)
    {
        PdfFont font = PdfFontFactory.CreateFont(_fontPath, PdfEncodings.IDENTITY_H);
        float marginBottom = 40;

        Table table = new Table(UnitValue.CreatePercentArray(new float[] { 1, 3 }))
            .UseAllAvailableWidth();
        Cell firstCell = CreateCell(font, footerFontSize, TextAlignment.RIGHT, "Лаборант:");
        Cell secondCell = CreateCell(font, footerFontSize, TextAlignment.CENTER, $"{staff.LastName} {staff.FirstName}");

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

    private void CreateLaboratoryPdf(PdfDocument pdf, Document document, Laboratory laboratory)
    {
        if (laboratory.LaboratoryTableType == LaboratoryTableType.AnalysisOfFeces)
        {
            var fontSize = 12;
            CreateHeader(document, fontSize, 65);
            CreateLaboratoryServiceDetails(document, laboratory.LaboratoryTableType, fontSize);
            CreateUserInfo(document, laboratory.Client, fontSize);
            CreateAnalysisOfFecesTable(document, laboratory.TableId);
            CreateFooter(pdf, document, laboratory.Staff, fontSize);
        }
        else if (laboratory.LaboratoryTableType == LaboratoryTableType.BiochemicalAnalysisOfBlood)
        {
            var fontSize = 15;
            CreateHeader(document, 12);
            CreateLaboratoryServiceDetails(document, laboratory.LaboratoryTableType, fontSize);
            CreateUserInfo(document, laboratory.Client, fontSize);
            CreateBiochemicalAnalysisOfBloodTable(document, laboratory.TableId);
            CreateFooter(pdf, document, laboratory.Staff, fontSize);
        }
        else if (laboratory.LaboratoryTableType == LaboratoryTableType.CommonAnalysisOfBlood)
        {
            var fontSize = 14;
            CreateHeader(document, 12);
            CreateLaboratoryServiceDetails(document, laboratory.LaboratoryTableType, fontSize);
            CreateUserInfo(document, laboratory.Client, fontSize);
            CreateCommonAnalysisOfBloodTable(document, laboratory.TableId);
            CreateFooter(pdf, document, laboratory.Staff, fontSize);
        }
        else if (laboratory.LaboratoryTableType == LaboratoryTableType.CommonAnalysisOfUrine)
        {
            var fontSize = 14;
            CreateHeader(document, 12);
            CreateLaboratoryServiceDetails(document, laboratory.LaboratoryTableType, fontSize);
            CreateUserInfo(document, laboratory.Client, fontSize);
            CreateCommonAnalysisOfUrineTable(document, laboratory.TableId);
            CreateFooter(pdf, document, laboratory.Staff, fontSize);
        }
        else if (laboratory.LaboratoryTableType == LaboratoryTableType.Torch)
        {
            var fontSize = 17;
            CreateHeader(document, 12);
            CreateLaboratoryServiceDetails(document, laboratory.LaboratoryTableType, fontSize);
            CreateUserInfo(document, laboratory.Client, fontSize);
            CreateTorchTable(document, laboratory.TableId);
            CreateFooter(pdf, document, laboratory.Staff, fontSize);
        }
        else
            throw new ArgumentIsNotValidException($"{nameof(laboratory.LaboratoryTableType)} table is not exists with id = {laboratory.TableId}");
    }

    private Cell CreateCell(PdfFont font, int fontSize, TextAlignment alignment, params string[] content)
    {
        var cell = new Cell()
            .SetFont(font)
            .SetFontSize(fontSize)
            .SetTextAlignment(alignment)
            .SetBorder(Border.NO_BORDER);

        foreach (var item in content)
            cell.Add(new Paragraph(item));

        return cell;
    }

    private Paragraph CreateParagraph(PdfFont font, int fontSize, TextAlignment alignment, string content)
    {
        return new Paragraph(content)
            .SetFont(font)
            .SetFontSize(fontSize)
            .SetTextAlignment(alignment);
    }
}