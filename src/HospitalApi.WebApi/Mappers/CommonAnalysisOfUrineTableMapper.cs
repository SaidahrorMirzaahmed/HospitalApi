using HospitalApi.Domain.Entities.Tables;
using HospitalApi.WebApi.Models.Tables;
using System.ComponentModel;
using System.Diagnostics.Metrics;

namespace HospitalApi.WebApi.Mappers;

public class CommonAnalysisOfUrineTableMapper
{
    private readonly static Dictionary<int, CommonAnalysisOfUrineTableResultViewModel> _keyValuePairs = new Dictionary<int, CommonAnalysisOfUrineTableResultViewModel>
    {
        { 1, new () { Indicator = "Миқдори", Result = null, Standard = "60 мл" } },
        { 2, new () { Indicator = "Ранги", Result = null, Standard = "Сомон-сариқ" } },
        { 3, new () { Indicator = "Тиниқлиги", Result = null, Standard = "Тиниқ" } },
        { 4, new () { Indicator = "Уробилиноген", Result = null, Standard = "<3.3 мкмоль/л" } },
        { 5, new () { Indicator = "Билирубин", Result = null, Standard = "abs" } },
        { 6, new () { Indicator = "Кетон таначалари", Result = null, Standard = "abs" } },
        { 7, new () { Indicator = "Оқсил", Result = null, Standard = "abs" } },
        { 8, new () { Indicator = "Нитратлар", Result = null, Standard = "abs" } },
        { 9, new () { Indicator = "Глюкоза", Result = null, Standard = "abs" } },
        { 10, new () { Indicator = "Нисбий зичлиги", Result = null, Standard = "1012-1020" } },
        { 11, new () { Indicator = "рН", Result = null, Standard = "5.0-6.5" } },
        { 12, new () { Indicator = "Эпителий (Ясси)", Result = null, Standard = "0-5 кўрув май" } },
        { 13, new () { Indicator = "Эпителий (Ўтувчи)", Result = null, Standard = "0-5 кўрув май" } },
        { 14, new () { Indicator = "Эпителий (Буйрак)", Result = null, Standard = "abs" } },
        { 15, new () { Indicator = "Лейкоцитлар", Result = null, Standard = "Эрк: 18 кат 0-3, Аёл: 18 кат 0-10, Уғ. Бол: 18 кич 0-5, Қиз бола 18 ёш кич 0-7" } },
        { 16, new () { Indicator = "Эритроцитлар (Ўзгарган)", Result = null, Standard = "0-1 кўрув май" } },
        { 17, new () { Indicator = "Эритроцитлар (Ўзгармаган)", Result = null, Standard = "0-3 кўрув май" } },
        { 18, new () { Indicator = "Цилиндрлар (Гиалинли)", Result = null, Standard = "abs" } },
        { 19, new () { Indicator = "Цилиндрлар (Мумсимон)", Result = null, Standard = "abs" } },
        { 20, new () { Indicator = "Цилиндрлар (Донадор)", Result = null, Standard = "abs" } },
        { 21, new () { Indicator = "Цилиндрлар (Эритроцитар)", Result = null, Standard = "abs" } },
        { 22, new () { Indicator = "Цилиндрлар (Лейкоцитар)", Result = null, Standard = "abs" } },
        { 23, new () { Indicator = "Цилиндрлар (Эпителиал)", Result = null, Standard = "abs" } },
        { 24, new () { Indicator = "Шиллиқ", Result = null, Standard = "abs" } },
        { 25, new () { Indicator = "Тузлар", Result = null, Standard = "abs" } },
        { 26, new () { Indicator = "Бактериялар", Result = null, Standard = "abs" } },
    };

    public static CommonAnalysisOfUrineTableViewModel GetCommonAnalysisOfUrineTableView(CommonAnalysisOfUrineTable table)
    {
        var view = new Dictionary<int, CommonAnalysisOfUrineTableResultViewModel>(_keyValuePairs);

        foreach (var item in table.Items.Select(x => new { x.Index, x.Result }))
        {
            if (view.ContainsKey(item.Index))
            {
                view[item.Index].Result = item.Result;
            }
        }

        return new CommonAnalysisOfUrineTableViewModel
        {
            Id = table.Id,
            Items = view.Values,
        };
    }

    public static CommonAnalysisOfUrineTable CreateCommonAnalysisOfUrineTable(long id, CommonAnalysisOfUrineTableUpdateModel updateModel)
    {
        var items = new List<CommonAnalysisOfUrineTableResult>
        {
            new() { CommonAnalysisOfUrineTableId = id, Result = updateModel.FirstItemResult, Index = 1 },
            new() { CommonAnalysisOfUrineTableId = id, Result = updateModel.SecondItemResult, Index = 2 },
            new() { CommonAnalysisOfUrineTableId = id, Result = updateModel.ThirdItemResult, Index = 3 },
            new() { CommonAnalysisOfUrineTableId = id, Result = updateModel.FourthItemResult, Index = 4 },
            new() { CommonAnalysisOfUrineTableId = id, Result = updateModel.FifthItemResult, Index = 5 },
            new() { CommonAnalysisOfUrineTableId = id, Result = updateModel.SixthItemResult, Index = 6 },
            new() { CommonAnalysisOfUrineTableId = id, Result = updateModel.SeventhItemResult, Index = 7 },
            new() { CommonAnalysisOfUrineTableId = id, Result = updateModel.EighthItemResult, Index = 8 },
            new() { CommonAnalysisOfUrineTableId = id, Result = updateModel.NinthItemResult, Index = 9 },
            new() { CommonAnalysisOfUrineTableId = id, Result = updateModel.TenthItemResult, Index = 10 },
            new() { CommonAnalysisOfUrineTableId = id, Result = updateModel.EleventhItemResult, Index = 11 },
            new() { CommonAnalysisOfUrineTableId = id, Result = updateModel.TwelfthItemResult, Index = 12 },
            new() { CommonAnalysisOfUrineTableId = id, Result = updateModel.ThirteenthItemResult, Index = 13 },
            new() { CommonAnalysisOfUrineTableId = id, Result = updateModel.FourteenthItemResult, Index = 14 },
            new() { CommonAnalysisOfUrineTableId = id, Result = updateModel.FifteenthItemResult, Index = 15 },
            new() { CommonAnalysisOfUrineTableId = id, Result = updateModel.SixteenthItemResult, Index = 16 },
            new() { CommonAnalysisOfUrineTableId = id, Result = updateModel.SeventeenthItemResult, Index = 17 },
            new() { CommonAnalysisOfUrineTableId = id, Result = updateModel.EighteenthItemResult, Index = 18 },
            new() { CommonAnalysisOfUrineTableId = id, Result = updateModel.NineteenthItemResult, Index = 19 },
            new() { CommonAnalysisOfUrineTableId = id, Result = updateModel.TwentiethItemResult, Index = 20 },
            new() { CommonAnalysisOfUrineTableId = id, Result = updateModel.TwentyFirstItemResult, Index = 21 },
            new() { CommonAnalysisOfUrineTableId = id, Result = updateModel.TwentySecondItemResult, Index = 22 },
            new() { CommonAnalysisOfUrineTableId = id, Result = updateModel.TwentyThirdItemResult, Index = 23 },
            new() { CommonAnalysisOfUrineTableId = id, Result = updateModel.TwentyFourthItemResult, Index = 24 },
            new() { CommonAnalysisOfUrineTableId = id, Result = updateModel.TwentyFifthItemResult, Index = 25 },
            new() { CommonAnalysisOfUrineTableId = id, Result = updateModel.TwentySixthItemResult, Index = 26 }
        };

        return new CommonAnalysisOfUrineTable { Id = id, Items = items, };
    }
}