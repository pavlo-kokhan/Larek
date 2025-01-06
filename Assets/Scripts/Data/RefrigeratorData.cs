using System;
using Newtonsoft.Json;
using Refrigerator;

namespace Data
{
    public class RefrigeratorData
    {
        private RefrigeratorContent _refrigeratorContent;

        [JsonConstructor]
        public RefrigeratorData(RefrigeratorContent refrigeratorContent)
        {
            _refrigeratorContent = refrigeratorContent;
        }

        public RefrigeratorContent RefrigeratorContent
        {
            get => _refrigeratorContent;

            set
            {
                if (_refrigeratorContent == null)
                {
                    throw new NullReferenceException($"{nameof(_refrigeratorContent)} can not be null");
                }
                
                _refrigeratorContent = value;
            }
        }
    }
}