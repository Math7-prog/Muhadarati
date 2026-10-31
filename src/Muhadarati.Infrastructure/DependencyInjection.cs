using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Muhadarati.Application.Common.Interfaces;
using Muhadarati.Infrastructure.Identity;
using Muhadarati.Infrastructure.Persistence;

namespace Muhadarati.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default")
            ?? throw new InvalidOperationException("سلسلة الاتصال 'ConnectionStrings:Default' غير موجودة بالإعدادات.");

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString)
                   .UseSnakeCaseNamingConvention());

        services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());

        // تسجيل بنيوي فقط لهذي الخطوة: جداول Users/Roles وUserManager/RoleManager جاهزة للاستخدام.
        // سياسة كلمة المرور، وتفعيل 2FA الإلزامي للمشرف/الأدمن، وربط JWT الفعلي — يُضاف بخطوة التسجيل القادمة.
        services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }
}
