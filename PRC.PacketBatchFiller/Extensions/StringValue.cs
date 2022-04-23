using System;

namespace PRC.PacketBatchFiller.Extensions
{
    //string valueOfAuthenticationMethod = StringEnum.GetStringValue(AuthenticationMethod.FORMS);

    public class StringValue : Attribute
    {
        public StringValue(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }

    public static class StringEnum
    {
        public static string GetStringValue(Enum value)
        {
            var type = value.GetType();

            var fieldInfo = type.GetField(value.ToString());
            var attributes = fieldInfo.GetCustomAttributes(typeof (StringValue), false) as StringValue[];

            if (attributes != null && attributes.Length > 0) return attributes[0].Value;

            return null;
        }
    }
}