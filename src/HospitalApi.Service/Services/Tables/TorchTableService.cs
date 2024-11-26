using HospitalApi.DataAccess.UnitOfWorks;
using HospitalApi.Domain.Entities.Tables;
using HospitalApi.Service.Exceptions;
using HospitalApi.Service.Extensions;

namespace HospitalApi.Service.Services.Tables;

public class TorchTableService(IUnitOfWork unitOfWork) : ITorchTableService
{
    public async Task<TorchTable> GetAsync(long id)
    {
        var exist = await unitOfWork.TorchTables.SelectAsync(x => x.Id == id && !x.IsDeleted, includes: ["Items"])
           ?? throw new NotFoundException($"{nameof(TorchTable)}with this Id is not found {id}");

        return exist;
    }

    public async Task<TorchTable> CreateAsync(bool saveChanges = true)
    {
        var torchTable = new TorchTable();

        torchTable.Create();

        var result = await unitOfWork.TorchTables.InsertAsync(torchTable);

        if (saveChanges)
            await unitOfWork.SaveAsync();

        return result;
    }

    public async Task<TorchTable> UpdateAsync(long id, TorchTable table, bool saveChanges = true)
    {
        var exists = await unitOfWork.TorchTables.SelectAsync(expression: torchTable => torchTable.Id == id && !torchTable.IsDeleted, isTracked: true)
            ?? throw new NotFoundException($"{nameof(TorchTable)} is not exists with the tableId = {id}");

        exists.Update();
        exists.Items = table.Items;

        await unitOfWork.TorchTables.UpdateAsync(exists);
        if (saveChanges)
            await unitOfWork.SaveAsync();

        return exists;
    }

    public async Task<bool> DeleteAsync(long tableId, bool saveChanges = true)
    {
        var exists = await unitOfWork.TorchTables.SelectAsync(torchTable => torchTable.Id == tableId && !torchTable.IsDeleted)
            ?? throw new NotFoundException($"{nameof(TorchTable)} is not exists with the tableId = {tableId}");

        exists.Delete();

        foreach (var item in exists.Items)
        {
            item.IsDeleted = true;
            item.DeletedAt = DateTime.Now;
            item.Delete();
        }

        await unitOfWork.TorchTables.DeleteAsync(exists);
        await unitOfWork.SaveAsync();

        return true;
    }
}