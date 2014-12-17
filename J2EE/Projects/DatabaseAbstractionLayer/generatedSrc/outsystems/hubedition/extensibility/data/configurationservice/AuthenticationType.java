/* 
 This source code (the "Generated Software") is generated by the OutSystems Platform 
 and is licensed by OutSystems (http://www.outsystems.com) to You solely for testing and evaluation 
 purposes, unless You and OutSystems have executed a specific agreement covering the use terms and 
 conditions of the Generated Software, in which case such agreement shall apply. 
*/

package outsystems.hubedition.extensibility.data.configurationservice;

import java.text.*;
import java.util.*;
import outsystems.hubedition.extensibility.data.*;
import outsystems.hubedition.util.*;
import outsystems.hubedition.util.delegates.*;
import java.math.BigDecimal;
import java.util.Map;


/**
 *	Values that represent the authentication type to be used to access the database.
 */
public enum AuthenticationType implements IEnum {
    Database_Authentication,
    Windows_Authentication;
    
    public static final TypeInformation<AuthenticationType> TypeInfo = TypeInformation.get(AuthenticationType.class);
    public int getIntValue() {
        return ordinal();
    }
    
    public static String[] names() {
        return EnumUtils.getNames(values());
    }
    
    public static AuthenticationType getDefaultValue() {
        return Database_Authentication;
    }
    
    private static Map<Integer, AuthenticationType> intToEnum;
    private static Map<Integer, AuthenticationType> getIntToEnum() {
        if (intToEnum == null) {
            intToEnum = EnumUtils.getIntToEnumValueMap(values());
        }
        return intToEnum;
    }
    
    private static Map<String, AuthenticationType> lowerCaseNameToEnum;
    private static Map<String, AuthenticationType> getLowerCaseNameToEnum() {
        if (lowerCaseNameToEnum == null) {
            lowerCaseNameToEnum = EnumUtils.getNameToEnumValueMap(values(), /*lowerCase*/true);
        }
        return lowerCaseNameToEnum;
    }
    
    private static Map<String, AuthenticationType> nameToEnum;
    private static Map<String, AuthenticationType> getNameToEnum() {
        if (nameToEnum == null) {
            nameToEnum = EnumUtils.getNameToEnumValueMap(values(), /*lowerCase*/false);
        }
        return nameToEnum;
    }
    
    public static AuthenticationType valueOf(int value) {
        AuthenticationType result = getIntToEnum().get(value);
        if (result == null) {
            throw new IllegalArgumentException("No enum const class AuthenticationType with int value " + value);
        }
        return result;
    }
    
    
    public static AuthenticationType valueOf(String value, boolean ignoreCase) {
        if (!ignoreCase) {
            return valueOf(value);
        }
        AuthenticationType result = getLowerCaseNameToEnum().get(value.toLowerCase());
        if (result == null) {
            throw new IllegalArgumentException("No enum const class AuthenticationType." + value);
        }
        return result;
    }
    
    
    public static boolean isDefined(String value) {
        return getNameToEnum().containsKey(value);
    }
    
    public static boolean isDefined(int value) {
        return getIntToEnum().containsKey(value);
    }
}