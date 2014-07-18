using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobManager.Data.Database.Entities;

namespace JobManager.Data.Database
{
    public class DataContext : DbContext
    {
        private static DataContext _dataContext;

        public DataContext() : base("JobManagerDatabase") { }

        public DbSet<JobDb> Jobs { get; set; }
        public DbSet<TriggerDb> Triggers { get; set; }

        public static DataContext DefaultContext
        {
            get
            {
                return _dataContext ?? (_dataContext = new DataContext());
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JobDb>().Map(m => m.Requires("IsDeleted").HasValue(false)).Ignore(m => m.IsDeleted);
            modelBuilder.Entity<TriggerDb>().Map(m => m.Requires("IsDeleted").HasValue(false)).Ignore(m => m.IsDeleted);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public override int SaveChanges()
        {
            var deletedEntries = ChangeTracker.Entries().Where(p => p.State == EntityState.Deleted).ToList();
            foreach (var entry in deletedEntries)
            {
                SoftDelete(entry);
            }

            var modifiedEntries = ChangeTracker.Entries().Where(p => p.State == EntityState.Modified).ToList();
            foreach (var entry in modifiedEntries)
            {
                UpdateEntry(entry);
            }

            var addedEntries = ChangeTracker.Entries().Where(p => p.State == EntityState.Added).ToList();
            foreach (var entry in addedEntries)
            {
                AddEntry(entry);
            }

            return base.SaveChanges();
        }

        private void AddEntry(DbEntityEntry entry)
        {
            var entity = entry.Entity as BaseEntity;
            if (entity != null)
            {
                entity.CreatedWhen = DateTime.Now;
            }
        }

        private void UpdateEntry(DbEntityEntry entry)
        {
            var entity = entry.Entity as BaseEntity;
            if (entity != null)
            {
                entity.UpdatedWhen = DateTime.Now;
            }
        }

        // Soft Delete pattern for EF from: http://www.wiktorzychla.com/2013/10/soft-delete-pattern-for-entity.html
        private void SoftDelete(DbEntityEntry entry)
        {
            Type entryEntityType = entry.Entity.GetType();

            string tableName = GetTableName(entryEntityType);
            string primaryKeyName = GetPrimaryKeyName(entryEntityType);

            string deletequery = string.Format("UPDATE {0} SET IsDeleted = 1 WHERE {1} = @id; UPDATE {0} SET DeletedOn = GETDATE() WHERE {1} = @id", tableName, primaryKeyName);

            Database.ExecuteSqlCommand(deletequery, new SqlParameter("@id", entry.OriginalValues[primaryKeyName]));

            //Marking it Unchanged prevents the hard delete
            //entry.State = EntityState.Unchanged;
            //So does setting it to Detached:
            //And that is what EF does when it deletes an item
            //http://msdn.microsoft.com/en-us/data/jj592676.aspx
            entry.State = EntityState.Detached;
        }

        private static Dictionary<Type, EntitySetBase> _mappingCache = new Dictionary<Type, EntitySetBase>();

        private EntitySetBase GetEntitySet(Type type)
        {
            if (!_mappingCache.ContainsKey(type))
            {
                ObjectContext octx = ((IObjectContextAdapter)this).ObjectContext;

                string typeName = ObjectContext.GetObjectType(type).Name;

                var es = octx.MetadataWorkspace
                                .GetItemCollection(DataSpace.SSpace)
                                .GetItems<EntityContainer>()
                                .SelectMany(c => c.BaseEntitySets
                                                .Where(e => e.Name == typeName))
                                .FirstOrDefault();

                if (es == null)
                    throw new ArgumentException("Entity type not found in GetTableName", typeName);

                _mappingCache.Add(type, es);
            }

            return _mappingCache[type];
        }

        private string GetTableName(Type type)
        {
            EntitySetBase es = GetEntitySet(type);

            return string.Format("[{0}].[{1}]",
                es.MetadataProperties["Schema"].Value,
                es.MetadataProperties["Table"].Value);
        }

        private string GetPrimaryKeyName(Type type)
        {
            EntitySetBase es = GetEntitySet(type);

            return es.ElementType.KeyMembers[0].Name;
        }
    }
}
