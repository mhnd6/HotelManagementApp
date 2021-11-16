CREATE PROCEDURE [dbo].[spBookings_BookGuest]
	@roomId int,
	@guestId int,
	@startDate date,
	@endDate date,
	@totalCost decimal,
	@checkIn bit
AS
begin

insert into dbo.Bookings (RoomId, GuestId, StartDate, EndDate, TotalCost, CheckedIn)
	values (@roomId, @guestId, @startDate, @endDate, @totalCost, @checkIn)

end
