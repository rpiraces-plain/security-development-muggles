using HashidsNet;
using Microsoft.Extensions.Options;
using ProCodeGuide.Samples.BrokenAccessControl.Infrastructure.HashIds;

namespace ProCodeGuide.Samples.BrokenAccessControl.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHashids(this IServiceCollection services)
    {
        services.AddSingleton(sp => sp.GetRequiredService<IOptions<HashIdsOptions>>().Value);
        services.AddSingleton<IHashids, Hashids>(sp =>
        {
            var options = sp.GetRequiredService<HashIdsOptions>();
            var hashids = new Hashids(options.Salt, options.MinHashLength, options.Alphabet, options.Steps);
            return hashids;
        });
        return services;
    }
}