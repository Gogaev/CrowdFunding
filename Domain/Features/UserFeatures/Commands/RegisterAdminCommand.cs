using Core.Dtos.User;

namespace Domain.Features.UserFeatures.Commands
{
    public class RegisterAdminCommand : RegisterUserCommand
    {
        public string? AdminKey { get; set; }
    }
}
