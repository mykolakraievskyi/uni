use taxi;

--Запит для отримання кількості поїздок кожного клієнта та ідентифікація найактивніших. 
--Вхідні дані – база даних Taxi. 
--Вихідні дані – список, що містить повні імена клієнтів та кількість поїздок, 
--посортований в спадному порядку за кількістю поїздок, а далі за алфавітним порядком за іменами.
select concat(firstname, ' ', lastname) as Fullname, count(t.id) as [Number of Trips]
from Customer c
left join Trip t on c.id = t.customerId
group by (concat(firstname, ' ', lastname))
order by count(t.id) desc, concat(firstname, ' ', lastname) asc;

--Середня кількість поїздок кожного місяця.
--Буде використана для аналізу попиту на наш сервіс впродовж року.
--Вхідні дані – база даних Taxi.
--Вихідні дані – список, що містить місяць та середню кількість поїздок цього місяця.
--Список має бути посортованим за місяцями за зростанням.
select datename(month, dateadd(month, [month], -1)) as [Month], avg(number) as [Average Number of Trips]
from (
	select month(t.pickUpTime) as [month],
	count(t.id) as number
	from Trip t
	group by year(pickUpTime), month(t.pickUpTime)
) as MonthlyTrips 
group by MonthlyTrips.[month]
order by MonthlyTrips.[month]

--Рейтинг поїздки відносно віку машини за виробниками. 
--Буде використана для аналізу, чи залежить рейтинг від віку машини, 
--та машини яких виробників отримують хороші відгуки попри вік. 
--Вхідні дані – база даних Taxi. 
--Вихідні дані – список, що містить виробників машин, 
--рік виробництва та середній рейтинг поїздок. 
--Список має бути посортованим за роком за спаданням, потім за виробником за зростанням.
select year(getdate()) - v.[year] as [Years], v.manufacturer as [Manufacturer], (
	select cast(avg(cast(rating as decimal(3, 2))) as decimal(3, 2))
	from Trip t
	inner join Vehicle iv on t.vehicleId = iv.id
	where iv.[year] = v.[year] 
	and iv.manufacturer = v.manufacturer
) as [Average Rating]
from Vehicle v
where exists (
	select id 
	from Trip it
	where it.vehicleId = v.id
)
group by v.[year], v.manufacturer
order by v.[year] asc, v.manufacturer asc;

create or alter view [Monthly Analythics] as
select 
	datename(month, dateadd(month, month(t.pickUpTime), -1)) as [Month],
	year(t.pickUpTime) as [Year],
	sum(case when t.statusId = 3 then t.cost else 0 end) as Revenue,  
    count(case when t.statusId = 3 then 1 else null end) as [Completed Trips] 
from Trip t
group by year(t.pickUpTime), month(t.pickUpTime); --створення view для аналітики

select * 
from [Monthly Analythics] 
where [Year] = 2023
and [Month] = 'October'
--order by month(concat([Month], '01, 2000')) asc;
--використання view

select t.id, ts.name
from Trip t
left join TripStatus ts on t.statusId = ts.id --використання join

select t.id, ts.name
from Trip t, TripStatus ts
where t.statusId = ts.id; --еквівалентний запит, але з where

(select firstname from Customer
union 
select firstname from Driver)
order by firstname; --всі імена (водіїв та клієнтів)

(select firstname from Customer
intersect
select firstname from Driver)
order by firstname desc; --імена, що є і в водіях, і в клієнтах

(select firstname from Customer
except
select firstname from Driver); --лише імена, що є в клієнтах, але немає в водіях


select t.Number, t.startPointId, t.endPointId, tj.number, tj.startPointId, tj.endPointId 
from Trip t
inner join Trip tj on t.startPointId = tj.endPointId; 
--пошук пар поїздок, серед яких кінцева точка однієї є початковою точкою іншої

select c.id, t.id from Customer c
left join Trip t on 1=1 --відсутнє співставлення колонок реляційних ключів (крінжово)

select concat(c.firstname, ' ', c.lastname) as [Full Name], t.id from Customer c
inner join Trip t on c.id = t.customerId --лише клієнти, що мають поїздки

select c.id as [Customer Id], 
	(case 
		when c.id is null then 'no customer'
		when t.id is null then 'no trip'
		else 'everything is present' end) as [Status],
	t.id as [Trip Id]
from Customer c
left outer join Trip t on c.id = t.customerId
--всі клієнти та всі поїздки