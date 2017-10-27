using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace PM.EntityFramework.EntityFramework.Repositories
{
    public class PMRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<PMDBContext, TEntity, TPrimaryKey>
        where TEntity:class,IEntity<TPrimaryKey>
    {
        public PMRepositoryBase(IDbContextProvider<PMDBContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        //为所有存储库添加常用方法
    }

    public class PMRepositoryBase<TEntity> : PMRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        public PMRepositoryBase(IDbContextProvider<PMDBContext> dbContextProvider) : base(dbContextProvider)
        {
        }
        //不要在这里添加任何方法，添加到上面的类（因为它继承它）
    }

}
