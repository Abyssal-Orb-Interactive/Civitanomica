using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ParametersAndTagsFramework
{
    [Serializable]
    public abstract class Tag 
    {
        [SerializeField] private List<string> _guaranteedParametersNames  = null;
        [field: SerializeField] public string Name { get; private set; } = null;

        public IEnumerable<string> GuaranteedParametersNames => _guaranteedParametersNames;
        
        protected Tag(string name, IEnumerable<string> guaranteedParametersNames)
        {
            Name = name;
            _guaranteedParametersNames = guaranteedParametersNames.ToList();
        }
        
    }
}