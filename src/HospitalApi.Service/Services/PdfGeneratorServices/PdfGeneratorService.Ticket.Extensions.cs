using HospitalApi.Domain.Entities;
using iText.IO.Font;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace HospitalApi.Service.Services.PdfGeneratorServices;

public partial class PdfGeneratorService
{

    private void CreateHeaderForTicket(Document document)
    {
        var logoPath = "D:\\F\\codysoftlogo.jpg";
        var font = PdfFontFactory.CreateFont(_fontPath, PdfEncodings.IDENTITY_H);

        Table table = new Table(UnitValue.CreatePercentArray(new float[] { 1, 2 }))
                .SetWidth(UnitValue.CreatePercentValue(50)) // Adjust the width as needed
                .SetHorizontalAlignment(HorizontalAlignment.CENTER);

        iText.Layout.Element.Image logo = new iText.Layout.Element.Image(ImageDataFactory.Create(logoPath))
            .SetWidth(50); // Adjust size as needed

        Paragraph paragraph = new Paragraph($"Cody Soft")
            .SetFont(font)
            .SetFontSize(14) // Font size
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
        document.Add(new Paragraph("\n")); // Add some spacing
        document.Add(lineSeparator);
    }

    private void CreateUserDetailsForTicket(Document document, User user)
    {
        Paragraph paragraph = new Paragraph("F.I")
            .SetFontColor(ColorConstants.GRAY)
            .SetFontSize(12)
            .SetMarginTop(10)
            .SetMarginBottom(0);

        Paragraph userDetails = new Paragraph($"{user.LastName} {user.FirstName}")
            .SetFontSize(20);

        document.Add(paragraph);
        document.Add(userDetails);
    }

    private void CreateTableForTicket(Document document, IEnumerable<MedicalServiceTypeHistory> histories)
    {
        PdfFont boldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
        PdfFont regularFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);


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

    private void CreateFooterForTicket(PdfDocument pdf, Document document, double price)
    {
        float pageHeight = pdf.GetLastPage().GetPageSize().GetHeight();
        float marginBottom = 40; // Distance from bottom edge
        float lineSpacing = 20;  // Spacing between two paragraphs
        float bottomParagraphY = marginBottom; // Y-coordinate for the first paragraph
        float secondParagraphY = marginBottom + 20 + lineSpacing; // Y-coordinate for the second paragraph (above the first)

        Paragraph paragraph = new Paragraph("To'liq")
            .SetFontColor(ColorConstants.GRAY)
            .SetFontSize(12)
            .SetMarginTop(10)
            .SetMarginBottom(0);

        Paragraph details = new Paragraph($"-To'liq hisob: {price} so'm")
            .SetFontSize(20);

        paragraph.SetFixedPosition(
            pdf.GetNumberOfPages(), // Page number
            document.GetLeftMargin(), // X-coordinate (left margin)
            secondParagraphY, // Y-coordinate (from bottom of the page)
            pdf.GetDefaultPageSize().GetWidth() - document.GetLeftMargin() - document.GetRightMargin() // Width
        );

        details.SetFixedPosition(
            pdf.GetNumberOfPages(), // Page number
            document.GetLeftMargin(), // X-coordinate (left margin)
            marginBottom, // Y-coordinate
            pdf.GetDefaultPageSize().GetWidth() - document.GetLeftMargin() - document.GetRightMargin() // Width
        );

        document.Add(paragraph);
        document.Add(details);
    }

    private Table CreateCellForTicket(MedicalServiceTypeHistory history)
    {
        PdfFont boldFont = PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA_BOLD);
        PdfFont regularFont = PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA);
        float borderWidth = 0.5f;

        Table table = new Table(UnitValue.CreatePercentArray(new float[] { 70, 30 }))
                .UseAllAvailableWidth()
                .SetBorder(new SolidBorder(DeviceGray.GRAY, borderWidth))
                .SetMarginBottom(10);

        // --- First Row ---
        Cell titleCell = new Cell()
            .Add(new Paragraph(history.MedicalServiceType.ServiceType)
            .SetFont(boldFont)
            .SetFontSize(14))
            .SetPadding(10)
            .SetBorder(Border.NO_BORDER);
        Cell queueCell = new Cell()
            .Add(new Paragraph($"{history.Queue}-Navbat")
            .SetFont(boldFont)
            .SetFontSize(14)
            .SetTextAlignment(TextAlignment.RIGHT))
            .SetPadding(10)
            .SetBorder(Border.NO_BORDER);

        table.AddCell(titleCell);
        table.AddCell(queueCell);

        // --- Second Row ---
        Cell nameCell = new Cell()
            .Add(new Paragraph($"{history.Client.LastName} {history.Client.FirstName[0]}")
            .SetFont(regularFont)
            .SetFontSize(12))
            .SetPaddingLeft(10)
            .SetPaddingBottom(10)
            .SetBorder(Border.NO_BORDER);
        Cell amountCell = new Cell()
            .Add(new Paragraph($"{history.MedicalServiceType.Price} so'm")
            .SetFont(regularFont)
            .SetFontSize(12)
            .SetTextAlignment(TextAlignment.RIGHT))
            .SetPaddingRight(10)
            .SetPaddingBottom(10)
            .SetBorder(Border.NO_BORDER);

        table.AddCell(nameCell);
        table.AddCell(amountCell);

        return table;
    }
}