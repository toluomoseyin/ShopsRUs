﻿using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopRUs.Infrastructure.Seeder
{
    public class ShopRUsDapperSeeder:IShopRUsDapperSeeder
    {
        private readonly DatabaseConfig _databaseConfig;

        public ShopRUsDapperSeeder(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }

        public void Setup()
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);

            var table1 = connection.Query<string>("SELECT name FROM sqlite_master WHERE type='table' AND name = 'Discount';");
            var tableName1 = table1.FirstOrDefault();
            if (!(!string.IsNullOrEmpty(tableName1) && tableName1 == "Discount"))
            {
                connection.Execute("CREATE TABLE Discount( Id                  INTEGER PRIMARY KEY, " +
                                                           "Name               TEXT                    NOT NULL, " +
                                                           "Description        TEXT                    NOT NULL, " +
                                                           "DiscountPercent    REAL                    NOT NULL, " +
                                                           "Created_at         TEXT                    NOT NULL, " +
                                                           "Modified_at        TEXT                    NULL); ");





                connection.Execute("INSERT INTO Discount ( Name,                    Description,                                  DiscountPercent,     Created_at,      Modified_at   ) " +

                                              "  VALUES (     'AffiliatedDiscount',      'Given to customers who close to the company',  0.1,                 datetime('now'),  datetime('now')), " +
                                                       "(     'EmployeeDiscount',        'Given to employees of the company',            0.3,                 datetime('now'),  datetime('now')), " +
                                                       "(     'Over2yearsDiscount',      'Customers above two years',                    0.05,                datetime('now'),  datetime('now'));");






            }


            var table2 = connection.Query<string>("SELECT name FROM sqlite_master WHERE type='table' AND name = 'Customer';");
            var tableName2 = table2.FirstOrDefault();
            if (!(!string.IsNullOrEmpty(tableName2) && tableName2 == "Customer"))
            {
                connection.Execute("CREATE TABLE Customer(Id               INTEGER PRIMARY KEY      NOT NULL," +
                                                       "FirstName          TEXT                     NOT NULL, " +
                                                       "LastName           TEXT                     NOT NULL, " +
                                                       "Email              TEXT                     NOT NULL, " +
                                                       "PhoneNumber        TEXT                     NOT NULL, " +
                                                       "Address            TEXT                     NOT NULL, " +
                                                       "IsEmployee         INTEGER                  NOT NULL, " +
                                                       "IsAfilliated       INTEGER                  NOT NULL, " +
                                                       "Created_at         TEXT                     NOT NULL," +
                                                       "DiscountId         INTEGER                  NOT NULL, FOREIGN KEY (DiscountId) REFERENCES Discount (Id));") ;


                connection.Execute("INSERT INTO Customer ( FirstName,          LastName,    Email,                   PhoneNumber,     Address,                  IsEmployee, IsAfilliated,   Created_at,             DiscountId   ) " +

                                          "  VALUES ('Cyrille',          'Waeland',    'cwaeland0@xrea.com',    '141-644-2578',  '66 Forest Way',           0 ,         1,             datetime('now'),         1 )," +
                                                    "('Corri',           'Farnin',     'cfarnin1@cisco.com',    '625-419-8167',  '4570 Redwing Park',       1 ,         0,             datetime('now'),         2 )," +
                                                    "('Mitchel',         'Mazey',      'mmazey2@prweb.com',     '402-177-5865',  '95415 Granby Junction',   1 ,         0,             datetime('now'),         2 )," +
                                                    "('Bambi',           'Spurling',   'bspurling3@apache.org', '544-773-9354',  '170 Lakewood Way',        0 ,         1,             '2021-04-05T08:38:39Z',  1 );");

            }


           

        }


           
    }
}
