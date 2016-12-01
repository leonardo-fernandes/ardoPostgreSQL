/* 
 This source code (the "Generated Software") is generated by the OutSystems Platform 
 and is licensed by OutSystems (http://www.outsystems.com) to You solely for testing and evaluation 
 purposes, unless You and OutSystems have executed a specific agreement covering the use terms and 
 conditions of the Generated Software, in which case such agreement shall apply. 
*/

package outsystems.hubedition.extensibility.data.transactionservice;

import java.text.*;
import java.util.*;
import outsystems.hubedition.extensibility.data.*;
import outsystems.hubedition.util.*;
import outsystems.hubedition.util.delegates.*;
import outsystems.runtimecommon.*;
import java.math.BigDecimal;


/**
 *	Exception raised when a database transaction is in an invalid state. Check the message to see the reason of the exception.
 */
public class InvalidTransactionException extends TransactionException {
    public static final TypeInformation<InvalidTransactionException> TypeInfo = TypeInformation.get(InvalidTransactionException.class);
    public InvalidTransactionException(String message){
        super(message);
    }
}