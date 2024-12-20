using HospitalApi.Domain.Entities.Tables;
using HospitalApi.Service.Models;

namespace HospitalApi.Service.Mappers;

public class CommonAnalysisOfBloodTableMapper
{
    #region
    private readonly static Dictionary<int, CommonAnalysisOfBloodTableResultDto> _keyValuePairs = new Dictionary<int, CommonAnalysisOfBloodTableResultDto>
    {
        { 1, new () { Indicator = "Гемоглобин (Hb)", Result = null, Standard = "Э: 130.0-160.0, А: 120.0-140.0", Unit = "г/л" } },
        { 2, new () { Indicator = "Эритроцитлар (RBC)", Result = null, Standard = "Э: 4.0-5.0, А: 3.9-4.7", Unit = "10¹²/л" } },
        { 3, new () { Indicator = "Ранг кўрсаткичи", Result = null, Standard = "0.85-1.05", Unit = null } },
        { 4, new () { Indicator = "Эритроцитлар ўртача ҳажми (MCV)", Result = null, Standard = "80-100", Unit = "Мкм³" } },
        { 5, new () { Indicator = "1 дона эритроцитдаги гемоглобиннинг миқдори (MCH)", Result = null, Standard = "30-35", Unit = "пг" } },
        { 6, new () { Indicator = "Эритроцитдаги гемоглобин концентрацияси (MCHC)", Result = null, Standard = "320-360", Unit = "г/л" } },
        { 7, new () { Indicator = "Эритроцитар анизоцитоз (RDW-CV)", Result = null, Standard = "11.5-14.5", Unit = "%" } },
        { 8, new () { Indicator = "Гематокрит (HCT)", Result = null, Standard = "Э: 35-49, А: 32-45", Unit = "%" } },
        { 9, new () { Indicator = "Тромбоцитлар (PLT)", Result = null, Standard = "180.0-320.0", Unit = "10⁹/л" } },
        { 10, new () { Indicator = "Тромбоцитлар ўртача ҳажми (MPV)", Result = null, Standard = "3.6-9.4", Unit = "Мкм³" } },
        { 11, new () { Indicator = "Тромбоцитар анизоцитоз (RDW)", Result = null, Standard = "1-20", Unit = "%" } },
        { 12, new () { Indicator = "Тромбокрит (RST)", Result = null, Standard = "0.15-0.45", Unit = "%" } },
        { 13, new () { Indicator = "Лейкоцитлар (WBC)", Result = null, Standard = "4.0-9.0", Unit = "10⁹/л" } },
        { 14, new () { Indicator = "Миелоцитлар", Result = null, Standard = "-", Unit = null } },
        { 15, new () { Indicator = "Метамиелоцитлар", Result = null, Standard = "-", Unit = null } },
        { 16, new () { Indicator = "Таёқча ядроли нейтрофил", Result = null, Standard = "1-6", Unit = "%" } },
        { 17, new () { Indicator = "Сегмент ядроли нейтрофил", Result = null, Standard = "47-72", Unit = "%" } },
        { 18, new () { Indicator = "Эозинофиллар", Result = null, Standard = "0.5-5", Unit = "%" } },
        { 19, new () { Indicator = "Базофиллар", Result = null, Standard = "0-1", Unit = "%" } },
        { 20, new () { Indicator = "Моноцитлар", Result = null, Standard = "3-11", Unit = "%" } },
        { 21, new () { Indicator = "Лимфоцитлар", Result = null, Standard = "19-37", Unit = "%" } },
        { 22, new () { Indicator = "Плазматик ҳужайралар", Result = null, Standard = "-", Unit = null } },
        { 23, new () { Indicator = "Эритроцитнинг чўкиш тезлиги (ЭЧТ)", Result = null, Standard = "Э: 2-10, А: 2-15", Unit = "Мм/соат" } },
    };
    #endregion

    public static CommonAnalysisOfBloodTableDto GetCommonAnalysisOfBloodTableView(CommonAnalysisOfBloodTable table)
    {
        var view = new Dictionary<int, CommonAnalysisOfBloodTableResultDto>(_keyValuePairs);

        foreach (var item in table.Items.Select(x => new { x.Index, x.Result }))
        {
            if (view.ContainsKey(item.Index))
            {
                view[item.Index].Result = item.Result;
            }
        }

        return new CommonAnalysisOfBloodTableDto
        {
            Id = table.Id,
            Items = view.Values,
        };
    }

    public static CommonAnalysisOfBloodTable CreateCommonAnalysisOfBloodTable(long id, CommonAnalysisOfBloodTableUpdateDto update)
    {
        var items = new List<CommonAnalysisOfBloodTableResult>
        {
            new() { CommonAnalysisOfBloodTableId = id, Result = update.FirstItemResult, Index = 1 },
            new() { CommonAnalysisOfBloodTableId = id, Result = update.SecondItemResult, Index = 2 },
            new() { CommonAnalysisOfBloodTableId = id, Result = update.ThirdItemResult, Index = 3 },
            new() { CommonAnalysisOfBloodTableId = id, Result = update.FourthItemResult, Index = 4 },
            new() { CommonAnalysisOfBloodTableId = id, Result = update.FifthItemResult, Index = 5 },
            new() { CommonAnalysisOfBloodTableId = id, Result = update.SixthItemResult, Index = 6 },
            new() { CommonAnalysisOfBloodTableId = id, Result = update.SeventhItemResult, Index = 7 },
            new() { CommonAnalysisOfBloodTableId = id, Result = update.EighthItemResult, Index = 8 },
            new() { CommonAnalysisOfBloodTableId = id, Result = update.NinthItemResult, Index = 9 },
            new() { CommonAnalysisOfBloodTableId = id, Result = update.TenthItemResult, Index = 10 },
            new() { CommonAnalysisOfBloodTableId = id, Result = update.EleventhItemResult, Index = 11 },
            new() { CommonAnalysisOfBloodTableId = id, Result = update.TwelfthItemResult, Index = 12 },
            new() { CommonAnalysisOfBloodTableId = id, Result = update.ThirteenthItemResult, Index = 13 },
            new() { CommonAnalysisOfBloodTableId = id, Result = update.FourteenthItemResult, Index = 14 },
            new() { CommonAnalysisOfBloodTableId = id, Result = update.FifteenthItemResult, Index = 15 },
            new() { CommonAnalysisOfBloodTableId = id, Result = update.SixteenthItemResult, Index = 16 },
            new() { CommonAnalysisOfBloodTableId = id, Result = update.SeventeenthItemResult, Index = 17 },
            new() { CommonAnalysisOfBloodTableId = id, Result = update.EighteenthItemResult, Index = 18 },
            new() { CommonAnalysisOfBloodTableId = id, Result = update.NineteenthItemResult, Index = 19 },
            new() { CommonAnalysisOfBloodTableId = id, Result = update.TwentiethItemResult, Index = 20 },
            new() { CommonAnalysisOfBloodTableId = id, Result = update.TwentyFirstItemResult, Index = 21 },
            new() { CommonAnalysisOfBloodTableId = id, Result = update.TwentySecondItemResult, Index = 22 },
            new() { CommonAnalysisOfBloodTableId = id, Result = update.TwentyThirdItemResult, Index = 23 },

        };

        return new CommonAnalysisOfBloodTable { Id = id, Items = items };
    }
}