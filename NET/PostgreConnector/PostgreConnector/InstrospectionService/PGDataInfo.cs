﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OutSystems.HubEdition.Extensibility.Data.IntrospectionService;
using OutSystems.HubEdition.Extensibility.Data;
using OutSystems.HubEdition.Extensibility.Data.DatabaseObjects;

namespace OutSystems.HubEdition.DatabaseProvider.Postgres.InstrospectionService
{
    class PGDataInfo : IDataTypeInfo
    {

        public PGDataInfo(string data_type, int maxLength, int precision, int precision_radix, int numeric_scale)
        {
            SqlDataType = data_type;
            switch (data_type.ToLower())
            {

                case "character":
                case "character varying":
                case "varchar":
                case "char":
                case "uuid":
                case "text":
                    Type = DBDataType.TEXT;
                    if (maxLength != -1)
                        Length = maxLength;
                    break;
                
                case "boolean":
                case "bool":
                    Type = DBDataType.BOOLEAN;
                    break;

                case "date":
                    Type = DBDataType.DATE;
                    break;
                
                case "timestamp":
                case "timestamp without time zone":
                case "timestamp with time zone":
                case "timestamptz":
                    Type = DBDataType.DATE_TIME;
                    break;

                case "time":
                case "time without time zone":
                case "timez":
                case "time with time zone":
                    Type = DBDataType.TIME;
                    break;

                case "integer":
                case "int":
                case "int4":
                case "smallint":
                case "int2":
                case "serial":
                case "serial4":
                    Type = DBDataType.INTEGER;
                    break;


                case "numeric":
                case "decimal":
                case "bigint": 
                case "int8":
                case "bigserial":
                case "serial8":
                case "real":
                case "double precision":
                    Type = DBDataType.DECIMAL;
                    Length = precision;
                    Decimals = numeric_scale; // not really sure if ...
                    break;

                case "bytea":
                    Type = DBDataType.BINARY_DATA;
                    break;

                // check for better place for some of these types
                case "box":
                case "cidr":
                case "circle":
                case "inet": // text?
                case "line":
                case "lseg":
                case "macaddr": // text ?
                case "money": // decimal ?
                case "path":
                case "point":
                case "polygon":
                case "tsquery":
                case "tsvector":
                case "txid_snapshot":
                case "xml": // text ?
                case "bit": // text ?
                case "bit varying": // text ?
                case "ARRAY":
                case "USER-DEFINED":
                case "interval":
                default:
                    Type = DBDataType.UNKNOWN;
                    break;
            }
        }

        public int Decimals
        {
            get;
            private set;
        }

        public int Length
        {
            get;
            private set;
        }

        public string SqlDataType
        {
            get;
            private set;
        }

        public DBDataType Type
        {
            get;
            private set;
        }
    }
}