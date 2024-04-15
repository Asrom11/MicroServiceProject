using Domain.Entities;
using Domain.Interfaces;
using MassTransit;
using Services.Contracts;
using Services.Contracts.Application;
using Services.Interfaces;

namespace Services.Services;

public class ApplicationService: IApplicationService
{
    private readonly IStoreApplication _storeApplication;
    private readonly ICheckUser _chekUser;
    private readonly IStandartStore<VacancyApplication> _standartStore;
    private readonly ICheckVacancy _checkVacancy;
    private readonly IBus _bus;
    public ApplicationService(IStoreApplication storeApplication, ICheckUser chekUser,  IStandartStore<VacancyApplication> standartStore, ICheckVacancy checkVacancy, IBus bus)
    {
        _bus = bus;
        _standartStore = standartStore;
        _storeApplication = storeApplication;
        _chekUser = chekUser;
        _checkVacancy = checkVacancy;
    }
    public async Task<IEnumerable<VacancyApplication>> GetApplicationsByVacancyIdAsync(Guid vacancyId)
    {
        var res = await _storeApplication.GetApplicationsByVacancyIdAsync(vacancyId);
        return res;
    }

    public async Task<VacancyApplication> GetApplicationByIdAsync(Guid id)
    {
        var res = await _standartStore.GetByIdAsync(id);
        return res;
    }

    public async Task<CreateApplicationResponse> CreateApplicationSagaAsync(VacancyApplication application)
    {
       var res = await _bus.Request<ApplicationSagaRequest, CreateApplicationResponse>(new ApplicationSagaRequest()
        {
            Id = Guid.NewGuid(),
            UserId = application.ApplicantId,
            VacancyId = application.VacancyId
        });
       
        return res.Message;
    }
    
    public async Task<Guid> CreateApplicationAsync(VacancyApplication application)
    {
        var res = await application.SaveAsycn(_standartStore,_storeApplication, _chekUser, _checkVacancy);
        return res;
    }
    public async Task UpdateApplicationAsync(VacancyApplication application, Guid employerId)
    {
        await application.UpdateAsync(_standartStore, _chekUser,_checkVacancy, employerId);
    }

    public async Task DeleteApplicationAsync(Guid applicationId, Guid userId)
    {
       var entity = await _storeApplication.IsUserApplicationAsync(applicationId, userId);
       await _standartStore.DeleteAsync(entity);
    }
}