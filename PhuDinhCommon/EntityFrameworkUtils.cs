using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace PhuDinhCommon
{
    public class EntityFrameworkUtils
    {
        public static void UndoContextChange(DbContext context, EntityState skippedState)
        {
            // Undo the changes of the all entries. 
            foreach (DbEntityEntry entry in context.ChangeTracker.Entries())
            {
                if ((skippedState & entry.State) != 0)
                {
                    continue;
                }

                UndoEntityEntryChange(entry);
            }
        }

        public static void UndoEntityTChange<T>(DbContext context, EntityState skippedState) where T : class
        {
            // Undo the changes of the all entries. 
            foreach (DbEntityEntry entry in context.ChangeTracker.Entries<T>())
            {
                if ((skippedState & entry.State) != 0)
                {
                    continue;
                }

                UndoEntityEntryChange(entry);
            }
        }

        public static void UndoEntityChange(DbContext context, object entity, EntityState skippedState)
        {
            var entry = context.Entry(entity);

            if ((skippedState & entry.State) != 0)
            {
                return;
            }

            UndoEntityEntryChange(entry);
        }

        public static void UndoEntityPropertyChange(DbContext context, object entity, string propertyName)
        {
            DbEntityEntry entry = context.Entry(entity);
            if (entry.State == EntityState.Added || entry.State == EntityState.Detached)
            {
                return;
            }

            entry.Property(propertyName).CurrentValue = entry.Property(propertyName).OriginalValue;
        }

        public static System.DateTime? GetTableLastUpdate(DbContext context, string tableName)
        {
            const string sqlLastUpdatePattern = "SELECT last_user_update FROM sys.dm_db_index_usage_stats WHERE database_id=DB_ID('{0}') AND OBJECT_ID=OBJECT_ID('{1}')";
            string sql = string.Format(sqlLastUpdatePattern, context.Database.Connection.Database, tableName);
            return context.Database.SqlQuery<System.DateTime?>(sql).ToList()[0];
        }

        public static System.DateTime? GetTablesLastUpdate(DbContext context, List<string> tablesName)
        {
            const string sqlLastUpdatePattern = "SELECT MAX(last_user_update) FROM sys.dm_db_index_usage_stats WHERE database_id=DB_ID('{0}') AND OBJECT_ID in ({1})";
            string s = "OBJECT_ID('" + tablesName[0] + "')";
            for (int i = 1; i < tablesName.Count; i++)
                s += ",OBJECT_ID('" + tablesName[i] + "')";
            string sql = string.Format(sqlLastUpdatePattern, context.Database.Connection.Database, s);
            return context.Database.SqlQuery<System.DateTime?>(sql).ToList()[0];
        }

        private static void UndoEntityEntryChange(DbEntityEntry entry)
        {
            switch (entry.State)
            {
                // Under the covers, changing the state of an entity from  
                // Modified to Unchanged first sets the values of all  
                // properties to the original values that were read from  
                // the database when it was queried, and then marks the  
                // entity as Unchanged. This will also reject changes to  
                // FK relationships since the original value of the FK  
                // will be restored. 
                case EntityState.Modified:
                    entry.State = EntityState.Unchanged;
                    break;
                case EntityState.Added:
                    entry.State = EntityState.Detached;
                    break;
                // If the EntityState is the Deleted, reload the date from the database.   
                case EntityState.Deleted:
                    entry.Reload();
                    break;
                default: break;
            }
        }
    }
}
