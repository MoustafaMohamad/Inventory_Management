using FluentEmail.Core;
using MediatR;

namespace Inventory_Management.CQRS.Email.Command
{
    public record SendEmailCommand(string ToEmail, string Subject, string Message) : IRequest<bool>;
    public class SendEmailCommandHandler : IRequestHandler<SendEmailCommand, bool>
    {
        private readonly IFluentEmail _fluentEmail;

        public SendEmailCommandHandler(IFluentEmail fluentEmail)
        {
            _fluentEmail = fluentEmail;
        }

        public async Task<bool> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var response = await _fluentEmail
                .To(request.ToEmail)
                .Subject(request.Subject)
                .Body(request.Message)
                .SendAsync();

            return response.Successful;
        }
    }

}
