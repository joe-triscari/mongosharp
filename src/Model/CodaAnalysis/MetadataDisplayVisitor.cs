﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Emit;

namespace MongoSharp.Model.CodaAnalysis
{
    class MetadataDisplayVisitor : SymbolVisitor
    {
        private StringBuilder _builder = new StringBuilder();
        public override string ToString()
        {
            return _builder.ToString();
        }
        public override void VisitNamespace(INamespaceSymbol symbol)
        {
            if (!symbol.IsGlobalNamespace)
            {
                base.Visit(symbol.ContainingNamespace);
                _builder.Append(symbol.MetadataName).Append('.');
            }
        }
        public override void VisitArrayType(IArrayTypeSymbol symbol)
        {
            base.Visit(symbol.ElementType);
            _builder.Append('[').Append(',', symbol.Rank - 1).Append(']');
        }
        public override void VisitPointerType(IPointerTypeSymbol symbol)
        {
            this.Visit(symbol.PointedAtType);
            _builder.Append('*');
        }
        public override void VisitDynamicType(IDynamicTypeSymbol symbol)
        {
            _builder.Append(typeof(object).FullName);
        }
        public override void VisitNamedType(INamedTypeSymbol symbol)
        {
            if (symbol.ContainingType != null)
            {
                base.Visit(symbol.ContainingType);
                _builder.Append('+');
            }
            else
            {
                base.Visit(symbol.ContainingNamespace);
            }

            _builder.Append(symbol.MetadataName);

            if (symbol.Arity > 0)
            {
                _builder.Append('[');
                this.VisitTypeArgument(symbol.TypeArguments[0]);
                for (int i = 1; i < symbol.TypeArguments.Length; i++)
                {
                    _builder.Append(", ");
                    this.VisitTypeArgument(symbol.TypeArguments[i]);
                }
                _builder.Append(']');
            }
        }
        private void VisitTypeArgument(ITypeSymbol symbol)
        {
            _builder.Append('[');
            base.Visit(symbol);
            this.AppendAssemblyName(symbol);
            _builder.Append(']');
        }
        private bool ShouldVisit(INamespaceSymbol symbol)
        {
            return symbol != null && !symbol.IsGlobalNamespace;
        }
        public void AppendAssemblyName(ITypeSymbol symbol)
        {
            switch (symbol.TypeKind)
            {
                case TypeKind.ArrayType:
                    this.AppendAssemblyName((symbol as IArrayTypeSymbol).ElementType);
                    break;
                case TypeKind.PointerType:
                    this.AppendAssemblyName((symbol as IPointerTypeSymbol).PointedAtType);
                    break;
                case TypeKind.DynamicType:
                    _builder.Append(", ").Append(typeof(object).Assembly.FullName);
                    break;
                default:
                    _builder.Append(", ").Append(symbol.ContainingAssembly.Identity.GetDisplayName());
                    break;
            }
        }
    }
    static class SymbolExtensions
    {
        public static string ToMetadataName(this ITypeSymbol symbol)
        {
            var visitor = new MetadataDisplayVisitor();
            visitor.Visit(symbol);
            //visitor.AppendAssemblyName(symbol);
            return visitor.ToString();
        }
    }
}
