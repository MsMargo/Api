using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace MedicalApp.Services
{
    public class JsonNullableStringValueProvider : IValueProvider
    {
        private readonly IValueProvider _underlyingValueProvider;

        public JsonNullableStringValueProvider(MemberInfo memberInfo)
        {
            _underlyingValueProvider = new DynamicValueProvider(memberInfo);
        }

        public void SetValue(object target, object value)
        {
            _underlyingValueProvider.SetValue(target, value);
        }

        public object GetValue(object target)
        {
            return _underlyingValueProvider.GetValue(target) ?? "";
        }
    }
}