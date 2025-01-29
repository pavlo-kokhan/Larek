using System;
using Kitchen.Refrigerator.Content;
using Newtonsoft.Json;

namespace Data
{
    public class RefrigeratorData
    {
        private RefrigeratorContentState _contentState;

        public RefrigeratorData(RefrigeratorContent content)
        {
            _contentState = new RefrigeratorContentState(content);
        }
        
        [JsonConstructor]
        public RefrigeratorData(RefrigeratorContentState contentStateState)
        {
            _contentState = contentStateState;
        }

        public RefrigeratorContentState ContentState
        {
            get => _contentState;

            set
            {
                if (_contentState == null)
                {
                    throw new NullReferenceException($"{nameof(_contentState)} can not be null");
                }
                
                _contentState = value;
            }
        }
    }
}