/* 
 This source code (the "Generated Software") is generated by the OutSystems Platform 
 and is licensed by OutSystems (http://www.outsystems.com) to You solely for testing and evaluation 
 purposes, unless You and OutSystems have executed a specific agreement covering the use terms and 
 conditions of the Generated Software, in which case such agreement shall apply. 
*/

namespace OutSystems.HubEdition.Extensibility.Data.DMLService {

    /// <summary>
    /// Generates the SQL aggregate functions required by the applications to perform DataSet queries.
    /// </summary>
    public interface IDMLAggregateFunctions {

        /// <summary>
        /// Gets the associated DML service.
        /// </summary>
        IDMLService DMLService { get; }

        /// <summary>
        /// Returns a DML expression that computes the maximum value of 'v' in the current group's rows.
        /// Ignores rows for which the value of 'v' is NULL.
        /// </summary>
        /// <param name="v">A DML expression that evaluates to an Integer, Decimal or DateTime.</param>
        /// <returns>A DML expression that evaluates to a value of the same type as the argument.</returns>
        string Max(string v);

        /// <summary>
        /// Returns a DML expression that computes the minimum value of 'v' in the current group's rows.
        /// Ignores rows for which the value of 'v' is NULL.
        /// </summary>
        /// <param name="v">A DML expression that evaluates to an Integer, Decimal or DateTime.</param>
        /// <returns>A DML expression that evaluates to a value of the same type as the argument.</returns>
        string Min(string v);

        /// <summary>
        /// Returns a DML expression that computes the average value of 'n' in the current group's rows.
        /// Ignores rows for which the value of 'n' is NULL.
        /// </summary>
        /// <param name="n">A DML expression that evaluates to an Integer or Decimal.</param>
        /// <returns>A DML expression that evaluates to a Decimal.</returns>
        string Avg(string n);

        /// <summary>
        /// Returns a DML expression that computes the sum of 'n' in the current group's rows.
        /// Ignores rows for which the value of 'n' is NULL.
        /// </summary>
        /// <param name="n">A DML expression that evaluates to an Integer or Decimal.</param>
        /// <returns>A DML expression that evaluates to a value of the same type as the argument.</returns>
        string Sum(string n);

        /// <summary>
        /// Returns a DML expression that computes the number of rows in the current group.
        /// </summary>
        /// <returns>A DML expression that evaluates to an Integer.</returns>
        string Count();

        /// <summary>
        /// Returns a DML expression that computes the number of rows in the current group.
        /// Ignores rows for which the value of 'v' is NULL.
        /// </summary>
        /// <param name="v">A DML expression that evaluates to a basic type.</param>
        /// <returns>A DML expression that evaluates to an Integer.</returns>
        string Count(string v);

    }
}