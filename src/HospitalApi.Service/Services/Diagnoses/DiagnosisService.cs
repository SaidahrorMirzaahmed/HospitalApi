using HospitalApi.DataAccess.UnitOfWorks;
using HospitalApi.Domain.Entities;
using HospitalApi.Service.Configurations;
using HospitalApi.Service.Exceptions;
using HospitalApi.Service.Extensions;
using HospitalApi.WebApi.Configurations;

namespace HospitalApi.Service.Services.Diagnoses;

public class DiagnosisService(IUnitOfWork unitOfWork) : IDiagnosisService
{
    public async Task<IEnumerable<Diagnosis>> GetAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var diagnoses = unitOfWork.Diagnoses.SelectAsQueryable().OrderBy(filter);

        if (!string.IsNullOrWhiteSpace(search))
            diagnoses = diagnoses.Where(diagnosis => diagnosis.Code.ToLower().Contains(search.ToLower())
                || diagnosis.Title.ToLower().Contains(search.ToLower())
                || diagnosis.TitleRu.ToLower().Contains(search.ToLower()));

        return await Task.FromResult(diagnoses.ToPaginateAsEnumerable(@params));
    }

    public async Task<Diagnosis> GetByIdAsync(long id)
    {
        var diagnosis = await unitOfWork.Diagnoses.SelectAsync(entity => entity.Id == id && !entity.IsDeleted)
            ?? throw new NotFoundException($"{nameof(Diagnosis)} is not exists with id = {id}");

        return diagnosis;
    }
}