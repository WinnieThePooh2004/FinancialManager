﻿using System.Net;

namespace FinancialManager.Shared.Exceptions.DomainExceptions
{
    public class NullIdException : DomainException
    {
        public NullIdException() : base(HttpStatusCode.BadRequest, "Can`t find object when id is null")
        {
        }
    }
}
