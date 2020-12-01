use
	Master
go

create database
	DBContact
go

use
	DBContact
go

create table Contact
(
	ID int primary key identity not null,
	SSN varchar(13) unique not null,
	FirstName varchar(32) not null,
	LastName varchar(32) not null
)
go

select * from Contact

create table ContactInfo
(
	ID int primary key identity not null,
	Info varchar(64) unique not null,
	ContactID int references Contact(ID) null
)
go

select * from ContactInfo
go

create procedure CreateContact
	-- Input parameters
	@SSN varchar(13),
	@FirstName varchar(32),
	@LastName varchar(32),

	-- Output parameters
	@ID int output
as begin
	insert into
		Contact
	values
		(@SSN, @FirstName, @LastName)

	set
		@ID = SCOPE_IDENTITY();
end
go

create procedure CreateContactInfo
	-- Input parameters
	@Info varchar(64),
	@ContactID int,

	-- Output parameters
	@ID int output
as begin
	insert into
		ContactInfo
	values
		(@Info, @ContactID)

	set
		@ID = SCOPE_IDENTITY();
end
go

declare
	@CID int,
	@CIID int

execute CreateContact '19620601-1234', 'Håkan', 'Johansson', @CID output
execute CreateContactInfo '070 464 74 31', @CID, @CIID output
execute CreateContactInfo 'hakan@kvarnskogen.st', @CID, @CIID output

execute CreateContact '19760809-1234', 'Marilyn', 'Johansson', @CID output
execute CreateContactInfo 'johansson.marilyn@gmail.com', @CID, @CIID output
execute CreateContactInfo '073 938 44 31', @CID, @CIID output
execute CreateContactInfo 'marilyn@kvarnskogen.st', @CID, @CIID output

execute CreateContact '20060610-1234', 'Nathalie', 'Johansson', @CID output
execute CreateContactInfo '08 123 45 67', @CID, @CIID output

execute CreateContact '20091205-1234', 'Kenneth', 'Johansson', @CID output
execute CreateContactInfo '08 987 65 43', @CID, @CIID output
go
-- TIME FOR C# Solution!!!

create procedure ReadContact @ID int
as begin
	select
		ID,
		SSN,
		FirstName,
		LastName
	from
		Contact
	where
		ID = @ID
end
go

execute ReadContact 6
go

create procedure ReadAllContacts as
begin
	select
		ID,
		SSN,
		FirstName,
		LastName
	from
		Contact
end
go

execute ReadAllContacts
go

create procedure UpdateContact @ID int, @SSN varchar(13), @FirstName varchar(32), @LastName varchar(32) as
begin
	update
		Contact
	set
		SSN = case when @SSN is null then SSN else @SSN end,
		FirstName = case when @FirstName is null then FirstName else @FirstName end,
		LastName = case when @LastName is null then LastName else @LastName end
	where
		ID = @ID
end

execute UpdateContact 1, null, Carl, null
go

create procedure DeleteContact @ID int
as begin
	delete
	from
		Contact
	where
		ID = @ID
end
go

begin tran
execute DeleteContact 5
rollback tran
go

--------------------------


create procedure ReadContactInfo @ID int
as begin
	select
		C.ID,
		C.Info,
		C.ContactID
	from
		ContactInfo as C
	where
		ID = @ID
end
go

execute ReadContactInfo 6
go

create procedure ReadAllContactInfo as
begin
	select
		C.ID,
		C.Info,
		C.ContactID
	from
		ContactInfo as C
end
go

execute ReadAllContactInfo
go

--create procedure UpdateContactInfo @ID int, @Info varchar(64), @ContactID int as
--begin
--	update
--		ContactInfo
--	set
--		Info = case when @Info is null then Info else @Info end,
--		ContactID = case when @ContactID is null then ContactID else @ContactID end
--	where
--		ID = @ID
--end

alter procedure UpdateContactInfo @ID int, @Info varchar(64), @ContactID int as
begin
	update
		ContactInfo
	set
		Info = @Info,
		ContactID = @ContactID
	where
		ID = @ID
end


select * from ContactInfo
execute UpdateContactInfo 8, null, 10
go

create procedure DeleteContactInfo @ID int
as begin
	delete from
		ContactInfo
	where
		ID = @ID
end
go

begin tran
execute DeleteContact 9
rollback tran

select
	C.*,
	' - - - - ',
	CI.*
from
	Contact as C
join
	ContactInfo as CI on
	C.ID = CI.ContactID

