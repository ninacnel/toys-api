﻿using Repository;
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
            builder.Services.AddScoped<IToyService, ToyService>();
            builder.Services.AddScoped<ToyRepository>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<CategoryRepository>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<OrderRepository>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<AuthRepository>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<RoleRepository>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<EmailRepository>();
            builder.Services.AddScoped<IStockService, StockService>();
            builder.Services.AddScoped<StockRepository>();
        }
    }
}
