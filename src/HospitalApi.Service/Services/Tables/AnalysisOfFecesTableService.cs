using HospitalApi.DataAccess.UnitOfWorks;
using HospitalApi.Domain.Entities.Tables;
using HospitalApi.Service.Exceptions;
using HospitalApi.Service.Extensions;

namespace HospitalApi.Service.Services.Tables;

public class AnalysisOfFecesTableService(IUnitOfWork unitOfWork) : IAnalysisOfFecesTableService
{
    public async Task<AnalysisOfFecesTable> GetAsync(long id)
    {
        var exist = await unitOfWork.AnalysisOfFecesTables.SelectAsync(x => x.Id == id && !x.IsDeleted, includes: ["Items"])
           ?? throw new NotFoundException($"{nameof(AnalysisOfFecesTable)} with this Id is not found {id}");

        return exist;
    }

    public async Task<AnalysisOfFecesTable> CreateAsync(bool saveChanges = true)
    {
        var table = new AnalysisOfFecesTable();

        table.Create();

        var result = await unitOfWork.AnalysisOfFecesTables.InsertAsync(table);

        if (saveChanges)
            await unitOfWork.SaveAsync();

        return result;
    }

    public async Task<AnalysisOfFecesTable> UpdateAsync(long id, AnalysisOfFecesTable table, bool saveChanges = true)
    {
        var exists = await unitOfWork.AnalysisOfFecesTables.SelectAsync(expression: torchTable => torchTable.Id == id && !torchTable.IsDeleted, isTracked: false)
            ?? throw new NotFoundException($"{nameof(AnalysisOfFecesTable)} is not exists with the id = {id}");

        exists.Update();
        exists.Items = table.Items;

        await unitOfWork.AnalysisOfFecesTables.UpdateAsync(exists);
        if (saveChanges)
            await unitOfWork.SaveAsync();
        return exists;
    }

    public async Task<bool> DeleteAsync(long id, bool saveChanges = true)
    {
        var exists = await unitOfWork.AnalysisOfFecesTables.SelectAsync(torchTable => torchTable.Id == id && !torchTable.IsDeleted)
            ?? throw new NotFoundException($"{nameof(AnalysisOfFecesTable)} is not exists with the id = {id}");

        exists.Delete();

        foreach (var item in exists.Items)
        {
            item.IsDeleted = true;
            item.DeletedAt = DateTime.Now;
            item.Delete();
        }

        await unitOfWork.AnalysisOfFecesTables.DeleteAsync(exists);
        await unitOfWork.SaveAsync();

        return true;
    }
}