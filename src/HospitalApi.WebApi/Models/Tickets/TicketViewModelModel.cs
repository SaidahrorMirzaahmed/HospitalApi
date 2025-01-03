﻿using HospitalApi.Domain.Entities;
using HospitalApi.WebApi.Models.MedicalServices;
using HospitalApi.WebApi.Models.Pdfs;
using HospitalApi.WebApi.Models.Users;

namespace HospitalApi.WebApi.Models.Tickets;

public class TicketViewModelModel
{
    public long Id { get; set; }

    public bool IsPaid { get; set; }

    public double CommonPrice { get; set; }

    public long ClientId { get; set; }
    public UserViewModel Client { get; set; }

    public long PdfDetailsId { get; set; }
    public PdfDetailsViewModel PdfDetails { get; set; }

    public IEnumerable<MedicalServiceTypeHistoryViewModel> MedicalServiceTypeHistories { get; set; }
}