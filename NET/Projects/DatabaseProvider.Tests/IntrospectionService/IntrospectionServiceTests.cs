/* 
 This source code (the "Generated Software") is generated by the OutSystems Platform 
 and is licensed by OutSystems (http://www.outsystems.com) to You solely for testing and evaluation 
 purposes, unless You and OutSystems have executed a specific agreement covering the use terms and 
 conditions of the Generated Software, in which case such agreement shall apply. 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NUnitExtension.OutSystems.Framework;
using OutSystems.HubEdition.Extensibility.Data;
using OutSystems.HubEdition.Extensibility.Data.DatabaseObjects;
using OutSystems.HubEdition.Extensibility.Data.Platform.DatabaseObjects;
using OutSystems.HubEdition.Extensibility.Data.IntrospectionService;
using OutSystems.RuntimeCommon;
using OutSystems.ServerTests.DatabaseProvider.Framework;

namespace OutSystems.ServerTests.DatabaseProvider.IntrospectionService {

    public class TestConfiguration : AgnosticDatabaseProviderTestConfiguration {

        protected override string ConfigurationPathSettingName {
            get {
                return "DatabaseProviderTests.IntrospectionServiceFilesPath";
            }
        }
    }
    
    [DashboardTestFixture(DashboardTest.DashboardTestKind)]
    public class IntrospectionServiceTests : DatabaseProviderTest<TestConfiguration> {

        internal class ExpectedTableSource : ITableSourceInfo {

            public ExpectedTableSource(string name) {
                Database = null;
                Name = name;
                QualifiedName = null;
            }

            public IDatabaseInfo Database { get; private set; }

            public string Name { get; private set; }

            public string DisplayName {
                get { return Name; }
            }

            public string QualifiedName { get; private set; }

            public bool Equals(ITableSourceInfo other) {
                return Name.EqualsIgnoreCase(other.Name);
            }

            public override bool Equals(object obj) {
                var other = obj as ExpectedTableSource;
                return (other != null) && Equals(other);
            }

            public override int GetHashCode() {
                return Name.GetHashCode();
            }
        }

        internal class ExpectedColumnInfo : IPlatformTableSourceColumnInfo {

            public ExpectedColumnInfo(string name, IPlatformDataTypeInfo dataType, bool isMandatory, bool isPrimaryKey, bool isAutoGenerated)
                : this(name, dataType, isMandatory, isPrimaryKey, isAutoGenerated, null) {}

            public ExpectedColumnInfo(string name, IPlatformDataTypeInfo dataType, bool isMandatory, bool isPrimaryKey, bool isAutoGenerated, object defaultValue) {
                TableSource = null;
                Name = name;
                DataType = dataType;
                IsMandatory = isMandatory;
                IsPrimaryKey = isPrimaryKey;
                IsAutoGenerated = isAutoGenerated;
                DefaultValue = defaultValue;
            }

            public ITableSourceInfo TableSource { get; set; }

            public string Name { get; private set; }

            public IPlatformDataTypeInfo DataType { get; set; }

            public bool IsMandatory { get; set; }

            public bool IsPrimaryKey { get; set; }

            public bool IsAutoGenerated { get; set; }

            public object DefaultValue { get; set; }

            IDataTypeInfo ITableSourceColumnInfo.DataType {
                get { return DataType; }
            }

        }

        internal class ExpectedDataTypeInfo : IPlatformDataTypeInfo {

            public ExpectedDataTypeInfo(DBDataType type) : this(type, 0, 0) { }

            public ExpectedDataTypeInfo(DBDataType type, int length) : this(type, length, 0) { }

            public ExpectedDataTypeInfo(DBDataType type, int length, int decimals) {
                Type = type;
                SqlDataType = "";
                Length = length;
                Decimals = decimals;
            }

            public DBDataType Type { get; private set; }

            public string SqlDataType { get; private set; }

            public int Length { get; private set; }

            public int Decimals { get; private set; }

            public bool IsEquivalent(IDataTypeInfo other) {
                return SqlDataType == other.SqlDataType;
            }
        }

        internal class ExpectedForeignKeyInfo : ITableSourceForeignKeyInfo {

            public ExpectedForeignKeyInfo(ITableSourceInfo tableSource, string name, string columnName,
                                          ITableSourceInfo referencedTableSource, string referencedColumnName) {
                TableSource = tableSource;
                Name = name;
                ColumnName = columnName;
                ReferencedTableSource = referencedTableSource;
                ReferencedColumnName = referencedColumnName;
            }

            public ITableSourceInfo TableSource { get; private set; }

            public string Name { get; private set; }

            public string ColumnName { get; private set; }

            public ITableSourceInfo ReferencedTableSource { get; private set; }

            public string ReferencedColumnName { get; private set; }

            public bool IsCascadeDelete { get; private set; }
        }

        private const int UNCHECKED_LENGTH = -1;
        private const int UNCHECKED_DECIMALS = -1;

        private static readonly ExpectedTableSource tableIntrospectionTestTable = new ExpectedTableSource("IntrospectionTestTable");
        private static readonly ExpectedTableSource viewIntrospectionViewOverTestTable = new ExpectedTableSource("IntrospectionViewOverTestTable");
        private static readonly ExpectedTableSource tableSelect = new ExpectedTableSource("SELECT");
        private static readonly ExpectedTableSource viewFrom = new ExpectedTableSource("FROM");
        private static readonly ExpectedTableSource tableForeignKeysTable = new ExpectedTableSource("ForeignKeysTable");

        // We are deliberately not testing the DBDataType.DATE and DBDataType.TIME types because there isn't much support for this concepts (at least separated from DATETIME) in different database vendors

        private static readonly ExpectedColumnInfo colId = new ExpectedColumnInfo("ID", new ExpectedDataTypeInfo(DBDataType.INTEGER), true, true, true);
        private static readonly ExpectedColumnInfo colDbText = new ExpectedColumnInfo("DBTEXT", new ExpectedDataTypeInfo(DBDataType.TEXT, 255), true, false, false);
        private static readonly ExpectedColumnInfo colDbInteger = new ExpectedColumnInfo("DBINTEGER", new ExpectedDataTypeInfo(DBDataType.INTEGER), false, false, false);
        private static readonly ExpectedColumnInfo colDbDecimal = new ExpectedColumnInfo("DBDECIMAL", new ExpectedDataTypeInfo(DBDataType.DECIMAL, 37, 8), false, false, false);
        private static readonly ExpectedColumnInfo colDbBoolean = new ExpectedColumnInfo("DBBOOLEAN", new ExpectedDataTypeInfo(DBDataType.BOOLEAN), false, false, false);
        private static readonly ExpectedColumnInfo colDbDateTime = new ExpectedColumnInfo("DBDATETIME", new ExpectedDataTypeInfo(DBDataType.DATE_TIME), false, false, false);
        private static readonly ExpectedColumnInfo colDbBinaryData = new ExpectedColumnInfo("DBBINARYDATA", new ExpectedDataTypeInfo(DBDataType.BINARY_DATA), false, false, false);
        
        private static readonly ExpectedColumnInfo[] expectedColumns = { colId, colDbText, colDbInteger, colDbDecimal, colDbBoolean, colDbDateTime, colDbBinaryData };

        private static readonly ExpectedForeignKeyInfo fkInstrospectionId = new ExpectedForeignKeyInfo(tableForeignKeysTable,
            "FK_FKT_IntrospectionTestTable", "INTROSPECTIONID", tableIntrospectionTestTable, "ID");
        private static readonly ExpectedForeignKeyInfo fkSelectId = new ExpectedForeignKeyInfo(tableForeignKeysTable,
            "FK_FKT_Select", "SELECTID", tableSelect, "TEXTID");

        private static readonly ExpectedForeignKeyInfo[] expectedForeignKeys = {fkInstrospectionId, fkSelectId};

        private readonly string[] bootstrappedTableName = { tableIntrospectionTestTable.Name, viewIntrospectionViewOverTestTable.Name, tableSelect.Name, viewFrom.Name, tableForeignKeysTable.Name };

        public static string GetDatabaseIdentifier(IDatabaseServices services) {
            return services.DMLService.Identifiers.EscapeIdentifier(services.DatabaseConfiguration.DatabaseIdentifier);
        }
    
        [IterativeTestCase(typeof(TestConfiguration), Description = "Validates that current database is returned in the list of databases, by looking for the database name associated with the current IDatabaseConfiguration.")]
        [TestDetails(TestIssue = "622296", Feature = "Database Abstraction Layer", CreatedBy = "rls")]
        public void TestListDatabasesFindCurrentDatabase(DatabaseProviderTestCase tc) {
            var databaseServices = tc.Services;
            IEnumerable<IDatabaseInfo> databases = databaseServices.IntrospectionService.ListDatabases();
            IDatabaseInfo currentDBInfo = databaseServices.ObjectFactory.CreateDatabaseInfo(GetDatabaseIdentifier(databaseServices));
            Assert.IsNotNull(databases.FirstOrDefault(db => db.Equals(currentDBInfo)), "Current database (" + currentDBInfo.Identifier + ") not found in the list.");
        }
        
        [IterativeTestCase(typeof(TestConfiguration), Description = "Validates that all the table sources created by the bootstrap script appear in the list.")]
        [TestDetails(TestIssue = "622296", Feature = "Database Abstraction Layer", CreatedBy = "rls")]
        public void TestListTableSourcesFindAllExpectedTableSources(DatabaseProviderTestCase tc) {
            var databaseServices = tc.Services;
            IDatabaseInfo db = databaseServices.ObjectFactory.CreateDatabaseInfo(GetDatabaseIdentifier(databaseServices));
            IEnumerable<ITableSourceInfo> tableSources = databaseServices.IntrospectionService.ListTableSourcesWithoutFilter(db);
            foreach (var tableSourceName in bootstrappedTableName) {
                Assert.IsNotNull(tableSources.FirstOrDefault(ts => ts.Name.Equals(tableSourceName, StringComparison.InvariantCultureIgnoreCase)), "Table source named '" + tableSourceName + "' not found in the database");
            }
        }

        [IterativeTestCase(typeof(TestConfiguration), Description = "Validates that all the ListTableSources can receive a filter.")]
        [TestDetails(TestIssue = "629873", Feature = "Database Abstraction Layer", CreatedBy = "rfe")]
        public void TestListTableSourcesFilter(DatabaseProviderTestCase tc) {
            var databaseServices = tc.Services;
            IDatabaseInfo db = databaseServices.ObjectFactory.CreateDatabaseInfo(GetDatabaseIdentifier(databaseServices));
            string tableToFilter = tableIntrospectionTestTable.Name;    // We'll ignore the tableIntrospectionTestTable

            IEnumerable<ITableSourceInfo> tableSources = databaseServices.IntrospectionService.ListTableSources(db, 
                tableName => tableName.Equals(tableToFilter, StringComparison.InvariantCultureIgnoreCase));

            Assert.IsFalse(tableSources.Any(ts => ts.Name.Equals(tableToFilter, StringComparison.InvariantCultureIgnoreCase)), 
                "Table source named '" + tableToFilter + "' was not filtered during the ListTableSources");

            foreach (var tableSourceName in bootstrappedTableName.Except(tableToFilter.ToEnumerable())) {
                Assert.IsTrue(tableSources.Any(ts => ts.Name.Equals(tableSourceName, StringComparison.InvariantCultureIgnoreCase)), 
                    "Table source named '" + tableSourceName + "' not found in the database");
            }
        }

        [IterativeTestCase(typeof(TestConfiguration), Description = "Validates that it is possible to query all the table sources by using their qualified name. This test depends on the IExecutionService.ExecuteScalar().")]
        [TestDetails(TestIssue = "622296", Feature = "Database Abstraction Layer", CreatedBy = "rls")]
        public void TestListTableSourcesValidateAllQualifiedNames(DatabaseProviderTestCase tc) {
            var databaseServices = tc.Services;
            IDatabaseInfo db = databaseServices.ObjectFactory.CreateDatabaseInfo(GetDatabaseIdentifier(databaseServices));
            IEnumerable<ITableSourceInfo> tableSources = databaseServices.IntrospectionService.ListTableSourcesWithoutFilter(db);
            foreach (var tableSource in tableSources) {
                SQLExecutor sqlExecutor = new SQLExecutor(databaseServices);
                string sql = "SELECT COUNT(*) FROM " + tableSource.QualifiedName;
                Scalar result = sqlExecutor.ExecuteScalar(sql);
                Assert.IsTrue(result.Value<int>() >= 0, "Error counting the rows of table source '" + tableSource.Name + "' using the SQL: " + sql);
            }
        }

        [IterativeTestCase(typeof(TestConfiguration), Description = "Validates that from all the IDatabaseInfo.Identifierdentifier of the current introspection service it is possible to obtain an instance of a IDatabaseInfoInfo that is equivalent to the one where the qualified name was retrieved from by using the Equals() method.")]
        [TestDetails(TestIssue = "622296", Feature = "Database Abstraction Layer", CreatedBy = "rls")]
        public void TestCreateDatabaseInfoFromQualifiedName(DatabaseProviderTestCase tc) {
            var databaseServices = tc.Services;
            IEnumerable<IDatabaseInfo> databases = databaseServices.IntrospectionService.ListDatabases();
            foreach (var database in databases) {
                IDatabaseInfo inferredDatabase = databaseServices.ObjectFactory.CreateDatabaseInfo(database.Identifier);
                bool equals = database.Equals(inferredDatabase);
                Assert.IsTrue(equals, "The inferred IDatabaseInfo is not equal to the original IDatabaseInfo for database with qualifiedName: " + database.Identifier);
            }
        }

        [IterativeTestCase(typeof(TestConfiguration), Description = "Validates that from all ITableSourceInfo.QualifiedName of the current database it is possible to obtain an instance of a ITableSourceInfo that is equivalent to the one where the qualified name was retrieved from by using the Equals() method.")]
        [TestDetails(TestIssue = "622296", Feature = "Database Abstraction Layer", CreatedBy = "rls")]
        public void TestCreateTableSourceInfoFromQualifiedName(DatabaseProviderTestCase tc) {
            var databaseServices = tc.Services;
            IDatabaseInfo db = databaseServices.ObjectFactory.CreateDatabaseInfo(GetDatabaseIdentifier(databaseServices));
            IEnumerable<ITableSourceInfo> tableSources = databaseServices.IntrospectionService.ListTableSourcesWithoutFilter(db);
            foreach (var tableSource in tableSources) {
                ITableSourceInfo inferredTableSource = databaseServices.ObjectFactory.CreateTableSourceInfo(tableSource.QualifiedName);
                bool equals = tableSource.Equals(inferredTableSource);
                Assert.IsTrue(equals, "The inferred ITableSourceInfo is not equal to the original ITableSourceInfo for table source with qualified name: " + tableSource.QualifiedName);
            }
        }

        [IterativeTestCase(typeof(TestConfiguration), Description = "Validates that the list of columns matches the ones defined in the bootstrap script for a given table and validates all the column contents.")]
        [TestDetails(TestIssue = "622296", Feature = "Database Abstraction Layer", CreatedBy = "rls")]
        public void TestGetTableSourceDetailsWithColumnsForTable(DatabaseProviderTestCase tc) {
            var databaseServices = tc.Services;
            ValidateTableSourceColumns(databaseServices, tableIntrospectionTestTable.Name, false);
        }
        
        [IterativeTestCase(typeof(TestConfiguration), Description = "Validates that the list of columns matches the ones defined in the bootstrap script for a given view and validates all the column contents.")]
        [TestDetails(TestIssue = "622296", Feature = "Database Abstraction Layer", CreatedBy = "rls")]
        public void TestGetTableSourceDetailsWithColumnsForView(DatabaseProviderTestCase tc) {
            var databaseServices = tc.Services;
            ValidateTableSourceColumns(databaseServices, viewIntrospectionViewOverTestTable.Name, true);
        }

        [IterativeTestCase(typeof(TestConfiguration), Description = "Validates that the list of foreign keys matches the ones defined in the bootstrap script for a given table and validates all the foreign key contents.")]
        [TestDetails(TestIssue = "622296", Feature = "Database Abstraction Layer", CreatedBy = "rls")]
        public void TestGetTableSourceDetailsWithForeignKeys(DatabaseProviderTestCase tc) {
            var databaseServices = tc.Services;
            
            AssertForeignKeys(expectedForeignKeys,
                databaseServices.IntrospectionService.GetTableSourceForeignKeys(GetTableSourceInfo(databaseServices, tableForeignKeysTable.Name)));
        }

        private void ValidateTableSourceColumns(IDatabaseServices databaseServices, string tableSourceName, bool isView) {
            AssertColumns(expectedColumns, 
                databaseServices.IntrospectionService.GetTableSourceColumns(GetTableSourceInfo(databaseServices, tableSourceName)), isView);
        }

        internal static ITableSourceInfo GetTableSourceInfo(IDatabaseServices databaseServices, string tableName) {
            return GetTableSourceInfo(databaseServices, tableName, true);
        }

        internal static ITableSourceInfo GetTableSourceInfo(IDatabaseServices databaseServices, string tableName, bool assertExists) {
            IDatabaseInfo db = databaseServices.ObjectFactory.CreateDatabaseInfo(GetDatabaseIdentifier(databaseServices));

            ITableSourceInfo result =
                databaseServices.IntrospectionService.ListTableSourcesWithoutFilter(db).FirstOrDefault(ts => ts.Name.EqualsIgnoreCase(tableName));

            if (assertExists) {
                Assert.IsNotNull(result, "Table source named '" + tableName + "' not found in the database");
            }

            return result;
        }

        internal static void AssertColumns(IEnumerable<ExpectedColumnInfo> expectedColumns, IEnumerable<ITableSourceColumnInfo> obtainedColumns, 
                bool isView) {

            Assert.IsNotNull(obtainedColumns, "Obtained columns for table source are null.");
            string tableSourceName = obtainedColumns.First().TableSource.Name;

            Assert.AreEqual(obtainedColumns.Count(), expectedColumns.Count(),
                "Number of columns retrieved for table source '" + tableSourceName + "' doesn't match the expected value.");
            foreach (var ec in expectedColumns) {
                var obtainedColumn = obtainedColumns.FirstOrDefault(col => col.Name.EqualsIgnoreCase(ec.Name));
                Assert.IsNotNull(obtainedColumn, "Couldn't find column '" + ec.Name + "' from table source '" + tableSourceName + "'");
                Assert.IsTrue(obtainedColumn.IsMandatory == ec.IsMandatory &&
                    (isView || obtainedColumn.IsPrimaryKey == ec.IsPrimaryKey) &&
                    (isView || obtainedColumn.IsAutoGenerated == ec.IsAutoGenerated) &&
                    obtainedColumn.DataType.Type == ec.DataType.Type &&
                    (!obtainedColumn.DataType.Type.HasLength() || ec.DataType.Length == UNCHECKED_LENGTH || obtainedColumn.DataType.Length == ec.DataType.Length) &&
                    (!obtainedColumn.DataType.Type.HasDecimals() || ec.DataType.Decimals == UNCHECKED_DECIMALS || obtainedColumn.DataType.Decimals == ec.DataType.Decimals),
                    "Expected information for column '" + ec.Name + "' from table source '" + tableSourceName + "' doesn't match the column information retrieved from the database");
            }
        }

        internal static void AssertColumns(IEnumerable<ExpectedColumnInfo> expectedColumns, IEnumerable<ITableSourceColumnInfo> obtainedColumns) {
            AssertColumns(expectedColumns, obtainedColumns, false);
        }
        
        internal static void AssertForeignKeys(IEnumerable<ExpectedForeignKeyInfo> expectedForeignKeys, 
                IEnumerable<ITableSourceForeignKeyInfo> obtainedForeignKeys) {

            Assert.IsNotNull(obtainedForeignKeys, "ForeignKeys for table source are null.");
            string tableSourceName = obtainedForeignKeys.First().TableSource.Name;

            Assert.AreEqual(obtainedForeignKeys.Count(), expectedForeignKeys.Count(),
                "Number of foreign keys retrieved for table source '" + tableSourceName + "' doesn't match the expected value.");

            foreach (var efk in expectedForeignKeys) {
                Assert.IsNotNull(obtainedForeignKeys.FirstOrDefault(fk =>
                    fk.Name.EqualsIgnoreCase(efk.Name) &&
                    fk.ColumnName.EqualsIgnoreCase(efk.ColumnName) &&
                    fk.ReferencedTableSource.Name.EqualsIgnoreCase(efk.ReferencedTableSource.Name) &&
                    fk.ReferencedColumnName.EqualsIgnoreCase(efk.ReferencedColumnName)),
                    "Expected information for foreign key '" + efk.Name + "' from table source '" + tableSourceName + "' doesn't match any foreign key information retrieved from the database");
            }
        }
    }
}
