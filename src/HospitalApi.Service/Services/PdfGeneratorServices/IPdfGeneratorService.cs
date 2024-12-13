using HospitalApi.Domain.Entities;

namespace HospitalApi.Service.Services.PdfGeneratorServices;

public interface IPdfGeneratorService
{
    Task<PdfDetails> CreateDocument(Laboratory laboratory);

    Task<PdfDetails> CreateDocument(Ticket ticket);
}