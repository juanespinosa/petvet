namespace VetPet.Infrastructure.Data.Seedwork
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using Domain.Seedwork;

    /// <summary>
    /// Repository base class
    /// </summary>
    /// <typeparam name="TEntity">The type of underlying entity in this repository</typeparam>
    public class Repository<TEntity> :IRepository<TEntity>
        where TEntity:Entity
    {
        #region Members

        IQueryableUnitOfWork _UnitOfWork;
       
        #endregion

        #region Constructor

        /// <summary>
        /// Create a new instance of repository
        /// </summary>
        /// <param name="unitOfWork">Associated Unit Of Work</param>
        public Repository(IQueryableUnitOfWork unitOfWork)
        {
            if (unitOfWork == (IUnitOfWork)null)
                throw new ArgumentNullException("unitOfWork");

            _UnitOfWork = unitOfWork;
        }

        #endregion

        #region IRepository Members

        /// <summary>
        /// <see cref="VetPet.Domain.Seedwork.IRepository{TValueObject}"/>
        /// </summary>
        public IUnitOfWork UnitOfWork
        {
            get 
            {
                return _UnitOfWork;
            }
        }

        /// <summary>
        /// <see cref="VetPet.Domain.Seedwork.IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="item"><see cref="VetPet.Domain.Seedwork.IRepository{TValueObject}"/></param>
        public virtual void Add(TEntity item)
        {

            if (item != (TEntity)null)
                GetSet().Add(item); // add new item in this set
            
        }
        /// <summary>
        /// <see cref="VetPet.Domain.Seedwork.IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="item"><see cref="VetPet.Domain.Seedwork.IRepository{TValueObject}"/></param>
        public virtual void Remove(TEntity item)
        {
            if (item != (TEntity)null)
            {
                //attach item if not exist
                _UnitOfWork.Attach(item);

                //set as "removed"
                GetSet().Remove(item);
            }
        }

        /// <summary>
        /// <see cref="VetPet.Domain.Seedwork.IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="item"><see cref="VetPet.Domain.Seedwork.IRepository{TValueObject}"/></param>
        public virtual void TrackItem(TEntity item)
        {
            if (item != (TEntity)null)
                _UnitOfWork.Attach<TEntity>(item);
        }

        /// <summary>
        /// <see cref="VetPet.Domain.Seedwork.IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="item"><see cref="VetPet.Domain.Seedwork.IRepository{TValueObject}"/></param>
        public virtual void Modify(TEntity item)
        {
            if (item != (TEntity)null)
                _UnitOfWork.SetModified(item);
        }

        /// <summary>
        /// <see cref="VetPet.Domain.Seedwork.IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="id"><see cref="VetPet.Domain.Seedwork.IRepository{TValueObject}"/></param>
        /// <returns><see cref="VetPet.Domain.Seedwork.IRepository{TValueObject}"/></returns>
        public virtual TEntity Get(Guid id)
        {
            if (id != Guid.Empty)
                return GetSet().Find(id);
            else
                return null;
        }
        /// <summary>
        /// <see cref="VetPet.Domain.Seedwork.IRepository{TValueObject}"/>
        /// </summary>
        /// <returns><see cref="VetPet.Domain.Seedwork.IRepository{TValueObject}"/></returns>
        public virtual IEnumerable<TEntity> GetAll()
        {
            return GetSet();
        }
        /// <summary>
        /// <see cref="VetPet.Domain.Seedwork.IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="specification"><see cref="VetPet.Domain.Seedwork.IRepository{TValueObject}"/></param>
        /// <returns><see cref="VetPet.Domain.Seedwork.IRepository{TValueObject}"/></returns>
        public virtual IEnumerable<TEntity> AllMatching(Domain.Seedwork.Specification.ISpecification<TEntity> specification)
        {
            return GetSet().Where(specification.SatisfiedBy());
        }
        /// <summary>
        /// <see cref="VetPet.Domain.Seedwork.IRepository{TValueObject}"/>
        /// </summary>
        /// <typeparam name="S"><see cref="VetPet.Domain.Seedwork.IRepository{TValueObject}"/></typeparam>
        /// <param name="pageIndex"><see cref="VetPet.Domain.Seedwork.IRepository{TValueObject}"/></param>
        /// <param name="pageCount"><see cref="VetPet.Domain.Seedwork.IRepository{TValueObject}"/></param>
        /// <param name="orderByExpression"><see cref="VetPet.Domain.Seedwork.IRepository{TValueObject}"/></param>
        /// <param name="ascending"><see cref="VetPet.Domain.Seedwork.IRepository{TValueObject}"/></param>
        /// <returns><see cref="VetPet.Domain.Seedwork.IRepository{TValueObject}"/></returns>
        public virtual IEnumerable<TEntity> GetPaged<KProperty>(int pageIndex, int pageCount, System.Linq.Expressions.Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending)
        {
            var set = GetSet();

            if (ascending)
            {
                return set.OrderBy(orderByExpression)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount);
            }
            else
            {
                return set.OrderByDescending(orderByExpression)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount);
            }
        }
        /// <summary>
        /// <see cref="VetPet.Domain.Seedwork.IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="filter"><see cref="VetPet.Domain.Seedwork.IRepository{TValueObject}"/></param>
        /// <returns><see cref="VetPet.Domain.Seedwork.IRepository{TValueObject}"/></returns>
        public virtual IEnumerable<TEntity> GetFiltered(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter)
        {
            return GetSet().Where(filter);
        }

        /// <summary>
        /// <see cref="VetPet.Domain.Seedwork.IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="persisted"><see cref="VetPet.Domain.Seedwork.IRepository{TValueObject}"/></param>
        /// <param name="current"><see cref="VetPet.Domain.Seedwork.IRepository{TValueObject}"/></param>
        public virtual void Merge(TEntity persisted, TEntity current)
        {
            _UnitOfWork.ApplyCurrentValues(persisted, current);
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// <see cref="M:System.IDisposable.Dispose"/>
        /// </summary>
        public void Dispose()
        {
            if (_UnitOfWork != null)
                _UnitOfWork.Dispose();
        }

        #endregion

        #region Private Methods

        public IDbSet<TEntity> GetSet()
        {
            return  _UnitOfWork.CreateSet<TEntity>();
        }
        #endregion
    }
}
