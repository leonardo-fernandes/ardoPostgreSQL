/* 
 This source code (the "Generated Software") is generated by the OutSystems Platform 
 and is licensed by OutSystems (http://www.outsystems.com) to You solely for testing and evaluation 
 purposes, unless You and OutSystems have executed a specific agreement covering the use terms and 
 conditions of the Generated Software, in which case such agreement shall apply. 
*/

using System;
using System.Data;
using System.Diagnostics;
using System.Threading;
using OutSystems.RuntimeCommon;
using OutSystems.RuntimeCommon.Log;

namespace OutSystems.HubEdition.Extensibility.Data.TransactionService {

    /// <summary>
    /// Database service that handles connection and transaction management to a access a database.
    /// This is a base implementation of an abstract transaction service.
    /// </summary>
    public abstract class BaseTransactionService: ITransactionService {
        /// <summary>
        /// Gets the isolation level to be used in the transactions.
        /// </summary>
        protected abstract IsolationLevel IsolationLevel { get; }

        /// <summary>
        /// Releases all connections in the pool.
        /// </summary>
        protected abstract void ReleaseAllPooledConnections();

        /// <summary>
        /// Returns a new transaction manager.
        /// </summary>
        /// <returns>The transaction manager.</returns>
        public virtual ITransactionManager CreateTransactionManager() {
            return new GenericTransactionManager(this);
        }

        /// <summary>
        /// Checks if a separate connection is needed to connect to another catalog.
        /// </summary>
        public abstract bool NeedsSeparateAdminConnection { get; }

        protected abstract IDbConnection GetConnectionFromDriver();

        /// <summary>
        /// Creates a new instance of the <see cref="BaseTransactionService"/> class.
        /// </summary>
        /// <param name="databaseServices">The database services to be used by this service.</param>
        public BaseTransactionService(IDatabaseServices databaseServices) {
            DatabaseServices = databaseServices;
        }

        /// <summary>
        /// This property represents the <see cref="IDatabaseServices"/> instance associated with this service.
        /// </summary>
        public virtual IDatabaseServices DatabaseServices { get; private set; }

        /// <summary>
        /// Returns a new connection to the database.
        /// This implementation calls <see cref="CreateConnection(int)"/> 
        /// with the number of retries equals to <see cref="TransactionServiceConstants.DEFAULT_CONNECTION_RETRIES"/>.
        /// </summary>
        /// <returns>The database connection.</returns>
        public virtual IDbConnection CreateConnection() {
            return CreateConnection(TransactionServiceConstants.DEFAULT_CONNECTION_RETRIES);
        }

        /// <summary>
        /// Returns a new connection to the database.
        /// This implementation calls <see cref="GetConnectionFromDriver"/>, opens the connection
        /// and returns it.
        /// </summary>
        /// <param name="numRetries">The number retries for establish the connection.</param>
        /// <returns>an open database connection.</returns>
        public virtual IDbConnection CreateConnection(int numRetries) {
            IDbConnection conn = null;
            int retryNumber = 0;

            // We make connectionCreationMaxRetries so the sql connection can be restored if a minor error happened
            while (retryNumber++ <= numRetries) {
                if (retryNumber != 1) {
                    // Sleep to allow a recover of the server
                    Thread.Sleep(TransactionServiceConstants.RETRY_CONNECTION_TIME);
                }

                try {
                    conn = GetConnection();
                    break;
                } catch (Exception e) {
                    
                    if (conn != null) {
                        conn.Close();
                    }

                    string message;

                    if (retryNumber >= numRetries) {
                        message = String.Format(
                            "Error openning connection to the database: {0}\nThe retrying was unsuccessful.\n\n{1}\n{2}",
                            e.Message, e.StackTrace, (new StackTrace(true)).ToString());

                        EventLogger.WriteError(message);

                        // Since we already retried it connectionCreationMaxRetries rethrow the exception occurred
                        throw;
                    } else if (retryNumber == 1) {
                        message = String.Format(
                            "Error openning connection to the database: {0}\nRetrying...\n\n{1}\n{2}", e.Message, e.StackTrace, (new StackTrace(true)).ToString());

                        EventLogger.WriteWarning(message);
                    } else if (retryNumber == TransactionServiceConstants.CONNECTION_POOL_CLEANUP_RETRIES) {

                        // We need to clean the connection pools (#26972)
                        ReleasePooledConnections(e.Message);
                    }
                }
            }

            return conn;
        }

        /// <summary>
        /// Checks if it is possible to establish a connection.
        /// </summary>
        /// <returns>True if it was established a connection successfully, False otherwise.</returns>
        public virtual bool TestConnection(out string errorMessage) {
            IDbConnection conn = null;
            errorMessage = string.Empty;
            try {
                conn = CreateConnection(0); // Try just one time
            } catch (Exception e) {
                errorMessage = e.Message;
                EventLogger.WriteError(String.Format(
                            "Error testing connection to the database: {0}\n\n{1}\n{2}",
                            e.Message, e.StackTrace, (new StackTrace(true))));

                return false;
            } 
            finally {
                
                if (conn != null) {
                    conn.Close();
                }

            }
            return true;
        }

        private IDbConnection GetConnection() {
            IDbConnection conn = null;
            try {
                conn = GetConnectionFromDriver();
                conn.Open();
                return conn;
            } catch {
                if (conn != null) {
                    conn.Dispose();
                }
                throw;
            }
        }

        /// <summary>
        /// Returns a new transaction for the connection provided.
        /// This implementation creates a new transaction with the
        /// <see cref="IsolationLevel"/> of the current service.
        /// </summary>
        /// <param name="conn">Connection where the transaction will be created.</param>
        /// <returns>A transaction using the connection provided.</returns>
        public virtual IDbTransaction CreateTransaction(IDbConnection conn) {
            IDbTransaction transaction;

            // #121301: I know this code looks odd, but according to http://connect.microsoft.com/VisualStudio/feedback/ViewFeedback.aspx?FeedbackID=384544
            //  we should use KeepAlive like they do in SqlTransaction.BeginTransaction() to keep transactions from losing connections...
            try {
                transaction = conn.BeginTransaction(IsolationLevel);

                return transaction;
            } finally {
                
                GC.KeepAlive(conn);
            }
        }

        /// <summary>
        /// Releases all connections in the connection pool.
        /// This implementation releases all connections in the connection pool and
        /// logs a warning with the reason.
        /// </summary>
        /// <param name="reason">Reason why the connections will be released.</param>
        public virtual void ReleasePooledConnections(string reason) {
            if (!reason.IsEmpty()) {
                EventLogger.WriteWarning(String.Format("Cleaning the connection pools because: {0}.\n\n{1}", reason, (new StackTrace(true)).ToString()));
            }

            // #54508 stupid .Net bug            
            ReleaseAllPooledConnections();
        }

        /// <summary>
        /// Checks if the connection is closed.
        /// </summary>
        /// <param name="connection">A database connection.</param>
        /// <returns>True if the connection is already closed, False otherwise.</returns>
        public virtual bool IsClosed(IDbConnection connection) {
            return (connection.State & ConnectionState.Open) != ConnectionState.Open;
        }

        /// <summary>
        /// Closes the provided database transaction.
        /// This implementation safely closes a transaction. If the transaction or its connection
        /// are Null it does nothing.
        /// </summary>
        /// <param name="tran">Transaction to be closed.</param>
        public virtual void CloseTransaction(IDbTransaction tran) {
            if (tran != null) {
                if (tran.Connection != null) {
                    try {
                        
                        tran.Connection.Close();
                    } catch {
                    }
                }
            }
        }
    }
}
