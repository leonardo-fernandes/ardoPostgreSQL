/* 
 This source code (the "Generated Software") is generated by the OutSystems Platform 
 and is licensed by OutSystems (http://www.outsystems.com) to You solely for testing and evaluation 
 purposes, unless You and OutSystems have executed a specific agreement covering the use terms and 
 conditions of the Generated Software, in which case such agreement shall apply. 
*/

using OutSystems.HubEdition.Extensibility.Data.DMLService;
using OutSystems.RuntimeCommon;

namespace OutSystems.HubEdition.Extensibility.Data.Platform.DMLService {
    /// <summary>
    /// Defines a contract for generating SQL fragments to interact with a database.
    /// </summary>
    public interface IPlatformDMLService : IDMLService {
        /// <summary>
        /// Gets an object that generates and manipulates SQL identifiers.
        /// </summary>
        new IPlatformDMLIdentifiers Identifiers { get; }

        /// <summary>
        /// Gets an object that generates the SQL functions required to execute simple queries
        /// </summary>
        new IPlatformDMLFunctions Functions { get; }

        /// <summary>
        /// Gets an object that generates Programmatic SQL fragments to interact with a database.
        /// </summary>
        IPlatformDMLProgrammaticSQL ProgrammaticSql { get; }
    }
}