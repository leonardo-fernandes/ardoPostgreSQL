/* 
 This source code (the "Generated Software") is generated by the OutSystems Platform 
 and is licensed by OutSystems (http://www.outsystems.com) to You solely for testing and evaluation 
 purposes, unless You and OutSystems have executed a specific agreement covering the use terms and 
 conditions of the Generated Software, in which case such agreement shall apply. 
*/

using System;
using System.Net;
using OutSystems.HubEdition.Extensibility.Data.ConfigurationService;

namespace OutSystems.HubEdition.Extensibility.Data.Platform.Configuration {

    public interface ISessionDatabaseConfiguration : IEquatable<ISessionDatabaseConfiguration>{

        /// <summary>
        /// Gets the database provider. It provides information about the database,
        /// and access to its services.
        /// </summary>
        IPlatformDatabaseProvider PlatformDatabaseProvider { get; }

        /// <summary>
        /// This property indicates if the plugin has operations that require elevated privileges user.
        /// If this is set to true, the caller (Configuration Tool) will prompt a form to ask for credentials, and use them in operations that require elevated user
        /// </summary>
        bool ImplementsElevatedPrivilegesOperations {
            get;
        }
        
        /// <summary>
        /// Indicates the current state of the Configuration, if the property is true, then IntegratedAuthenticationMode is set
        /// </summary>
        AuthenticationType AuthenticationMode {
            set;
            get;
        }

        /// <summary>
        /// Gets the object that compacts all the needed configuration parameters to be used in runtime.
        /// </summary>
        /// <returns>The configuration that allows acessing the database as the session user.</returns>
        IRuntimeDatabaseConfiguration RuntimeDatabaseConfiguration();

        /// <summary>
        /// Indicates the current state of the Configuration, if the property is true, then Advanced Configuration mode is set is set
        /// </summary>
        bool AdvancedConfigurationMode {
            get;
            set;
        }
        
        /// <summary>
        /// Contextual text to help the user understand what does the Advanced configuration consist of.
        /// </summary>
        string ContextualHelpForAdvancedMode {
            get;
        }

        /// <summary>
        /// Contextual text to help the user understand what does the Basic configuration consist of.
        /// </summary>
        string ContextualHelpForBasicMode {
            get;
        }

        #region Session User

        /// <summary>
        /// This property returns the credentials for the session user. 
        /// </summary>
        NetworkCredential SessionAuthenticationCredential {
            get;
        }

        #endregion

    }
}
