
use taxi;

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
