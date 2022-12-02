using System.Net;
namespace FinancialManager.Shared.Exceptions.DomainExceptions
{
    public class DateRangeExpeption : DomainException
    {
        private readonly struct DatePair
        {
            public readonly DateTime Begin;
            public readonly DateTime End;
            public DatePair(DateTime begin, DateTime end)
            {
                Begin = begin;
                End = end;
            }
        }

        public DateRangeExpeption(DateTime begin, DateTime end) 
            : base(HttpStatusCode.BadRequest, $"date perion begin({begin}) must " +
                  $"be less or equal than date perion end({end})", new DatePair(begin, end))
        {

        }
    }
}
