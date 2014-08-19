using System;
using System.Collections.Generic;
using System.Reflection;

namespace TestUtilities.ConsoleApplication
{
    public class AttributeManager<T> where T : Attribute
    {
        #region Fields

        private readonly Dictionary<Type,string> _propertiesByType = new Dictionary<Type, string>(); 

        #endregion

        #region Constructors

        static AttributeManager()
        {
            Instance = new AttributeManager<T>();
        }
 
        private AttributeManager()
        {
            this.Initialize();
        }

        #endregion

        #region Properties

        #region Singleton member

        public static AttributeManager<T> Instance { get; private set; }

        #endregion

        #endregion

        #region Methods

        public string GetTargetField(string sourceField)
        {
            return this._fieldMappingsBySourceKey.ContainsKey(sourceField) ? this._fieldMappingsBySourceKey[sourceField] : null;
        }

        private void Initialize()
        {
            var t = this.GetType();
            t

            
        }

        #endregion
    }
}
