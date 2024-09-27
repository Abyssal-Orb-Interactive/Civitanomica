using System;
using System.Linq;

namespace ParametersAndTagsFramework
{
    public class TagFactory
    {
        public T Create<T>(string tagName) where T : Tag
        {
            var tagType = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .FirstOrDefault(type => type.Name == tagName && type.IsSubclassOf(typeof(Tag)) && !type.IsAbstract);
            
            
            if (tagType == null)
            {
                throw new ArgumentException($"Tag with name {tagName} does not exist.");
            }
            
            return Activator.CreateInstance(tagType) as T;
        }
        
        public string[] GetParameterNames<T>() where T : Tag
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsSubclassOf(typeof(T)) && !type.IsAbstract)
                .Select(type => type.Name)
                .ToArray();
        }
    }
}