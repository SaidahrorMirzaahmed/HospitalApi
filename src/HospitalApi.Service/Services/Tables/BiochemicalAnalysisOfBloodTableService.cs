using HospitalApi.DataAccess.UnitOfWorks;
using HospitalApi.Domain.Entities.Tables;
using HospitalApi.Service.Exceptions;
using HospitalApi.Service.Extensions;

namespace HospitalApi.Service.Services.Tables;

public class BiochemicalAnalysisOfBloodTableService(IUnitOfWork unitOfWork) : IBiochemicalAnalysisOfBloodTableService
{
    public async Task<BiochemicalAnalysisOfBloodTable> GetAsync(long id)
    {
        var exist = await unitOfWork.BiochemicalAnalysisOfBloodTables.SelectAsync(x => x.Id == id && !x.IsDeleted, includes: ["Items"])
           ?? throw new NotFoundException($"{nameof(BiochemicalAnalysisOfBloodTable)} with this Id is not found {id}");

        return exist;
    }

    public async Task<BiochemicalAnalysisOfBloodTable> CreateAsync(bool saveChanges = true)
    {
        var table = new BiochemicalAnalysisOfBloodTable();

        table.Create();

        var result = await unitOfWork.BiochemicalAnalysisOfBloodTables.InsertAsync(table);

        if (saveChanges)
            await unitOfWork.SaveAsync();

        return result;
    }

    public async Task<BiochemicalAnalysisOfBloodTable> UpdateAsync(long id, BiochemicalAnalysisOfBloodTable table, bool saveChanges = true)
    {
        var exists = await unitOfWork.BiochemicalAnalysisOfBloodTables.SelectAsync(expression: torchTable => torchTable.Id == id && !torchTable.IsDeleted, isTracked: false)
            ?? throw new NotFoundException($"{nameof(BiochemicalAnalysisOfBloodTable)} is not exists with the id = {id}");

        exists.Update();
        exists.Items = table.Items;

        await unitOfWork.BiochemicalAnalysisOfBloodTables.UpdateAsync(exists);
        if (saveChanges)
            await unitOfWork.SaveAsync();

        return exists;
    }

    public async Task<bool> DeleteAsync(long id, bool saveChanges = true)
    {
        var exists = await unitOfWork.BiochemicalAnalysisOfBloodTables.SelectAsync(torchTable => torchTable.Id == id && !torchTable.IsDeleted)
            ?? throw new NotFoundException($"{nameof(BiochemicalAnalysisOfBloodTable)} is not exists with the id = {id}");

        exists.Delete();

        foreach (var item in exists.Items)
        {
            item.IsDeleted = true;
            item.DeletedAt = DateTime.Now;
            item.Delete();
        }

        await unitOfWork.BiochemicalAnalysisOfBloodTables.DeleteAsync(exists);
        await unitOfWork.SaveAsync();

        return true;
    }
}