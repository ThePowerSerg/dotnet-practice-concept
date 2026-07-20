Here is a curated list of common T-SQL (Transact-SQL) interview questions, ranging from core concepts to advanced performance-tuning topics, complete with detailed answers and code examples. [1, 2, 3]

## 1. What is the difference between DELETE and TRUNCATE?

- DELETE: Is a Data Manipulation Language (DML) operation. It removes rows one by one and logs each row deletion in the transaction log. It can use a WHERE clause to filter rows, and it fires any associated database triggers. [4, 5, 6, 7, 8]
- TRUNCATE: Is a Data Definition Language (DDL) operation. It deallocates the data pages used by the table, making it much faster and more efficient on log space. It cannot use a WHERE clause, it removes all rows, and it does not fire triggers. It also resets IDENTITY columns back to their seed value. [9, 10, 11, 12, 13]

## 2. Explain the difference between WHERE and HAVING clauses.

- WHERE: Filters individual rows before any groupings or aggregations are calculated. It cannot contain aggregate functions (like SUM(), AVG(), or COUNT()).
- HAVING: Filters grouped rows after the GROUP BY clause has been applied. It is explicitly used to filter on aggregate conditions. [14, 15, 16, 17, 18]

-- WHERE filters the raw data; HAVING filters the aggregated resultsSELECT DepartmentID, SUM(Salary) AS TotalSalaryFROM EmployeesWHERE Status = 'Active'GROUP BY DepartmentIDHAVING SUM(Salary) > 100000;

## 3. What are Window Functions? Give an example of ROW_NUMBER(), RANK(), and DENSE_RANK().

Window functions perform a calculation across a set of table rows that are related to the current row. Unlike aggregate functions, they do not collapse rows into a single summary output. [19, 20, 21]

- ROW_NUMBER(): Assigns a unique, sequential integer to rows, starting at 1.
- RANK(): Assigns a sequential integer, but if there is a tie, it gives them the same rank and skips the next ranking numbers (e.g., 1, 2, 2, 4).
- DENSE_RANK(): Assigns a sequential integer, but if there is a tie, it gives them the same rank without skipping any numbers (e.g., 1, 2, 2, 3). [22, 23, 24, 25, 26]

SELECT EmployeeName, Salary,
ROW_NUMBER() OVER (ORDER BY Salary DESC) AS RowNum,
RANK() OVER (ORDER BY Salary DESC) AS RankNum,
DENSE_RANK() OVER (ORDER BY Salary DESC) AS DenseRankNumFROM Employees;

## 4. What is a Common Table Expression (CTE) and when should you use it?

A CTE is a temporary named result set that you can reference within a single SELECT, INSERT, UPDATE, or DELETE statement. It is defined using the WITH keyword. [27, 28, 29, 30, 31]
When to use it: CTEs are used to replace complex subqueries, dramatically improving code readability. They are also unique because they can be recursive, allowing you to query hierarchical data like organizational charts or bill-of-materials. [32, 33, 34, 35, 36]

WITH HighEarnersCTE AS (
SELECT EmployeeID, DepartmentID, Salary
FROM Employees
WHERE Salary > 80000
)SELECT \* FROM HighEarnersCTE WHERE DepartmentID = 5;

## 5. What is the difference between a Clustered and a Non-Clustered index?

- Clustered Index: Determines the physical storage order of the rows in the table. Because data can only be physically sorted one way, you can have only one clustered index per table (usually automatically created on the Primary Key). The index is the data. [37, 38, 39, 40]
- Non-Clustered Index: Is structured completely separate from the table data rows. It contains pointers back to the physical data rows (like an index at the back of a textbook). You can have multiple non-clustered indexes on a single table. [41, 42, 43, 44, 45]

## 6. Explain the difference between UNION and UNION ALL.

- UNION: Combines the result sets of two or more queries into a single output, and then actively removes duplicate rows. It incurs a performance cost because SQL Server must perform a distinct sort behind the scenes. [46, 47, 48, 49]
- UNION ALL: Combines the result sets exactly as they are, retaining all duplicate rows. It is significantly faster because it avoids the sorting overhead. [50, 51, 52, 53, 54]

## 7. What are the variations of temporary storage in T-SQL (#Temp, ##Temp, and @Table)?

- Local Temporary Tables (#Table): Stored in tempdb. They are visible only to the current connection/session that created them and are automatically destroyed when the connection closes. [55, 56, 57, 58, 59]
- Global Temporary Tables (##Table): Stored in tempdb. They are visible to all database connections and sessions. They are destroyed when the creating session closes and all other sessions stop referencing them. [60, 61, 62, 63, 64]
- Table Variables (@Table): Created in memory (though they can spill to tempdb if large). They follow standard variable scoping rules and are cleared as soon as the specific batch or stored procedure finishes executing. [65]

---

If you are prepping for a technical interview, I can provide deep dives into more specific architecture paths. Would you like me to:

- Show how to write a Recursive CTE to navigate a manager-employee hierarchy
- Explain performance tuning concepts like Index Scans vs. Index Seeks

## Here are typical T-SQL interview questions focused on Views and Stored Procedures, detailing their core concepts, use cases, and performance considerations.

## Questions Related to Views## 1. What is a View, and what are the primary benefits of using one?

A View is a virtual table whose contents are defined by a pre-written query. It does not store data physically (unless it is an Indexed View). It simply runs its underlying query every time you reference it. [1, 2, 3, 4]
Primary Benefits:

- Security: You can restrict user access by granting permissions to a view rather than exposing the underlying base tables.
- Simplicity: It hides complex joins, calculations, and logic from application developers, allowing them to write clean queries against a single "table."
- Consistency: It acts as a single point of truth for common business logic modifications. [5, 6, 7]

-- Creating a simple view to hide sensitive columns (like SSN or Salary)CREATE VIEW v_EmployeePublicDirectory ASSELECT EmployeeID, FirstName, LastName, Department, EmailFROM EmployeesWHERE IsActive = 1;
-- How a user queries itSELECT \* FROM v_EmployeePublicDirectory;

## 2. Can you update data through a View? What are the limitations?

Yes, you can run INSERT, UPDATE, and DELETE statements against a view, but it must be an Updatable View. [8, 9, 10, 11, 12]
Key Limitations:

- The modifications can only affect one base table at a time. If the view joins Table A and Table B, a single statement cannot modify columns in both tables. [13]
- You cannot update columns that are derived from aggregate functions (SUM(), AVG()), calculations, or the DISTINCT clause. [14]
- Any inserted rows must satisfy the underlying base table constraints (like supplying non-null values for columns not included in the view).

## 3. What is the purpose of the WITH CHECK OPTION in a View?

The WITH CHECK OPTION forces any data modifications (INSERT or UPDATE) done through the view to strictly adhere to the filtering criteria defined in the view's WHERE clause. [15, 16, 17, 18, 19]
Without this option, a user could update a row through the view in a way that makes it instantly disappear from that view's results.

CREATE VIEW v_HighSalaryEmployees ASSELECT EmployeeID, Name, SalaryFROM EmployeesWHERE Salary >= 100000WITH CHECK OPTION;
-- This UPDATE will FAIL because it violates the view's WHERE condition:UPDATE v_HighSalaryEmployees SET Salary = 45000 WHERE EmployeeID = 101;

---

## Questions Related to Stored Procedures## 4. What is a Stored Procedure, and how does it differ from a View?

A Stored Procedure is a precompiled collection of T-SQL statements stored directly on the database server. [20, 21, 22]

| Feature        | View                                                                      | Stored Procedure                                                                |
| -------------- | ------------------------------------------------------------------------- | ------------------------------------------------------------------------------- |
| Purpose        | Expose a virtual table structure.                                         | Execute a multi-step programmatic task.                                         |
| Parameters     | Does not accept input parameters.                                         | Accepts Input and Output parameters.                                            |
| DML Operations | Limited to selecting and reading data (mostly).                           | Can execute complex inserts, updates, deletes, and administrative transactions. |
| Logic Flow     | Cannot contain procedural logic like loops, IF...ELSE, or error handling. | Full programming capabilities (IF...ELSE, WHILE loops, TRY...CATCH).            |

-- Creating a Stored Procedure with an Input and Output parameterCREATE PROCEDURE sp_GetEmployeeBonus
@EmpId INT,
@BonusMultiplier DECIMAL(3,2),
@FinalBonus MONEY OUTPUTASBEGIN
SET NOCOUNT ON; -- Prevents sending 'X rows affected' messages for performance

    DECLARE @CurrentSalary MONEY;

    -- Fetching data into a local variable
    SELECT @CurrentSalary = Salary
    FROM Employees
    WHERE EmployeeID = @EmpId;

    -- Calculating output variable
    SET @FinalBonus = @CurrentSalary * @BonusMultiplier;END;

## 5. What is Parameter Sniffing in Stored Procedures, and how do you resolve it?

Parameter Sniffing occurs when a stored procedure is compiled for the very first time. SQL Server looks at the specific parameter values passed into that first execution and designs an optimal execution plan around those values.
The Problem: If that initial parameter value was an anomaly (e.g., fetching a term that returns 2 rows instead of a typical term that returns 2,000,000 rows), the cached execution plan will be highly inefficient for subsequent runs.
Common Resolutions:

- OPTIMIZE FOR UNKNOWN: Tells the optimizer to use statistical averages rather than the specific first parameter value.
- Local Variables: Copying parameters into locally declared variables inside the procedure breaks the parameter-sniffing chain.
- WITH RECOMPILE: Forces the database engine to generate a brand-new plan every single time the procedure executes (ideal if data variation is extremely volatile). [23, 24]

CREATE PROCEDURE sp_GetOrdersByCustomer
@CustomerId INTASBEGIN
-- Fix: Instructing the optimizer to use an average statistics profile
SELECT \* FROM Orders
WHERE CustomerID = @CustomerId
OPTION (OPTIMIZE FOR UNKNOWN);END;

## 6. How do you handle errors inside a Stored Procedure? [25]

Modern T-SQL structures error handling using BEGIN TRY...BEGIN CATCH blocks. This allows you to gracefully capture runtime database errors, roll back broken transactions, and log failures cleanly without crashing your application layer. [26, 27]

CREATE PROCEDURE sp_TransferFunds
@SourceAccount INT,
@TargetAccount INT,
@Amount MONEYASBEGIN
SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Step 1: Deduct funds
        UPDATE Accounts SET Balance = Balance - @Amount WHERE AccountID = @SourceAccount;
        -- Step 2: Add funds
        UPDATE Accounts SET Balance = Balance + @Amount WHERE AccountID = @TargetAccount;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- If anything fails above, reverse all partial updates safely
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        -- Log or throw the error details
        PRINT 'An error occurred: ' + ERROR_MESSAGE();
    END CATCHEND;

---

If you are continuing your interview preparation, we can target specific advanced implementation options. Would you like me to:

- Explain the performance benefits and limitations of Indexed (Materialized) Views
- Show how to handle dynamic SQL safely inside a Stored Procedure to prevent SQL injection
