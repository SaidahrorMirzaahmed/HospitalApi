﻿using HospitalApi.DataAccess.Contexts;
using HospitalApi.DataAccess.Repositories;
using HospitalApi.Domain.Entities;
using HospitalApi.Domain.Entities.Tables;
using Microsoft.EntityFrameworkCore.Storage;

namespace HospitalApi.DataAccess.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext context;
    public IRepository<Asset> Assets { get; }
    public IRepository<Booking> Bookings { get; }
    public IRepository<NewsList> NewsList { get; }
    public IRepository<Recipe> Recipes { get; }
    public IRepository<User> Users { get; }
    public IRepository<Laboratory> Laboratories { get; }
    public IRepository<MedicalServiceType> MedicalServiceTypes { get; }
    public IRepository<MedicalServiceTypeHistory> MedicalServiceTypeHistories { get; }
    public IRepository<Ticket> Tickets { get; }
    public IRepository<ClinicQueue> ClinicQueues { get; }
    public IRepository<PdfDetails> PdfDetails { get; }
    public IRepository<Diagnosis> Diagnoses { get; }
    // Table
    public IRepository<AnalysisOfFecesTable> AnalysisOfFecesTables { get; }
    public IRepository<BiochemicalAnalysisOfBloodTable> BiochemicalAnalysisOfBloodTables { get; }
    public IRepository<CommonAnalysisOfBloodTable> CommonAnalysisOfBloodTables { get; }
    public IRepository<CommonAnalysisOfUrineTable> CommonAnalysisOfUrineTable { get; }
    public IRepository<TorchTable> TorchTables { get; }

    private IDbContextTransaction transaction;

    public UnitOfWork(AppDbContext context)
    {
        this.context = context;
        Users = new Repository<User>(this.context);
        Assets = new Repository<Asset>(this.context);
        Bookings = new Repository<Booking>(this.context);
        NewsList = new Repository<NewsList>(this.context);
        Recipes = new Repository<Recipe>(this.context);
        Laboratories = new Repository<Laboratory>(this.context);
        MedicalServiceTypes = new Repository<MedicalServiceType>(this.context);
        MedicalServiceTypeHistories = new Repository<MedicalServiceTypeHistory>(this.context);
        Tickets = new Repository<Ticket>(this.context);
        // Table
        AnalysisOfFecesTables = new Repository<AnalysisOfFecesTable>(this.context);
        BiochemicalAnalysisOfBloodTables = new Repository<BiochemicalAnalysisOfBloodTable>(this.context);
        CommonAnalysisOfBloodTables = new Repository<CommonAnalysisOfBloodTable>(this.context);
        CommonAnalysisOfUrineTable = new Repository<CommonAnalysisOfUrineTable>(this.context);
        TorchTables = new Repository<TorchTable>(this.context);
        ClinicQueues = new Repository<ClinicQueue>(this.context);
        PdfDetails = new Repository<PdfDetails>(this.context);
        Diagnoses = new Repository<Diagnosis>(this.context);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public async Task<bool> SaveAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public async Task BeginTransactionAsync()
    {
        transaction = await this.context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        await transaction.CommitAsync();
    }
}