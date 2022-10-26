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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FinancialManagerTest.Mocks.Data
{
    internal class MockDbSet<TEntity> : Mock<DbSet<TEntity>> where TEntity : class
    {
        IQueryable<TEntity> _query;
        List<TEntity> _entities;
        public MockDbSet(List<TEntity> entities)
        {
            _entities = entities;
            _query = entities.AsQueryable();
            As<IQueryable<TEntity>>()
                .Setup(m => m.Provider)
                .Returns(new TestAsyncEnumerableEfCore<TEntity>(_query));

            As<IQueryable<TEntity>>().Setup(m => m.Expression).Returns(_query.Expression);
            As<IQueryable<TEntity>>().Setup(m => m.ElementType).Returns(_query.ElementType);
            As<IAsyncEnumerable<TEntity>>()
                .Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                .Returns(new TestAsyncEnumerator<TEntity>(entities.GetEnumerator()));
            Setup(m => m.Remove(It.IsAny<TEntity>())).Callback<TEntity>(entity => entities.Remove(entity));
            Setup(m => m.Add(It.IsAny<TEntity>())).Callback<TEntity>(entity => entities.Add(entity));
        }
    }
}