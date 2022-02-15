using System;

namespace EmailSender.WebApi.Options
{
    public partial class FormClientMetadata : IEquatable<FormClientMetadata>
    {
        public bool Equals(FormClientMetadata other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(ClientId, other.ClientId, StringComparison.OrdinalIgnoreCase);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((FormClientMetadata) obj);
        }

        public override int GetHashCode()
        {
            return (ClientId != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(ClientId) : 0);
        }

        public static bool operator ==(FormClientMetadata left, FormClientMetadata right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(FormClientMetadata left, FormClientMetadata right)
        {
            return !Equals(left, right);
        }
    }
}