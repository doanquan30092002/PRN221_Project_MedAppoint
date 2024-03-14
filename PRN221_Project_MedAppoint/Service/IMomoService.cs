using PRN221_Project_MedAppoint.Model;

namespace PRN221_Project_MedAppoint.Service
{
    public interface IMomoService
    {
        Task<MomoCreatePaymentResponseModel> CreatePaymentAsync(Payments order);

        Task<MomoExecuteResponseModel> PaymentExecuteAsync(IQueryCollection collection);
    }
}
