/* 
 This source code (the "Generated Software") is generated by the OutSystems Platform 
 and is licensed by OutSystems (http://www.outsystems.com) to You solely for testing and evaluation 
 purposes, unless You and OutSystems have executed a specific agreement covering the use terms and 
 conditions of the Generated Software, in which case such agreement shall apply. 
*/

package outsystems.hubedition.extensibility.data.platform.databaseobjects;

import java.text.*;
import java.util.*;
import outsystems.hubedition.extensibility.data.*;
import outsystems.hubedition.extensibility.data.databaseobjects.*;
import outsystems.hubedition.extensibility.data.platform.*;
import outsystems.hubedition.util.*;
import outsystems.hubedition.util.delegates.*;
import java.math.BigDecimal;
import outsystems.hubedition.extensibility.data.databaseobjects.IDataTypeInfo;



/**
 *	Contains information about a data type.
 */
public interface IPlatformDataTypeInfo extends IDataTypeInfo
{
    
    /**
	 *	Returns true if both objects represent an equivalent database data type.
	 *	@param	other	Other datatype object to compare with
	 *	@return	true if the current object is equivalent to the other parameter; otherwise, false.
	 */
    public boolean isEquivalent(IDataTypeInfo other);
}