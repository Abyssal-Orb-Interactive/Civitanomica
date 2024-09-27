using System;
using System.Linq;

namespace ParametersAndTagsFramework
{
    public class ParameterFactory
    {

        public bool TryCreate<T>(string parameterName, out T parameter) where T : Parameter
        {
            parameter = default;
            
            var parameterType = FindNotAbstractTypeInAppDomainBy<T>(parameterName);
            
            if (parameterType == null) return false;

            parameter = Create<T>(parameterType);

            return true;
        }

        private Type FindNotAbstractTypeInAppDomainBy<T>(string parameterName) where T : Parameter
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .FirstOrDefault(type => type.Name == parameterName && type.IsSubclassOf(typeof(T)) && !type.IsAbstract);
        }

        private T Create<T>(Type parameterType) where T : Parameter
        {
            return Activator.CreateInstance(parameterType) as T;
        }
    
        public string[] GetParameterNames<T>() where T : Parameter
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsSubclassOf(typeof(T)) && !type.IsAbstract)
                .Select(type => type.Name)
                .ToArray();
        }
    }
}