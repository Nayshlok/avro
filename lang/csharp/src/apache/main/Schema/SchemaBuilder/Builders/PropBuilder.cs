using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Avro
{
    abstract class PropBuilder<T> where T : PropBuilder<T>
    {
        private IDictionary<string, JToken> _props;
        private bool HasProps => _props != null;

        protected PropBuilder() { }

        public T Prop(string name, string value)
        {

        }

        public T Prop(string name, object value)
        {

        }

        public T Prop(string name, JToken val)
        {

        }
    }
}
