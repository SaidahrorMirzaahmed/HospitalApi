using HospitalApi.DataAccess.UnitOfWorks;
using HospitalApi.Domain.Entities.Tables;
using HospitalApi.Service.Exceptions;
using HospitalApi.Service.Extensions;

namespace HospitalApi.Service.Services.Tables;

public class CommonAnalysisOfBloodTableService(IUnitOfWork unitOfWork) : ICommonAnalysisOfBloodTableService
{
    public async Task<CommonAnalysisOfBloodTable> GetAsync(long id)
    {
        var exist = await unitOfWork.CommonAnalysisOfBloodTables.SelectAsync(x => x.Id == id && !x.IsDeleted, includes: ["Items"])
           ?? throw new NotFoundException($"{nameof(CommonAnalysisOfBloodTable)} with this Id is not found {id}");

        return exist;
    }

    public async Task<CommonAnalysisOfBloodTable> CreateAsync(bool saveChanges = true)
    {
        var table = new CommonAnalysisOfBloodTable();

        table.Create();

        var result = await unitOfWork.CommonAnalysisOfBloodTables.InsertAsync(table);

        if (saveChanges)
            await unitOfWork.SaveAsync();

        return result;
    }

    public async Task<CommonAnalysisOfBloodTable> UpdateAsync(long id, CommonAnalysisOfBloodTable table, bool saveChanges = true)
    {
        var exists = await unitOfWork.CommonAnalysisOfBloodTables.SelectAsync(expression: torchTable => torchTable.Id == id && !torchTable.IsDeleted, isTracked: false)
            ?? throw new NotFoundException($"{nameof(CommonAnalysisOfBloodTable)} is not exists with the id = {id}");

        exists.Update();
        exists.Items = table.Items;

        await unitOfWork.CommonAnalysisOfBloodTables.UpdateAsync(exists);
        if (saveChanges)
            await unitOfWork.SaveAsync();
        return exists;
    }

    public async Task<bool> DeleteAsync(long id, bool saveChanges = true)
    {
        var exists = await unitOfWork.CommonAnalysisOfBloodTables.SelectAsync(torchTable => torchTable.Id == id && !torchTable.IsDeleted)
            ?? throw new NotFoundException($"{nameof(CommonAnalysisOfBloodTable)} is not exists with the id = {id}");

        exists.Delete();

        foreach (var item in exists.Items)
        {
            item.IsDeleted = true;
            item.DeletedAt = DateTime.Now;
            item.Delete();
        }

        await unitOfWork.CommonAnalysisOfBloodTables.DeleteAsync(exists);
        await unitOfWork.SaveAsync();

        return true;
    }
}