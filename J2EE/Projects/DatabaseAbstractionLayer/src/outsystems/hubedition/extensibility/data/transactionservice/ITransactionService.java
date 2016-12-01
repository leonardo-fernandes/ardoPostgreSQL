/* 
 This source code (the "Generated Software") is generated by the OutSystems Platform 
 and is licensed by OutSystems (http://www.outsystems.com) to You solely for testing and evaluation 
 purposes, unless You and OutSystems have executed a specific agreement covering the use terms and 
 conditions of the Generated Software, in which case such agreement shall apply. 
*/

package outsystems.hubedition.extensibility.data.transactionservice;

import java.sql.*;
import java.text.*;
import java.util.*;
import outsystems.hubedition.databaseabstractionlayer.adoadapters.*;
import outsystems.hubedition.extensibility.data.*;
import outsystems.hubedition.util.*;
import outsystems.hubedition.util.delegates.*;
import outsystems.runtimecommon.*;
import java.math.BigDecimal;
import outsystems.hubedition.databaseabstractionlayer.adoadapters.ADOTransaction;
import outsystems.hubedition.databaseabstractionlayer.adoadapters.ConnectionUtils;
import outsystems.hubedition.extensibility.data.IDatabaseServices;



/**
 *	Database service that handles connection and transaction management to a access a database.
 */
public interface ITransactionService
{
    
    /**
	 *	Gets the IDatabaseServices instance associated with this service.
	 *	@return	The database services associated.
	 */
    public IDatabaseServices getDatabaseServices();
    
    /**
	 *	Returns a new transaction manager.
	 *	@return	The transaction manager.
	 */
    public ITransactionManager createTransactionManager();
    
    /**
	 *	Returns a new connection to the database.
	 *	@return	The database connection.
	 *	@throws	java.sql.SQLException	If an error occurs while accessing the database.
	 */
    public Connection createConnection() throws SQLException;
    
    /**
	 *	Returns a new connection to the database.
	 *	@param	numRetries	The number of retries for establishing the connection.
	 *	@return	The database connection.
	 *	@throws	java.sql.SQLException	If an error occurs while accessing the database.
	 */
    public Connection createConnection(int numRetries) throws SQLException;
    
    /**
	 *	Checks if it is possible to establish a connection.
	 *	@param	errorMessage	Error message raised during the creation of the connection.
	 *	@return	True if it was established a connection successfully, False otherwise.
	 *	@throws	java.sql.SQLException	If an error occurs while accessing the database.
	 */
    public boolean testConnection(RefParmHolder<String> errorMessage) throws SQLException;
    
    /**
	 *	Returns a new transaction for the connection provided.
	 *	@param	conn	Connection from which the transaction is created.
	 *	@return	A transaction created from the given connection.
	 *	@throws	java.sql.SQLException	If an error occurs while accessing the database.
	 */
    public ADOTransaction createTransaction(Connection conn) throws SQLException;
    
    /**
	 *	Releases all connections in the connection pool.
	 *	@param	reason	Reason why the connections will be released.
	 */
    public void releasePooledConnections(String reason);
    
    /**
	 *	Checks if the connection is closed.
	 *	@param	connection	A database connection.
	 *	@return	True if the connection is already closed, False otherwise.
	 *	@throws	java.sql.SQLException	If an error occurs while accessing the database.
	 */
    public boolean isClosed(Connection connection) throws SQLException;
    
    /**
	 *	Closes the provided database transaction.
	 *	@param	tran	Transaction to be closed.
	 */
    public void closeTransaction(ADOTransaction tran);
}