select * from Customer; --селекція

select firstname, lastname, dateOfBirth from Customer; --проекція

select * from Trip, TripStatus; --декартовий добуток

select * from Trip
where round(rating, 0) in (4, 5); -- всі поїздки з рейтингом 4 або 5

select * from Driver
where firstname like 'A%'; -- всі водії з ім'ям, що починається на а

select * from Customer
where dateOfBirth between '1990-01-01' and '2000-01-01'; -- всі клієнти, що народились з 1990 по 2000 рік

select * from Driver
where firstname not like '_____'; -- всі клієнти, ім'я яких не 5 символів

select * from Vehicle
where [year] >= '2019' and seats > 4; -- всі машини, що вироблені в 2019 та пізніше і мають більше 4 сидінь

select * from Vehicle
where manufacturer = 'Mercedes Benz' or [year] = 2014; -- всі машини, що вироблені в 2014 або виробник - мерседес

select * from Trip 
where rating is not null; -- поїздки, в яких є рейтинг

select * from Trip 
where rating is  null; -- поїздки, в яких нема рейтингy

select concat(firstname, ' ', lastname) as [Drivers Full Name]
from Driver; -- повне ім'я водіїв

select firstname, clientFrom, dateadd(day, 5 * 365, clientFrom) as [5 Years Anniversary]
from Customer; -- дата, коли клієнти вже 5 років

select cost, cost * 10 as [1000%], cost / 10 as [10%], cost - cost * 0.1 as [New price] 
from Trip; -- операції +-*/

select distinct manufacturer 
from Vehicle; -- виробники машин

select top 10 concat(firstname, ' ', lastname) as [Full Name], clientFrom as [Client From], datediff(year, clientFrom, getdate()) as [Years With Us]
from Customer 
order by clientFrom; -- 10 перших клієнтів


select manufacturer, model, count(model) as [Model Count] from Vehicle
group by manufacturer, model
having count(model) > 1; --кількість машин певної моделі (має бути > 1)

select count(distinct customerId) as [Active Customers Number] 
from Trip; --кількість активних клієнтів

--select sum(cost) as [Total Cost], sum(cost) filter (where rating > 4) as [Top Trips Cost] from Trip
--не підтримується в SQL Server

select sum(cost) as [Total Income], 
	sum(
	case when statusId = 3 
		then cost
		else 0 
	end) as [Recieved] 
from Trip; --очікуваний дохід і отриманий дохід

select sum(cost * 1.20) as [Total Income] 
from Trip; --дохід з надбавкою (використання виразу як аргументу)

select concat(firstname, ' ', lastname) as [Full Name] 
from Customer
where exists (
	select distinct customerId
	from Trip
); --клієнти, що робили замовлення

select concat(firstname, ' ', lastname) as [Full Name] 
from Customer c
where c.dateOfBirth < any (
	select dateOfBirth 
	from Driver
); --клієнти, що молодші хоча б від одного водія

select concat(firstname, ' ', lastname) as [Full Name] 
from Customer c
where c.dateOfBirth < all (
	select dateOfBirth 
	from Driver
); --клієнти, що молодші від всіх водіїв

update t
set t.cost = t.cost * 0.9
from Trip t
left join Customer c on c.id = t.customerId
where datediff(year, c.clientFrom, getdate()) > 4;
--знижка 10% клієнтам, що з нами вже більше 4 років

with ClientsTime as (
	select id, datediff(year, clientFrom, getdate()) as years
	from Customer)
update t
set t.cost = t.cost * 0.8
from Trip t
left join ClientsTime ct on ct.id = t.id
where ct.years > 7; 
--знижка 20% клієнтам, що з нами вже більше 7 років