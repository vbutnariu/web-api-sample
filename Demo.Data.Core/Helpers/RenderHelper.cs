using Demo.Common.Attributes;
using Demo.Core.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Demo.Core.Data.Helpers

{

    public static class TypeExtensions
    {
        public static string Alias(this Type type)
        {
            return TypeAliases.ContainsKey(type) ?
                TypeAliases[type] : string.Empty;
        }

        public static string AliasOrName(this Type type)
        {
            return TypeAliases.ContainsKey(type) ?
                TypeAliases[type] : type.FullName;
        }

        private static readonly Dictionary<Type, string> TypeAliases = new Dictionary<Type, string>
    {
        { typeof(byte), "byte" },
        { typeof(sbyte), "sbyte" },
        { typeof(short), "short" },
        { typeof(ushort), "ushort" },
        { typeof(int), "int" },
        { typeof(uint), "uint" },
        { typeof(long), "long" },
        { typeof(ulong), "ulong" },
        { typeof(float), "float" },
        { typeof(double), "double" },
        { typeof(decimal), "decimal" },
        { typeof(object), "object" },
        { typeof(bool), "bool" },
        { typeof(char), "char" },
        { typeof(string), "string" },
        { typeof(void), "void" },
        { typeof(byte?), "byte?" },
        { typeof(sbyte?), "sbyte?" },
        { typeof(short?), "short?" },
        { typeof(ushort?), "ushort?" },
        { typeof(int?), "int?" },
        { typeof(uint?), "uint?" },
        { typeof(long?), "long?" },
        { typeof(ulong?), "ulong?" },
        { typeof(float?), "float?" },
        { typeof(double?), "double?" },
        { typeof(decimal?), "decimal?" },
        { typeof(bool?), "bool?" },
        { typeof(char?), "char?" },

        { typeof(byte[]), "byte[]" },
        { typeof(sbyte[]), "sbyte[]" },
        { typeof(short[]), "short[]" },
        { typeof(ushort[]), "ushort[]" },
        { typeof(int[]), "int[]" },
        { typeof(uint[]), "uint[]" },
        { typeof(long[]), "long[]" },
        { typeof(ulong[]), "ulong[]" },
        { typeof(float[]), "float[]" },
        { typeof(double[]), "double[]" },
        { typeof(decimal[]), "decimal[]" },
        { typeof(object[]), "object[]" },
        { typeof(bool[]), "bool[]" },
        { typeof(char[]), "char[]" },
        { typeof(string[]), "string[]" },
        { typeof(byte?[]), "byte?[]" },
        { typeof(sbyte?[]), "sbyte?[]" },
        { typeof(short?[]), "short?[]" },
        { typeof(ushort?[]), "ushort?[]" },
        { typeof(int?[]), "int?[]" },
        { typeof(uint?[]), "uint?[]" },
        { typeof(long?[]), "long?[]" },
        { typeof(ulong?[]), "ulong?[]" },
        { typeof(float?[]), "float?[]" },
        { typeof(double?[]), "double?[]" },
        { typeof(decimal?[]), "decimal?[]" },
        { typeof(bool?[]), "bool?[]" },
        { typeof(char?[]), "char?[]" }
    };
    }

    public class RenderHelper
    {


        public string GetTypeName(Type type)
        {
            if (type.IsGenericType)
            {
                var genericArguments = type.GetGenericArguments().Select(t => GetTypeName(t)).ToArray();
                var typeDefinition = type.GetGenericTypeDefinition().FullName;
                typeDefinition = typeDefinition.Substring(0, typeDefinition.IndexOf('`'));
                return string.Format("{0}<{1}>", typeDefinition, string.Join(",", genericArguments));
            }
            else
            {
                return type.AliasOrName();
            }
        }


        //public IEnumerable<Type> GetDomainModelTypes()
        //{
        //    var types = from t in typeof(BaseEntity).Assembly.GetTypes()
        //                where t.IsClass && t.Namespace == "NC.Core.DomainModel"
        //                select t;
        //    return types;
        //}

        public IEnumerable<Type> GetModelTypes()
        {
            var types = typeof(Demo.Core.BaseModels.BaseModel).Assembly.GetTypes();
            var result = types.Where(t => t.Name.ToLower().EndsWith("model") && !t.Name.ToLower().Contains("validator") && !t.Name.ToLower().Contains("base") && !t.Name.ToLower().StartsWith("export"));
            return result.Where(t => !(t.IsClass && t.IsSealed && t.IsAbstract)).ToList();

        }

        public IEnumerable<Type> GetExportModelTypes()
        {
            var types = typeof(Demo.Core.BaseModels.BaseModel).Assembly.GetTypes().Where(t => !t.IsGenericType);
            var result = from t in types
                         let attributes = t.GetCustomAttributes(typeof(GenerateWrapperAttribute), true)
                         where attributes != null && attributes.Length > 0
                         select t;
            return result.Where(t => !(t.IsClass && t.IsSealed && t.IsAbstract)).ToList();

        }

        public IEnumerable<Type> GetDomainModelTypes()
        {
            var types = from t in typeof(BaseEntity).Assembly.GetTypes()
                        where t.IsClass && t.Namespace == "NC.Core.DomainModel"
                        select t;
            return types;
        }

        public List<PropertyInfo> GetSimpleProperties(Type modelType)
        {
            var result = from p in modelType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy)
                         let attributes = p.GetCustomAttributes(typeof(DoNotRenderAttribute), true)
                         where attributes != null && attributes.Length == 0 && (p.PropertyType.IsValueType || p.PropertyType == typeof(string))
                         select p;


            return result.ToList();
        }

        private static readonly Dictionary<Type, string> Aliases = new Dictionary<Type, string>()
                {
                    { typeof(byte), "byte" },
                    { typeof(sbyte), "sbyte" },
                    { typeof(short), "short" },
                    { typeof(ushort), "ushort" },
                    { typeof(int), "int" },
                    { typeof(uint), "uint" },
                    { typeof(long), "long" },
                    { typeof(ulong), "ulong" },
                    { typeof(float), "float" },
                    { typeof(double), "double" },
                    { typeof(decimal), "decimal" },
                    { typeof(object), "object" },
                    { typeof(bool), "bool" },
                    { typeof(char), "char" },
                    { typeof(string), "string" },
                    { typeof(void), "void" }
                };


        public List<PropertyInfo> GetComplexProperties(Type modelType)
        {
            var simpleProperties = GetSimpleProperties(modelType);
            var complexProperties = modelType.GetProperties(BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance).Except(simpleProperties)
                .Where(p => p.PropertyType.IsClass && !typeof(IEnumerable).IsAssignableFrom(p.PropertyType)).ToList();
            return complexProperties;
        }
        public List<PropertyInfo> GetCollectionProperties(Type modelType)
        {

            var simpleProperties = GetSimpleProperties(modelType);
            var complexProperties = GetComplexProperties(modelType);
            var collectionProperties = modelType.GetProperties(BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance).Except(simpleProperties)
              .Except(complexProperties)
              .Where(p => p.PropertyType.IsGenericType).ToList();
            return collectionProperties;
        }

        public List<PropertyInfo> GetArrayProperties(Type modelType)
        {

            var simpleProperties = GetSimpleProperties(modelType);
            var complexProperties = GetComplexProperties(modelType);
            var collectionProperties = GetCollectionProperties(modelType);
            var arrayProperties = modelType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy).Except(simpleProperties)
              .Except(complexProperties).Except(collectionProperties)
              .Where(p => p.PropertyType.IsArray).ToList();
            return arrayProperties;
        }


        public string ResolveNamespace(PropertyInfo property)
        {
            string itemType;

            if (property.PropertyType.GenericTypeArguments[0].IsEnum || property.PropertyType.GenericTypeArguments[0].FullName.Contains("EntityMetadata"))
            {
                itemType = property.PropertyType.GenericTypeArguments[0].FullName;
            }
            else
            {
                itemType = property.PropertyType.GenericTypeArguments[0].FullName.Replace("NC.", "NC.WPF.");
            }

            return itemType;
        }


    }


}
