using HospitalApi.DataAccess.UnitOfWorks;
using HospitalApi.Domain.Entities;
using HospitalApi.WebApi.Configurations;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;

namespace HospitalApi.Service.Services.PdfGeneratorServices;

public partial class PdfGeneratorService(IUnitOfWork unitOfWork) : IPdfGeneratorService
{
    public async Task<PdfDetails> CreateDocument(Laboratory laboratory)
    {
        var folderName = "Laboratories";
        var directory = System.IO.Path.Combine(EnvironmentHelper.WebRootPath, folderName);
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
                pdf.SetDefaultPageSize(PageSize.A4);
                Document document = new Document(pdf);

                CreateLaboratoryPdf(pdf, document, laboratory);

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
        var directory = System.IO.Path.Combine(EnvironmentHelper.WebRootPath, folderName);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        string fileName = $"{ticket.Client.FirstName}-{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.pdf";
        var fullPath = System.IO.Path.Combine(directory, fileName);
        float width = 416;
        float height = 568;
        PageSize customPageSize = new PageSize(width, height);

        using (PdfWriter writer = new PdfWriter(fullPath))
        {
            using (PdfDocument pdf = new PdfDocument(writer))
            {
                pdf.SetDefaultPageSize(customPageSize);
                Document document = new Document(pdf);

                CreateHeaderForTicket(document);
                CreateUserDetailsForTicket(document, ticket.Client);
                CreateTableForTicket(document, ticket.MedicalServiceTypeHistories);
                CreateFooterForTicket(pdf, document, ticket.CommonPrice, ticket.MedicalServiceTypeHistories.First().QueueDate);

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

    public async Task<PdfDetails> CreateDocument(Recipe recipe)
    {
        var folderName = "Recipes";
        var directory = System.IO.Path.Combine(EnvironmentHelper.WebRootPath, folderName);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        string fileName = $"{recipe.Client.FirstName}-{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.pdf";
        var fullPath = System.IO.Path.Combine(directory, fileName);
        float width = 416;
        float height = 568;
        PageSize customPageSize = new PageSize(width, height);

        using (PdfWriter writer = new PdfWriter(fullPath))
        {
            using (PdfDocument pdf = new PdfDocument(writer))
            {
                pdf.SetDefaultPageSize(PageSize.A4);
                Document document = new Document(pdf);

                CreateHeader(document, 12);
                CreateDetailsForRecipe(document);
                CreateUserDetailsForRecipe(document, recipe.Client);
                CreateTableForRecipe(document, recipe);
                CreateRecipeFooter(pdf, document, recipe.Staff);

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