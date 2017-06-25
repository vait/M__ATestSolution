using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

namespace WordCounter.Infrastructure.Ninject.Modules
{
    public abstract class AutoBindModule<T> : NinjectModule
    {
        protected ModulesConfiguration config;

        private List<Type> relevantTypes;

        private List<string> relevantTypesName;

        protected Type necessaryType;

        protected string necessaryTypeName;

        public AutoBindModule(ModulesConfiguration config)
            :base()
        {
            if (!typeof(T).IsInterface)
                throw new Exception("T may be interface only");

            this.config = config;
            relevantTypes = new List<Type>();
            necessaryType = null;
            necessaryTypeName = String.Empty;
        }

        /// <summary>
        /// Загрузить все типы со сборок в домене, реализующие интерфейс T
        /// </summary>
        public void LoadAssebliesTypes()
        {
            //Загружаем типы со сборок

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var assemblyTTypes = assembly.GetTypes()
                    .Where(t => t.GetInterfaces()
                        .Any(i => i == typeof(T)));

                relevantTypes.AddRange(assemblyTTypes);
            }

            relevantTypes.TrimExcess();
            relevantTypesName = new List<string>(relevantTypes.Count);

            relevantTypes.ForEach(t =>
            {
                var attr =
                    Attribute.GetCustomAttribute(t, typeof(DisplayNameAttribute)) as DisplayNameAttribute;
                if (attr != null)
                {
                    relevantTypesName.Insert(relevantTypes.IndexOf(t), attr.DisplayName);
                }
                else
                {
                    relevantTypesName.Insert(relevantTypes.IndexOf(t), t.Name);
                }
            });
        }
        #region Над этими двумя методами нужно поработать, чтобы добавить больше гибкости
        /// <summary>
        /// Названия всех типов, релизующих класс Т
        /// </summary>
        public string[] AvailableTypeNames
        {
            get
            {
                return relevantTypesName.ToArray();
            }
        }

        /// <summary>
        /// Задает требуемый для биндинга тип по умолчанию
        /// </summary>
        /// <param name="index">номер в списке</param>
        /// <remarks>Если индекс не верен, то берется первый по умолчанию</remarks>
        public void SetNecessaryType(string name)
        {
            var index = relevantTypesName.IndexOf(name);
            if (index < 0 || index >= relevantTypes.Count)
            {
                index = 0;
            }

            necessaryType = relevantTypes[index];
            necessaryTypeName = relevantTypesName[index];
        }

        #endregion
    }
}
