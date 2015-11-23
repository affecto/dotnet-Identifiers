using Affecto.Patterns.Specification;

namespace Affecto.Identifiers
{
    public class EmailAddressSpecification : RegexSpecification
    {
        public EmailAddressSpecification()
            : base(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", "Email address '{0}' is of invalid format.")
        {
        }
    }
}