using FinancialManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Net.Sockets;
using System.Reflection.Metadata;
using MockQueryable.EntityFrameworkCore;

namespace FinancialManagerTest.Mocks.Data
{
    internal class MockDbSet<T> : Mock<DbSet<T>> where T : class
    {
        IQueryable<T> _query;
        List<T> _entities;
        public MockDbSet(List<T> entities)
        {
            _entities = entities;
            _query = entities.AsQueryable();
            As<IAsyncEnumerable<T>>()
                .Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                .Returns(new TestAsyncEnumerator<T>(_query.GetEnumerator()));

            As<IQueryable<T>>()
                .Setup(m => m.Provider)
                .Returns(new TestAsyncEnumerableEfCore<T>(_query));

            As<IQueryable<T>>().Setup(m => m.Expression).Returns(_query.Expression);
            As<IQueryable<T>>().Setup(m => m.ElementType).Returns(_query.ElementType);
            As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => _entities.GetEnumerator());
            As<IAsyncEnumerable<T>>()
                .Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                .Returns(new TestAsyncEnumerator<T>(_entities.GetEnumerator()));
            Setup(m => m.Add(It.IsAny<T>()))
                .Callback<T>(entity => _entities.Add(entity));
        }
    }
}