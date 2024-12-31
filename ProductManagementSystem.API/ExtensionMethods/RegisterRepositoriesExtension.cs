using System.Reflection;

namespace ProductManagementSystem.API.ExtensionMethods
{
    public static class RegisterRepositoriesExtension
    {
        public static IServiceCollection RegistrRepositories(this IServiceCollection serviceDescriptors,Assembly assembly)
        {
            var allRepoClasses = assembly.GetTypes().Where(x => x.IsClass && !x.IsAbstract)
                .Select(type => new
                {
                    Interface = type.GetInterfaces().FirstOrDefault(i => i.Name.EndsWith("Repository"))
                ,
                    Implementation = type
                }).Where(t => t.Interface is not null).ToList();

            foreach (var repo in allRepoClasses) {
                serviceDescriptors.AddScoped(repo.Interface, repo.Implementation);
            }
            return serviceDescriptors;
        }
    }
}
