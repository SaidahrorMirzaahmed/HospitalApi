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
        var labTable = unitOfWork.AnalysisOfFecesTables.SelectAsync(entity => entity.Id == tableId && !entity.IsDeleted).GetAwaiter().GetResult();
        var dto = AnalysisOfFecesTableMapper.GetAnalysisOfFecesTableView(labTable);
        var table = new Table(UnitValue.CreatePercentArray(new float[] { 1, 8, 3, 4 }))
            .UseAllAvailableWidth();

        var borderWidth = 0.5f;
        var category = dto.Items.First().Category;
        var order = 1;
        var indicator = new string[] { "№", "Кўрсаткичлар", "Натижа", "Норма" };

        CreateCategoryNameForTable(document, category);
        CreateRawForTable(table, true, indicator);

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

                CreateCategoryNameForTable(document, category);
                CreateRawForTable(table, true, args: indicator);
            }

            var raw = new string[] { order.ToString(), item.Indicator, item.Result, item.Standard };
            CreateRawForTable(table, args: raw);

            order++;
        }

        document.Add(table);
    }

    private void CreateBiochemicalAnalysisOfBloodTable(Document document, long tableId)
    {
        var dtoTable = unitOfWork.BiochemicalAnalysisOfBloodTables.SelectAsync(entity => entity.Id == tableId && !entity.IsDeleted).GetAwaiter().GetResult();
        var dto = BiochemicalAnalysisOfBloodTableMapper.GetBiochemicalAnalysisOfBloodTableView(dtoTable);
        var table = new Table(UnitValue.CreatePercentArray(new float[] { 1, 8, 3, 3, 3 }))
            .UseAllAvailableWidth()
            .SetMarginTop(20);

        var borderWidth = 0.5f;
        var order = 1;
        var indicator = new string[] { "№", "Текширув", "Натижа", "Норма", "Бирлиги" };

        CreateRawForTable(table, true, indicator);

        foreach(var item in dto.Items)
        {
            var raw = new string[] { order.ToString(), item.CheckUp, item.Result, item.Standard, item.Unit };
            CreateRawForTable(table, args: raw);
            order++;
        }

        document.Add(table);
    }

    private void CreateCommonAnalysisOfBloodTable(Document document, long tableId)
    {
        var dtoTable = unitOfWork.CommonAnalysisOfBloodTables.SelectAsync(entity => entity.Id == tableId && !entity.IsDeleted).GetAwaiter().GetResult();
        var dto = CommonAnalysisOfBloodTableMapper.GetCommonAnalysisOfBloodTableView(dtoTable);
        var table = new Table(UnitValue.CreatePercentArray(new float[] { 1, 8, 3, 3, 3 }))
            .UseAllAvailableWidth()
            .SetMarginTop(20);

        var borderWidth = 0.5f;
        var order = 1;
        var indicator = new string[] { "№", "Кўрсаткич", "Натижа", "Норма", "СИ бирлиги" };

        CreateRawForTable(table, true, indicator);

        foreach (var item in dto.Items)
        {
            var raw = new string[] { order.ToString(), item.Indicator, item.Result, item.Standard, item.Unit };
            CreateRawForTable(table, args: raw);
            order++;
        }

        document.Add(table);
    }

    private void CreateCommonAnalysisOfUrineTable(Document document, long tableId)
    {
        var dtoTable = unitOfWork.CommonAnalysisOfUrineTable.SelectAsync(entity => entity.Id == tableId && !entity.IsDeleted).GetAwaiter().GetResult();
        var dto = CommonAnalysisOfUrineTableMapper.GetCommonAnalysisOfUrineTableView(dtoTable);
        var table = new Table(UnitValue.CreatePercentArray(new float[] { 1, 8, 4, 4 }))
            .UseAllAvailableWidth()
            .SetMarginTop(20);

        var borderWidth = 0.5f;
        var order = 1;
        var indicator = new string[] { "№", "Кўрсаткич", "Натижа", "Норма" };

        CreateRawForTable(table, true, indicator);

        foreach (var item in dto.Items)
        {
            var raw = new string[] { order.ToString(), item.Indicator, item.Result, item.Standard };
            CreateRawForTable(table, args: raw);
            order++;
        }

        document.Add(table);
    }

    private void CreateTorchTable(Document document, long tableId)
    {
        var dtoTable = unitOfWork.TorchTables.SelectAsync(entity => entity.Id == tableId && !entity.IsDeleted).GetAwaiter().GetResult();
        var dto = TorchMapper.GetTorchTableView(dtoTable);
        var table = new Table(UnitValue.CreatePercentArray(new float[] { 1, 8, 4, 4 }))
            .UseAllAvailableWidth()
            .SetMarginTop(20);

        var borderWidth = 0.5f;
        var order = 1;
        var indicator = new string[] { "№", "Текширув", "Натижа", "Норма" };

        CreateRawForTable(table, true, indicator);

        foreach (var item in dto.Items)
        {
            var raw = new string[] { order.ToString(), item.CheckUp, item.Result, item.Standard };
            CreateRawForTable(table, args: raw);
            order++;
        }

        document.Add(table);
    }

    #region
    private void CreateCategoryNameForTable(Document document, string content)
    {
        PdfFont font = PdfFontFactory.CreateFont(_fontPath, PdfEncodings.IDENTITY_H);

        var paragraph = new Paragraph(content)
                .SetFontColor(ColorConstants.RED)
                .SetFont(font)
                .SetFontSize(16)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetMarginTop(20);

        document
            .Add(paragraph);
    }

    private void CreateRawForTable(Table table, bool isHeader = false, params string[] args)
    {
        PdfFont font = PdfFontFactory.CreateFont(_fontPath, PdfEncodings.IDENTITY_H);

        foreach (string arg in args)
        {
            table.AddCell(CreateCellForTable(arg, isHeader));
        }
    }

    private Cell CreateCellForTable(string content, bool isHeader = false)
    {
        PdfFont font = PdfFontFactory.CreateFont(_fontPath, PdfEncodings.IDENTITY_H);

        Cell cell = new Cell().Add(new Paragraph(content is null ? "" : content)).SetFont(font).SetPaddingLeft(5).SetPaddingRight(5);

        if (isHeader)
            cell
                .SetBold()
                .SetBackgroundColor(new DeviceRgb(150, 190, 245))
                .SetTextAlignment(TextAlignment.CENTER);
        else
            cell.SetTextAlignment(TextAlignment.LEFT);


        return cell;
    }
    #endregion
}