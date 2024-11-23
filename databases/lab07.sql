select * 
from Customer
where firstname = 'John';
--Колонка firstname не має індексу. СУБД виконуватиме Table Scan для пошуку відповідних рядків.

select * 
from Customer
where phoneNumber = '1234567890';
--Колонка phoneNumber має унікальний індекс (UQ_Customer_phoneNumber). 
--СУБД використовуватиме Index Seek для вибірки даних.

select * 
from Customer
where phoneNumber = '1234567890';
--Очікується операція Index Seek, оскільки предикат = дозволяє використати індекс.

select * 
from Customer
where phoneNumber != '1234567890';
--Очікується Index Scan, оскільки індекс не може ефективно обробити умову нерівності.

select * 
from Customer
where phoneNumber > '1234560000';
--Очікується Index Seek, 
--оскільки предикат > дозволяє пошук у напрямку 
--від вказаного значення до кінця індексованих даних.

select * 
from Customer
where phoneNumber between '1234560000' and '1234569999';
--Очікується Index Seek, бо умова between обмежує діапазон в індексі.

select * 
from Customer
where (phoneNumber = '1234567890' or phoneNumber = '0987654321')
and firstname = 'John';
--Складна комбінація, яка може включати Index Seek для phoneNumber та Filter для firstname.

select t.id, c.firstname, d.firstname 
from Trip t
join Customer c on t.customerId = c.id
join Driver d on t.driverId = d.id;
--Очікувані операції:
--Nested Loops або Hash Join (залежно від розміру таблиць).
--СУБД виконуватиме Table Scan для зовнішніх ключів, бо індекси відсутні.

create index IX_Trip_customerId on Trip(customerId);
create index IX_Trip_driverId on Trip(driverId);
--створення індексів

select t.id, c.firstname, d.firstname 
from Trip t
join Customer c on t.customerId = c.id
join Driver d on t.driverId = d.id;
--Повторний запит
--Index Seek для customerId і driverId.
--Замість Table Scan, СУБД використовуватиме індекси для швидкого доступу до рядків.
--Зниження складності виконання до O(log n).

select t.id, c.firstname, d.firstname 
from Trip t
join Customer c on t.customerId = c.id
join Driver d on t.driverId = d.id
where t.pickUpTime >= '2024-01-01';
--Завдяки індексам на зовнішніх ключах, операції join будуть ефективнішими.
--Умову pickUpTime >= може обробити Index Seek (якщо є індекс на pickUpTime).

select t.id, c.firstname, d.rating 
from Trip t
join Customer c on t.customerId = c.id
join Driver d on t.driverId = d.id
where d.rating > 4;
--Індекс на driverId прискорить jojn.
--Умова rating > 4 також може скористатися індексом, якщо він створений на rating.

select t.id, c.firstname, d.rating 
from Trip t
join Customer c on t.customerId = c.id
join Driver d on t.driverId = d.id
where d.rating = 5;
--Індекс на driverId буде використано для join.

select t.id, c.firstname, d.firstname 
from Trip t
join Customer c on t.customerId = c.id
join Driver d on t.driverId = d.id
where d.firstname like '%hn';
--Предикат like '%5' не дозволяє використання індексу через шаблон із символом % на початку.

select t.id, c.firstname, d.rating 
from Trip t
join Customer c on t.customerId = c.id
join Driver d on t.driverId = d.id
where d.rating > 4 or c.firstname LIKE '%John%';
--Предикат OR ускладнює оптимізацію, і, ймовірно, буде виконано Index Scan або Table Scan.

select d.rating, count(*) as tripCount 
from Trip t
join Driver d on t.driverId = d.id
group by d.rating
order by d.rating desc;
--Сортування потребує повного перегляду таблиці (операція Sort).
--Групування виконується через Hash Aggregate.

select d.rating, count(*) as tripcount 
from trip t
join driver d on t.driverid = d.id
group by d.rating;
--join: використовуються індекси на driverid.
--групування:
--без індексу: виконується hash aggregate (o(n)).
--з індексом: stream aggregate використовується, якщо дані вже відсортовані.

select t.id, t.pickuptime, t.cost 
from trip t
order by t.pickuptime desc;
--сортування:
--без індексу: sort (o(n log n)).
--з індексом: використовується index seek (o(log n)).

select top 10 t.id, t.pickuptime, t.cost 
from trip t
order by t.pickuptime desc
--обмеження кількості результатів:
--без індексу: субд сортує всі записи і вибирає перші 10 (o(n log n + m), де m — кількість обраних записів).
--з індексом: субд зчитує лише перші 10 записів із впорядкованого індексу (top-n sort o(log n + m)).

-- індекс на колонку statusid у таблиці trip
create index idx_trip_statusid
on trip (statusid);

-- індекс на колонку pickuptime у таблиці trip
create index idx_trip_pickuptime
on trip (pickuptime);

-- індекс на колонку driverid у таблиці trip
create index idx_trip_driverid
on trip (driverid);

--комплексний індекс: покриває кілька колонок для складних запитів.
create index idx_trip_statusid_pickuptime 
on trip (statusid, pickuptime);

--хеш-індекс (для sql server еквівалент реалізується через кластерний індекс):
create clustered index idx_trip_driverid_cost 
on trip (driverid, cost);

--індекс на функцію: створимо обчислювану колонку, потім індекс.
alter table trip
add cost_per_seat as (cost / seats) persisted;

create index idx_trip_cost_per_seat
on trip (cost_per_seat);

--Використаємо таблицю customer для пошуку по колонці email. 
--Для використання повнотекстового пошуку потрібно створити повнотекстовий індекс:
create fulltext catalog ft_catalog as default;

create fulltext index on customer(email) 
key index pk_customer_id
on ft_catalog
with stoplist = system;

--запити
select * 
from customer 
where email like '%gmail%';

select * 
from customer 
where contains(email, 'gmail');

--Індекс 1: Для швидкого пошуку та агрегації в trip по статусу.
create index idx_trip_statusid 
on trip (statusid);

--Індекс 2: Для оптимізації сортування по pickuptime у запитах до таблиці trip.
create index idx_trip_pickuptime 
on trip (pickuptime);

--Індекс 3: Унікальний індекс на email у таблиці customer для швидкого пошуку.
create unique index idx_customer_email 
on customer (email);