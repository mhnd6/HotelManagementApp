CREATE PROCEDURE [dbo].[spGuests_SaveGuest]
	@firstName text,
	@lastName text
AS
begin
	
	insert into dbo.Guests (FirstName, LastName) values (@firstName, @lastName)
end
