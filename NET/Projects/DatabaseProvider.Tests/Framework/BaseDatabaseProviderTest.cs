/* 
 This source code (the "Generated Software") is generated by the OutSystems Platform 
 and is licensed by OutSystems (http://www.outsystems.com) to You solely for testing and evaluation 
 purposes, unless You and OutSystems have executed a specific agreement covering the use terms and 
 conditions of the Generated Software, in which case such agreement shall apply. 
*/

using OutSystems.HubEdition.Extensibility.Data;
using OutSystems.HubEdition.Extensibility.Data.ConfigurationService;
using OutSystems.HubEdition.Extensibility.Data.ExecutionService;
using OutSystems.RuntimeCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace OutSystems.ServerTests.DatabaseProvider.Framework {
    public abstract class BaseDatabaseProviderTest: DashboardTest {
        public abstract class BaseDatabaseProviderTestCase<TDatabaseProvider, TDatabaseServices> : IDatabaseProviderTestCase<TDatabaseProvider>
            where TDatabaseProvider : IDatabaseProvider
            where TDatabaseServices : IDatabaseServices {

            public string Name { get; set; }

            public override string ToString() {
                return Name;
            }

            public bool RunWithBootstrapServices { get; protected set; }

            public TDatabaseServices RuntimeServices { get; protected set; }

            public TDatabaseServices Services {
                get { return RunWithBootstrapServices ? BootstrapServices : RuntimeServices; }
            }

            public TDatabaseServices BootstrapServices { get; protected set; }


            public IEnumerable<string> BootstrapScripts { get; set; }

            public IEnumerable<string> TeardownScripts { get; set; }

            public bool ExecuteScriptsWithoutTransaction { get; set; }

            private void ExecuteScripts(IEnumerable<string> scripts, bool throwExceptions) {



                if (scripts == null) {
                    return;
                }
                var sqlExecutor = new SQLExecutor(BootstrapServices);
                IList<string> errors = new List<string>();
                foreach (var sql in scripts) {
                    try {
                        if (ExecuteScriptsWithoutTransaction) {
                            sqlExecutor.ExecuteNonQueryWithoutTransaction(sql);
                        } else {
                            sqlExecutor.ExecuteNonQuery(sql);
                        }
                    } catch (Exception e) {
                        errors.Add(e.Message);
                    }
                }
                if (throwExceptions && errors.Count > 0) {
                    throw new Exception(
                        string.Format("Error executing scripts for provider '{0}' with connection string '{1}':{2}{3}",
                            BootstrapServices.DatabaseConfiguration.DatabaseProvider.Properties.DisplayName,
                            BootstrapServices.DatabaseConfiguration.ConnectionString,
                            Environment.NewLine,
                            string.Join(Environment.NewLine, errors.ToArray())));
                }
            }

            [System.Diagnostics.DebuggerNonUserCode]
            public void ExecuteTeardown() {
                try {
                    ExecuteScripts(TeardownScripts, false);
                } catch (Exception e) {
                    throw new Exception("Error executing teardown scripts:" + Environment.NewLine + e.Message);
                }
            }

            public void ExecuteBootstrap() {
                try {
                    ExecuteTeardown();
                } catch { }

                try {
                    ExecuteScripts(BootstrapScripts, true);
                } catch (Exception bootstrapException) {
                    try {
                        ExecuteTeardown();
                    } catch (Exception teardownException) {
                        throw new Exception(
                            "Error executing bootstrap scripts:" + Environment.NewLine + bootstrapException.Message + Environment.NewLine +
                            "Automatically tried to recover by executing teardown scripts, but got an error:" + Environment.NewLine + teardownException.Message);
                    }
                    throw new Exception("Error executing bootstrap scripts:" + Environment.NewLine + bootstrapException.Message);
                }
            }

            public abstract void InitializeServices(TDatabaseProvider provider, IRuntimeDatabaseConfiguration runtimeConfiguration,
                IRuntimeDatabaseConfiguration bootstrapConfiguration, bool runWithBootstrapServices);
        }

        protected class SQLExecutor {
            private IDatabaseServices services;

            public SQLExecutor(IDatabaseServices services) {
                this.services = services;
            }

            public Scalar ExecuteScalar(string sql, params object[] parameters) {
                return ExecuteScalar(sql, GetParameterName, parameters);
            }

            public Scalar ExecuteScalar(string sql, Func<int, Type, string> getParamName, params object[] parameters) {
                var executionService = services.ExecutionService;
                var value = ExecuteInTransactionAndRollback(sql, cmd => executionService.ExecuteScalar(cmd), getParamName
                , parameters);
                return new Scalar(executionService, value);
            }

            public object ExecuteNonQueryWithoutTransaction(string sql, params object[] parameters) {
                var executionService = services.ExecutionService;
                return ExecuteInConnection(sql, cmd => executionService.ExecuteNonQuery(cmd), GetParameterName
                , parameters);
            }

            public object ExecuteNonQuery(string sql, params object[] parameters) {
                var executionService = services.ExecutionService;
                return ExecuteInTransactionAndCommit(sql, cmd => executionService.ExecuteNonQuery(cmd), GetParameterName
                , parameters);
            }

            public void ExecuteReader(string sql, Action<IDataReader> action, params object[] parameters) {
                var executionService = services.ExecutionService;
                ExecuteInTransactionAndRollback(sql, cmd => {
                    using (var reader = executionService.ExecuteReader(cmd)) {
                        action(reader);
                    }
                    return null;
                }, GetParameterName
                ,parameters);
            }
            
            public string GetParameterName(int paramIndex, Type paramType) {                
                return services.ExecutionService.ParameterPrefix + paramType.Name.Substring(0,2) + PARAM_NAME_PREFIX + paramIndex;
            }

            private object ExecuteWithCommand(string sql, Func<IDbCommand, object> action, Action<IDbTransaction> applyTrans, Func<int, Type, string> getParamName, params object[] parameters) {
                try {
                    using (IDbConnection conn = services.TransactionService.CreateConnection()) {
                        IDbTransaction tran = null;
                        bool hasErrors = false;
                        try {
                            if (applyTrans != null) {
                                tran = services.TransactionService.CreateTransaction(conn);
                            }
                            using (
                                IDbCommand cmd = tran != null
                                    ? services.ExecutionService.CreateCommand(tran, sql)
                                    : services.ExecutionService.CreateCommand(conn, sql)) {
                                for (int i = 0; i < parameters.Length; i++) {
                                    var param = parameters[i];
                                    DbType type = services.ExecutionService.ConvertToDbType(param.GetType());
                                    services.ExecutionService.CreateParameter(cmd, getParamName(i, param.GetType()), type, param);
                                }
                                return action(cmd);
                            }
                        } catch {
                            hasErrors = true;
                            throw;
                        } finally {
                            if (tran != null) {
                                if (!hasErrors) {
                                    applyTrans(tran);
                                } else {
                                    tran.Rollback();
                                }
                            }
                        }
                    }
                } catch (Exception e) {
                    throw new Exception("Error executing the statement: " + sql + Environment.NewLine + e.Message, e);
                }
            }

            private object ExecuteInTransactionAndCommit(string sql, Func<IDbCommand, object> action, Func<int, Type, string> getParamName, params object[] parameters) {
                return ExecuteWithCommand(sql, action, trans => trans.Commit(), getParamName, parameters);
            }

            private object ExecuteInTransactionAndRollback(string sql, Func<IDbCommand, object> action, Func<int, Type, string> getParamName, params object[] parameters) {
                return ExecuteWithCommand(sql, action, trans => trans.Rollback(), getParamName, parameters);
            }

            private object ExecuteInConnection(string sql, Func<IDbCommand, object> action, Func<int, Type, string> getParamName, params object[] parameters) {
                return ExecuteWithCommand(sql, action, null, getParamName, parameters);
            }
        }

        protected class Scalar {
            private IExecutionService service;
            private object value;

            public Scalar(IExecutionService service, object value) {
                this.service = service;
                this.value = value;
            }

            public object Value() {
                return value;
            }

            public object RuntimeValue() {
                return service.TransformDatabaseToRuntimeValue(Value());
            }

            public V Value<V>() {
                return Cast<V>(Value());
            }

            public V RuntimeValue<V>() {
                return Cast<V>(RuntimeValue());
            }

            private V Cast<V>(object o) {
                try {
                    if (Convert.IsDBNull(o)) {
                        if (!typeof(V).Equals(typeof(string))) {
                            return default(V);
                        }
                        // Default value for string is "", not (null)
                        o = string.Empty;
                    }
                    return (V)Convert.ChangeType(o, typeof(V));
                } catch (Exception e) {
                    throw new Exception("Error converting the result: " + o + Environment.NewLine + e.Message, e);
                }
            }
        }

        private const string PARAM_NAME_PREFIX = "param";

        private static string _machineName = null;

        public static string MachineName {
            get {
                if (_machineName == null) {
                    string machine = Environment.MachineName.ToUpperInvariant();

                    // Reduce machine name length to avoid long oracle identifiers
                    machine = Regex.Replace(machine, "^(RE?G?|VM)", "");
                    machine = Regex.Replace(machine, "0|-", "");
                    _machineName = machine.Substring(0, Math.Min(8, machine.Length));
                }

                return _machineName;
            }
        }
    }

    public abstract class BaseDatabaseProviderTest<TDatabaseProvider, TConfiguration>: BaseDatabaseProviderTest 
            where TDatabaseProvider: IDatabaseProvider 
            where TConfiguration : IDatabaseProviderTestConfiguration<TDatabaseProvider>, new() {

        private static readonly TConfiguration Configuration = new TConfiguration();

        public static void SetupCurrentThreadCulture() {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
        }


        public override void SetUp() {
            base.SetUp();
            SetupCurrentThreadCulture();
        }
        
        public override void InitFixture() {
            base.InitFixture();
            ExecuteBootstrapScripts();
        }
        
        public override void DisposeFixture() {
            ExecuteTeardownScripts();
            base.DisposeFixture();
        }

        private void ExecuteBootstrapScripts() {
            foreach (var testCase in Configuration.TestCases) {
                testCase.ExecuteBootstrap();
            }
        }

        private void ExecuteTeardownScripts() {
            foreach (var testCase in Configuration.TestCases) {
                testCase.ExecuteTeardown();
            }
        }
    }

}
