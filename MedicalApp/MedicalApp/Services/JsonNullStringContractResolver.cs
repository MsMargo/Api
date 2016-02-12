using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace MedicalApp.Services
{
    public class JsonNullStringContractResolver: DefaultContractResolver
    {
        protected override IValueProvider CreateMemberValueProvider(MemberInfo member)
        {
            if (member.MemberType == MemberTypes.Property)
            {
                var pi = (PropertyInfo)member;

                if (pi.PropertyType == typeof(string))
                {
                    return new JsonNullableStringValueProvider(member);
                }
            }
            else if (member.MemberType == MemberTypes.Field)
            {
                var fi = (FieldInfo)member;
                if (fi.FieldType == typeof(string))
                {
                    return new JsonNullableStringValueProvider(member);
                }
            }

            return base.CreateMemberValueProvider(member);
        }
    }
}