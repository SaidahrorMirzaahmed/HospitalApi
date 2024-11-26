using HospitalApi.DataAccess.UnitOfWorks;
using HospitalApi.Domain.Entities.Tables;
using HospitalApi.Service.Exceptions;
using HospitalApi.Service.Extensions;

namespace HospitalApi.Service.Services.Tables;

public class CommonAnalysisOfUrineTableService(IUnitOfWork unitOfWork) : ICommonAnalysisOfUrineTableService
{
    public async Task<CommonAnalysisOfUrineTable> GetAsync(long id)
    {
        var exist = await unitOfWork.CommonAnalysisOfUrineTable.SelectAsync(x => x.Id == id && !x.IsDeleted, includes: ["Items"])
           ?? throw new NotFoundException($"{nameof(CommonAnalysisOfUrineTable)}with this Id is not found {id}");

        return exist;
    }

    public async Task<CommonAnalysisOfUrineTable> CreateAsync(bool saveChanges = true)
    {
        var table = new CommonAnalysisOfUrineTable();

        table.Create();

        var result = await unitOfWork.CommonAnalysisOfUrineTable.InsertAsync(table);

        if (saveChanges)
            await unitOfWork.SaveAsync();

        return result;
    }

    public async Task<CommonAnalysisOfUrineTable> UpdateAsync(long id, CommonAnalysisOfUrineTable table, bool saveChanges = true)
    {
        var exists = await unitOfWork.CommonAnalysisOfUrineTable.SelectAsync(expression: torchTable => torchTable.Id == id && !torchTable.IsDeleted, isTracked: true)
            ?? throw new NotFoundException($"{nameof(CommonAnalysisOfUrineTable)} is not exists with the id = {id}");

        exists.Update();
        exists.Items = table.Items;

        await unitOfWork.CommonAnalysisOfUrineTable.UpdateAsync(exists);
        if (saveChanges)
            await unitOfWork.SaveAsync();
        return exists;
    }

    public async Task<bool> DeleteAsync(long id, bool saveChanges = true)
    {
        var exists = await unitOfWork.CommonAnalysisOfUrineTable.SelectAsync(torchTable => torchTable.Id == id && !torchTable.IsDeleted)
            ?? throw new NotFoundException($"{nameof(CommonAnalysisOfUrineTable)} is not exists with the id = {id}");

        exists.Delete();

        foreach (var item in exists.Items)
        {
            item.IsDeleted = true;
            item.DeletedAt = DateTime.Now;
            item.Delete();
        }

        await unitOfWork.CommonAnalysisOfUrineTable.DeleteAsync(exists);
        await unitOfWork.SaveAsync();

        return true;
    }
}