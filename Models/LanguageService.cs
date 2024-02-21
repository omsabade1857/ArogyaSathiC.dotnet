using Microsoft.Extensions.Localization;
using System.Reflection;

namespace AarogyaSaathi.Models
{
    public class LanguageService
    {
        private readonly IStringLocalizer _stringLocalizer;

        public LanguageService(IStringLocalizerFactory factory) {
        
        var type = typeof(SharedResource);
            var assmblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _stringLocalizer = factory.Create("SharedResource", assmblyName.Name);

        
        }

        public LocalizedString Getkey(string key) { 
        return _stringLocalizer.GetString(key);
        }


    }
}
