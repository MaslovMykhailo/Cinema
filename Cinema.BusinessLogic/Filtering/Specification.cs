using System;
using System.Linq.Expressions;

namespace Cinema.BusinessLogic.Filtering
{
    public abstract class Specification<T>
    {
        public abstract Expression<Func<T, bool>> IsSatisfiedBy();
    }
}
