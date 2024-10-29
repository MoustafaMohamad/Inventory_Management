using Inventory_Management.Common.Repositories;
using Inventory_Management.Common;
using MediatR;
using Inventory_Management.Entities;
using Inventory_Management.CQRS.OTP.Commands;
using FluentEmail.Core;
using static System.Net.WebRequestMethods;

namespace Inventory_Management.CQRS.OTP.Queries
{

    public record GetOtpQuery(string email, string otp) : IRequest<OtpVerification>;


    //public record CourseDTO(int ID, string Name);

    public class GetOtpQueryHandler : BaseRequestHandler<OtpVerification, GetOtpQuery, OtpVerification>
    {
        // IRepository<Course> _courseRepository;
        // UserState _userState;
        public GetOtpQueryHandler(RequestParameters<OtpVerification> requestParameters) : base(requestParameters)
        {
        }
       

        public override Task<OtpVerification> Handle(GetOtpQuery request, CancellationToken cancellationToken)
        {
           Task<OtpVerification> returned= _repository.First(o => o.Email == request.email  && o.Otp == request.otp);
            if (returned != null)
            {
                return returned;
            }
            return Task.FromResult<OtpVerification>(null);
        }


    }
}
