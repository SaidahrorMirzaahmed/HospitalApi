﻿using HospitalApi.DataAccess.UnitOfWorks;
using HospitalApi.Domain.Entities;
using HospitalApi.Domain.Enums;
using HospitalApi.Service.Configurations;
using HospitalApi.Service.Exceptions;
using HospitalApi.Service.Extensions;
using HospitalApi.Service.Helpers;
using HospitalApi.Service.Services.Notifications;
using HospitalApi.WebApi.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace HospitalApi.Service.Services.Users;

public class UserService(IUnitOfWork unitOfWork, IMemoryCache cache, ICodeSenderService codeSenderService) : IUserService
{
    public async Task<User> CreateStaffAsync(User user)
    {
        var existUser = await unitOfWork.Users.SelectAsync(u => (u.Phone == user.Phone) && !u.IsDeleted);
        if (existUser is not null)
            throw new AlreadyExistException($"This user already exists with this email={user.Phone}");

        user.Role = Domain.Enums.UserRole.Staff;

        user.CreatedByUserId = HttpContextHelper.UserId;
        var createdUser = await unitOfWork.Users.InsertAsync(user);
        await unitOfWork.SaveAsync();

        return createdUser;
    }

    public async Task<User> CreateUserAsync(User user)
    {
        var existUser = await unitOfWork.Users.SelectAsync(u => (u.Phone == user.Phone) && !u.IsDeleted);
        if (existUser is not null)
            throw new AlreadyExistException($"This user already exists with this email={user.Phone}");

        user.Role = Domain.Enums.UserRole.Client;

        user.CreatedByUserId = HttpContextHelper.UserId;
        var createdUser = await unitOfWork.Users.InsertAsync(user);
        await unitOfWork.SaveAsync();

        return createdUser;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existUser = await unitOfWork.Users.SelectAsync(u => u.Id == id && !u.IsDeleted)
            ?? throw new NotFoundException($"User is not found with this ID={id}");

        existUser.DeletedByUserId = HttpContextHelper.UserId;
        await unitOfWork.Users.DeleteAsync(existUser);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async Task<IEnumerable<User>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var users = unitOfWork.Users
            .SelectAsQueryable(expression: user => !user.IsDeleted, isTracked: false)
            .OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            users = users.Where(user =>
                user.FirstName.ToLower().Contains(search) || user.LastName.ToLower().Contains(search)
                || user.Phone.Contains(search) || user.Address.ToLower().Contains(search));

        return await users.ToPaginateAsQueryable(@params).ToListAsync();
    }

    public async Task<User> GetByIdAsync(long id)
    {
        var existUser = await unitOfWork.Users.SelectAsync(expression: u => u.Id == id && !u.IsDeleted)
            ?? throw new NotFoundException($"User is not found with this ID={id}");

        return existUser;
    }

    public async Task<bool> SendSMSCodeAsync(string phone)
    {
        var code = await codeSenderService.SendCodeToPhone(phone);
        cache.Set(phone, code, TimeSpan.FromMinutes(5));

        return await Task.FromResult(true);
    }

    public async Task<User> UpdateStaffAsync(long id, User user)
    {
        var existUser = await unitOfWork.Users.SelectAsync(expression: u => u.Id == id && !u.IsDeleted)
            ?? throw new NotFoundException($"User is not found with this ID={id}");

        var alreadyExistUser = await unitOfWork.Users.SelectAsync(u => (u.Phone == user.Phone || u.Phone == user.Phone) && !u.IsDeleted && u.Id != id);
        if (alreadyExistUser is not null)
            throw new AlreadyExistException($"This user already exists with this email={user.Phone}");

        existUser.Id = id;
        existUser.Phone = user.Phone;
        existUser.LastName = user.LastName;
        existUser.FirstName = user.FirstName;
        existUser.Address = user.Address;
        existUser.Birth = user.Birth;
        existUser.UpdatedAt = DateTime.UtcNow;
        existUser.UpdatedByUserId = HttpContextHelper.UserId;

        await unitOfWork.SaveAsync();
        return existUser;
    }

    public async Task<User> UpdateClientAsync(long id, User user)
    {
        var existUser = await unitOfWork.Users.SelectAsync(expression: u => u.Id == id && !u.IsDeleted)
                        ?? throw new NotFoundException($"User is not found with this ID={id}");

        var alreadyExistUser = await unitOfWork.Users.SelectAsync(u => (u.Phone == user.Phone || u.Phone == user.Phone) && !u.IsDeleted && u.Id != id);
        if (alreadyExistUser is not null)
            throw new AlreadyExistException($"This user already exists with this email={user.Phone}");

        existUser.Id = id;
        existUser.Phone = user.Phone;
        existUser.LastName = user.LastName;
        existUser.FirstName = user.FirstName;
        existUser.Address = user.Address;
        existUser.Birth = user.Birth;
        existUser.UpdatedAt = DateTime.UtcNow;
        existUser.UpdatedByUserId = HttpContextHelper.UserId;

        await unitOfWork.SaveAsync();
        return existUser;
    }

    public async Task<(User user, string token)> VerifySMSCode(string phone, long code)
    {
        if (cache.TryGetValue(phone, out long storedCode))
        {
            if (storedCode == code)
            {
                cache.Remove(phone);
                var user = await unitOfWork.Users.SelectAsync(x => x.Phone == phone && !x.IsDeleted);
                if (user is null)
                {
                    user = new User
                    {
                        FirstName = "",
                        LastName = "",
                        Address = "",
                        Phone = phone,
                        Birth = DateOnly.FromDateTime(DateTime.UtcNow),
                        Role = UserRole.Client,
                    };
                    user.Create();
                    await unitOfWork.Users.InsertAsync(user);
                    await unitOfWork.SaveAsync();
                }
                var token = AuthHelper.GenerateToken(user);
                return (user, token);
            }
            else
            {
                throw new ArgumentIsNotValidException("Invalid code");
            }
        }
        else
        {
            throw new NotFoundException("Code not found or expired");
        }
    }

    public async Task<IEnumerable<User>> GetAllClientAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var users = unitOfWork.Users
            .SelectAsQueryable(expression: user => !user.IsDeleted && user.Role == UserRole.Client, isTracked: false)
            .OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            users = users.Where(user =>
                user.FirstName.ToLower().Contains(search) || user.LastName.ToLower().Contains(search)
                || user.Phone.Contains(search) || user.Address.ToLower().Contains(search));

        return await users.ToPaginateAsQueryable(@params).ToListAsync();
    }

    public async Task<IEnumerable<User>> GetAllStaffAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var users = unitOfWork.Users
            .SelectAsQueryable(expression: user => !user.IsDeleted && user.Role == UserRole.Staff, isTracked: false)
            .OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            users = users.Where(user =>
            user.FirstName.ToLower().Contains(search) || user.LastName.ToLower().Contains(search)
            || user.Phone.Contains(search) || user.Address.ToLower().Contains(search));

        return await users.ToPaginateAsQueryable(@params).ToListAsync();
    }
}
