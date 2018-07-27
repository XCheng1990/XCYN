--查询1-10的数据
SELECT * FROM 
(
SELECT sum(CODE_ACT_LEN) over(partition by REC_CREATOR) AS TotalCode_Len
      ,ROW_NUMBER() OVER(ORDER BY REC_ID) AS RN
      ,RANK() OVER(ORDER BY REC_ID) AS R
      ,DENSE_RANK() OVER(ORDER BY REC_ID) AS DR
      ,NTILE(10) OVER(ORDER BY REC_ID) AS N
  FROM [BGMES].[dbo].[TTS0091]
) N
WHERE N.RN > 0 AND N.RN <= 10
  
--删除用户id重复的行 只保留最小的值
DELETE FROM TTS0091
WHERE REC_ID NOT IN
(
  SELECT MIN(REC_ID) AS MIN_REC_ID
  FROM TTS0091
  GROUP BY REC_CREATOR
)

--case表达式
SELECT width, case
when width > 100 then '大于100'
else '小于100'
end AS 长度
FROM TRWCU02

SELECT CU02.WIDTH,CU02.*
FROM TRWCU02 AS CU02
WHERE CASE
WHEN CU02.WIDTH > 100 THEN '1'
ELSE '0'
END = '1'

--日期格式和转换，比较

SELECT CASE 
WHEN CAST('2018-01-23T00:00:00' AS DATETIME) = '20180123' THEN 'TRUE'
ELSE 'FALSE'
END 只比较日期是否相等

SELECT CAST('20181001' AS SMALLDATETIME)

SELECT CAST('20000901 07:30:00' AS DATETIME2)

SELECT CAST('19:45:00' AS TIME)

--第二章练习题
--1.求出6月份所有订单
SELECT orderid,orderdate,custid,empid FROM Sales.Orders
WHERE MONTH(orderdate) = 6 and YEAR(orderdate) = 2008
--这种方式可以避免索引失效
SELECT orderid,orderdate,custid,empid FROM Sales.Orders
WHERE orderdate >= '2008-06-01' and orderdate < '2008-07-01'

--3.求出所有名字的字母包含两个以上的a
SELECT * FROM PERSON
WHERE FIRSTNAME LIKE '%a%a%'

--4.求总价格大于10000的所有订单,并按照总价排序
SELECT ORDERID,(NUM * PRICE) AS TOTALVALUE           --错误
FROM SALES.ORDERDETAILS
WHERE NUM * PRICE > 10000
ORDER TOTALVALUE

SELECT ORDERID,SUM(PRICE * NUM) AS TOTALVALUE        --正确
FROM SALES.ORDERDETAILS
GROUP BY ORDERID
HAVING SUM(PRICE * NUM) > 10000
ORDER BY SUM(PRICE * NUM)

--5.返回2007年平均运费最高的三个国家
SELECT TOP 3 SHIPCOUNTRY,AVG(AVGFREIGHT)
FROM SALES.ORDERS
WHERE ORDERDATE >= '2007-01-01' AND ORDERDATE < '2008-01-01'
GROUP BY SHIPCOUNTRY
ORDER BY AVG(AVGFREIGHT) DESC

--6.为每个顾客单独根据订单日期的顺序，来计算其订单的行号

SELECT CUSTID,ORDERDATE,ORDERID,
ROW_NUMBER() OVER(PARTITION BY CUSTID ORDER BY ORDERDATE,ORDERID) AS ROWNUM
FROM SALES.ORDERS
ORDER BY CUSTID,ROWNUM

--7.构建一个SELECT语句，让它根据每个雇员的友好称谓来返回不同的性别，如：对于'Ms.'和'Mrs.'返回'female'；
--对于'Mr.'，返回'male'，对于其他情况，返回'UnKnown'

SELECT EMPID,FIRSTNAME,LASTNAME,TITLEOFCOURTESY,
CASE TITLEOFCOURTESY WHEN 'MS.' THEN 'FEMALE'
WHEN 'MSR.' THEN 'FEMALE'
WHEN 'MR.' THEN 'MALE'
ELSE 'UNKNOWN' END AS GENDER
FROM HR.EMPLOYEES.

--8.返回每个客户ID和所在区域。对输出中的行按照区域排序，NULL值排在最后面

SELECT CUSTID,REGION                 --错误，因为ISNULL()函数有两个参数，并且它的作用是，当行为空时用另一个参数代替之
FROM SALES.CUSTOMERS
WHERE !ISNULL(REGION)
ORDER BY CUSTID
UNION
SELECT CUSTID,REGION
FROM SALES.CUSTOMERS
WHERE ISNULL(REGION)
ORDER BY CUSTID

SELECT CUSTID,REGION				--正确
FROM SALES.CUSTOMERS
ORDER BY CASE WHEN REGION IS NULL THEN 0 ELSE 1 END DESC

CREATE TABLE #T1
(
	digit int
)
--插入十条数据依次为0-9
insert into #T1
values (0),(1),(2),(3),(4),(5),(6),(7),(8),(9)
--利用交叉连接，算出1000个不同的数字
SELECT (T3.digit * 100) + (T2.digit * 10) + T1.digit + 1 AS D2 FROM #T1 AS T1
CROSS JOIN #T1 AS T2
CROSS JOIN #T1 AS T3
ORDER BY D2 

FROM TABLE1 AS T1
JOIN TABLE2 AS T2
ON T1.col1 = T2.col1 AND T1.col2 = T2.col2

--日期函数
--DATEADD(part,n,dt_val)
--part:日期部分，有效值有时，分，秒，天，月，年，季度
--n:增加的数值
--dt_val:原值
SELECT DATEADD(DAY,1,'20180712')
SELECT DATEADD(MONTH,1,'20180712')
SELECT DATEADD(YEAR,1,'20180712')


--求日期的差值
--DATEDIFF(part,dt_val1,dt_val2)
--part:日期部分，有效值有时，分，秒，天，月，年，季度
--公式：dt_val2 - dt_val1
SELECT DATEDIFF(DAY,'20180101','20180712')
SELECT DATEDIFF(MONTH,'20180101','20180712')
SELECT DATEDIFF(YEAR,'20180101','20180712')


CREATE TABLE #NUM
(n int)

DECLARE @i int;
SET @i = 0;
WHILE @i < 100000
BEGIN
INSERT INTO #NUM VALUES(@i);
SET @i = @i + 1;
END

--算出2000年-2018年每天
SELECT DATEADD(DAY,n,'2000-01-01') AS everyday,T91.*
FROM #NUM
LEFT JOIN [BGMES].[dbo].[TTS0091] AS T91
ON DATEADD(DAY,n,'2000-01-01') =  SUBSTRING(T91.REC_CREATE_TIME + '',0,9)
WHERE DATEDIFF(DAY,'2000-01-01','2018-01-01') >= n


--第三章 联接查询 练习题
--创建一个辅助表Nums
CREATE TABLE Nums
(n int)

DECLARE @i int;
SET @i = 0;
WHILE @i < 100000
BEGIN
INSERT INTO Nums VALUES(@i);
SET @i = @i + 1;
END

--1.写一条SQL语句，把所有雇员记录复制5次

SELECT Empid,FirstName,LastName,n
FROM T_Employee AS emp CROSS JOIN Nums
WHERE NUMS.n <= 5 AND Nums.n > 0

--2.写一个查询，为每个雇员和从2009-06-12至2009-06-16日范围内每天返回一行。
SELECT T_Employee.Empid,T1.dt
FROM T_Employee Cross Join 
(
SELECT DATEADD(DAY,n,'2009-06-12') AS dt
FROM Nums
WHERE n <= DATEDIFF(DAY,'2009-06-12','2009-06-16')
) AS T1
Order by T_Employee.Empid

--3.返回来自美国的客户，并为每一个客户返回其订单总数和交易总额
SELECT c.CustID,COUNT(o.OrderID) AS NumOrders,SUM(od.qty) AS TotalQty
FROM T_Customer AS c
LEFT JOIN T_Order AS o On c.CustID = o.CustID
LEFT JOIN T_OrderDetail AS od ON o.OrderID = od.OrderID
WHERE c.Country = 'USA'
GROUP BY c.CustID

--4.返回客户及其订单信息，包括没有下过任何订单的客户
SELECT c.CustID,o.OrderID
FROM T_Customer AS c LEFT JOIN T_Order AS o
ON c.CustID = o.CustID

--5.返回没有下过订单的用户
SELECT c.CustID
FROM T_Customer AS c LEFT JOIN T_Order AS o
ON c.CustID = o.CustID
WHERE o.OrderID is null

SELECT * 
FROM T_Order AS o1
WHERE OrderID = 
(
	select MAX(OrderID)
	from T_Order AS o2
	where o1.CustID = o2.CustID
)

--获取所有下过订单的美国用户
SELECT CustID
FROM T_Customer AS c
WHERE Country = 'USA' AND
EXISTS(
SELECT * 
FROM T_Order AS o
WHERE c.CustID = o.CustID
)

--返回ORDER表最后一天所有订单
SELECT * FROM T_Order
WHERE OrderDate = 
(
	SELECT MAX(OrderDate) FROM T_Order
)

--返回订单数量最多的客户的所有订单
SELECT * FROM T_Order
WHERE CustID = 
(
	SELECT top 1 CustID FROM T_Order
	GROUP BY CustID
	Order by Count(CustID) DESC
)

--返回2012年2月15日后没有处理过订单的雇员
SELECT * FROM T_Employee
WHERE Empid NOT IN
(
	SELECT CustID FROM T_Order
	WHERE OrderDate > '2017-02-15'
)

--返回在客户表出现过，但没有在雇员表中出现过的国家

SELECT C.Country FROM T_Customer AS C
WHERE C.Country NOT IN
(
	SELECT  E.Country FROM T_Employee AS E
)

--返回每一个客户最后一条订单
SELECT * FROM T_Order AS O1
WHERE O1.OrderDate IN
(
SELECT MAX(O2.OrderDate) AS OrderID FROM T_Order AS O2
WHERE O1.CustID = O2.CustID
) 
--此处的MAX(OrderDate)表示的是，每个客户的最大订单

--生成一个由10个数字组成的虚拟辅助表，要求不能使用循环
SELECT 1 AS n
UNION ALL
SELECT 2
UNION ALL
SELECT 3
UNION ALL
SELECT 4
UNION ALL
SELECT 5
UNION ALL
SELECT 6
UNION ALL 
SELECT 7 
UNION ALL
SELECT 8 
UNION ALL
SELECT 9
UNION ALL
SELECT 10

--也可以使用这种方式来解决该问题
SELECT n 
FROM (VALUES(1),(2),(3)) AS #Nums(n)

--求出2017年1月有订单且2月份没有订单的客户
SELECT CustID
FROM T_Order
WHERE OrderDate >= '2017-01-01' and OrderDate < '2017-02-01'

EXCEPT

SELECT CustID
FROM T_Order
WHERE OrderDate >= '2017-02-01' and OrderDate < '2017-03-01'

--求出在2017年1月和2月都有订单的客户
SELECT CustID
FROM T_Order
WHERE OrderDate >= '2017-01-01' AND OrderDate < '2017-02-01'

INTERSECT

SELECT CustID
FROM T_Order
WHERE OrderDate >= '2017-02-01' AND OrderDate < '2017-03-01' 

--返回在2017年1月和2月都有订单，但在2018年没有订单的客户
SELECT CustID
FROM T_Order
WHERE OrderDate >= '2017-01-01' AND OrderDate < '2017-02-01'

INTERSECT

SELECT CustID
FROM T_Order
WHERE OrderDate >= '2017-02-01' AND OrderDate < '2017-03-01'

EXCEPT

SELECT CustID
FROM T_Order
WHERE OrderDate >= '2018-01-01' AND OrderDate < '2019-01-01' 

--查询T_Customer和表T_Employee，让T_Customer表里的数据都要在T_Employee表前面，并让他们按照国家排序

SELECT 1 AS SortID, Country
FROM T_Customer AS c

UNION ALL

SELECT 2 AS SortID,Country
FROM T_Employee AS e

ORDER BY SortID,Country

SELECT CURRENT_TIMESTAMP

--VALUES子句，创建虚拟表

SELECT * FROM 
(VALUES(1,'USA'),(2,'CHI'),(3,'FRA'))
AS O(ID,COUNTRY)

--SELECT VALUES多个行，可以进行原子性操作

INSERT INTO T_Order
VALUES
(1,1,'冰箱','2017-01-01'),
(2,1,'洗衣机','2017-01-01'),
(3,1,'电视','2017-01-01')

INSERT INTO #T_Order(CustID,Name,OrderDate)
SELECT 2,'电视','2018-01-01' 
UNION ALL
SELECT 2,'电视','2018-01-02'
go

--也可以套用之前的VALUES子句，创建虚拟表

INSERT INTO #T_Order(CustID,Name,OrderDate)
SELECT * FROM 
(VALUES(3,'电视','2018-01-01'),(3,'电视','2018-01-02'))
AS O(CustID,Name,OrderDate)

use Test
if OBJECT_ID('P_InsertOrder','p') is not null
drop proc P_InsertOrder
go
create PROCEDURE P_InsertOrder
as
begin
SELECT * FROM (VALUES
(1,'冰箱','2017-01-01'),
(1,'洗衣机','2017-01-01'),
(1,'电视','2017-01-01'))
AS O(OrderID,Name,OrderDate)
end
GO

--执行INSERT EXEC
INSERT INTO T_Order(CustID,Name,OrderDate)
EXEC P_InsertOrder

--执行SELECT INTO语句复制表
SELECT * 
INTO #T_Order
FROM T_Order WHERE T_Order.OrderID > 5

SELECT * FROM #T_Order

INSERT INTO T_Order
VALUES
(1,'冰箱','2017-01-01')
SELECT @@IDENTITY
SELECT SCOPE_IDENTITY()