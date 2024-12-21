using HospitalApi.Domain.Entities;
using HospitalApi.WebApi.Configurations;
using iText.IO.Font;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.Globalization;

namespace HospitalApi.Service.Services.PdfGeneratorServices;

public partial class PdfGeneratorService
{
    #region
    private void CreateHeaderForTicket(Document document)
    {
        var logoPath = Path.Combine(EnvironmentHelper.WebRootPath, "Clinic\\turonlogo.png");
        var font = PdfFontFactory.CreateFont(_fontPath, PdfEncodings.IDENTITY_H);

        Table table = new Table(UnitValue.CreatePercentArray(new float[] { 1, 2 }))
                .SetWidth(UnitValue.CreatePercentValue(70))
                .SetHorizontalAlignment(HorizontalAlignment.CENTER);

        Image logo = new Image(ImageDataFactory.Create(logoPath))
            .SetWidth(50);

        Paragraph paragraph = new Paragraph($"Turon Tibbiyot Medical")
            .SetFont(font)
            .SetFontSize(14)
            .SetBold()
            .SetHorizontalAlignment(HorizontalAlignment.CENTER);

        table.AddCell(new Cell()
            .Add(logo)
            .SetBorder(Border.NO_BORDER)
            .SetBold()
            .SetVerticalAlignment(VerticalAlignment.MIDDLE));

        table.AddCell(new Cell()
            .Add(paragraph)
            .SetBorder(Border.NO_BORDER)
            .SetBold()
            .SetVerticalAlignment(VerticalAlignment.MIDDLE));

        document.Add(table);

        LineSeparator lineSeparator = new LineSeparator(new SolidLine(1));
        lineSeparator.SetWidth(UnitValue.CreatePercentValue(100));
        document.Add(new Paragraph("\n"));
        document.Add(lineSeparator);
    }
    #endregion

    #region
    private void CreateUserDetailsForTicket(Document document, User user)
    {
        Paragraph paragraph = new Paragraph("F.I")
            .SetFontColor(ColorConstants.GRAY)
            .SetFontSize(12)
            .SetMarginTop(10)
            .SetMarginBottom(0);

        Paragraph userDetails = new Paragraph($"{user.LastName} {user.FirstName}")
            .SetFontSize(14)
            .SetBold();

        document.Add(paragraph);
        document.Add(userDetails);
    }
    #endregion

    #region
    private void CreateTableForTicket(Document document, IEnumerable<MedicalServiceTypeHistory> histories)
    {
        PdfFont font = PdfFontFactory.CreateFont(_fontPath, PdfEncodings.IDENTITY_H);


        Paragraph paragraph = new Paragraph("Xizmatlar:")
            .SetFontColor(ColorConstants.GRAY)
            .SetFontSize(12)
            .SetMarginTop(10)
            .SetMarginBottom(0);
        document.Add(paragraph);

        foreach (var item in histories)
        {
            var table = CreateCellForTicket(item);

            document.Add(table);
        }
    }
    #endregion

    #region
    private void CreateFooterForTicket(PdfDocument pdf, Document document, double price, DateOnly date)
    {
        float pageHeight = pdf.GetLastPage().GetPageSize().GetHeight();
        float marginBottom = 40;
        float lineSpacing = 20;
        float bottomParagraphY = marginBottom;
        float secondParagraphY = marginBottom + lineSpacing;
        float thirdParagraphY = marginBottom + 20 + lineSpacing;


        Paragraph paragraph = new Paragraph("To'liq")
            .SetFontColor(ColorConstants.GRAY)
            .SetFontSize(12)
            .SetMarginTop(10)
            .SetMarginBottom(0);

        Paragraph dateParagraph = new Paragraph($"-Sanasi: {date.ToString("dd.MM.yyyy")}")
            .SetFontSize(14);

        Paragraph details = new Paragraph($"-To'liq hisob: {price.ToString("N0", new NumberFormatInfo() { NumberGroupSeparator = " " })} so'm")
            .SetFontSize(14)
            .SetBold();

        paragraph.SetFixedPosition(
            pdf.GetNumberOfPages(),
            document.GetLeftMargin(),
            thirdParagraphY,
            pdf.GetDefaultPageSize().GetWidth() - document.GetLeftMargin() - document.GetRightMargin()
        );

        dateParagraph.SetFixedPosition(
            pdf.GetNumberOfPages(),
            document.GetLeftMargin(),
            secondParagraphY,
            pdf.GetDefaultPageSize().GetWidth() - document.GetLeftMargin() - document.GetRightMargin()
        );

        details.SetFixedPosition(
            pdf.GetNumberOfPages(),
            document.GetLeftMargin(),
            marginBottom,
            pdf.GetDefaultPageSize().GetWidth() - document.GetLeftMargin() - document.GetRightMargin()
        );

        document.Add(paragraph);
        document.Add(dateParagraph);
        document.Add(details);
    }
    #endregion

    #region
    private Table CreateCellForTicket(MedicalServiceTypeHistory history)
    {
        PdfFont font = PdfFontFactory.CreateFont(_fontPath, PdfEncodings.IDENTITY_H);
        float borderWidth = 0.5f;

        Table table = new Table(UnitValue.CreatePercentArray(new float[] { 70, 30 }))
                .UseAllAvailableWidth()
                .SetBorder(new SolidBorder(DeviceGray.GRAY, borderWidth))
                .SetMarginBottom(10);

        // --- First Row ---
        Cell titleCell = new Cell()
            .Add(new Paragraph(history.MedicalServiceType.ServiceTypeTitle)
            .SetFont(font)
            .SetFontSize(14))
            .SetBold()
            .SetPadding(10)
            .SetBorder(Border.NO_BORDER);
        Cell queueCell = new Cell()
            .Add(new Paragraph($"{history.Queue}-Navbat")
            .SetFont(font)
            .SetFontSize(14)
            .SetBold()
            .SetTextAlignment(TextAlignment.RIGHT))
            .SetPadding(10)
            .SetBorder(Border.NO_BORDER);

        table.AddCell(titleCell);
        table.AddCell(queueCell);

        // --- Second Row ---
        Cell nameCell = new Cell()
            .Add(new Paragraph($"{history.MedicalServiceType.Staff.LastName} {history.MedicalServiceType.Staff.FirstName[0]}.")
            .SetFont(font)
            .SetFontSize(12))
            .SetPaddingLeft(10)
            .SetPaddingBottom(10)
            .SetBorder(Border.NO_BORDER);
        Cell amountCell = new Cell()
            .Add(new Paragraph($"{history.MedicalServiceType.Price.ToString("N0", new NumberFormatInfo() { NumberGroupSeparator = " " })} so'm")
            .SetFont(font)
            .SetFontSize(12)
            .SetTextAlignment(TextAlignment.RIGHT))
            .SetPaddingRight(10)
            .SetPaddingBottom(10)
            .SetBorder(Border.NO_BORDER);

        table.AddCell(nameCell);
        table.AddCell(amountCell);

        return table;
    }
    #endregion
}