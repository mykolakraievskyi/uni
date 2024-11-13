
use taxi;
--процедура для оновлення рейтингу водія
create procedure updateDriversRating(@driverId int, @newRating float) 
as
begin
	if @newRating >= 0 and @newRating <= 5
	begin 
		update Driver
		set rating = @newRating
		where id = @driverId
	end
	else
	begin
		print 'rating should be between 0 and 5'
	end
end	

--процедура для оновлення даних користувача
create procedure updateCustomerInfo(@customerId int, @phone nvarchar(10), @email nvarchar(255))
as 
begin 
	begin try
		update Customer
		set phoneNumber = @phone, email = @email
		where id = @customerId
	end try
	begin catch
		declare @errorNumber int = error_number();
		if @errorNumber = 2627 or @errorNumber = 2601
		begin
			print 'Номер телефону або адреса електронної пошти вже існують в базі даних'
		end
		else
		begin
			print 'Під час додавання даних виникла помилка'
		end
	end catch
end

--процедура для заповнення відсутніх оцінок
create procedure UpdateMissingTripRatings
as
begin
    declare @tripId int;
    declare @tripNumber nvarchar(255);
    declare @defaultRating int = 3; 

    declare missingRatingCursor cursor for
    select id, number
    from Trip
    where rating IS NULL;

    open missingRatingCursor;

    fetch next from missingRatingCursor into @tripId, @tripNumber;

    while @@fetch_status = 0
    begin
        update Trip
        set rating = @defaultRating
        where id = @tripId;

        fetch next from missingRatingCursor into @tripId, @tripNumber;
    end

    close missingRatingCursor;
    deallocate missingRatingCursor;
end;

--функція для обчислення віку
create function getAge(@birthdate date)
returns int 
as
begin
	return datediff(year, @birthdate, getdate()) - 
		case when dateadd(year, datediff(year, @birthdate, getdate()), @birthdate) > getdate()
			then 1
			else 0
		end
end

select top 1 id, dateOfBirth, dbo.getAge(dateOfBirth) as age
from Customer

--функція для отримання повного ім'я
create function getFullName(@firstname varchar(50), @lastname varchar(50))
returns varchar(100)
as 
	begin
		return concat(@firstname, ' ', @lastname)
	end

select top 1 firstname, lastname, [dbo].getFullName(firstname, lastname)
from Customer

--функція, що повертає користувачів, які стали клієнтами в певний проміжок часу
create function getClientsFromDateRange(@start date, @end date)
returns table
as 
	return (select * 
			from Customer
			where clientFrom > @start
			and clientFrom < @end)

select [dbo].getClientsFromDateRange('2018-01-01', '2023-12-31')

--створення тригера
create trigger tripDataIntegrity
on Trip
after insert, update, delete
as
begin
	if exists (
		select 1
		from inserted i
		left join Driver d on d.id = i.driverId
		left join Vehicle v on v.id = i.vehicleId
		where d.id is null or v.id is null
	)
