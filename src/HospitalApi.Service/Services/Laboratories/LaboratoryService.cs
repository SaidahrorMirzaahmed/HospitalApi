using HospitalApi.DataAccess.UnitOfWorks;
using HospitalApi.Domain.Entities;
using HospitalApi.Domain.Entities.Tables;
using HospitalApi.Domain.Enums;
using HospitalApi.Service.Configurations;
using HospitalApi.Service.Exceptions;
using HospitalApi.Service.Extensions;
using HospitalApi.Service.Helpers;
using HospitalApi.Service.Services.Tables;
using HospitalApi.WebApi.Configurations;

namespace HospitalApi.Service.Services.Laboratories;

public class LaboratoryService(IUnitOfWork unitOfWork, ITorchTableService torchTableService) : ILaboratoryService
{
    public async Task<IEnumerable<Laboratory>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var laboratory = unitOfWork.Laboratories
            .SelectAsQueryable(expression: x => !x.IsDeleted, isTracked: false, includes: ["Staff", "Client"])
            .OrderBy(filter);

        if (!string.IsNullOrWhiteSpace(search))
            laboratory = laboratory.Where(x => x.Staff.FirstName.Contains(search) || x.Staff.LastName.Contains(search) || x.Client.FirstName.Contains(search) || x.Client.LastName.Contains(search));

        return await Task.FromResult(laboratory);
    }

    public async Task<IEnumerable<Laboratory>> GetAllByUserIdAsync(long id, PaginationParams @params, Filter filter, string search = null)
    {
        var laboratory = unitOfWork.Laboratories
            .SelectAsQueryable(expression: x => (x.ClientId == id || x.StaffId == id) && !x.IsDeleted, isTracked: false, includes: ["Staff", "Client"])
            .OrderBy(filter);

        if (!string.IsNullOrWhiteSpace(search))
            laboratory = laboratory.Where(x => x.Staff.FirstName.Contains(search) || x.Staff.LastName.Contains(search) || x.Client.FirstName.Contains(search) || x.Client.LastName.Contains(search));

        return await Task.FromResult(laboratory);
    }

    public async Task<Laboratory> GetAsync(long id)
    {
        var laboratory = await unitOfWork.Laboratories
            .SelectAsync(expression: x => x.Id == id && !x.IsDeleted, includes: ["Staff", "Client"])
            ?? throw new NotFoundException($"{nameof(Laboratory)} with this Id is not found {id}");

        return laboratory;
    }

    public async Task<Laboratory> CreateByTorchTableAsync(long clientId)
    {
        var laboratory = new Laboratory();
        laboratory.Create();

        laboratory.ClientId = clientId;
        laboratory.StaffId = HttpContextHelper.UserId;
        laboratory.LaboratoryTableType = LaboratoryTableType.Torch;
        var torch = await torchTableService.CreateAsync();
        laboratory.TableId = torch.Id;

        await unitOfWork.Laboratories.InsertAsync(laboratory);
        await unitOfWork.SaveAsync();

        return laboratory;
    }

    public async Task<Laboratory> UpdateAsync(long id, long clientId)
    {
        var exists = await unitOfWork.Laboratories.SelectAsync(laboratory => laboratory.Id == id && !laboratory.IsDeleted)
            ?? throw new NotFoundException($"{nameof(Laboratory)} is not exists with the id = {id}");

        exists.Update();
        exists.ClientId = clientId;

        await unitOfWork.SaveAsync();

        return exists;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var exists = await unitOfWork.Laboratories.SelectAsync(laboratory => laboratory.Id == id && !laboratory.IsDeleted)
            ?? throw new NotFoundException($"{nameof(Laboratory)} is not exists with the id = {id}");

        exists.Delete();
        await unitOfWork.Laboratories.DeleteAsync(exists);
        await torchTableService.DeleteAsync(exists.TableId);
        await unitOfWork.SaveAsync();

        return true;
    }

}