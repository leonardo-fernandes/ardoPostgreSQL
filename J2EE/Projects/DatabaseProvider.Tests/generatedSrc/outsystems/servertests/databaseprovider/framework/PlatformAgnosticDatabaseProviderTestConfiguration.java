/* 
 This source code (the "Generated Software") is generated by the OutSystems Platform 
 and is licensed by OutSystems (http://www.outsystems.com) to You solely for testing and evaluation 
 purposes, unless You and OutSystems have executed a specific agreement covering the use terms and 
 conditions of the Generated Software, in which case such agreement shall apply. 
*/

package outsystems.servertests.databaseprovider.framework;

import java.io.*;
import java.text.*;
import java.util.*;
import outsystems.hubedition.extensibility.data.*;
import outsystems.hubedition.extensibility.data.platform.*;
import outsystems.hubedition.util.*;
import outsystems.hubedition.util.delegates.*;
import java.math.BigDecimal;


public abstract class PlatformAgnosticDatabaseProviderTestConfiguration extends BaseAgnosticDatabaseProviderTestConfiguration<IPlatformDatabaseProvider, PlatformDatabaseProviderTestCase> {
    public static final TypeInformation<PlatformAgnosticDatabaseProviderTestConfiguration> TypeInfo = TypeInformation.get(PlatformAgnosticDatabaseProviderTestConfiguration.class);
    public PlatformAgnosticDatabaseProviderTestConfiguration() {
        super(TypeInformation.get(IPlatformDatabaseProvider.class), PlatformDatabaseProviderTestCase.TypeInfo);
    }
}