using System;
using System.Linq.Expressions;

namespace Cinema.BusinessLogic.Searching
{
    public interface ISearchBuilder<TEntity>
    {
        Expression<Func<TEntity, bool>> Filter { get; set; }

        Expression<Func<TEntity, bool>> Build();
    }

}
