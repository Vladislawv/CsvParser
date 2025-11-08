# CsvParser

**CsvParser** is a simple console application that works with `.csv` files.  
The program is designed to efficiently handle large CSV files, even up to **20GB or more**, without running out of memory or causing unexpected behavior.

---

## Stack

- **C#**
- **MSSQL**

---

## Features

1. Extract, process, and save data to the database and `.csv` files.
2. Find the `PULocationId` (Pick-up Location ID) with the highest average `tip_amount`.
3. Find the top 100 longest fares by `trip_distance`.
4. Find the top 100 longest fares by travel time.
5. Search trips with conditions including `PULocationId`.

---

## How it works

- The program **streams** CSV files line by line.
- Processes data in **chunks** for batch insertion.
- Uses **`IAsyncEnumerable`** to minimize memory usage and keep processing asynchronous.
- Designed to scale for huge CSV files (20GB+).

---

## Assumptions

- `StoreAndForwardFlag` is set to `"No"` by default.

---

## How to run

### 1. Prerequisites

- C# (.NET 9) installed
- Docker installed

### 2. Run MSSQL via Docker

Open **cmd** and execute:

```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=YourStrong@Password123" -p 1433:1433 --name mssql -d mcr.microsoft.com/mssql/server:2022-latest
```

### 3. Download source code from GitHub

### 4. Navigate to the DCsvParser\CsvParser.ConsoleApplication\bin\Debug\net9.0\

### 5. Paste sample-cab-data.csv

### 6. Run execution file CsvParser\CsvParser.ConsoleApplication\bin\Debug\net9.0\CsvParser.ConsoleApplication

## Testing

- After the first method ran, there were 29,889 rows inserted in the database and 111 duplicates inserted into the duplicates.csv file

## SQL scripts that were used to project a database

```sql
CREATE TABLE [Trips] (
    [Id] int NOT NULL IDENTITY,
    [PickupTime] datetime2 NOT NULL,
    [DropoffTime] datetime2 NOT NULL,
    [PassengerCount] tinyint NOT NULL,
    [Distance] real NOT NULL,
    [StoreAndForwardFlag] nvarchar(5) NOT NULL,
    [PickUpLocationId] smallint NOT NULL,
    [DropOffLocationId] smallint NOT NULL,
    [FareAmount] decimal(6,2) NOT NULL,
    [TipAmount] decimal(5,2) NOT NULL,
    CONSTRAINT [PK_Trips] PRIMARY KEY ([Id])
);

CREATE INDEX [IX_Trips_Distance] ON [Trips] ([Distance]);
CREATE INDEX [IX_Trips_PickUpLocationId] ON [Trips] ([PickUpLocationId]);
CREATE INDEX [IX_Trips_PickupTime_DropoffTime] ON [Trips] ([PickupTime], [DropoffTime]);
```