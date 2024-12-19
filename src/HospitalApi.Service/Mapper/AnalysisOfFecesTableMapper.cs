using HospitalApi.Domain.Entities.Tables;
using HospitalApi.Service.Models;

namespace HospitalApi.Service.Mappers;

public class AnalysisOfFecesTableMapper
{
    private readonly static Dictionary<int, AnalysisOfFecesTableResultDto> _keyValuePairs = new Dictionary<int, AnalysisOfFecesTableResultDto>
    {
        // Макроскопия
        { 1, new () { Category = "Макроскопия", Indicator = "Миқдори", Result = null, Standard = "Қаттиқ" } },
        { 2, new () { Category = "Макроскопия", Indicator = "Консистенцияси", Result = null, Standard = "Шаклланган" } },
        { 3, new () { Category = "Макроскопия", Indicator = "Шакли", Result = null, Standard = "Жиғарранг" } },
        { 4, new () { Category = "Макроскопия", Indicator = "Ранги", Result = null, Standard = "abs" } },
        { 5, new () { Category = "Макроскопия", Indicator = "Шиллиқ", Result = null, Standard = "abs" } },
        { 6, new () { Category = "Макроскопия", Indicator = "Қон", Result = null, Standard = "abs" } },
    
        // Биохимия
        { 7, new () { Category = "Биохимия", Indicator = "pH", Result = null, Standard = "6-8" } },
        { 8, new () { Category = "Биохимия", Indicator = "Билирубин", Result = null, Standard = "abs" } },
        { 9, new () { Category = "Биохимия", Indicator = "Нейтрал ёғ", Result = null, Standard = "abs" } },
        { 10, new () { Category = "Биохимия", Indicator = "Ёғ кислoталари", Result = null, Standard = "abs" } },
        { 11, new () { Category = "Биохимия", Indicator = "Совун", Result = null, Standard = "Оз миқдорда" } },
        { 12, new () { Category = "Биохимия", Indicator = "Крахмал", Result = null, Standard = "abs" } },
    
        // Микроскопия
        { 13, new () { Category = "Микроскопия", Indicator = "Мушак толалари", Result = null, Standard = "Ҳазм бўлган" } },
        { 14, new () { Category = "Микроскопия", Indicator = "Бириктирувчи тўқима", Result = null, Standard = "Ҳазм бўлмаган" } },
        { 15, new () { Category = "Микроскопия", Indicator = "Йодофил флора", Result = null, Standard = "abs" } },
        { 16, new () { Category = "Микроскопия", Indicator = "Ўсимлик толаси (Ҳазм бўлган)", Result = null, Standard = "abs" } },
        { 17, new () { Category = "Микроскопия", Indicator = "Ўсимлик толаси (Ҳазм бўлмаган)", Result = null, Standard = "abs" } },
        { 18, new () { Category = "Микроскопия", Indicator = "Эритроцитлар", Result = null, Standard = "abs" } },
        { 19, new () { Category = "Микроскопия", Indicator = "Лейкоцитлар", Result = null, Standard = "abs" } },
        { 20, new () { Category = "Микроскопия", Indicator = "Гижжа тухумлари", Result = null, Standard = "abs" } },
        { 21, new () { Category = "Микроскопия", Indicator = "Сода ҳайвонлар", Result = null, Standard = "abs" } },
        { 22, new () { Category = "Микроскопия", Indicator = "Замбуруғлар", Result = null, Standard = "-" } },
    };

    public static AnalysisOfFecesTableDto GetAnalysisOfFecesTableView(AnalysisOfFecesTable table)
    {
        var view = new Dictionary<int, AnalysisOfFecesTableResultDto>(_keyValuePairs);

        foreach (var item in table.Items.Select(x => new { x.Index, x.Result }))
        {
            if (view.ContainsKey(item.Index))
            {
                view[item.Index].Result = item.Result;
            }
        }

        return new AnalysisOfFecesTableDto { Id = table.Id, Items = view.Values };
    }

    public static AnalysisOfFecesTable CreateAnalysisOfFecesTable(long id, AnalysisOfFecesTableUpdateDto model)
    {
        var items = new List<AnalysisOfFecesTableResult>
        {
            new () { AnalysisOfFecesTableId = id, Result = model.FirstItemResult, Index = 1 },
            new () { AnalysisOfFecesTableId = id, Result = model.SecondItemResult, Index = 2 },
            new () { AnalysisOfFecesTableId = id, Result = model.ThirdItemResult, Index = 3 },
            new () { AnalysisOfFecesTableId = id, Result = model.FourthItemResult, Index = 4 },
            new () { AnalysisOfFecesTableId = id, Result = model.FifthItemResult, Index = 5 },
            new () { AnalysisOfFecesTableId = id, Result = model.SixthItemResult, Index = 6 },
            new () { AnalysisOfFecesTableId = id, Result = model.SeventhItemResult, Index = 7 },
            new () { AnalysisOfFecesTableId = id, Result = model.EighthItemResult, Index = 8 },
            new () { AnalysisOfFecesTableId = id, Result = model.NinthItemResult, Index = 9 },
            new () { AnalysisOfFecesTableId = id, Result = model.TenthItemResult, Index = 10 },
            new () { AnalysisOfFecesTableId = id, Result = model.EleventhItemResult, Index = 11 },
            new () { AnalysisOfFecesTableId = id, Result = model.TwelfthItemResult, Index = 12 },
            new () { AnalysisOfFecesTableId = id, Result = model.ThirteenthItemResult, Index = 13 },
            new () { AnalysisOfFecesTableId = id, Result = model.FourteenthItemResult, Index = 14 },
            new () { AnalysisOfFecesTableId = id, Result = model.FifteenthItemResult, Index = 15 },
            new () { AnalysisOfFecesTableId = id, Result = model.SixteenthItemResult, Index = 16 },
            new () { AnalysisOfFecesTableId = id, Result = model.SeventeenthItemResult, Index = 17 },
            new () { AnalysisOfFecesTableId = id, Result = model.EighteenthItemResult, Index = 18 },
            new () { AnalysisOfFecesTableId = id, Result = model.NineteenthItemResult, Index = 19 },
            new () { AnalysisOfFecesTableId = id, Result = model.TwentiethItemResult, Index = 20 },
            new () { AnalysisOfFecesTableId = id, Result = model.TwentyFirstItemResult, Index = 21 },
            new () { AnalysisOfFecesTableId = id, Result = model.TwentySecondItemResult, Index = 22 },
        };

        return new AnalysisOfFecesTable { Id = id, Items = items };
    }
}