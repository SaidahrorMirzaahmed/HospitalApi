using HospitalApi.DataAccess.UnitOfWorks;
using HospitalApi.Domain.Entities;
using HospitalApi.Domain.Enums;
using HospitalApi.Service.Configurations;
using HospitalApi.Service.Exceptions;
using HospitalApi.Service.Extensions;
using HospitalApi.Service.Helpers;
using HospitalApi.Service.Services.Tables;
using HospitalApi.WebApi.Configurations;

namespace HospitalApi.Service.Services.Laboratories;

public class LaboratoryService(IUnitOfWork unitOfWork,
    IAnalysisOfFecesTableService analysisOfFecesTableService,
    ITorchTableService torchTableService,
    IBiochemicalAnalysisOfBloodTableService biochemicalAnalysisOfBloodTableService,
    ICommonAnalysisOfBloodTableService commonAnalysisOfBloodTableService,
    ICommonAnalysisOfUrineTableService commonAnalysisOfUrineTableService) : ILaboratoryService
{
    public async Task<IEnumerable<Laboratory>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var laboratory = unitOfWork.Laboratories
            .SelectAsQueryable(expression: x => !x.IsDeleted, isTracked: false, includes: ["Staff", "Client", "PdfDetails"])
            .OrderBy(filter);

        if (!string.IsNullOrWhiteSpace(search))
            laboratory = laboratory.Where(x => x.Staff.FirstName.Contains(search) || x.Staff.LastName.Contains(search)
                || x.Staff.Phone.Contains(search) || x.Staff.Address.ToLower().Contains(search.ToLower())
                || x.Client.FirstName.Contains(search) || x.Client.LastName.Contains(search)
                || x.Client.Phone.Contains(search) || x.Client.Address.ToLower().Contains(search));

        return await Task.FromResult(laboratory.ToPaginateAsEnumerable(@params));
    }

    public async Task<IEnumerable<Laboratory>> GetAllByUserIdAsync(long id, PaginationParams @params, Filter filter, string search = null)
    {
        var laboratory = unitOfWork.Laboratories
            .SelectAsQueryable(expression: x => (x.ClientId == id || x.StaffId == id) && !x.IsDeleted, isTracked: false, includes: ["Staff", "Client", "PdfDetails"])
            .OrderBy(filter);

        if (!string.IsNullOrWhiteSpace(search))
            laboratory = laboratory.Where(x => x.Staff.FirstName.Contains(search) || x.Staff.LastName.Contains(search)
                || x.Staff.Phone.Contains(search) || x.Staff.Address.ToLower().Contains(search.ToLower())
                || x.Client.FirstName.Contains(search) || x.Client.LastName.Contains(search)
                || x.Client.Phone.Contains(search) || x.Client.Address.ToLower().Contains(search));

        return await Task.FromResult(laboratory.ToPaginateAsEnumerable(@params));
    }

    public async Task<Laboratory> GetAsync(long id)
    {
        var laboratory = await unitOfWork.Laboratories
            .SelectAsync(expression: x => x.Id == id && !x.IsDeleted, includes: ["Staff", "Client", "PdfDetails"])
                ?? throw new NotFoundException($"{nameof(Laboratory)} with this Id is not found {id}");

        return laboratory;
    }

    public async Task<Laboratory> CreateByAnalysisOfFecesTableAsync(long clientId)
    {
        var laboratory = new Laboratory();
        laboratory.Create();

        laboratory.ClientId = clientId;
        laboratory.StaffId = HttpContextHelper.UserId;
        laboratory.LaboratoryTableType = LaboratoryTableType.AnalysisOfFeces;
        var torch = await analysisOfFecesTableService.CreateAsync();
        laboratory.TableId = torch.Id;

        await unitOfWork.Laboratories.InsertAsync(laboratory);
        await unitOfWork.SaveAsync();

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

    public async Task<Laboratory> CreateByBiochemicalAnalysisOfBloodTableAsync(long clientId)
    {
        var laboratory = new Laboratory();
        laboratory.Create();

        laboratory.ClientId = clientId;
        laboratory.StaffId = HttpContextHelper.UserId;
        laboratory.LaboratoryTableType = LaboratoryTableType.BiochemicalAnalysisOfBlood;
        var torch = await biochemicalAnalysisOfBloodTableService.CreateAsync();
        laboratory.TableId = torch.Id;

        await unitOfWork.Laboratories.InsertAsync(laboratory);
        await unitOfWork.SaveAsync();

        return laboratory;
    }

    public async Task<Laboratory> CreateByCommonAnalysisOfBloodAsync(long clientId)
    {
        var laboratory = new Laboratory();
        laboratory.Create();

        laboratory.ClientId = clientId;
        laboratory.StaffId = HttpContextHelper.UserId;
        laboratory.LaboratoryTableType = LaboratoryTableType.CommonAnalysisOfBlood;
        var torch = await commonAnalysisOfBloodTableService.CreateAsync();
        laboratory.TableId = torch.Id;

        await unitOfWork.Laboratories.InsertAsync(laboratory);
        await unitOfWork.SaveAsync();

        return laboratory;
    }

    public async Task<Laboratory> CreateByCommonAnalysisOfUrineAsync(long clientId)
    {
        var laboratory = new Laboratory();
        laboratory.Create();

        laboratory.ClientId = clientId;
        laboratory.StaffId = HttpContextHelper.UserId;
        laboratory.LaboratoryTableType = LaboratoryTableType.CommonAnalysisOfUrine;
        var torch = await commonAnalysisOfUrineTableService.CreateAsync();
        laboratory.TableId = torch.Id;

        await unitOfWork.Laboratories.InsertAsync(laboratory);
        await unitOfWork.SaveAsync();

        return laboratory;
    }

    public async Task<Laboratory> UpdateAsync(long id, long clientId, LaboratoryTableType laboratoryTableType)
    {
        var exists = await unitOfWork.Laboratories.SelectAsync(laboratory => laboratory.Id == id && !laboratory.IsDeleted)
            ?? throw new NotFoundException($"{nameof(Laboratory)} is not exists with the id = {id}");

        exists.Update();
        exists.ClientId = clientId;

        if (laboratoryTableType != exists.LaboratoryTableType)
        {
            var updated = await DeleteByTable(exists, laboratoryTableType, true);
            exists = updated;
        }

        exists.LaboratoryTableType = laboratoryTableType;
        await unitOfWork.SaveAsync();

        return exists;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var exists = await unitOfWork.Laboratories.SelectAsync(laboratory => laboratory.Id == id && !laboratory.IsDeleted)
            ?? throw new NotFoundException($"{nameof(Laboratory)} is not exists with the id = {id}");

        exists.Delete();
        await unitOfWork.Laboratories.DeleteAsync(exists);
        await DeleteByTable(exists, exists.LaboratoryTableType);
        await unitOfWork.SaveAsync();

        return true;
    }

    private async Task<Laboratory> DeleteByTable(Laboratory exists, LaboratoryTableType laboratoryTableType, bool isUpdate = false)
    {
        if (exists.LaboratoryTableType == LaboratoryTableType.AnalysisOfFeces)
        {
            await analysisOfFecesTableService.DeleteAsync(exists.TableId);
        }
        else if (exists.LaboratoryTableType == LaboratoryTableType.BiochemicalAnalysisOfBlood)
        {
            await biochemicalAnalysisOfBloodTableService.DeleteAsync(exists.TableId);
        }
        else if (exists.LaboratoryTableType == LaboratoryTableType.CommonAnalysisOfBlood)
        {
            await commonAnalysisOfBloodTableService.DeleteAsync(exists.TableId);
        }
        else if (exists.LaboratoryTableType == LaboratoryTableType.CommonAnalysisOfUrine)
        {
            await commonAnalysisOfBloodTableService.DeleteAsync(exists.TableId);
        }
        else if (exists.LaboratoryTableType == LaboratoryTableType.Torch)
        {
            await torchTableService.DeleteAsync(exists.TableId);
        }
        else
            throw new ArgumentIsNotValidException($"Table is not exists with id = {exists.TableId}");

        if (!isUpdate)
            return exists;

        if (laboratoryTableType == LaboratoryTableType.AnalysisOfFeces)
        {
            var table = await analysisOfFecesTableService.CreateAsync();
            exists.TableId = table.Id;
        }
        else if (laboratoryTableType == LaboratoryTableType.BiochemicalAnalysisOfBlood)
        {
            var table = await biochemicalAnalysisOfBloodTableService.CreateAsync();
            exists.TableId = table.Id;
        }
        else if (laboratoryTableType == LaboratoryTableType.CommonAnalysisOfBlood)
        {
            var table = await commonAnalysisOfBloodTableService.CreateAsync();
            exists.TableId = table.Id;
        }
        else if (laboratoryTableType == LaboratoryTableType.CommonAnalysisOfUrine)
        {
            var table = await commonAnalysisOfUrineTableService.CreateAsync();
            exists.TableId = table.Id;
        }
        else if (laboratoryTableType == LaboratoryTableType.Torch)
        {
            var table = await torchTableService.CreateAsync();
            exists.TableId = table.Id;
        }
        else
            throw new ArgumentIsNotValidException($"Table is not exists with id = {exists.TableId}");

        return exists;
    }
}