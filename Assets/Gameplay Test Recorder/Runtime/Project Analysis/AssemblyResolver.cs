using JetBrains.Annotations;
using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace TwoGuyGames.GTR.Core
{
    public class AssemblyResolver : IAssemblyResolver
    {
        [NotNull] public static readonly AssemblyResolver INSTANCE;
        private static readonly Assembly[] assemblies;
        private static Dictionary<string, AssemblyDefinition> readDefinitions;

        static AssemblyResolver()
        {
            INSTANCE = new AssemblyResolver();
            readDefinitions = new Dictionary<string, AssemblyDefinition>();
            assemblies = AppDomain.CurrentDomain.GetAssemblies();
        }

        public static AssemblyDefinition ReadAssembly(Assembly assembly)
        {
            try
            {
                if (readDefinitions.TryGetValue(assembly.FullName, out AssemblyDefinition def))
                {
                    return def;
                }
                else
                {
                    ReaderParameters readerParameters = new ReaderParameters { AssemblyResolver = INSTANCE };
                    readerParameters.ReadWrite = false;
                    AssemblyDefinition aDef = AssemblyDefinition.ReadAssembly(assembly.Location, readerParameters);
                    readDefinitions[assembly.FullName] = aDef;
                    return aDef;
                }
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                return null;
            }
        }

        public static AssemblyDefinition ReadAssemblyFromType(Type type)
        {
            return ReadAssembly(type.Assembly);
        }

        public static bool TryFindTypeDefinition(AssemblyDefinition assembly, Type type,
            out TypeDefinition typeDefinition)
        {
            try
            {
                typeDefinition = FindTypeDefinition(assembly, type);
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError(type);
                Debug.LogError(type.Assembly);
                Debug.LogError($"Could not find type `{type.AssemblyQualifiedName}`.");
                Debug.LogException(ex);
                typeDefinition = null;
                return false;
            }
        }

        public void Dispose()
        {
            foreach (AssemblyDefinition def in readDefinitions.Values)
            {
                def.Dispose();
            }

            readDefinitions.Clear();
        }

        public AssemblyDefinition Resolve(AssemblyNameReference name)
        {
            string fullName = name.FullName;
            try
            {
                Assembly assembly = assemblies.First(a => a.FullName.Equals(fullName));
                return ReadAssembly(assembly);
            }
            catch (Exception)
            {
                Debug.LogWarning($"Could not find type `{fullName}`.");
                return null;
            }
        }

        public AssemblyDefinition Resolve(AssemblyNameReference name, ReaderParameters parameters)
        {
            throw new NotImplementedException();
        }

        private static TypeDefinition FindNestedType(AssemblyDefinition assembly, Type type)
        {
            string name = type.FullName.Replace('+', '/'); // mono cecil uses + instead of / to delimit nested classes.
            if (TryFindTypeDefinition(assembly, type.DeclaringType, out TypeDefinition outerClass))
            {
                foreach (TypeDefinition nestedType in outerClass.NestedTypes)
                {
                    if (nestedType.FullName.Equals(name))
                    {
                        return nestedType;
                    }
                }
            }

            return null;
        }

        private static TypeDefinition FindTypeDefinition(AssemblyDefinition assembly, Type type)
        {
            if (type.IsNested)
            {
                return FindNestedType(assembly, type);
            }
            else
            {
                TypeDefinition typeDef = assembly.MainModule.GetType(type.FullName);
                return typeDef;
            }
        }
    }
}