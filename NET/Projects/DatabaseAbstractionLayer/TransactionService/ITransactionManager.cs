/* 
 This source code (the "Generated Software") is generated by the OutSystems Platform 
 and is licensed by OutSystems (http://www.outsystems.com) to You solely for testing and evaluation 
 purposes, unless You and OutSystems have executed a specific agreement covering the use terms and 
 conditions of the Generated Software, in which case such agreement shall apply. 
*/

using System;
using System.Data;
using OutSystems.HubEdition.Extensibility.Data.ConfigurationService;
using OutSystems.RuntimeCommon;

namespace OutSystems.HubEdition.Extensibility.Data.TransactionService {
    
    /// <summary>
    /// Represents a service to manage transactions.
    /// </summary>
    public interface ITransactionManager : IDisposable {

        /// <summary>
        /// This property represents the <see cref="ITransactionService"/> associated with this transaction manager.
        /// </summary>
        ITransactionService TransactionService { get; set; }

        /// <summary>
        /// Gets the number of busy transactions.
        /// </summary>
        int BusyTransactionCount { get; }

        /// <summary>
        /// Returns a transaction to be used during a web request.
        /// This transaction is only commited or rolled back by invoking <see cref="FreeupResources"/>.
        /// </summary>
        /// <returns>A transaction to be used in the applications.</returns>
        IDbTransaction GetRequestTransaction();

        /// <summary>
        /// Returns a transaction that ismanaged by extension developers.
        /// Extension developers explicitly commit or rollback this transaction.
        /// </summary>
        /// <returns>A private transaction managed by the user.</returns>
        IDbTransaction GetCommitableTransaction();

        /// <summary>
        /// Returns a transaction with read only access suitable to iterate results.
        /// </summary>
        /// <returns>A Transaction only for read purposes.</returns>
        IDbTransaction GetReadOnlyTransaction();

        /// <summary>
        /// Releases a transaction to the pool.
        /// </summary>
        /// <param name="trans">Transaction to be released.</param>
        void ReleaseTransaction(IDbTransaction trans);

        /// <summary>
        /// Commits a transaction.
        /// </summary>
        /// <param name="trans">The transaction to be committed.</param>
        void CommitTransaction(IDbTransaction trans);

        /// <summary>
        /// Rolls back a transaction.
        /// </summary>
        /// <param name="trans">The transaction to be rolled back</param>
        void RollbackTransaction(IDbTransaction trans);

        /// <summary>
        /// Commits all transactions.
        /// </summary>
        void CommitAllTransactions();

        /// <summary>
        /// Rolls back all transactions.
        /// </summary>
        void RollbackAllTransactions();

        /// <summary>
        /// Releases all database resources being used.
        /// All transactions are committed or rolled back, and connections are returned to the pool.
        /// </summary>
        /// <param name="commit">If True, all transaction are commited. Otherwise, are rolled back.</param>
        void FreeupResources(bool commit);

        /// <summary>
        /// Associates a reader to a transaction.
        /// </summary>
        /// <param name="trans">The transaction that will be associated with the reader.</param>
        /// <param name="reader">The reader to associate.</param>
        /// <returns>Returns True if the reader was successfully associated, False otherwise.</returns>
        bool AssociateReader(IDbTransaction trans, IDataReader reader);

        /// <summary>
        /// Checks if this manager is managing a given transaction.
        /// </summary>
        /// <param name="trans">A transaction.</param>
        /// <returns>Returns True if the transaction belongs to this transaction manager, False otherwise.</returns>
        bool IsManaging(IDbTransaction trans);
    }
}