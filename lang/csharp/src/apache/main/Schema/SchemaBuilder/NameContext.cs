/**
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements.  See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership.  The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License.  You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System.Collections.Generic;
using System.Linq;

namespace Avro
{
    /// <summary>
    /// * internal class for passing the naming context around. This allows for the
    /// following:
    /// <li>Cache and re-use primitive schemas when they do not set
    /// properties.</li>
    ///<li>Provide a default namespace for nested contexts(as
    /// the JSON Schema spec does).</li>
    /// <li>Allow previously defined named types or primitive types
    /// to be referenced by name.</li>
    /// </summary>
    class NameContext
    {
        private static readonly HashSet<string> Primitives = new HashSet<string>
        {
            "null",
            "boolean",
            "int",
            "long",
            "float",
            "double",
            "bytes",
            "string"
        };

        private readonly Dictionary<string, Schema> Schemas;
        private readonly string Namespace;

        public NameContext()
        {
            Schemas = new Dictionary<string, Schema>
            {
                {"null", PrimitiveSchema.NewInstance("null") },
                {"boolean", PrimitiveSchema.NewInstance("boolean") },
                {"int", PrimitiveSchema.NewInstance("int") },
                {"long", PrimitiveSchema.NewInstance("long") },
                {"float", PrimitiveSchema.NewInstance("float") },
                {"double", PrimitiveSchema.NewInstance("double") },
                {"bytes", PrimitiveSchema.NewInstance("bytes") },
                {"string", PrimitiveSchema.NewInstance("string") }
            };
        }

        public NameContext(Dictionary<string, Schema> schemas, string name)
        {
            Schemas = schemas;
            Namespace = string.IsNullOrEmpty(name) ? null : name;
        }

        private NameContext changeNamespace(string name)
        {
            return new NameContext(Schemas, name);
        }

        public Schema GetSchema(string name, string space)
        {
            return getFullName(resolveName(name, space));
        }

        private Schema getFullName(string fullName)
        {
            Schema schema;
            Schemas.TryGetValue(fullName, out schema);
            if (schema == null)
            {
                throw new SchemaParseException($"Undefined name: {fullName}");
            }
            return schema;
        }

        private string resolveName(string name, string space)
        {
            if (Primitives.Contains(name) && space == null)
            {
                return name;
            }

            if (name.Contains('.'))
            {
                var lookupNamespace = space ?? Namespace;
                if (!string.IsNullOrEmpty(lookupNamespace))
                {
                    return $"{lookupNamespace}.{name}";
                }
            }

            return name;
        }
    }
}
