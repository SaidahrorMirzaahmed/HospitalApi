using HospitalApi.DataAccess.UnitOfWorks;
using HospitalApi.Domain.Entities;
using HospitalApi.WebApi.Configurations;
using iText.Kernel.Pdf;
using iText.Layout;

namespace HospitalApi.Service.Services.PdfGeneratorServices;

public partial class PdfGeneratorService(IUnitOfWork unitOfWork) : IPdfGeneratorService
{
    public async Task<PdfDetails> CreateDocument(Laboratory laboratory)
    {
        var folderName = "Laboratories";
        var directory = Path.Combine(EnvironmentHelper.WebRootPath, folderName);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        string fileName = $"{laboratory.Client.FirstName}-{laboratory.LaboratoryTableType.ToString()}-{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.pdf";
        var fullPath = System.IO.Path.Combine(directory, fileName);

        using (PdfWriter writer = new PdfWriter(fullPath))
        {
            using (PdfDocument pdf = new PdfDocument(writer))
            {
                Document document = new Document(pdf);

                CreateHeader(document);
                CreateLaboratoryServiceDetails(document, laboratory.LaboratoryTableType);
                CreateUserDetails(document, laboratory.Client);
                CreateTable();
                CreateFooter(document, laboratory.Staff);

                document.Close();
            }
        }

        var result = await unitOfWork.PdfDetails.InsertAsync(new PdfDetails
        {
            PdfName = fileName,
            PdfPath = $"{folderName}//{fileName}"
        });

        return result;
    }

    public async Task<PdfDetails> CreateDocument(Ticket ticket)
    {
        var folderName = "Tickets";
        var directory = Path.Combine(EnvironmentHelper.WebRootPath, folderName);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        string fileName = $"{ticket.Client.FirstName}-{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.pdf";
        var fullPath = System.IO.Path.Combine(directory, fileName);

        using (PdfWriter writer = new PdfWriter(fullPath))
        {
            using (PdfDocument pdf = new PdfDocument(writer))
            {
                Document document = new Document(pdf);

                CreateHeaderForTicket(document);
                CreateUserDetailsForTicket(document, ticket.Client);
                CreateTableForTicket(document, ticket.MedicalServiceTypeHistories);
                CreateFooterForTicket(document, ticket.CommonPrice);

                document.Close();
            }
        }

        var result = await unitOfWork.PdfDetails.InsertAsync(new PdfDetails
        {
            PdfName = fileName,
            PdfPath = $"{folderName}//{fileName}"
        });

        return result;
    }
}