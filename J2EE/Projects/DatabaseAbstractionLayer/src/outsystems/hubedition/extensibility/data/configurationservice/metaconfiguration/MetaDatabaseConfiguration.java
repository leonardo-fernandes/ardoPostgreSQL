/* 
 This source code (the "Generated Software") is generated by the OutSystems Platform 
 and is licensed by OutSystems (http://www.outsystems.com) to You solely for testing and evaluation 
 purposes, unless You and OutSystems have executed a specific agreement covering the use terms and 
 conditions of the Generated Software, in which case such agreement shall apply. 
*/

package outsystems.hubedition.extensibility.data.configurationservice.metaconfiguration;

import java.lang.reflect.*;
import java.net.*;
import java.text.*;
import java.util.*;
import java.util.concurrent.*;
import linqbridge.linq.*;
import outsystems.hubedition.extensibility.data.*;
import outsystems.hubedition.extensibility.data.configurationservice.*;
import outsystems.hubedition.util.*;
import outsystems.hubedition.util.delegates.*;
import outsystems.runtimecommon.*;
import java.math.BigDecimal;
import linqbridge.linq.LinqMethods;
import outsystems.hubedition.extensibility.data.configurationservice.AdvancedConfiguration;
import outsystems.hubedition.extensibility.data.configurationservice.IIntegrationDatabaseConfiguration;
import outsystems.hubedition.extensibility.data.configurationservice.ParameterRegion;



/**
 *	Represents the meta-information about a database configuration.
 */
public class MetaDatabaseConfiguration {
    public static final TypeInformation<MetaDatabaseConfiguration> TypeInfo = TypeInformation.get(MetaDatabaseConfiguration.class);
    
    private static final ConcurrentHashMap<Class<?>, MetaParameterExtractor> parameterExtractorCache = new ConcurrentHashMap<Class<?>, MetaParameterExtractor>();
    
    private Object configuration;
    private List<IParameter> parameters;
    
    public MetaDatabaseConfiguration(Object configuration){
        this.configuration = configuration;
        this.parameters = parameterExtractorCache.computeIfAbsent(configuration.getClass(),
    (Class<?> t) -> {
        return new MetaParameterExtractor(t);
    }).toParameters(configuration);
    }
    
    /**
	 *	Returns a parameter with the given name.
	 *	@param	name	The parameter&#39;s name.
	 *	@return	A parameter with the given name.
	 */
    public final IParameter getParameter(final String name) {
        IParameter coalescetemp;
        
        Func.Func1_Lambda<IParameter, Boolean> matchesName = (IParameter p) -> {
            return p.getName().equals(name);
        };
        
        return ((coalescetemp = LinqMethods.firstOrDefault(TypeInformation.get(IParameter.class), getParameters(), matchesName)) != null ? coalescetemp : LinqMethods.firstOrDefault(TypeInformation.get(IParameter.class), LinqMethods.cast(TypeInformation.get(IParameter.class), advancedModeParameters()), matchesName));
    }
    
    private final Iterable<IUserDefinedParameter> advancedModeParameters() {
        return new Iterable<IUserDefinedParameter>() {
            public Iterator<IUserDefinedParameter> iterator() {
                return new Yielder<IUserDefinedParameter>() {
                    
                    IIntegrationDatabaseConfiguration integrationConf;
                    
                    protected boolean advance() throws Exception
                    {
                        switch (currentStatement) {
                            case BEGIN:
                                integrationConf = ClassUtils.safeCast(configuration, IIntegrationDatabaseConfiguration.class);
                                if (integrationConf == null)
                                    return yieldBreak();
                                return yieldReturn(new AdvancedConnectionStringParam(integrationConf), STATEMENT_1);
                            case STATEMENT_1:
                                return yieldReturn(new ConnStringTemplateParam(integrationConf), NO_NEXT_STATEMENT);
                        }
                        return yieldBreak();
                    }
                };
            }
        };
    }
    
    public final String get(String pname)
    {
        return getParameter(pname).get();
    }
    
    public final void put(String pname, String value)
    {
        getParameter(pname).set(value);
    }
    
    /**
	 *	Gets a list of visible parameters.
	 *	@return	The list of visible parameters.
	 */
    public final List<IUserDefinedParameter> getVisibleParameters()
    {
        if (configuration instanceof IIntegrationDatabaseConfiguration && ((IIntegrationDatabaseConfiguration) configuration).getAdvancedConfiguration().isSet())
        {
            return LinqMethods.toList(LinqMethods.union(LinqMethods.where(LinqMethods.ofType(TypeInformation.get(IUserDefinedParameter.class), parameters),
                (IUserDefinedParameter p) -> {
                    return p.isVisible() && p.getRegion().equals(EnumSet.of(ParameterRegion.UserSpecific));
                }),
                LinqMethods.where(advancedModeParameters(), (IUserDefinedParameter p) -> {
                    return p.isVisible();
                })));
        }
        return LinqMethods.toList(LinqMethods.where(LinqMethods.ofType(TypeInformation.get(IUserDefinedParameter.class), parameters), (IUserDefinedParameter p) -> {
            return p.isVisible();
        }));
    }
    
    public final List<IParameter> getPersistableParameters()
    {
        return LinqMethods.toList(LinqMethods.where(parameters, (IParameter p) -> {
            return p.isPersist();
        }));
    }
    
    /**
	 *	Gets a list of all parameters.
	 *	@return	The list of parameters.
	 */
    public final List<IParameter> getParameters()
    {
        return parameters;
    }
}
