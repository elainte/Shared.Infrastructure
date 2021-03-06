﻿using Autofac;
using Cassandra;
using System;

namespace Shared.Infrastructure.UnitOfWork.Cassandra
{
    public class CassandraUnitOfWork : UnitOfWorkBase, IUnitOfWork
    {
        private ISession Session { get; set; }

        private ILifetimeScope LifetimeScope { get; set; }

        internal CassandraUnitOfWork(ILifetimeScope lifetimeScope)
        {
            LifetimeScope = lifetimeScope ?? throw new ArgumentNullException(nameof(lifetimeScope));
            this.Session = this.LifetimeScope.Resolve<ISession>();
        }

        public override ITransaction BeginTransaction()
        {
            throw new NotImplementedException();
        }

        protected override T ResolveRepository<T>()
        {
            return this.LifetimeScope.Resolve<T>();
        }

        public override void Dispose()
        {
            base.Dispose();

            if (this.Session != null)
            {
                this.Session.Dispose();
            }
            if (this.LifetimeScope != null)
            {
                this.LifetimeScope.Dispose();
            }
        }
    }
}
