using HospitalApi.Domain.Entities;
using HospitalApi.Domain.Enums;
using iText.IO.Font;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace HospitalApi.Service.Services.PdfGeneratorServices;

public partial class PdfGeneratorService
{

    private void CreateHeaderForTicket(Document document)
    {

    }

    private void CreateUserDetailsForTicket(Document document, User user)
    {

    }

    private void CreateTableForTicket(Document document, IEnumerable<MedicalServiceTypeHistory> histories)
    {

    }

    private void CreateFooterForTicket(Document document, double price)
    {

    }
}