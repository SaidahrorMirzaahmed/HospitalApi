using HospitalApi.Domain.Entities.Tables;
using HospitalApi.Service.Models;

namespace HospitalApi.Service.Mappers;

public class BiochemicalAnalysisOfBloodTableMapper
{
    private readonly static Dictionary<int, BiochemicalAnalysisOfBloodTableResultDto> _keyValuePairs = new Dictionary<int, BiochemicalAnalysisOfBloodTableResultDto>
{
    { 1, new () { CheckUp = "Билирубин (Умумий)", Result = null, Standard = "3.4-20.5", Unit = "мкмоль/л" } },
    { 2, new () { CheckUp = "Билирубин (Боғланмаган)", Result = null, Standard = "1.7-17.1", Unit = "мкмоль/л" } },
    { 3, new () { CheckUp = "Билирубин (Боғланган)", Result = null, Standard = "0.86-5.3", Unit = "мкмоль/л" } },
    { 4, new () { CheckUp = "АлТ", Result = null, Standard = "<40", Unit = "Ед/моль" } },
    { 5, new () { CheckUp = "АсТ", Result = null, Standard = "<35", Unit = "Ед/моль" } },
    { 6, new () { CheckUp = "Қондагы қанд микдори", Result = null, Standard = "3.2-6.1", Unit = "ммоль/л" } },
    { 7, new () { CheckUp = "Умумий оқсил", Result = null, Standard = "3 ёш 46-70, 3 ёш 66-85", Unit = "г/л" } },
    { 8, new () { CheckUp = "Мочевина", Result = null, Standard = "2.5-8.3", Unit = "ммоль/л" } },
    { 9, new () { CheckUp = "Креатинин", Result = null, Standard = "Э: 44-115, А: 44-97", Unit = "мкмоль/л" } },
    { 10, new () { CheckUp = "Қондагы кальций (Ca²⁺)", Result = null, Standard = "2.0-2.6", Unit = "ммоль/л" } },
    { 11, new () { CheckUp = "ПТИ (Протромбин индекси)", Result = null, Standard = "80-90", Unit = "%" } },
    { 12, new () { CheckUp = "Холестерин (Умумий)", Result = null, Standard = "<5.2", Unit = "ммоль/л" } },
    { 13, new () { CheckUp = "РФ (Ревматоид фактор)", Result = null, Standard = "Манфий", Unit = null } },
    { 14, new () { CheckUp = "СРБ (С реактив белок)", Result = null, Standard = "Манфий", Unit = null } },
    { 15, new () { CheckUp = "Серомукоид", Result = null, Standard = "16-20", Unit = "Ед" } },
    { 16, new () { CheckUp = "ASLO (Антистрептолизин)", Result = null, Standard = "Манфий", Unit = null } },
};

    public static BiochemicalAnalysisOfBloodTableDto GetBiochemicalAnalysisOfBloodTableView(BiochemicalAnalysisOfBloodTable table)
    {
        var view = new Dictionary<int, BiochemicalAnalysisOfBloodTableResultDto>(_keyValuePairs);

        foreach (var item in table.Items.Select(x => new { x.Index, x.Result }))
        {
            if (view.ContainsKey(item.Index))
            {
                view[item.Index].Result = item.Result;
            }
        }

        return new BiochemicalAnalysisOfBloodTableDto
        {
            Id = table.Id,
            Items = view.Values
        };
    }

    public static BiochemicalAnalysisOfBloodTable CreateBiochemicalAnalysisOfBloodTable(long id, BiochemicalAnalysisOfBloodTableUpdateDto updateModel)
    {
        var items = new List<BiochemicalAnalysisOfBloodTableResult>
    {
        new() { BiochemicalAnalysisOfBloodTableId = id, Result = updateModel.FirstItemResult, Index = 1 },
        new() { BiochemicalAnalysisOfBloodTableId = id, Result = updateModel.SecondItemResult, Index = 2 },
        new() { BiochemicalAnalysisOfBloodTableId = id, Result = updateModel.ThirdItemResult, Index = 3 },
        new() { BiochemicalAnalysisOfBloodTableId = id, Result = updateModel.FourthItemResult, Index = 4 },
        new() { BiochemicalAnalysisOfBloodTableId = id, Result = updateModel.FifthItemResult, Index = 5 },
        new() { BiochemicalAnalysisOfBloodTableId = id, Result = updateModel.SixthItemResult, Index = 6 },
        new() { BiochemicalAnalysisOfBloodTableId = id, Result = updateModel.SeventhItemResult, Index = 7 },
        new() { BiochemicalAnalysisOfBloodTableId = id, Result = updateModel.EighthItemResult, Index = 8 },
        new() { BiochemicalAnalysisOfBloodTableId = id, Result = updateModel.NinthItemResult, Index = 9 },
        new() { BiochemicalAnalysisOfBloodTableId = id, Result = updateModel.TenthItemResult, Index = 10 },
        new() { BiochemicalAnalysisOfBloodTableId = id, Result = updateModel.EleventhItemResult, Index = 11 },
        new() { BiochemicalAnalysisOfBloodTableId = id, Result = updateModel.TwelfthItemResult, Index = 12 },
        new() { BiochemicalAnalysisOfBloodTableId = id, Result = updateModel.ThirteenthItemResult, Index = 13 },
        new() { BiochemicalAnalysisOfBloodTableId = id, Result = updateModel.FourteenthItemResult, Index = 14 },
        new() { BiochemicalAnalysisOfBloodTableId = id, Result = updateModel.FifteenthItemResult, Index = 15 },
        new() { BiochemicalAnalysisOfBloodTableId = id, Result = updateModel.SixteenthItemResult, Index = 16 }
    };


        return new BiochemicalAnalysisOfBloodTable { Id = id, Items = items, };
    }
}