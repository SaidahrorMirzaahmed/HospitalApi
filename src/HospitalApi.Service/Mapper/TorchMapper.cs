using HospitalApi.Domain.Entities.Tables;
using HospitalApi.Service.Models;

namespace HospitalApi.Service.Mappers;

public static class TorchMapper
{
    #region
    private readonly static Dictionary<int, TorchTableResultDto> _keyValuePairs = new Dictionary<int, TorchTableResultDto>
    {
        {1, new TorchTableResultDto { CheckUp = "TOX IgG (токсоплазма)", Result = null, Standard = "Манфий" }},
        {2, new TorchTableResultDto { CheckUp = "TOX IgM (токсоплазма)", Result = null, Standard = "Манфий" }},
        {3, new TorchTableResultDto { CheckUp = "RV IgG (рубелла)", Result = null, Standard = "Манфий" }},
        {4, new TorchTableResultDto { CheckUp = "RV IgM (рубелла)", Result = null, Standard = "Манфий" }},
        {5, new TorchTableResultDto { CheckUp = "CMV IgG (цитомегаловирус)", Result = null, Standard = "Манфий" }},
        {6, new TorchTableResultDto { CheckUp = "CMV IgM (цитомегаловирус)", Result = null, Standard = "Манфий" }},
        {7, new TorchTableResultDto { CheckUp = "HSV-1 IgG (герпес 1 тип)", Result = null, Standard = "Манфий" }},
        {8, new TorchTableResultDto { CheckUp = "HSV-1 IgM (герпес 1 тип)", Result = null, Standard = "Манфий" }},
        {9, new TorchTableResultDto { CheckUp = "HSV-2 IgG (герпес 2 тип)", Result = null, Standard = "Манфий" }},
        {10, new TorchTableResultDto { CheckUp = "HSV-2 IgM (герпес 2 тип)", Result = null, Standard = "Манфий" }},
    };
    #endregion

    public static TorchTableDto GetTorchTableView(TorchTable torchTable)
    {
        var view = new Dictionary<int, TorchTableResultDto>(_keyValuePairs);

        foreach (var item in torchTable.Items.Select(x => new { x.Index, x.Result }))
        {
            if (view.ContainsKey(item.Index))
            {
                view[item.Index].Result = item.Result;
            }
        }

        return new TorchTableDto
        {
            Id = torchTable.Id,
            Items = view.Values,
        };
    }

    public static TorchTable CreateTorchTable(long id, TorchTableUpdateDto update)
    {
        var items = new List<TorchTableResult>
        {
            new TorchTableResult { TorchTableId = id, Result = update.FirstItemResult, Index = 1 },
            new TorchTableResult { TorchTableId = id, Result = update.SecondItemResult, Index = 2 },
            new TorchTableResult { TorchTableId = id, Result = update.ThirdItemResult, Index = 3 },
            new TorchTableResult { TorchTableId = id, Result = update.FourthItemResult, Index = 4 },
            new TorchTableResult { TorchTableId = id, Result = update.FirstItemResult, Index = 5 },
            new TorchTableResult { TorchTableId = id, Result = update.SixthItemResult, Index = 6 },
            new TorchTableResult { TorchTableId = id, Result = update.SecondItemResult, Index = 7 },
            new TorchTableResult { TorchTableId = id, Result = update.EighthItemResult, Index = 8 },
            new TorchTableResult { TorchTableId = id, Result = update.NinthItemResult, Index = 9 },
            new TorchTableResult { TorchTableId = id, Result = update.TenthItemResult, Index = 10 },
        };

        return new TorchTable { Id = id, Items = items };
    }
}