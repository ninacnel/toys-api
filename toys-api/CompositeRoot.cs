using Repository;
using Services.IServices;
using Services.Services;

namespace api
{
    public static class CompositeRoot
    {
        public static void DependencyInjection(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<UserRepository>();
        }
    }
}
