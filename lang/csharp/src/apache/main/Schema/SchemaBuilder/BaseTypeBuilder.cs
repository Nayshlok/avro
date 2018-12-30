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
namespace Avro
{
    class BaseTypeBuilder<T>
    {
        private readonly Completion<T> _context;
        private readonly NameContext _names;

        public BaseTypeBuilder(Completion<T> context, NameContext names)
        {
            _context = context;
            _names = names;
        }

        public T Type(Schema schema)
        {
            return _context.Complete(schema);
        }

        public T Type(string name)
        {
            return Type(name, null);
        }

        public T Type(string name, string space)
        {
            return Type(_names.GetSchema(name, space)));
        }
    }
}
