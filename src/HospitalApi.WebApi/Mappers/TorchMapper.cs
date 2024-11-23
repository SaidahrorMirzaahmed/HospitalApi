using HospitalApi.Domain.Entities.Tables;
using HospitalApi.WebApi.Models.Tables;

namespace HospitalApi.WebApi.Mappers;

public static class TorchMapper
{
    #region
    private readonly static Dictionary<int, TorchTableResultViewModel> _keyValuePairs = new Dictionary<int, TorchTableResultViewModel>
    {
        {1, new TorchTableResultViewModel { CheckUp = "TOX IgG (токсоплазма)", Result = null, Standard = "Манфий" }},
        {2, new TorchTableResultViewModel { CheckUp = "TOX IgM (токсоплазма)", Result = null, Standard = "Манфий" }},
        {3, new TorchTableResultViewModel { CheckUp = "RV IgG (рубелла)", Result = null, Standard = "Манфий" }},
        {4, new TorchTableResultViewModel { CheckUp = "RV IgM (рубелла)", Result = null, Standard = "Манфий" }},
        {5, new TorchTableResultViewModel { CheckUp = "CMV IgG (цитомегаловирус)", Result = null, Standard = "Манфий" }},
        {6, new TorchTableResultViewModel { CheckUp = "CMV IgM (цитомегаловирус)", Result = null, Standard = "Манфий" }},
        {7, new TorchTableResultViewModel { CheckUp = "HSV-1 IgG (герпес 1 тип)", Result = null, Standard = "Манфий" }},
        {8, new TorchTableResultViewModel { CheckUp = "HSV-1 IgM (герпес 1 тип)", Result = null, Standard = "Манфий" }},
        {9, new TorchTableResultViewModel { CheckUp = "HSV-2 IgG (герпес 2 тип)", Result = null, Standard = "Манфий" }},
        {10, new TorchTableResultViewModel { CheckUp = "HSV-2 IgM (герпес 2 тип)", Result = null, Standard = "Манфий" }},
    };
    #endregion

    public static TorchTableViewModel GetTorchTableView(TorchTable torchTable)
    {
        var view = new Dictionary<int, TorchTableResultViewModel>(_keyValuePairs);

        foreach (var item in torchTable.Items.Select(x => new { x.Index, x.Result }))
        {
            if (view.ContainsKey(item.Index))
            {
                view[item.Index].Result = item.Result;
            }
        }

        return new TorchTableViewModel
        {
            Id = torchTable.Id,
            Items = view.Values,
        };
    }

    public static TorchTable CreateTorchTable(long id, TorchTableUpdateModel update)
    {
        var items = new List<TorchTableResult>
        {
            new TorchTableResult { Id = id, Result = update.FirstItemResult, Index = 1 },
            new TorchTableResult { Id = id, Result = update.SecondItemResult, Index = 2 },
            new TorchTableResult { Id = id, Result = update.ThirdItemResult, Index = 3 },
            new TorchTableResult { Id = id, Result = update.FourthItemResult, Index = 4 },
            new TorchTableResult { Id = id, Result = update.FirstItemResult, Index = 5 },
            new TorchTableResult { Id = id, Result = update.SixthItemResult, Index = 6 },
            new TorchTableResult { Id = id, Result = update.SecondItemResult, Index = 7 },
            new TorchTableResult { Id = id, Result = update.EighthItemResult, Index = 8 },
            new TorchTableResult { Id = id, Result = update.NinthItemResult, Index = 9 },
            new TorchTableResult { Id = id, Result = update.TenthItemResult, Index = 10 },
        };

        return new TorchTable { Id = id, Items = items };
    }
}