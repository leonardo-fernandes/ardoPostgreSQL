/* 
 This source code (the "Generated Software") is generated by the OutSystems Platform 
 and is licensed by OutSystems (http://www.outsystems.com) to You solely for testing and evaluation 
 purposes, unless You and OutSystems have executed a specific agreement covering the use terms and 
 conditions of the Generated Software, in which case such agreement shall apply. 
*/

namespace OutSystems.HubEdition.Extensibility.Data.TransactionService {

    /// <summary>
    /// Class with constants to be used by <see cref="TransactionService"/>.
    /// </summary>
    public class TransactionServiceConstants {

        /// <summary>
        /// Time, in miliseconds, to wait for a connection retry.
        /// </summary>
        public const int RETRY_CONNECTION_TIME = 1000;

        /// <summary>
        /// Maximum number of retries for a connection pool cleanup operation.
        /// </summary>
        public const int CONNECTION_POOL_CLEANUP_RETRIES = 3;

        /// <summary>
        /// Maximum number of times to try establishing a connection.
        /// </summary>
        public const int DEFAULT_CONNECTION_RETRIES = 5;
    }
}
