/* 
 This source code (the "Generated Software") is generated by the OutSystems Platform 
 and is licensed by OutSystems (http://www.outsystems.com) to You solely for testing and evaluation 
 purposes, unless You and OutSystems have executed a specific agreement covering the use terms and 
 conditions of the Generated Software, in which case such agreement shall apply. 
*/

using System;
using NUnit.Framework;
using NUnitExtension.OutSystems.Framework;
using OutSystems.HubEdition.Extensibility.Data;
using OutSystems.HubEdition.Extensibility.Data.DMLService;
using OutSystems.ServerTests.DatabaseProvider.Framework;

namespace OutSystems.ServerTests.DatabaseProvider.DMLService {

    [DashboardTestFixture(DashboardTest.DashboardTestKind)]
    public class DMLDefaultValuesTests : DMLTest {

        private const string errorMessageFormat = "Error validating {0} default value with SQL: {1}";

        private void AssertValue<T>(IDatabaseServices services, string defaultValueName, string defaultValue, object expectedValue) {
            var sqlExecutor = new SQLExecutor(services);
            string sql = "SELECT " + defaultValue + " o1 FROM DUMMY";
            object result = sqlExecutor.ExecuteScalar(sql).RuntimeValue<T>();
            AssertEqual(expectedValue, result, string.Format(errorMessageFormat, defaultValueName, sql));
        }

        [IterativeTestCase(typeof(DMLTestsConfiguration), Description = "Validates that reading the value produced by the Integer default value returns the number zero")]
        [TestDetails(TestIssue = "610152", Feature = "Database Abstraction Layer", CreatedBy = "mar")]
        public void TestIntegerDefaultValue(DatabaseProviderTestCase tc) {
            var databaseServices = tc.Services;
            AssertValue<int>(databaseServices, "Integer", databaseServices.DMLService.DefaultValues.Integer, 0);
        }

        [IterativeTestCase(typeof(DMLTestsConfiguration), Description = "Validates that reading the value produced by the Decimal default value returns the number zero")]
        [TestDetails(TestIssue = "610152", Feature = "Database Abstraction Layer", CreatedBy = "mar")]
        public void TestDecimalDefaultValue(DatabaseProviderTestCase tc) {
            var databaseServices = tc.Services;
            AssertValue<decimal>(databaseServices, "Decimal", databaseServices.DMLService.DefaultValues.Decimal, 0m);
        }

        [IterativeTestCase(typeof(DMLTestsConfiguration), Description = "Validates that reading the value produced by the Boolean default value returns the boolean value 'false'")]
        [TestDetails(TestIssue = "610152", Feature = "Database Abstraction Layer", CreatedBy = "mar")]
        public void TestBooleanDefaultValue(DatabaseProviderTestCase tc) {
            var databaseServices = tc.Services;
            AssertValue<bool>(databaseServices, "Boolean", databaseServices.DMLService.DefaultValues.Boolean, false);
        }

        [IterativeTestCase(typeof(DMLTestsConfiguration), Description = "Validates that reading the value produced by the Text default value returns an empty text value")]
        [TestDetails(TestIssue = "610152", Feature = "Database Abstraction Layer", CreatedBy = "mar")]
        public void TestTextDefaultValue(DatabaseProviderTestCase tc) {
            var databaseServices = tc.Services;
            AssertValue<string>(databaseServices, "Text", databaseServices.DMLService.DefaultValues.Text, string.Empty);
        }

        [IterativeTestCase(typeof(DMLTestsConfiguration), Description = "Validates that reading the value produced by the Date default value returns a date value that represents the date '1900-01-01'")]
        [TestDetails(TestIssue = "610152", Feature = "Database Abstraction Layer", CreatedBy = "mar")]
        public void TestDateDefaultValue(DatabaseProviderTestCase tc) {
            var databaseServices = tc.Services;
            AssertValue<DateTime>(databaseServices, "Date", databaseServices.DMLService.DefaultValues.Date, new DateTime(1900, 1, 1, 0, 0, 0));
        }

        [IterativeTestCase(typeof(DMLTestsConfiguration), Description = "Validates that reading the value produced by the Time default value returns a time value that represents the time '00:00:00'")]
        [TestDetails(TestIssue = "610152", Feature = "Database Abstraction Layer", CreatedBy = "mar")]
        public void TestTimeDefaultValue(DatabaseProviderTestCase tc) {
            var databaseServices = tc.Services;
            AssertValue<DateTime>(databaseServices, "Time", databaseServices.DMLService.DefaultValues.Time, new DateTime(1900, 1, 1, 0, 0, 0));
        }

        [IterativeTestCase(typeof(DMLTestsConfiguration), Description = "Validates that reading the value produced by the DateTime default value returns a datetime value that represents the datetime '1900-01-01 00:00:00'")]
        [TestDetails(TestIssue = "610152", Feature = "Database Abstraction Layer", CreatedBy = "mar")]
        public void TestDateTimeDefaultValue(DatabaseProviderTestCase tc) {
            var databaseServices = tc.Services;
            AssertValue<DateTime>(databaseServices, "DateTime", databaseServices.DMLService.DefaultValues.DateTime, new DateTime(1900, 1, 1, 0, 0, 0));
        }

        [IterativeTestCase(typeof(DMLTestsConfiguration), Description = "Validates that reading the value produced by the BinaryData default value returns a null value or an empty byte array")]
        [TestDetails(TestIssue = "610152", Feature = "Database Abstraction Layer", CreatedBy = "mar")]
        public void TestBinaryDataDefaultValue(DatabaseProviderTestCase tc) {
            var databaseServices = tc.Services;
            var sqlExecutor = new SQLExecutor(databaseServices);
            IDMLDefaultValues dmlDefaultValues = databaseServices.DMLService.DefaultValues;
            string sql = "SELECT " + dmlDefaultValues.BinaryData + " o1 FROM DUMMY";
            byte[] result = sqlExecutor.ExecuteScalar(sql).RuntimeValue<byte[]>();
            AssertEqual(0, (result ?? new byte[0]).Length, string.Format(errorMessageFormat, "Null", sql));
        }

        [IterativeTestCase(typeof(DMLTestsConfiguration), Description = "Validates that reading the value produced by the Null default value returns a nonexistent value")]
        [TestDetails(TestIssue = "610152", Feature = "Database Abstraction Layer", CreatedBy = "mar")]
        public void TestNullDefaultValue(DatabaseProviderTestCase tc) {
            var databaseServices = tc.Services;
            AssertValue<object>(databaseServices, "Null", databaseServices.DMLService.DefaultValues.Null, null);
        }
    }
}
