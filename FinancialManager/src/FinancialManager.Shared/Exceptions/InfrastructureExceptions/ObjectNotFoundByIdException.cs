﻿using System;
using System.Net;

namespace FinancialManager.Shared.Exceptions.InfrastructureExceptions
{
    public class ObjectNotFoundByIdException : InfrastructureExceptions
    {
        public ObjectNotFoundByIdException(Type objectType, int id) : base(HttpStatusCode.NotFound, $"Object of type {objectType} with id={id} not found")
        {

        }
    }
}
