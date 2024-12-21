using HospitalApi.Service.Mappers;
using iText.IO.Font;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace HospitalApi.Service.Services.PdfGeneratorServices;

public partial class PdfGeneratorService
{
    private void CreateAnalysisOfFecesTable(Document document, long tableId)
    {
        var font = PdfFontFactory.CreateFont(_fontPath, PdfEncodings.IDENTITY_H);
        var labTable = unitOfWork.AnalysisOfFecesTables.SelectAsync(entity => entity.Id == tableId && !entity.IsDeleted).GetAwaiter().GetResult();
        var dto = AnalysisOfFecesTableMapper.GetAnalysisOfFecesTableView(labTable);
        var table = new Table(UnitValue.CreatePercentArray(new float[] { 1, 8, 3, 4 }))
            .UseAllAvailableWidth();

        var borderWidth = 0.5f;
        var category = dto.Items.First().Category;
        var order = 1;
        var indicator = new List<(string indicator, TextAlignment alignment)>() { ("№", TextAlignment.CENTER), ("Кўрсаткичлар", TextAlignment.LEFT), ("Натижа", TextAlignment.CENTER), ("Норма", TextAlignment.CENTER) };

        document.Add(CreateCategoryNameForTable(category).SetMarginTop(0));
        CreateRawForTable(table, indicator, font, 10, true);

        foreach (var item in dto.Items)
        {
            if (!string.Equals(category, item.Category, StringComparison.OrdinalIgnoreCase))
            {
                document.Add(table);

                order = 1;
                category = item.Category;

                table = new Table(UnitValue.CreatePercentArray(new float[] { 1, 8, 3, 4 }))
                    .UseAllAvailableWidth()
                    .SetBorder(new SolidBorder(ColorConstants.GRAY, borderWidth));

                document.Add(CreateCategoryNameForTable(category));
                CreateRawForTable(table, indicator, font, 10, true);
            }

            var raw = new List<(string indicator, TextAlignment alignment)>() { (order.ToString(), TextAlignment.CENTER), (item.Indicator, TextAlignment.LEFT), (item.Result, TextAlignment.CENTER), (item.Standard, TextAlignment.CENTER) };
            CreateRawForTable(table, raw, font, 8);

            order++;
        }

        document.Add(table);
    }

    private void CreateBiochemicalAnalysisOfBloodTable(Document document, long tableId)
    {
        var font = PdfFontFactory.CreateFont(_fontPath, PdfEncodings.IDENTITY_H);
        var dtoTable = unitOfWork.BiochemicalAnalysisOfBloodTables.SelectAsync(entity => entity.Id == tableId && !entity.IsDeleted).GetAwaiter().GetResult();
        var dto = BiochemicalAnalysisOfBloodTableMapper.GetBiochemicalAnalysisOfBloodTableView(dtoTable);
        var table = new Table(UnitValue.CreatePercentArray(new float[] { 1, 8, 3, 3, 3 }))
            .UseAllAvailableWidth()
            .SetMarginTop(20);

        var borderWidth = 0.5f;
        var order = 1;
        var indicator = new List<(string indicator, TextAlignment alignment)>() { ("№", TextAlignment.CENTER), ("Текширув", TextAlignment.LEFT), ("Натижа", TextAlignment.CENTER), ("Норма", TextAlignment.CENTER), ("Бирлиги", TextAlignment.CENTER) };

        CreateRawForTable(table, indicator, font, 15, true);

        foreach (var item in dto.Items)
        {
            var raw = new List<(string indicator, TextAlignment alignment)>() { (order.ToString(), TextAlignment.CENTER), (item.CheckUp, TextAlignment.LEFT), (item.Result, TextAlignment.CENTER), (item.Standard, TextAlignment.CENTER), (item.Unit, TextAlignment.CENTER) };
            CreateRawForTable(table, raw, font, 12);
            order++;
        }

        document.Add(table);
    }

    private void CreateCommonAnalysisOfBloodTable(Document document, long tableId)
    {
        var font = PdfFontFactory.CreateFont(_fontPath, PdfEncodings.IDENTITY_H);
        var dtoTable = unitOfWork.CommonAnalysisOfBloodTables.SelectAsync(entity => entity.Id == tableId && !entity.IsDeleted).GetAwaiter().GetResult();
        var dto = CommonAnalysisOfBloodTableMapper.GetCommonAnalysisOfBloodTableView(dtoTable);
        var table = new Table(UnitValue.CreatePercentArray(new float[] { 1, 8, 3, 3, 3 }))
            .UseAllAvailableWidth()
            .SetMarginTop(20);

        var borderWidth = 0.5f;
        var order = 1;
        var indicator = new List<(string indicator, TextAlignment alignment)>() { ("№", TextAlignment.CENTER), ("Кўрсаткич", TextAlignment.LEFT), ("Натижа", TextAlignment.CENTER), ("Норма", TextAlignment.CENTER), ("СИ бирлиги", TextAlignment.CENTER) };

        CreateRawForTable(table, indicator, font, 12, true);

        foreach (var item in dto.Items)
        {
            var raw = new List<(string indicator, TextAlignment alignment)>() { (order.ToString(), TextAlignment.CENTER), (item.Indicator, TextAlignment.LEFT), (item.Result, TextAlignment.CENTER), (item.Standard, TextAlignment.CENTER), (item.Unit, TextAlignment.CENTER) };
            CreateRawForTable(table, raw, font, 9);
            order++;
        }

        document.Add(table);
    }

    private void CreateCommonAnalysisOfUrineTable(Document document, long tableId)
    {
        var font = PdfFontFactory.CreateFont(_fontPath, PdfEncodings.IDENTITY_H);
        var dtoTable = unitOfWork.CommonAnalysisOfUrineTable.SelectAsync(entity => entity.Id == tableId && !entity.IsDeleted).GetAwaiter().GetResult();
        var dto = CommonAnalysisOfUrineTableMapper.GetCommonAnalysisOfUrineTableView(dtoTable);
        var table = new Table(UnitValue.CreatePercentArray(new float[] { 1, 8, 4, 4 }))
            .UseAllAvailableWidth()
            .SetMarginTop(20);

        var borderWidth = 0.5f;
        var order = 1;
        var indicator = new List<(string indicator, TextAlignment alignment)>() { ("№", TextAlignment.CENTER), ("Кўрсаткич", TextAlignment.LEFT), ("Натижа", TextAlignment.CENTER), ("Норма", TextAlignment.CENTER) };

        CreateRawForTable(table, indicator, font, 12, true);

        foreach (var item in dto.Items)
        {
            var raw = new List<(string indicator, TextAlignment alignment)>() { (order.ToString(), TextAlignment.CENTER), (item.Indicator, TextAlignment.LEFT), (item.Result, TextAlignment.CENTER), (item.Standard, TextAlignment.CENTER) };
            CreateRawForTable(table, raw, font, 8);
            order++;
        }

        document.Add(table);
    }

    private void CreateTorchTable(Document document, long tableId)
    {
        var font = PdfFontFactory.CreateFont(_fontPath, PdfEncodings.IDENTITY_H);
        var dtoTable = unitOfWork.TorchTables.SelectAsync(entity => entity.Id == tableId && !entity.IsDeleted).GetAwaiter().GetResult();
        var dto = TorchMapper.GetTorchTableView(dtoTable);
        var table = new Table(UnitValue.CreatePercentArray(new float[] { 1, 8, 4, 4 }))
            .UseAllAvailableWidth()
            .SetMarginTop(20);

        var borderWidth = 0.5f;
        var order = 1;
        var indicator = new List<(string indicator, TextAlignment alignment)>(){ ("№", TextAlignment.CENTER), ("Текширув", TextAlignment.LEFT), ("Натижа", TextAlignment.CENTER), ("Норма", TextAlignment.CENTER) };

        CreateRawForTable(table, indicator, font, 15, true);

        foreach (var item in dto.Items)
        {
            var raw = new List<(string indicator, TextAlignment alignment)>() { (order.ToString(), TextAlignment.CENTER), (item.CheckUp, TextAlignment.LEFT), (item.Result, TextAlignment.CENTER), (item.Standard, TextAlignment.CENTER) };
            CreateRawForTable(table, raw, font, 15);
            order++;
        }

        document.Add(table);
    }

    #region
    private Paragraph CreateCategoryNameForTable(string content)
    {
        PdfFont font = PdfFontFactory.CreateFont(_fontPath, PdfEncodings.IDENTITY_H);

        return new Paragraph(content)
                .SetFontColor(ColorConstants.RED)
                .SetFont(font)
                .SetFontSize(12)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetMarginTop(10);
    }

    private void CreateRawForTable(Table table, IEnumerable<(string indicator, TextAlignment alignment)> values, PdfFont font, int fontSize, bool isHeader = false)
    {
        foreach (var value in values)
        {
            table.AddCell(CreateCellForTable(value.indicator, font, fontSize, isHeader, value.alignment));
        }
    }

    private Cell CreateCellForTable(string content, PdfFont font, int fontSize ,bool isHeader = false, TextAlignment alignment = TextAlignment.LEFT)
    {
        Cell cell = new Cell().Add(new Paragraph(content is null ? "" : content)).SetFont(font).SetFontSize(fontSize).SetPaddingLeft(3).SetPaddingRight(3);

        if (isHeader)
            cell
                .SetBold()
                .SetBackgroundColor(new DeviceRgb(150, 190, 245))
                .SetTextAlignment(TextAlignment.CENTER);
        else
            cell.SetTextAlignment(alignment);


        return cell;
    }
    #endregion
}