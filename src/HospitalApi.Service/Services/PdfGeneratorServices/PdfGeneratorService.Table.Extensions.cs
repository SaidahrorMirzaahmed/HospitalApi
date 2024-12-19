using iText.Kernel.Pdf;
using iText.Layout;
using HospitalApi.WebApi.Configurations;

namespace HospitalApi.Service.Services.PdfGeneratorServices;

public partial class PdfGeneratorService
{
    private async void CreateAnalysisOfFecesTable(PdfDocument pdf, Document document, long tableId)
    {
        var table = await unitOfWork.AnalysisOfFecesTables.SelectAsync(entity => entity.Id == tableId && !entity.IsDeleted);

        
    }

    private void CreateBiochemicalAnalysisOfBloodTable(PdfDocument pdf, Document document, long tableId)
    {

    }

    private void CreateCommonAnalysisOfBloodTable(PdfDocument pdf, Document document, long tableId)
    {

    }

    private void CreateCommonAnalysisOfUrineTable(PdfDocument pdf, Document document, long tableId)
    {

    }

    private void CreateTorchTable(PdfDocument pdf, Document document, long tableId)
    {

    }
}